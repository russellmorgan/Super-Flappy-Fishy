using UnityEngine;
using System.Collections;

public class AppEventHandlerExample : MonoBehaviour {

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	void Awake() {

		IOSNative.instance.Init();

		IOSNative.instance.addEventListener (IOSNative.APPLICATION_DID_BECOME_ACTIVE,                OnApplicationDidBecomeActive);
		IOSNative.instance.addEventListener (IOSNative.APPLICATION_DID_RECEIVE_MEMORY_WARNING,       OnApplicationDidReceiveMemoryWarning);
	}

	//--------------------------------------
	// EVENTS
	//--------------------------------------
	

	private void OnApplicationDidBecomeActive() {
		// Called when application become active again. Optionally refresh the user interface, check for some data than probably was chenged wile application was paused

		Debug.Log ("Catched  OnApplicationDidBecomeActive event");
	}

	private void OnApplicationDidReceiveMemoryWarning() {
		//Called application receives a memory warning from the system.

		Debug.Log ("Catched  OnApplicationDidReceiveMemoryWarning event");
	}


	//--------------------------------------
	// PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	// DESTROY
	//--------------------------------------
}
