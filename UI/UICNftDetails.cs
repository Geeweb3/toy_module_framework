using System.Collections;
using System.Collections.Generic;
using PFramework;
using PFramework.AutoQuote;
using PFramework.Localization;
using PFramework.Resource;
using UnityEngine;

namespace GameMain
{
    [AAutoQuote]
    public partial class UICNftDetails : MonoBehaviour
    {
        private static Sprite[] GenderSprites;
        public void Init()
        {
            InitUI();
            GenderSprites = new Sprite[2]
            {
                ResourceMgr.I.Load<Sprite>("female.png"),
                ResourceMgr.I.Load<Sprite>("male.png")
            };
        }

        public void Enter()
        {
            GameEventMgr.Register(SwitchCharacterEventArgs.EventID, OnSwitchCharacterEvent);
        }


        public void Close()
        {
            GameEventMgr.Unregister(SwitchCharacterEventArgs.EventID, OnSwitchCharacterEvent);
        }

        private void OnSwitchCharacterEvent(object sensor,GameEventArgs e)
        {
            Refresh(((SwitchCharacterEventArgs) e).data);
        }

        public void Refresh(CharacterData data)
        {
            mTMPNftId.text = data.NftID;
            mImgGender.sprite = GenderSprites[data.Gender];
            mTMPLevel.text = data.Level.ToString();
            mTMPRank.text = data.Rank.ToString();
            mTMPMint.text = $"{data.CurMint}/{data.MaxMint}";    
            mTMPExp.text = data.CurExp + "/" + data.MaxExp;
            mSliderExp.value = data.CurExp / (data.MaxExp * 1.0f);
            mTMPAbility1.text = data.Attributes[0].ToString();
            mTMPAbility2.text = data.Attributes[1].ToString();
            mTMPAbility3.text = data.Attributes[2].ToString();
            mTMPAbility4.text = data.Attributes[3].ToString();
            mTMPGainsValue.text = data.Gains + "GGG/h";
            mTMPEnergyValue.text = data.CurEnergy + "/" + data.MaxEnergy;
        }
    }
}