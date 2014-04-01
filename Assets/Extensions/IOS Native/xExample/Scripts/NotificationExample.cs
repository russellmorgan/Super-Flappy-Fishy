////////////////////////////////////////////////////////////////////////////////
//  
// @module <module_name>
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class NotificationExample : MonoBehaviour {

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------


	void Awake() {
		IOSNative.instance.Init();
	}


	void Start() {




		#if UNITY_IPHONE
		//push notifocations and RemoteNotificationType class can be used only on IOS Platfrom.
		
		IOSNotificationController.instance.RegisterForRemoteNotifications (RemoteNotificationType.Alert |  RemoteNotificationType.Badge |  RemoteNotificationType.Sound);
		IOSNotificationController.instance.addEventListener (IOSNotificationController.DEVICE_TOKEN_RECIVED, OnTokenReived);
		
		#endif
	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	void OnGUI() {

		if(GUI.Button(new Rect(650, 10, 150, 50), "Schedule Notification Silet")) {
			IOSNotificationController.instance.ScheduleNotification (15, "Your Notification Text No Sound", false);
		}

		if(GUI.Button(new Rect(650, 70, 150, 50), "Schedule Notification Silet")) {
			IOSNotificationController.instance.ScheduleNotification (15, "Your Notification Text", true);
		}

		if(GUI.Button(new Rect(650, 130, 150, 50), "Cansel All Notifications")) {
			IOSNotificationController.instance.CancelNotifications();
		}
		
		if(GUI.Button(new Rect(650, 190, 150, 50), "Show Banner")) {
			IOSNotificationController.instance.ShowNotificationBanner("Title", "Message");
		}


	}
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------

	private void OnTokenReived(CEvent e) {
		IOSNotificationDeviceToken token = e.data as IOSNotificationDeviceToken;
		Debug.Log ("OnTokenReived");
		Debug.Log (token.tokenString);


		IOSNotificationController.instance.removeEventListener (IOSNotificationController.DEVICE_TOKEN_RECIVED, OnTokenReived);
	}
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------

}
