using System.Collections; 
using System.Collections.Generic;   
using UnityEngine;     
using UnityEngine.UI;     
using TMPro;       
using PFramework.UI;
    
namespace GameMain
{ 
	public partial class UIMain:UIBase
	{			
		[SerializeField] private Button mBtnStart = null;
		
		[SerializeField] private Button mBtnConfig = null;
		
		[SerializeField] private Button mBtnExit = null;
	
		partial void InitCustom();
		
		partial void OnClickBtnStart(Button go);

		partial void OnClickBtnConfig(Button go);

		partial void OnClickBtnExit(Button go);

		private void InitUI()
		{					
			if(mBtnStart != null)
				mBtnStart.onClick.AddListener(delegate { OnClickBtnStart(mBtnStart); });
				
			if(mBtnConfig != null)
				mBtnConfig.onClick.AddListener(delegate { OnClickBtnConfig(mBtnConfig); });
				
			if(mBtnExit != null)
				mBtnExit.onClick.AddListener(delegate { OnClickBtnExit(mBtnExit); });

			InitCustom();           
		}
			
	}
}