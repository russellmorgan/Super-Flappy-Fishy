////////////////////////////////////////////////////////////////////////////////
//  
// @module <module_name>
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class ClickManager : MonoBehaviour {
	

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	void Awake() {
		GameCenterMultiplayer.instance.addEventListener (GameCenterMultiplayer.DATA_RECIVED, OnData);
	}


	void Update() {
		if(Input.GetMouseButtonDown(0)) {
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			pos.z = 1f;

			PTPGameController.instance.createGreenSphere (pos);



			ObjectCreatePackage p = new ObjectCreatePackage (pos.x, pos.y);
			p.send ();

		}
	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------

	private  void OnData(CEvent e) {

		GameCenterDataPackage package = e.data as GameCenterDataPackage;

		ByteByffer b = new ByteByffer (package.buffer);


		Vector3 pos = new Vector3 (0, 0, 1);
		pos.x = b.readFloat ();
		pos.y = b.readFloat ();



		PTPGameController.instance.createRedSphere (pos);

	}
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------

}
