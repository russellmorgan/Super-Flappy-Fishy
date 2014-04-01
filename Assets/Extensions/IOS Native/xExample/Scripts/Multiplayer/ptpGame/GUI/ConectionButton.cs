////////////////////////////////////////////////////////////////////////////////
//  
// @module <module_name>
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class ConectionButton : MonoBehaviour {

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	private float w;
	private float h;

	private Rect r;

	void Start() {
		w = Screen.width * 0.2f;
		h = Screen.height * 0.1f;

		r.x = (Screen.width - w) / 2f;
		r.y = (Screen.height - h) / 2f;

		r.width = w;
		r.height = h;
	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	void OnGUI() {
		if(GUI.Button(r, "Find Match")) {
			GameCenterMultiplayer.instance.FindMatch (2, 2);
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
