using System.Collections;
using System.Collections.Generic;
using System.IO;
using GameMain;
using PFramework;
using PFramework.Resource;
using UnityEngine;
using UnityEngine.UI;

namespace GameMain
{

    public class NftItem : MonoBehaviour
    {
        private CharacterData mData;

        [SerializeField] private Button mBtnClick;
        [SerializeField] private Image mImgIcon;
        [SerializeField] private Image mImgBtn;
        private static Sprite[] mImgBtnStates;

        public void Awake()
        {
            mImgBtnStates = new Sprite[2]
            {
                ResourceMgr.I.Load<Sprite>("3.png"),
                ResourceMgr.I.Load<Sprite>("4.png")
            };
        }

        public void Refresh(CharacterData da)
        {
            mBtnClick.onClick.RemoveAllListeners();
            mBtnClick.onClick.AddListener(() =>
            {
                if (da.isSelected)
                    return;

                mImgBtn.sprite = mImgBtnStates[0];
                UserDataMgr.I.HeroEntity.LoadCharacterModel(da);
                
                UserDataMgr.I.SetCharacterSelected(da);
                GameEventMgr.Fire(this, ReferencePool.Acquire<SwitchCharacterEventArgs>().Create(da));
                GameEventMgr.Fire(this, ReferencePool.Acquire<NftItemSelectEventArgs>().Create());
                GameEventMgr.Register(NftItemSelectEventArgs.EventID, OnBtnSelectEvent);
            });

            if (GameEventMgr.Check(NftItemSelectEventArgs.EventID))
                GameEventMgr.Unregister(NftItemSelectEventArgs.EventID, OnBtnSelectEvent);

            if (da.isSelected)
            {
                mImgBtn.sprite = mImgBtnStates[0];
                GameEventMgr.Register(NftItemSelectEventArgs.EventID, OnBtnSelectEvent);
            }
            else
            {
                mImgBtn.sprite = mImgBtnStates[1];
            }

            LoadNftIcon(da.NftID);
        }

        private void OnBtnSelectEvent(object sensor, GameEventArgs e)
        {
            mImgBtn.sprite = mImgBtnStates[1];
            GameEventMgr.Unregister(NftItemSelectEventArgs.EventID, OnBtnSelectEvent);
        }

        private void LoadNftIcon(string nftID)
        {
            if (File.Exists(Application.persistentDataPath + $"/{nftID}.jpg"))
            {
                var bytes = File.ReadAllBytes(Application.persistentDataPath + $"/{nftID}.jpg");
                Texture2D texture = new Texture2D(464, 464);
                texture.LoadImage(bytes);
                Sprite sp = Sprite.Create(texture,new Rect(0,0,texture.width,texture.height),Vector2.zero);
                mImgIcon.sprite = sp;
            }
            else
            {
                mImgIcon.sprite = ResourceMgr.I.Load<Sprite>("transparent.png");
            }
        }
    }
}