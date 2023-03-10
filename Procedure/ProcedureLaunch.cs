using Database;
using PFramework;
using PFramework.Localization;
using PFramework.UI;
using UnityEngine;
using Logger = PFramework.Logger;

namespace GameMain
{
    public class ProcedureLaunch : ProcedureBase
    {
        public override void OnEnter(ProcedureBase preProcedure,params object[] parms)
        {
            base.OnEnter(preProcedure, parms);
            
            Config.I.Language = EnumLanguage.zh;
            LocalMgr.I.Init();

            UIMgr.I.LoadUIDataTable();
            UIMgr.ShowUI<UILoading>();

            SoundMgr.I.LoadSoundDataTable();

            EntityMgr.I.LoadEntityDataTable();

            DataTable.I.InitDataTable();

            UserDataMgr.I.RequestData();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            ProcedureMgr.I.ChangeProcedure<ProcedureGameRun>();
        }
    }
}