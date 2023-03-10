using System;
using System.Collections.Generic;
using System.Linq;
using GameMain;
using PFramework;
using PFramework.AutoQuote;
using PFramework.Utils;
using UnityEngine;
using UnityEngine.UI;
using Logger = PFramework.Logger;
using Random = UnityEngine.Random;

namespace GameMain
{
    [AAutoQuote]
    public partial class BagContent : MonoBehaviour, ISuperScrollRectDataProvider
    {
        private int mCount;
        public SuperScrollRect ScrollRect;
        private List<EquipData> mBagDatas = new List<EquipData>();

        private EnumEquipPart curBagType;
        private int curSelectIndex = -1;

        public void Init()
        {
            InitUI();
        }

        public void Enter()
        {
            GameEventMgr.Register(BagItemSelectEventArgs.EventID, OnEquipSelectEvent);
            GameEventMgr.Register(SwitchCharacterEventArgs.EventID, OnSwitchCharacterEvent);
            GameEventMgr.Register(RemoveEquipEventArgs.EventID, OnRemoveEquipEvent);
            
            curBagType = EnumEquipPart.Cap;
            ChangeBagType();
        }

        public void Close()
        {
            GameEventMgr.Unregister(BagItemSelectEventArgs.EventID, OnEquipSelectEvent);
            GameEventMgr.Unregister(SwitchCharacterEventArgs.EventID, OnSwitchCharacterEvent);
            GameEventMgr.Unregister(RemoveEquipEventArgs.EventID, OnRemoveEquipEvent);
        }

        public void Refresh(List<EquipData> datas, EnumEquipPart type)
        {
            mBagDatas = datas;
            mCount = datas.Count;
            curBagType = type;
            ScrollRect.DoAwake(this);
        }

        public void SortUp()
        {
            Utility.Collection.OrderBy(mBagDatas, (t) => t.PartType);
            OnValueChange();
        }

        public void SortDown()
        {
            Utility.Collection.OrderByDescending(mBagDatas, (t) => t.PartType);
            OnValueChange();
        }

        public void AddItem(EquipData it)
        {
            mBagDatas.Add(it);
            mCount++;
            OnValueChange();
        }

        public void RemoveItem(int index)
        {
            mBagDatas.RemoveAt(index);
            mCount--;
            OnValueChange();
        }

        public void ModifyItem(int index, EquipData it)
        {
            mBagDatas[index] = it;
            ScrollRect.GetCell<BagItem>(index).Refresh(it);
        }

        private void OnValueChange()
        {
            ScrollRect.DoAwake(this);
        }

        public int GetCellCount()
        {
            return mCount;
        }

        public void SetCell(GameObject cell, int index)
        {
            BagItem it = cell.GetComponent<BagItem>();
            it.Refresh(mBagDatas[index]);
        }

        private void OnEquipSelectEvent(object sensor, GameEventArgs e)
        {
            EquipData da = ((BagItemSelectEventArgs) e).data;
            if (mBagDatas.Contains(da))
            {
                if (curSelectIndex != -1)
                    mBagDatas[curSelectIndex].isSelected = false;
                curSelectIndex = mBagDatas.IndexOf(da);
                da.isSelected = true;
            }
        }

        private void OnSwitchCharacterEvent(object sensor, GameEventArgs e)
        {
            if (curSelectIndex != -1)
            {
                mBagDatas[curSelectIndex].isEquiped = true;
                mBagDatas[curSelectIndex].isSelected = false;
            }
            
            CharacterData data = ((SwitchCharacterEventArgs) e).data;
            for (var i = 0; i < mBagDatas.Count; i++)
            {
                var it = mBagDatas[i];
                if (it.SerialId == data.EquipMap[curBagType]?.SerialId)
                {
                    it.isSelected = true;
                    it.isEquiped = false;
                    curSelectIndex = i;
                    break;
                }
            }
            OnValueChange();
        }

        private void OnRemoveEquipEvent(object sensor, GameEventArgs e)
        {
            EquipData da = ((RemoveEquipEventArgs) e).data;
            if (mBagDatas.Contains(da))
            {
                mBagDatas[curSelectIndex].isSelected = false;
                mBagDatas[curSelectIndex].isEquiped = false;
                curSelectIndex = -1;
            }
            else
            {
                if (UserDataMgr.I.EquipMap.Contains(da))
                {
                    var index = UserDataMgr.I.EquipMap.IndexOf(da);
                    UserDataMgr.I.EquipMap[index].isSelected = false;
                    UserDataMgr.I.EquipMap[index].isEquiped = false;
                }
            }
            
            OnValueChange();
        }

        partial void OnClickBtnCloth(Button go)
        {
            if (curBagType == EnumEquipPart.Cloth)
                return;
            curBagType = EnumEquipPart.Cloth;
            ChangeBagType();
        }
        
        partial void OnClickBtnPants(Button go)
        {
            if (curBagType == EnumEquipPart.Leg)
                return;
            curBagType = EnumEquipPart.Leg;

            ChangeBagType();
        }

        partial void OnClickBtnShoes(Button go)
        {
            if (curBagType == EnumEquipPart.Shoes)
                return;
            curBagType = EnumEquipPart.Shoes;

            ChangeBagType();
        }

        partial void OnClickBtnCap(Button go)
        {
            if (curBagType == EnumEquipPart.Cap)
                return;
            curBagType = EnumEquipPart.Cap;

            ChangeBagType();
        }


        partial void OnClickBtnHand(Button go)
        {
            if (curBagType == EnumEquipPart.Hand)
                return;
            curBagType = EnumEquipPart.Hand;

            ChangeBagType();
        }

        partial void OnClickBtnGlasses(Button go)
        {
            if (curBagType == EnumEquipPart.Glasses)
                return;
            curBagType = EnumEquipPart.Glasses;

            ChangeBagType();
        }



        partial void OnClickBtnMore(Button go)
        {
            /*if (curBagType == EnumEquipPart.Suit)
                return;
            curBagType = EnumEquipPart.Suit;

            ChangeBagType();*/
        }
        
        
        private void ChangeBagType()
        {
            if (curSelectIndex != -1)
            {
                mBagDatas[curSelectIndex].isSelected = false;
                mBagDatas[curSelectIndex].isEquiped = true;
            }

            var array = Utility.Collection.FindAll(UserDataMgr.I.EquipMap,
                (t) => t.PartType == curBagType);
            if (array.Length != 0)
            {
                if (UserDataMgr.I.CurCharacter.EquipMap[curBagType] == null)
                    curSelectIndex = -1;
                else
                {
                    for (var i = 0; i < array.Length; i++)
                    {
                        var it = array[i];
                        if (it.SerialId == UserDataMgr.I.CurCharacter.EquipMap[curBagType]?.SerialId)
                        {
                            it.isSelected = true;
                            it.isEquiped = false;
                            curSelectIndex = i;
                            break;
                        }
                    }
                }

                Refresh(array.ToList(), curBagType);
            }
        }
    }
}