using System.Collections; 
using System.Collections.Generic;   
using UnityEngine;     
using UnityEngine.UI;     
using TMPro;       
using PFramework.UI;
    
namespace GameMain
{ 
	public partial class UIInventory:UIBase
	{			
		[SerializeField] private Button mBtnBack = null;
		
		[SerializeField] private Button mBtnRandom = null;
	
		partial void InitCustom();
		
		partial void OnClickBtnRandom(Button go);
		
		partial void OnClickBtnBack(Button go);

		private void InitUI()
		{					
			if(mBtnRandom != null)
				mBtnRandom.onClick.AddListener(delegate { OnClickBtnRandom(mBtnRandom); });
			
			if(mBtnBack != null)
				mBtnBack.onClick.AddListener(delegate { OnClickBtnBack(mBtnBack); });

			InitCustom();           
		}
			
	}
}