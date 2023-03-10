using Database;
using PFramework;

namespace GameMain
{
    public class EquipData : IEntityData
    {
        public static int aaa = 0;
        public int SerialId;
        public int Id;
        public string MeshName;
        public string MaterialName;
        public EnumEquipPart PartType;
        public bool isSelected;
        public bool isEquiped;

        public EquipData(int id)
        {
            SerialId = aaa++;
            Id = id;
            DEquip eq = DataTable.I.mEquipDataTable[Id];
            MeshName = eq.AssetName;
            PartType = eq.EquipPart;
            MaterialName = eq.MatName;
        }
    }
}