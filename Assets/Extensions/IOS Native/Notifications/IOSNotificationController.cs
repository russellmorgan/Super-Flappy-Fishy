////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class IOSNotificationController : EventDispatcher  {


	private static IOSNotificationController _instance;

	public const string DEVICE_TOKEN_RECIVED = "device_tocken_recived";


	[DllImport ("__Internal")]
	private static extern void _scheduleNotification (int time, string message, bool sound);
	
	[DllImport ("__Internal")]
	private static extern  void _showNotificationBanner (string title, string messgae);
	
	[DllImport ("__Internal")]
	private static extern void _cancelNotifications();

	

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------


	public static IOSNotificationController instance {

		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType(typeof(IOSNotificationController)) as IOSNotificationController;
				if (_instance == null) {
					_instance = new GameObject ("IOSNotificationController").AddComponent<IOSNotificationController> ();
				}
			}

			return _instance;

		}

	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	public void ShowNotificationBanner (string title, string messgae) {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_showNotificationBanner (title, messgae);
		}
	}

	public void CancelNotifications () {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_cancelNotifications();
		}
	}

	public void ScheduleNotification(int time, string message, bool sound) {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_scheduleNotification (time, message, sound);
		}
	}


	
	
	#if UNITY_IPHONE
	public void RegisterForRemoteNotifications(RemoteNotificationType notificationTypes) {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {

			NotificationServices.RegisterForRemoteNotificationTypes(notificationTypes);
			DeviceTokenListner.Create ();
		}
	}
	
	#endif

	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------

	public void OnDeviceTockeRecived(IOSNotificationDeviceToken token) {
		dispatch (DEVICE_TOKEN_RECIVED, token);
	}
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------

}
