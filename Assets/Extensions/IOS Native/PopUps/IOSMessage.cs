////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IOSMessage : BaseIOSPopup {
	
	
	public string ok;
	
	//--------------------------------------
	// INITIALIZE
	//--------------------------------------
	
	public static IOSMessage Create(string title, string message) {
		return Create(title, message, "Ok");
	}
		
	public static IOSMessage Create(string title, string message, string ok) {
		IOSMessage dialog;
		dialog  = new GameObject("IOSPopUp").AddComponent<IOSMessage>();
		dialog.title = title;
		dialog.message = message;
		dialog.ok = ok;
		
		dialog.init();
		
		return dialog;
	}
	
	
	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	public void init() {
		IOSNative.showMessage(title, message, ok);
	}
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------
	
	public void onPopUpCallBack(string buttonIndex) {
		
		dispatch(BaseEvent.COMPLETE);
		Destroy(gameObject);
		
		
	}
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------


}
