using PFramework;

namespace GameMain
{
    public class LoadSceneFromAndroidEventArgs:GameEventArgs
    {
        public static readonly int EventID = typeof(LoadSceneFromAndroidEventArgs).GetHashCode();
        public override int Id => EventID;

        public string msg { private set; get; }

        public override void Clear()
        {
            msg = "";
        }
        
        public LoadSceneFromAndroidEventArgs Create(string _msg)
        {
            msg = _msg;
            return this;
        }
    }
}