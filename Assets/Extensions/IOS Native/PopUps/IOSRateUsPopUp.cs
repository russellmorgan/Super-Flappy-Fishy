////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IOSRateUsPopUp : BaseIOSPopup {
	
	public string rate;
	public string remind;
	public string declined;
	
	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	public static IOSRateUsPopUp Create() {
		return Create("Like the Game?", "Rate US");
	}
	
	public static IOSRateUsPopUp Create(string title, string message) {
		return Create(title, message, "Rate Now", "Ask me later", "No, thanks");
	}
	
	public static IOSRateUsPopUp Create(string title, string message, string rate, string remind, string declined) {
		IOSRateUsPopUp popup = new GameObject("IOSRateUsPopUp").AddComponent<IOSRateUsPopUp>();
		popup.title = title;
		popup.message = message;
		popup.rate = rate;
		popup.remind = remind;
		popup.declined = declined;
		
		popup.init();
		
	
		return popup;
	}
	
	
	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	
	public void init() {
		IOSNative.showRateUsPopUP(title, message, rate, remind, declined);
	}
	
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------
	
	public void onPopUpCallBack(string buttonIndex) {
		int index = System.Convert.ToInt16(buttonIndex);
		switch(index) {
			case 0: 
				dispatch(BaseEvent.COMPLETE, IOSDialogResult.RATED);
				break;
			case 1:
				dispatch(BaseEvent.COMPLETE, IOSDialogResult.REMIND);
				break;
			case 2:
				dispatch(BaseEvent.COMPLETE, IOSDialogResult.DECLINED);
				break;
		}
		
		
		
		Destroy(gameObject);
	} 
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------


}
