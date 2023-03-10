using PFramework;

namespace GameMain
{
    public class NftItemSelectEventArgs:GameEventArgs
    {
        public static readonly int EventID = typeof(NftItemSelectEventArgs).GetHashCode();
        public override int Id => EventID;

        public override void Clear()
        {
        }
        
        public NftItemSelectEventArgs Create()
        {
            return this;
        }
    }
}