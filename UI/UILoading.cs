using System;
using PFramework;
using PFramework.AutoQuote;
using PFramework.UI;

namespace GameMain
{
    [AAutoQuote]
    public partial class UILoading : UIBase
    {
        public override void OnInit(object args = null)
        {
            base.OnInit(args);
            InitUI();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            float progress = ScenesMgr.I.Progres;
            mTextProgress.text = (progress*100).ToString("F2") + "%";
            mSliderProgress.value = progress;
        }
        
        public override void OnExit(object args = null)
        {
            base.OnExit(args);
        }

        public void OnLoadComplete()
        {
            mTextProgress.text = "100%";
            mSliderProgress.value = 1;
        }
    }
}