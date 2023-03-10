using PFramework;

namespace GameMain
{
    public class RemoveEquipEventArgs:GameEventArgs
    {
        public static readonly int EventID = typeof(RemoveEquipEventArgs).GetHashCode();
        public override int Id => EventID;

        public EquipData data;
        public override void Clear()
        {
            data = null;
        }
        
        public RemoveEquipEventArgs Create(EquipData da)
        {
            data = da;
            return this;
        }
    }
}