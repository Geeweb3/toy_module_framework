using PFramework.UI;
using PFramework;
using PFramework.AutoQuote;
using PFramework.Localization;
using UnityEngine;
using UnityEngine.UI;
namespace GameMain
{
    [AAutoQuote]
    public partial class UIMain : UIBase
    {
        private Game game;
        public override void OnInit(object args = null)
        {
            base.OnInit(args);
            InitUI();
            game = GameObject.FindObjectOfType<Game>();
        }

        partial void OnClickBtnStart(Button go)
        {
            game.LoadScene("ShowRoom");
            UIMgr.CloseUI<UIMain>();
        }

        partial void OnClickBtnExit(Button go)
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        
        partial void OnClickBtnConfig(Button go)
        {
            game.LoadScene("OpenRoom");
            UIMgr.CloseUI<UIMain>();
        }
    }
}