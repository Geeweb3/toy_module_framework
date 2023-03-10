using PFramework;
using PFramework.AutoQuote;
using PFramework.UI;
using UnityEngine;
using UnityEngine.UI;

namespace GameMain
{
    [AAutoQuote]
    public partial class UIOpenRoom : UIBase
    {
        private PlayerMaterialCtrler pc;
        public override void OnInit(object args = null)
        {
            base.OnInit(args);
            InitUI();

            pc = FindObjectOfType<PlayerMaterialCtrler>();
        }

        public override void OnEnter(object args = null)
        {
            base.OnEnter(args);
           
            pc.OnFingerDown();
            
            mBtnSpace.onClick.RemoveAllListeners();
            MonoSub.I.DoInSecond(()=>
            {
                FindObjectOfType<ScreenShot>().CapturePic((((CharacterData) args)!).NftID);
                mBtnSpace.onClick.AddListener(ExitOpenRoom);
            },2.5f);
        }

        partial void OnClickBtnBack(Button go)
        {
            ExitOpenRoom();
        }

        private void ExitOpenRoom()
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
            UIMgr.CloseUI<UIOpenRoom>();
        }
    }
}