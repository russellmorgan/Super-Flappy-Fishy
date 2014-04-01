////////////////////////////////////////////////////////////////////////////////
//  
// @module <module_name>
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class DeviceTokenListner : MonoBehaviour {
	
	

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	public static void Create() {
		 new GameObject ("DeviceTockenListner").AddComponent<DeviceTokenListner> ();
	}


	void Awake() {
		DontDestroyOnLoad (gameObject);
	}


	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	/*
	  
	 
    #if UNITY_IPHONE
	
	private bool tokenSent = false;
	#endif
	void  FixedUpdate () {
		#if UNITY_IPHONE
		
		if (!tokenSent) {

			byte[] token   = NotificationServices.deviceToken;
			if(token != null) {

				IOSNotificationDeviceToken t = new IOSNotificationDeviceToken(token);
				IOSNotificationController.instance.OnDeviceTockeRecived (t);
				Destroy (gameObject);
			}
		}
		
		#endif

	}
	
	*/
	
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
