using System.Collections;
using System.Collections.Generic;
using PFramework;
using UnityEngine;

namespace GameMain
{

    public class SwichEquipEventArgs : GameEventArgs
    {
        public static readonly int EventID = typeof(SwichEquipEventArgs).GetHashCode();
        public override int Id => EventID;

        public EquipData data { private set; get; }
        public EnumEquipPart PartType { private set; get; }

        public override void Clear()
        {
            data = null;
        }
        
        public SwichEquipEventArgs Create(EquipData _data,EnumEquipPart part = EnumEquipPart.Backpack)
        {
            data = _data;
            PartType = part;
            return this;
        }
    }
}