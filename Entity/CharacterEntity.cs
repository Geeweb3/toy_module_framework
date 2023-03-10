using System.Collections.Generic;
using Lean.Touch;
using PFramework;
using PFramework.Resource;
using UnityEngine;

namespace GameMain
{
    public class CharacterEntity : Entity
    {
        private Dictionary<string, Renderer> _mMeshMap;
        [SerializeField] private Transform mMeshNode;
        [SerializeField] private Transform mFaceNode;

        private Dictionary<EnumEquipPart, EquipEntity> _curEquipments;
        private SkinnedMeshRenderer _mHeadSkinRenderer;

        private Dictionary<EnumBodyPart, Renderer> _curBodyRenders;
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        private static readonly int BaseMap = Shader.PropertyToID("_BaseMap");

        const string NakedHand="HAN_00_Naked_01";
        const string NakedFeet="FEET_00_Naked_01";
        private const int DefaultLeg = 60;
        private const int DefaultCloth = 4;
        
        public override void OnInit(object userData)
        {
            _mMeshMap = new Dictionary<string, Renderer>();
            _curEquipments = new Dictionary<EnumEquipPart, EquipEntity>();

            for (int i = 0; i < mMeshNode.childCount; ++i)
            {
                var temp = mMeshNode.GetChild(i);
                _mMeshMap.Add(temp.name, temp.GetComponent<Renderer>());
            }

            for (int i = 0; i < mFaceNode.childCount; ++i)
            {
                var temp = mFaceNode.GetChild(i);
                _mMeshMap.Add(temp.name, temp.GetComponent<Renderer>());
            }

            _mHeadSkinRenderer = _mMeshMap["HEAD"] as SkinnedMeshRenderer;
            _curBodyRenders = new Dictionary<EnumBodyPart, Renderer>();
            
            if (gameObject.name.Contains("Open"))
                GetComponentInChildren<PlayerMaterialCtrler>().OnInit();
        }

        public override void OnShow(IEntityData entityData)
        {
            base.OnShow(entityData);
            gameObject.SetActive(true);
            var da = entityData as CharacterData;
            LoadCharacterModel(da);
            if (!gameObject.name.Contains("Open"))
            {
                LeanTouch.OnFingerUpdate += OnFingerUpdate;
                AttachDefaultEquip();
                
                foreach (var (k,v) in da.EquipMap)
                {
                    if (v != null)
                    {
                        EntityMgr.I.Attach(EntityMgr.I.ShowEntity<EquipEntity>(new EquipData(v.Id)), this);
                        GameEventMgr.Fire(this, ReferencePool.Acquire<SwichEquipEventArgs>().Create(v));
                    }
                    else
                    {
                        GameEventMgr.Fire(this, ReferencePool.Acquire<SwichEquipEventArgs>().Create(null,k));
                    }
                }
                
            }
            else
            {
                GetComponentInChildren<PlayerMaterialCtrler>().OnShow(da.Rank);
            }
        }

        protected override void OnAttached(Entity childEntity)
        {
            base.OnAttached(childEntity);
            if (childEntity.GetType().Name == "EquipEntity")
            {
                RefreshEquipTable(childEntity as EquipEntity);
            }
        }

        private void OnFingerUpdate(LeanFinger finger)
        {
            if (finger.IsOverGui)
                return;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            if (finger.Index == -1)
            {
                var finalDelta = finger.ScreenDelta;
                transform.Rotate(Vector3.up, -finalDelta.x * 0.5f);
            }
#else
            if (finger.Set)
            {
            var finalDelta = finger.ScreenDelta;
            transform.Rotate(Vector3.up, -finalDelta.x * 0.5f);
            }
#endif
        }


        public void LoadCharacterModel(CharacterData data)
        {
            ChangeFaceNode("FACC_01_FaceShape", data.FaceShape);
            ChangeFaceNode("FACC_01_NoseShape", data.NoseShape);
            ChangeFaceNode("FACC_01_EyesIn", data.EyesIn);
            ChangeFaceNode("FACC_01_EyesUp", data.EyesUp);
            ChangeFaceNode("FACC_01_EyesSmall", data.EyesSmall);
            ChangeFaceNode("FACC_01_Lips", data.Lips);
            ChangeFaceNode("FACC_01_Ears", data.Ears);
            ChangeFaceNode("FACC_01_EarsElf", data.EarsElf);

            _mMeshMap[data.Eyebrow].enabled = true;
            if (_curBodyRenders.ContainsKey(EnumBodyPart.Eyebrow) && _curBodyRenders[EnumBodyPart.Eyebrow].gameObject.name != data.Eyebrow)
            {
                _curBodyRenders[EnumBodyPart.Eyebrow].enabled = false;  
            }

            _curBodyRenders[EnumBodyPart.Eyebrow] = _mMeshMap[data.Eyebrow];
            _curBodyRenders[EnumBodyPart.Eyebrow].sharedMaterial.SetColor(BaseColor, data.EyebrowColor);

            _mMeshMap[data.Hair].enabled = true;
            if (_curBodyRenders.ContainsKey(EnumBodyPart.Hair) && _curBodyRenders[EnumBodyPart.Hair].gameObject.name != data.Hair)
            {
                _curBodyRenders[EnumBodyPart.Hair].enabled = false;
            }

            _curBodyRenders[EnumBodyPart.Hair] = _mMeshMap[data.Hair];
            _curBodyRenders[EnumBodyPart.Hair].sharedMaterial.SetColor(BaseColor, data.HairColor);

            _mHeadSkinRenderer.sharedMaterials[0]
                .SetTexture(BaseMap, ResourceMgr.I.Load<Texture>(data.Skin + ".png"));
            _mHeadSkinRenderer.sharedMaterials[1]
                .SetTexture(BaseMap, ResourceMgr.I.Load<Texture>(data.EyeBase + ".png"));
            _mHeadSkinRenderer.sharedMaterials[1].SetColor(BaseColor, data.EyeBaseColor);
            _mHeadSkinRenderer.sharedMaterials[2]
                .SetTexture(BaseMap, ResourceMgr.I.Load<Texture>(data.EyeLight + ".png"));
            _mHeadSkinRenderer.sharedMaterials[2].SetColor(BaseColor, data.EyeLightColor);

            if (gameObject.name.Contains("Open"))
            {
                _mMeshMap["FEE_00_Naked_01"].sharedMaterial
                    .SetTexture(BaseMap, ResourceMgr.I.Load<Texture>(data.Skin + ".png"));
                _mMeshMap["HAN_00_Naked_01"].sharedMaterial
                    .SetTexture(BaseMap, ResourceMgr.I.Load<Texture>(data.Skin + ".png"));
                _mMeshMap["LEG_00_Shorts_01"].sharedMaterials[0]
                    .SetTexture(BaseMap, ResourceMgr.I.Load<Texture>(data.Skin + ".png"));
                _mMeshMap["TOR_00_BasicTshirt_01"].sharedMaterials[0]
                    .SetTexture(BaseMap, ResourceMgr.I.Load<Texture>(data.Skin + ".png"));
            }
        }

        private void RefreshEquipTable(EquipEntity equipEntity)
        {
            EnumEquipPart part = equipEntity.EquipData.PartType;

            if (_curEquipments.ContainsKey(part))
            {
                if (_curEquipments[part].EquipData.Id == equipEntity.EquipData.Id)
                {
                    equipEntity.isSame = true;
                    EntityMgr.I.Recycle(equipEntity);
                    return;
                }
                
                EntityMgr.I.Recycle(_curEquipments[part]);
                _curEquipments[part] = equipEntity;
            }
            else
            {
                if (part == EnumEquipPart.Suit)
                {
                    if (_curEquipments.ContainsKey(EnumEquipPart.Cloth))
                    {
                        EntityMgr.I.Recycle(_curEquipments[EnumEquipPart.Cloth]);
                        _curEquipments.Remove(EnumEquipPart.Cloth);
                    }

                    if (_curEquipments.ContainsKey(EnumEquipPart.Leg))
                    {
                        EntityMgr.I.Recycle(_curEquipments[EnumEquipPart.Leg]);
                        _curEquipments.Remove(EnumEquipPart.Leg);
                    }
                }
                else if (part == EnumEquipPart.Cloth || part == EnumEquipPart.Leg)
                {
                    if (_curEquipments.ContainsKey(EnumEquipPart.Suit))
                    {
                        EntityMgr.I.Recycle(_curEquipments[EnumEquipPart.Suit]);
                        _curEquipments.Remove(EnumEquipPart.Suit);
                        
                        if(part == EnumEquipPart.Cloth)
                            EntityMgr.I.Attach(EntityMgr.I.ShowEntity<EquipEntity>(new EquipData(DefaultCloth)), this);
                        else
                            EntityMgr.I.Attach(EntityMgr.I.ShowEntity<EquipEntity>(new EquipData(DefaultLeg)), this);
                    }
                }
                _curEquipments[part] = equipEntity;
            }

            if (part == EnumEquipPart.Cap)
                HideHair();
            else if (part == EnumEquipPart.Shoes)
                _mMeshMap[NakedFeet].enabled = false;
            else if (part == EnumEquipPart.Hand)
                _mMeshMap[NakedHand].enabled = false;
            
            equipEntity.LoadEquip(_mMeshMap[equipEntity.EquipData.MeshName]);
        }

        public void RemoveEquip(EnumEquipPart part)
        {
            if (_curEquipments.ContainsKey(part))
            {
                EntityMgr.I.Recycle(_curEquipments[part]);
                _curEquipments.Remove(part);
                
                if (part == EnumEquipPart.Cap)
                    ShowHair();
                else if (part == EnumEquipPart.Shoes)
                    _mMeshMap[NakedFeet].enabled = true;
                else if (part == EnumEquipPart.Hand)
                    _mMeshMap[NakedHand].enabled = true;
                else if (part == EnumEquipPart.Cloth)
                    EntityMgr.I.Attach(EntityMgr.I.ShowEntity<EquipEntity>(new EquipData(DefaultCloth)), this);
                else if (part == EnumEquipPart.Leg)
                    EntityMgr.I.Attach(EntityMgr.I.ShowEntity<EquipEntity>(new EquipData(DefaultLeg)), this);
            }
        }

        private void HideHair()
        {
            _curBodyRenders[EnumBodyPart.Hair].enabled = false;
            _curBodyRenders[EnumBodyPart.Hair] =
                _mMeshMap[_curBodyRenders[EnumBodyPart.Hair].gameObject.name.Replace("60", "61")];
            _curBodyRenders[EnumBodyPart.Hair].enabled = true;
        }

        private void ShowHair()
        {
            _curBodyRenders[EnumBodyPart.Hair].enabled = false;
            _curBodyRenders[EnumBodyPart.Hair] =
                _mMeshMap[_curBodyRenders[EnumBodyPart.Hair].gameObject.name.Replace("61", "60")];
            _curBodyRenders[EnumBodyPart.Hair].enabled = true;
        }

        private void AttachDefaultEquip()
        {
            EntityMgr.I.Attach(EntityMgr.I.ShowEntity<EquipEntity>(new EquipData(DefaultCloth)), this);
            EntityMgr.I.Attach(EntityMgr.I.ShowEntity<EquipEntity>(new EquipData(DefaultLeg)), this);
        }

        private void ChangeFaceNode(string nodeName, float newValue)
        {
            for (int i = 0; i < _mHeadSkinRenderer.sharedMesh.blendShapeCount; i++)
            {
                if (_mHeadSkinRenderer.sharedMesh.GetBlendShapeName(i) == nodeName)
                {
                    _mHeadSkinRenderer.SetBlendShapeWeight(i, newValue * 100);
                    break;
                }
            }
        }

        public override void OnRecycle()
        {
            base.OnRecycle();
            gameObject.SetActive(false);

            foreach (var (_,v) in _curEquipments)
            {
                if (v != null)
                    _mMeshMap[v.EquipData.MeshName].enabled = false;
            }

            _curEquipments.Clear();
            _curBodyRenders[EnumBodyPart.Hair].enabled = false;
            _mMeshMap[NakedHand].enabled = true;
            _mMeshMap[NakedFeet].enabled = true;
            _curBodyRenders.Clear();
        }

    }
}