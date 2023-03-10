using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
namespace Database
{
    [Serializable]
    public class DEquip: DataItem
    {

        public string AssetName{ get;protected set; }

        public EnumEquipPart EquipPart{ get; protected set; }

        public string MatName{ get;protected set; }

        public override void ParseByString(string data)
        {
            string[] tempStrs = data.Split('-');
            int count = -1;
            Id = int.Parse(tempStrs[count++]);
            AssetName = tempStrs[count++];
            EquipPart = (EnumEquipPart)int.Parse(tempStrs[count++]);
            MatName = tempStrs[count++];
        }

        public override void ParseByBytes(MemoryStream ms)
        {
            BinaryReader rd = new BinaryReader(ms); 
            Id = rd.ReadInt32();
            AssetName = rd.ReadString();
            EquipPart = (EnumEquipPart)rd.ReadInt16();
            MatName = rd.ReadString();        
        }
    }
}   
