using System.Collections.Generic;
using Database;
using PFramework;

namespace GameMain
{
    public class DataTable:Singleton<DataTable>
    {
        public List<DEquip> mEquipDataTable { get; private set; }
        public List<DBody> mBodyDataTable { get; private set; }

        public string GetBodyName(EnumBodyPart type)
        {
            foreach (var it in mBodyDataTable)
            {
                if (it.BodyPart == type)
                    return it.AssetName;
            }

            return null;
        }

        public string GetBodyName(int id)
        {
            if (mBodyDataTable.Count > id)
                return mBodyDataTable[id].AssetName;
            throw new PFrameworkException($"No id:{id} in bodyData");
        }


        private void LoadDataTable()
        {
            mEquipDataTable = DataLoader.GetDataTable<List<DEquip>>("DEquip.json");
            mBodyDataTable = DataLoader.GetDataTable<List<DBody>>("DBody.json");
        }

        public void InitDataTable()
        {
            LoadDataTable();
        }
    }
}