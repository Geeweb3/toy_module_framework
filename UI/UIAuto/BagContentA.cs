using System.Collections; 
using System.Collections.Generic;   
using UnityEngine;     
using UnityEngine.UI;     
using TMPro;       
using PFramework.UI;
    
namespace GameMain
{ 
	public partial class BagContent:MonoBehaviour
	{			
		[SerializeField] private Button mBtnCap = null;
		
		[SerializeField] private Button mBtnCloth = null;
		
		[SerializeField] private Button mBtnHand = null;
		
		[SerializeField] private Button mBtnGlasses = null;
		
		[SerializeField] private Button mBtnPants = null;
		
		[SerializeField] private Button mBtnShoes = null;
		
		[SerializeField] private Button mBtnMore = null;
	
		partial void InitCustom();
		
		partial void OnClickBtnCap(Button go);

		partial void OnClickBtnCloth(Button go);

		partial void OnClickBtnHand(Button go);

		partial void OnClickBtnGlasses(Button go);

		partial void OnClickBtnPants(Button go);

		partial void OnClickBtnShoes(Button go);

		partial void OnClickBtnMore(Button go);

		private void InitUI()
		{					
			if(mBtnCap != null)
				mBtnCap.onClick.AddListener(delegate { OnClickBtnCap(mBtnCap); });
				
			if(mBtnCloth != null)
				mBtnCloth.onClick.AddListener(delegate { OnClickBtnCloth(mBtnCloth); });
				
			if(mBtnHand != null)
				mBtnHand.onClick.AddListener(delegate { OnClickBtnHand(mBtnHand); });
				
			if(mBtnGlasses != null)
				mBtnGlasses.onClick.AddListener(delegate { OnClickBtnGlasses(mBtnGlasses); });
				
			if(mBtnPants != null)
				mBtnPants.onClick.AddListener(delegate { OnClickBtnPants(mBtnPants); });
				
			if(mBtnShoes != null)
				mBtnShoes.onClick.AddListener(delegate { OnClickBtnShoes(mBtnShoes); });
				
			if(mBtnMore != null)
				mBtnMore.onClick.AddListener(delegate { OnClickBtnMore(mBtnMore); });

			InitCustom();           
		}
			
	}
}