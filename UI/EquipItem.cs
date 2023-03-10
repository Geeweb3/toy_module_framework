using System;
using System.Collections;
using System.Collections.Generic;
using GameMain;
using PFramework;
using PFramework.Resource;
using UnityEngine;
using UnityEngine.UI;

namespace GameMain
{
    public class EquipItem : MonoBehaviour
    {
        [SerializeField] private Image mIcon;
        private Button mBtnClick;

        private void Awake()
        {
            mBtnClick = GetComponent<Button>();

        }

        public void Refresh(EquipData data)
        {
            if (data != null)
                mIcon.sprite = ResourceMgr.I.Load<Sprite>(data.MaterialName + ".png");
            else
                mIcon.sprite = ResourceMgr.I.Load<Sprite>("transparent.png");

            mBtnClick.onClick.RemoveAllListeners();
            mBtnClick.onClick.AddListener(() =>
            {
                if (data == null)
                    return;

                GameEventMgr.Fire(this, ReferencePool.Acquire<RemoveEquipEventArgs>().Create(data));
                UserDataMgr.I.HeroEntity.RemoveEquip(data.PartType);
                UserDataMgr.I.CurCharacter.EquipMap[data.PartType] = null;
                mIcon.sprite = ResourceMgr.I.Load<Sprite>("transparent.png");
            });
        }
    }
}