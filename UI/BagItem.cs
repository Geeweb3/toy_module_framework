using System;
using GameMain;
using PFramework;
using PFramework.Resource;
using UnityEngine;
using UnityEngine.UI;

namespace GameMain
{
    public class BagItem : MonoBehaviour
    {
        [SerializeField] private Image mImgIcon;
        [SerializeField] private Button mBtnClick;
        [SerializeField] private Image mImgBtn;
        private static Sprite[] mImgBtnStates;
        private bool isSelected;

        public void Awake()
        {
            mImgBtnStates = new Sprite[2]
            {
                ResourceMgr.I.Load<Sprite>("1.png"),
                ResourceMgr.I.Load<Sprite>("2.png")
            };
        }

        public void Refresh(EquipData da)
        {
            mImgIcon.sprite = ResourceMgr.I.Load<Sprite>(da.MaterialName + ".png");
            if (da.isEquiped)
            {
                mBtnClick.onClick.RemoveAllListeners();
                mImgIcon.color = new Color(1, 1, 1, 40 / 255f);
                mImgBtn.sprite = mImgBtnStates[1];
                return;
            }

            mImgIcon.color = Color.white;

            if (GameEventMgr.Check(BagItemSelectEventArgs.EventID))
                GameEventMgr.Unregister(BagItemSelectEventArgs.EventID, OnBtnSelectEvent);

            mBtnClick.onClick.RemoveAllListeners();
            mBtnClick.onClick.AddListener(() =>
            {
                if (da.isSelected)
                    return;

                mImgBtn.sprite = mImgBtnStates[0];
                EntityMgr.I.Attach(EntityMgr.I.ShowEntity<EquipEntity>(new EquipData(da.Id)), UserDataMgr.I.HeroEntity);
                UserDataMgr.I.SwitchEquipInCurSelectCharacter(da);
                GameEventMgr.Fire(this, ReferencePool.Acquire<SwichEquipEventArgs>().Create(da));
                GameEventMgr.Fire(this, ReferencePool.Acquire<BagItemSelectEventArgs>().Create(da));
                isSelected = true;
                GameEventMgr.Register(BagItemSelectEventArgs.EventID, OnBtnSelectEvent);
            });

            isSelected = da.isSelected;
            if (!da.isSelected)
                mImgBtn.sprite = mImgBtnStates[1];
            else
            {
                mImgBtn.sprite = mImgBtnStates[0];
                GameEventMgr.Register(BagItemSelectEventArgs.EventID, OnBtnSelectEvent);
            }
        }

        private void OnBtnSelectEvent(object sensor, GameEventArgs e)
        {
            if (isSelected)
            {
                mImgBtn.sprite = mImgBtnStates[1];
                isSelected = false;
                GameEventMgr.Unregister(BagItemSelectEventArgs.EventID, OnBtnSelectEvent);
            }
        }
    }
}