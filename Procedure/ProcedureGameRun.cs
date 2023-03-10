using PFramework;
using PFramework.Resource;
using PFramework.UI;
using UnityEngine;
using Logger = PFramework.Logger;

namespace GameMain
{
    public class ProcedureGameRun : ProcedureBase
    {
        private string SceneName;

        public override void OnEnter(ProcedureBase preProcedure, params object[] parms)
        {
            base.OnEnter(preProcedure, parms);

            SoundMgr.I.Play(0);

#if UNITY_EDITOR
            //ScenesMgr.I.LoadScene("ShowRoom", OnLoadShowRoom);
            ScenesMgr.I.LoadScene("OpenRoom", OnLoadOpenRoom);

#else
            using (AndroidJavaClass jc = new AndroidJavaClass("com.game.app.MainActivity"))
            {
                string temp = jc.CallStatic<string>("UnityCallBack","Loading Completed...");    
                Logger.Log(temp);
            }
#endif
            GameEventMgr.Register(LoadSceneFromAndroidEventArgs.EventID, OnLoadSceneEvent);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override void OnExit()
        {
            UserDataMgr.I.Exit();
            GameEventMgr.Unregister(LoadSceneFromAndroidEventArgs.EventID, OnLoadSceneEvent);
        }

        private void OnLoadSceneEvent(object sensor, GameEventArgs e)
        {
            SceneName = (e as LoadSceneFromAndroidEventArgs)?.msg;
            if (string.IsNullOrEmpty(SceneName))
                throw new PFrameworkException("No SceneName");

            if (SceneName == "ShowRoom")
                ScenesMgr.I.LoadScene(SceneName, OnLoadShowRoom);
            else if (SceneName == "OpenRoom")
                ScenesMgr.I.LoadScene(SceneName, OnLoadOpenRoom);
        }

        private void OnLoadOpenRoom()
        {
            var data = new CharacterData();
            UserDataMgr.I.CharacterMap.Add(data);
            EntityMgr.I.ShowEntity<CharacterEntity>("OpenCharacter", data,
                new Vector3(0, 0.247f, -0.265f), Quaternion.Euler(0, 180f, 0));
            UIMgr.CloseUI<UILoading>();
            UIMgr.ShowUI<UIOpenRoom>(data);
        }

        private void OnLoadShowRoom()
        {
            UserDataMgr.I.CurCharacter.isSelected = true;
            EntityMgr.I.ShowEntity<CharacterEntity>("Character", UserDataMgr.I.CurCharacter,
                new Vector3(0, 0.38f, 1.03f), Quaternion.Euler(0, 180, 0));
            
            Object.Instantiate(ResourceMgr.I.Load<GameObject>("LightsFull.prefab"));
            UIMgr.ShowUI<UIInventory>();
            MonoSub.I.DoInSecond(() => { UIMgr.CloseUI<UILoading>(); }, 0.5f);
        }
    }
}