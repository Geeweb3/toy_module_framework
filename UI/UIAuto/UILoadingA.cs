using System.Collections; 
using System.Collections.Generic;   
using UnityEngine;     
using UnityEngine.UI;     
using TMPro;       
using PFramework.UI;
    
namespace GameMain
{ 
	public partial class UILoading:UIBase
	{			
		[SerializeField] private TextMeshProUGUI mTextProgress = null;
		
		[SerializeField] private Slider mSliderProgress = null;
	
		partial void InitCustom();
		
		partial void OnClickSliderProgress(Slider go);

		private void InitUI()
		{					
			if(mSliderProgress != null)
				mSliderProgress.onValueChanged.AddListener(delegate { OnClickSliderProgress(mSliderProgress); });

			InitCustom();           
		}
			
	}
}