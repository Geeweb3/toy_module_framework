using System.Collections.Generic;
using PFramework;
using UnityEngine;

namespace GameMain
{
    public class UserDataMgr : Singleton<UserDataMgr>
    {
        private List<CharacterData> mCharacterMap = new List<CharacterData>();
        public List<CharacterData> CharacterMap => mCharacterMap;
        private int curSelectCharacterIndex = 0;

        public CharacterData CurCharacter => CharacterMap[curSelectCharacterIndex];
        
        private List<EquipData> mEquipMap = new List<EquipData>();
        public List<EquipData> EquipMap => mEquipMap;

        private CharacterEntity hero;

        public CharacterEntity HeroEntity
        {
            get
            {
                if (hero == null)
                    hero = Object.FindObjectOfType<CharacterEntity>();
                return hero;
            }
        }

        public void RecycleCharacter()
        {
            mCharacterMap[curSelectCharacterIndex].isSelected = false;
            EntityMgr.I.Recycle(HeroEntity);
            curSelectCharacterIndex = 0;
            hero = null;
        }
        
        public void RequestData()
        {
            foreach (var it in DataTable.I.mEquipDataTable)
            {
                mEquipMap.Add(new EquipData(it.Id));
            }

            for (int i = 0; i < 1; ++i)
            {
                mCharacterMap.Add(new CharacterData()); 
            }

            mCharacterMap[0].isSelected = true;
        }

        public void Exit()
        {
            curSelectCharacterIndex = 0;
            mCharacterMap.Clear();
            mEquipMap.Clear();
        }

        public void SetCharacterSelected(CharacterData da)
        {
            mCharacterMap[curSelectCharacterIndex].isSelected = false;
            curSelectCharacterIndex = mCharacterMap.IndexOf(da);
            da.isSelected = true;
        }

        public void SwitchEquipInCurSelectCharacter(EquipData data)
        {
            mCharacterMap[curSelectCharacterIndex].EquipMap[data.PartType] = data;
        }

        public void OrderByCharacter(bool isDescending)
        {
            var ct = CurCharacter;
            if(!isDescending)
                PFramework.Utils.Utility.Collection.OrderBy(CharacterMap, (t) => t.Rank);
            else 
                PFramework.Utils.Utility.Collection.OrderByDescending(CharacterMap, (t) => t.Rank);

            curSelectCharacterIndex = CharacterMap.IndexOf(ct);
        }
    }
}