using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Database;
using PFramework;
using PFramework.AutoQuote;
using PFramework.UI;
using UnityEngine;
using UnityEngine.UI;
using Logger = PFramework.Logger;

namespace GameMain
{
    [AAutoQuote]
    public partial class UIInventory : UIBase
    {
        private BagContent _mBagContent;
        private NftContent _mNftContent;
        private UICNftDetails _mUicNftDetails;
        private UICEquipPanel _mUicEquipPanel;
        
        public override void OnInit(object args = null)
        {
            base.OnInit(args);
            InitUI();
            _mBagContent = GetComponentInChildren<BagContent>();
            _mBagContent.Init();
            _mNftContent = GetComponentInChildren<NftContent>();
            _mNftContent.Init();
            _mUicNftDetails = GetComponentInChildren<UICNftDetails>();
            _mUicNftDetails.Init();
            _mUicEquipPanel = GetComponentInChildren<UICEquipPanel>();
            _mUicEquipPanel.Init();
        }

        public override void OnEnter(object args = null)
        {
            base.OnEnter(args);

            _mBagContent.Enter();
           _mNftContent.Refresh();
           _mUicNftDetails.Enter();
           _mUicEquipPanel.Enter();

           if (UserDataMgr.I.CharacterMap.Count > 0)
           {
               GameEventMgr.Fire(this, ReferencePool.Acquire<SwitchCharacterEventArgs>().Create(UserDataMgr.I.CharacterMap[0]));
           }
        }

        public override void OnPause(object args = null)
        {
            base.OnPause(args);
        }

        public override void OnResume(object args = null)
        {
            base.OnResume(args);
        }

        public override void OnExit(object args = null)
        {
            base.OnExit(args);
            _mUicEquipPanel.Close();
            _mBagContent.Close();
            _mUicNftDetails.Close();
        }
        
        partial void OnClickBtnRandom(Button go)
        {
            UserDataMgr.I.HeroEntity.LoadCharacterModel(new CharacterData());
        }

        partial void OnClickBtnBack(Button go)
        {
            
#if !UNITY_EDITOR
            using (AndroidJavaClass jc = new AndroidJavaClass("com.game.app.MainActivity"))
            {
                using (AndroidJavaObject jo = jc.CallStatic<AndroidJavaObject>("GetInstance"))
                {
                    jo.Call("ExitUnityActivity");
                }
            }
#else
            UIMgr.ShowUI<UIMain>();
#endif
            UserDataMgr.I.RecycleCharacter();
            UIMgr.CloseUI<UIInventory>();
        }
    }
}