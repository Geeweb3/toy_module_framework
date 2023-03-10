using System.Collections;
using System.Collections.Generic;
using Database;
using PFramework;
using UnityEngine;
using Logger = PFramework.Logger;

namespace GameMain
{
    public class CharacterData : IEntityData
    {
        #region ModleInfos
        public string Eyebrow;
        public Color EyebrowColor;

        public string EyeBase;
        public Color EyeBaseColor;
        
        public string EyeLight;
        public Color EyeLightColor;

        public string Hair;
        public Color HairColor;
        
        public string Skin;
        public string SkinMakeUp;
        
        public float FaceShape;
        public float NoseShape;
        public float EyesIn;
        public float EyesUp;
        public float EyesSmall;
        public float Lips;
        public float Ears;
        public float EarsElf;
        #endregion

        public CharacterData()
        {
            //TestData
            FaceShape = Random.Range(0, 1f);
            NoseShape = Random.Range(0, 1f);
            EyesIn = Random.Range(0, 1f);
            EyesUp = Random.Range(0, 1f);
            EyesSmall = Random.Range(0, 1f);
            Lips = Random.Range(0, 1f);
            Ears = Random.Range(0, 1f);
            EarsElf = Random.Range(0, 1f);

            Eyebrow = DataTable.I.GetBodyName(Random.Range(0, 3));
            EyebrowColor = Color.black;//ColorTool.RandomColRGB();

            EyeBase = DataTable.I.GetBodyName(Random.Range(17,22));
            EyeBaseColor = ColorTool.RandomColRGB(108, 255);

            EyeLight = DataTable.I.GetBodyName(Random.Range(22,26));
            EyeLightColor = ColorTool.RandomColRGB();

            Hair = DataTable.I.GetBodyName(Random.Range(3, 10));
            HairColor = ColorTool.RandomColRGB();

            Skin = DataTable.I.GetBodyName(Random.Range(26, 37));


            NftID = "#0x" + Random.Range(100000, 999999);
            Gender = Random.Range(0, 2);
            CurMint = Random.Range(0, 6);
            MaxMint = 5;
            Rank = Random.Range(1, 6);
            Level = Random.Range(1, 50);
            CurExp = Random.Range(0, 100);
            MaxExp = 100;

            Attributes = new List<int>()
            {
                Random.Range(1, 100), Random.Range(1, 100), Random.Range(1, 100), Random.Range(1, 100),
                Random.Range(1, 100)
            };
            
            Gains = Random.Range(2, 100);
            CurEnergy = Random.Range(2, 100);
            EquipMap = new Dictionary<EnumEquipPart, EquipData>();
            EquipMap.Add(EnumEquipPart.Cap, null);
            EquipMap.Add(EnumEquipPart.Cloth, null);
            EquipMap.Add(EnumEquipPart.Hand, null);
            EquipMap.Add(EnumEquipPart.Glasses, null);
            EquipMap.Add(EnumEquipPart.Leg, null);
            EquipMap.Add(EnumEquipPart.Shoes, null);
            EquipMap.Add(EnumEquipPart.Suit, null);
        }


        public bool isSelected;

        public string NftID ;
        public int Gender ;
        public int CurMint ;
        public int MaxMint ;
        public int Rank ;
        public int Level ;
        public int CurExp;
        public int MaxExp;
        public List<int> Attributes;
        public int Gains;
        public int CurEnergy;
        public int MaxEnergy;
        public Dictionary<EnumEquipPart,EquipData> EquipMap;
    }
}