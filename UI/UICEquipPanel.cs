using System.Collections;
using System.Collections.Generic;
using PFramework;
using UnityEngine;

namespace GameMain
{
    public class UICEquipPanel : MonoBehaviour
    {
        private Dictionary<EnumEquipPart, EquipItem> mEquipMap;

        public void Init()
        {
            mEquipMap = new Dictionary<EnumEquipPart, EquipItem>();
            mEquipMap.Add(EnumEquipPart.Cap, TransformTool.FindChild<EquipItem>(transform, "Cap"));
            mEquipMap.Add(EnumEquipPart.Cloth, TransformTool.FindChild<EquipItem>(transform, "Cloth"));
            mEquipMap.Add(EnumEquipPart.Hand, TransformTool.FindChild<EquipItem>(transform, "Hand"));
            mEquipMap.Add(EnumEquipPart.Glasses, TransformTool.FindChild<EquipItem>(transform, "Glasses"));
            mEquipMap.Add(EnumEquipPart.Leg, TransformTool.FindChild<EquipItem>(transform, "Leg"));
            mEquipMap.Add(EnumEquipPart.Shoes, TransformTool.FindChild<EquipItem>(transform, "Shoes"));
            mEquipMap.Add(EnumEquipPart.Suit, TransformTool.FindChild<EquipItem>(transform, "Cloth"));
        }

        public void Enter()
        {
            GameEventMgr.Register(SwichEquipEventArgs.EventID, OnSwitchEquipEvent);
            GameEventMgr.Register(SwitchCharacterEventArgs.EventID, OnSwitchCharacterEvent);
        }

        public void Close()
        {
            GameEventMgr.Unregister(SwichEquipEventArgs.EventID, OnSwitchEquipEvent);
            GameEventMgr.Unregister(SwitchCharacterEventArgs.EventID, OnSwitchCharacterEvent);
        }

        private void OnSwitchEquipEvent(object sensor, GameEventArgs e)
        {
            var args = e as SwichEquipEventArgs;
            if (args.data != null)
                mEquipMap[args.data.PartType].Refresh(args.data);
            else
                mEquipMap[args.PartType].Refresh(null);
        }

        private void OnSwitchCharacterEvent(object sensor, GameEventArgs e)
        {
            SwitchCharacterEventArgs args = e as SwitchCharacterEventArgs;
            foreach (var (k, v) in args.data.EquipMap)
            {
                mEquipMap[k].Refresh(v);
                if(v != null)
                    EntityMgr.I.Attach(EntityMgr.I.ShowEntity<EquipEntity>(v), UserDataMgr.I.HeroEntity);
                else
                    UserDataMgr.I.HeroEntity.RemoveEquip(k);
            }
        }
    }
}