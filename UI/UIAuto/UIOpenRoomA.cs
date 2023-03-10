using System.Collections; 
using System.Collections.Generic;   
using UnityEngine;     
using UnityEngine.UI;     
using TMPro;       
using PFramework.UI;
    
namespace GameMain
{ 
	public partial class UIOpenRoom:UIBase
	{			
		[SerializeField] private Button mBtnSpace = null;
		
		[SerializeField] private Button mBtnBack = null;
	
		partial void InitCustom();
		

		partial void OnClickBtnBack(Button go);

		private void InitUI()
		{					
			if(mBtnBack != null)
				mBtnBack.onClick.AddListener(delegate { OnClickBtnBack(mBtnBack); });

			InitCustom();           
		}
			
	}
}