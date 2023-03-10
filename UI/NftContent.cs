using System.Collections;
using System.Collections.Generic;
using PFramework.AutoQuote;
using UnityEngine;
using UnityEngine.UI;

namespace GameMain
{
    [AAutoQuote]
    public partial class NftContent : MonoBehaviour, ISuperScrollRectDataProvider
    {
        public SuperScrollRect ScrollRect;
        private bool isDescending;

        public void Init()
        {
            InitUI();
            isDescending = false;
        }

        public void Refresh()
        {
            ScrollRect.DoAwake(this);
        }

        public int GetCellCount()
        {
            return UserDataMgr.I.CharacterMap.Count;
        }

        public void SetCell(GameObject cell, int index)
        {
            NftItem it = cell.GetComponent<NftItem>();
            it.Refresh(UserDataMgr.I.CharacterMap[index]);
        }

        partial void OnClickBtnSortName(Button go)
        {
        }

        partial void OnClickBtnSortGrade(Button go)
        {
            UserDataMgr.I.OrderByCharacter(isDescending);
            isDescending = !isDescending;
            Refresh();
        }
    }
}