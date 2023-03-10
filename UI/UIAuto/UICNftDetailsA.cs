using System.Collections; 
using System.Collections.Generic;   
using UnityEngine;     
using UnityEngine.UI;     
using TMPro;       
using PFramework.UI;
    
namespace GameMain
{ 
	public partial class UICNftDetails:MonoBehaviour
	{			
		[SerializeField] private TextMeshProUGUI mTMPNftId = null;
		
		[SerializeField] private Image mImgGender = null;
		
		[SerializeField] private TextMeshProUGUI mTMPLevel = null;
		
		[SerializeField] private TextMeshProUGUI mTMPRank = null;
		
		[SerializeField] private TextMeshProUGUI mTMPMint = null;
		
		[SerializeField] private TextMeshProUGUI mTMPExp = null;
		
		[SerializeField] private Button mBtnAdd = null;
		
		[SerializeField] private Slider mSliderExp = null;
		
		[SerializeField] private TextMeshProUGUI mTMPAbility1 = null;
		
		[SerializeField] private TextMeshProUGUI mTMPAbility2 = null;
		
		[SerializeField] private TextMeshProUGUI mTMPAbility3 = null;
		
		[SerializeField] private TextMeshProUGUI mTMPAbility4 = null;
		
		[SerializeField] private TextMeshProUGUI mTMPGainsValue = null;
		
		[SerializeField] private TextMeshProUGUI mTMPEnergyValue = null;
		
		[SerializeField] private Button mBtnSave = null;
	
		partial void InitCustom();
		
		partial void OnClickBtnAdd(Button go);

		partial void OnClickSliderExp(Slider go);

		partial void OnClickBtnSave(Button go);

		private void InitUI()
		{					
			if(mBtnAdd != null)
				mBtnAdd.onClick.AddListener(delegate { OnClickBtnAdd(mBtnAdd); });
				
			if(mSliderExp != null)
				mSliderExp.onValueChanged.AddListener(delegate { OnClickSliderExp(mSliderExp); });
				
			if(mBtnSave != null)
				mBtnSave.onClick.AddListener(delegate { OnClickBtnSave(mBtnSave); });

			InitCustom();           
		}
			
	}
}