using System.Collections; 
using System.Collections.Generic;   
using UnityEngine;     
using UnityEngine.UI;     
using TMPro;       
using PFramework.UI;
    
namespace GameMain
{ 
	public partial class NftContent:MonoBehaviour
	{			
		[SerializeField] private Button mBtnSortName = null;
		
		[SerializeField] private Button mBtnSortGrade = null;
	
		partial void InitCustom();
		
		partial void OnClickBtnSortName(Button go);

		partial void OnClickBtnSortGrade(Button go);

		private void InitUI()
		{					
			if(mBtnSortName != null)
				mBtnSortName.onClick.AddListener(delegate { OnClickBtnSortName(mBtnSortName); });
				
			if(mBtnSortGrade != null)
				mBtnSortGrade.onClick.AddListener(delegate { OnClickBtnSortGrade(mBtnSortGrade); });

			InitCustom();           
		}
			
	}
}