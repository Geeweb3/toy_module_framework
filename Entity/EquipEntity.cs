using Database;
using PFramework;
using PFramework.Resource;
using UnityEditor;
using UnityEngine;
using Logger = PFramework.Logger;

namespace GameMain
{
    public class EquipEntity : Entity
    {
        public EquipData EquipData { get; private set; }

        private Renderer _skin;

        public bool isSame; 

        public override void OnShow(IEntityData entityData)
        {
            base.OnShow(entityData);
            EquipData = entityData as EquipData;
        }
        
        protected override void OnDetachFrom(Entity parentEntity)
        {
            base.OnDetachFrom(parentEntity);

            if (!isSame)
            {
                if (_skin != null)
                {
                    _skin.enabled = false;
                    if (EquipData.MeshName == "FACP_30_GlassesTime_01")
                        _skin.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            
            transform.parent = null;
            isSame = false;
        }

        protected override void OnAttachTo(Entity parentEntity)
        {
            base.OnAttachTo(parentEntity);
            transform.SetParent(parentEntity.transform);
        }

        /* Dropped
         public void SwitchMaterial(EquipData newData)
        {
            EquipData = newData;
            
            Material mat = ResourceMgr.I.Load<Material>(DataTable.I.mEquipDataTable[EquipData.Id].MatName+".mat");

            if (_skin.sharedMaterials.Length == 1)
                _skin.sharedMaterial = mat;
            else
                _skin.sharedMaterials[1] = mat;
            
            if(EquipData.MeshName=="FACP_30_GlassesTime_01")
                _skin.transform.GetChild(0).gameObject.SetActive(true); 
        }*/


        public void LoadEquip(Renderer skin)
        {
            _skin = skin;
            _skin.enabled = true;

            gameObject.name = EquipData.MeshName;
            Material mat = ResourceMgr.I.Load<Material>(DataTable.I.mEquipDataTable[EquipData.Id].MatName + ".mat");

            if (_skin.sharedMaterials.Length == 1)
                _skin.sharedMaterial = mat;
            else
            {
                if (EquipData.PartType != EnumEquipPart.Glasses)
                    _skin.sharedMaterials = new Material[2] {_skin.sharedMaterials[0], mat};
                else
                {
                    _skin.sharedMaterials = new Material[2] {mat, _skin.sharedMaterials[1]};
                }
            }

            if (EquipData.MeshName == "FACP_30_GlassesTime_01")
                _skin.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}