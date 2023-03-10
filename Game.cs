using PFramework;
using PFramework.Resource;
using PFramework.UI;
using UnityEngine;
using Logger = PFramework.Logger;
using PFramework.Utils;

namespace GameMain
{
    public class Game : MonoBehaviour
    {
        public enum LoadMode
        {
            Editor,
            AssetBundle,
        }
        
        public string typeName="GameMain.ProcedureLaunch";
        [SerializeReference]public ProcedureBase startProcedure;
        public static Game ActiveGame { get; private set; }

        [SerializeField]private LoadMode loadMode;
        
        private void Awake()
        {
            if (ActiveGame != null)
            {
                DestroyImmediate(this);
            }
            else
            {
                ActiveGame = this;
            }
            Config.I.Init();
            
#if UNITY_EDITOR
            loadMode = LoadMode.Editor;
#else
            loadMode = (LoadMode)Config.I.loadMode;
#endif

            InitModule();
            
            System.Type type = System.Type.GetType(typeName);
            startProcedure = Utility.Reflect.CreateInstance<ProcedureBase>(type);
            if (startProcedure != null)
            {
                ProcedureMgr.I.AddProcedure(startProcedure);
                ProcedureMgr.I.ChangeProcedure(startProcedure.GetType());
            }
            else
                Logger.Error("No Start Procedure");

            DontDestroyOnLoad(this);
        }
        
        void Update()
        {
            GameModuleMgr.ModuleUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        }

        private void InitModule()
        {
            GameModuleMgr.AddModule<LogMgr>();

            if (loadMode == LoadMode.Editor)    
            {
#if UNITY_EDITOR
                GameModuleMgr.AddModule<ResourceMgr>(new AssetDataBaseLoadHelper(), $"{Application.streamingAssetsPath}/ResourceMap.txt");
#else
                GameModuleMgr.AddModule<ResourceMgr>(new AssetBundleLoadHelper(), $"{Application.persistentDataPath}/ResourceMap.txt");
#endif
            }
            else if(loadMode == LoadMode.AssetBundle)
                GameModuleMgr.AddModule<ResourceMgr>(new AssetBundleLoadHelper(), $"{Application.persistentDataPath}/ResourceMap.txt");
            
            GameModuleMgr.AddModule<UIMgr>();
            GameModuleMgr.AddModule<PoolMgr>();
            GameModuleMgr.AddModule<GameEventMgr>();
            GameModuleMgr.AddModule<SoundMgr>();
            GameModuleMgr.AddModule<EntityMgr>(transform);
        }
        
        public void LoadScene(string msg)   
        {
            GameEventMgr.Fire(this, ReferencePool.Acquire<LoadSceneFromAndroidEventArgs>().Create(msg));
        }
    }
}