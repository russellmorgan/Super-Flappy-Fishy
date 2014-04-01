////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarketExample : MonoBehaviour {

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------
	
	void Awake() {
		IOSNative.instance.Init();
		PaymnetManagerExample.init();
	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	void OnGUI() {
		if(GUI.Button(new Rect(170, 10, 150, 50), "Perfrom Buy")) {
			PaymnetManagerExample.buyItem(PaymnetManagerExample.SMALL_PACK);
		}


		if(GUI.Button(new Rect(170, 70, 150, 50), "Perfrom Buy2")) {
			PaymnetManagerExample.buyItem(PaymnetManagerExample.NC_PACK);
		}
		
		if(GUI.Button(new Rect(170, 130, 150, 50), "Restore Purshases")) {
			InAppPurchaseManager.instance.restorePurchases();
		}


		if(GUI.Button(new Rect(170, 190, 150, 50), "Verifay Last Purshase")) {
			InAppPurchaseManager.instance.verifyLastPurchase(InAppPurchaseManager.SANDBOX_VERIFICATION_SERVER);
		}
	}
	
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------


}
