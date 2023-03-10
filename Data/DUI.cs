using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
namespace Database
{
    [Serializable]
    public class DUI: DataItem
    {

        public string AssetName{ get;protected set; }

        public EnumUIType UIType{ get; protected set; }

        public EnumUILayer UILayer{ get; protected set; }

        public bool AllowMultiInstance{ get;protected set; }

        public bool PauseCoveredUIForm{ get;protected set; }

        public override void ParseByString(string data)
        {
            string[] tempStrs = data.Split('-');
            int count = -1;
            Id = int.Parse(tempStrs[count++]);
            AssetName = tempStrs[count++];
            UIType = (EnumUIType)int.Parse(tempStrs[count++]);
            UILayer = (EnumUILayer)int.Parse(tempStrs[count++]);
            AllowMultiInstance = int.Parse(tempStrs[count++]) != 0;
            PauseCoveredUIForm = int.Parse(tempStrs[count++]) != 0;
        }

        public override void ParseByBytes(MemoryStream ms)
        {
            BinaryReader rd = new BinaryReader(ms); 
            Id = rd.ReadInt32();
            AssetName = rd.ReadString();
            UIType = (EnumUIType)rd.ReadInt16();
            UILayer = (EnumUILayer)rd.ReadInt16();
            AllowMultiInstance = rd.ReadBoolean();
            PauseCoveredUIForm = rd.ReadBoolean();        
        }
    }
}   
