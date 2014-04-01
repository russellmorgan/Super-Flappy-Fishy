////////////////////////////////////////////////////////////////////////////////
//  
// @module <module_name>
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class iAdUseExample : MonoBehaviour {



	private GUIStyle style;
	private GUIStyle style2;

	private iAdBanner banner1;
	private iAdBanner banner2;

	private bool IsInterstisialsAdReady = false;

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	void Start() {

		//iAdBannerController.instance.addEventListener(iAdEvent.INTERSTITIAL_AD_DID_LOAD, OnInterstisialsLoaded);
		//iAdBannerController.instance.addEventListener(iAdEvent.INTERSTITIAL_AD_ACTION_DID_FINISH, OnInterstisialsFinish);


		InitStyles();

		float StartY = 20;
		float StartX = 10;
		
		
		StartY+= 80;
		StartX = 10;
		banner1 = iAdBannerController.instance.CreateAdBanner(TextAnchor.UpperLeft);
		banner1.Show();
	}


	private void InitStyles () {
		style =  new GUIStyle();
		style.normal.textColor = Color.white;
		style.fontSize = 16;
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperLeft;
		style.wordWrap = true;
		
		
		style2 =  new GUIStyle();
		style2.normal.textColor = Color.white;
		style2.fontSize = 12;
		style2.fontStyle = FontStyle.Italic;
		style2.alignment = TextAnchor.UpperLeft;
		style2.wordWrap = true;
	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	void OnGUI() {





	}
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------

	private void OnInterstisialsLoaded() {
		IsInterstisialsAdReady = true;
	}

	private void OnInterstisialsFinish() {
		IsInterstisialsAdReady = false;
	}


	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------

}
