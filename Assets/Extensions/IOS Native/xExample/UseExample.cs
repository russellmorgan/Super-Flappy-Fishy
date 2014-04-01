////////////////////////////////////////////////////////////////////////////////
//  
// @module <module_name>
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class UseExample : MonoBehaviour {

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	void Awake() {
		IOSNative.instance.Init();
		GameCenterManager.init();
	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	void OnGUI() {
		if(GUI.Button(new Rect(330, 10, 150, 50), "Show Rate pop up")) {
			IOSNative.showRateUsPopUP("Your title", "Your Message", "Okay", "Later", "No");
		}

		
		if(GUI.Button(new Rect(330, 70, 150, 50), "Show leadeboard")) {
			if(GameCenterManager.player != null) {
				GameCenterManager.showLeaderBoards ();
			}
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
