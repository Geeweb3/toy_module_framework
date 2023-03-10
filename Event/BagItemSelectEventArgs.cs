using PFramework;

namespace GameMain
{
    public class BagItemSelectEventArgs:GameEventArgs
    {
        public static readonly int EventID = typeof(BagItemSelectEventArgs).GetHashCode();
        public override int Id => EventID;

        public EquipData data { private set; get; }

        public override void Clear()
        {
            data = null;
        }
        
        public BagItemSelectEventArgs Create(EquipData _data)
        {
            data = _data;
            return this;
        }
    }
}