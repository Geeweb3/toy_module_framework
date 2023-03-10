using PFramework;

namespace GameMain
{
    public class SwitchCharacterEventArgs:GameEventArgs
    {
        public static readonly int EventID = typeof(SwitchCharacterEventArgs).GetHashCode();
        public override int Id => EventID;

        public CharacterData data { private set; get; }

        public override void Clear()
        {
            data = null;
        }
        
        public SwitchCharacterEventArgs Create(CharacterData _data)
        {
            data = _data;
            return this;
        }
    }
}