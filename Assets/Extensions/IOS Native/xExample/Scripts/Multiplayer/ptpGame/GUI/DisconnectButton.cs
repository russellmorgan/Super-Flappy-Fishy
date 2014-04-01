////////////////////////////////////////////////////////////////////////////////
//  
// @module <module_name>
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class DisconnectButton : MonoBehaviour {

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	private float w;
	private float h;

	private Rect r;

	void Start() {
		w = Screen.width * 0.2f;
		h = Screen.height * 0.1f;

		r.x = w * 0.1f;
		r.y = h * 0.1f;

		r.width = w;
		r.height = h;
	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	void OnGUI() {
		if(GUI.Button(r, "Disonnect")) {
			GameCenterMultiplayer.instance.disconnect ();
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
