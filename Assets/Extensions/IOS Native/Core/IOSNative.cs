////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class IOSNative : EventDispatcher {

	public const string APP_ID = "com.subreference.flappypixel";


	public const string APPLICATION_DID_ENTER_BACKGROUND 		= "applicationDidEnterBackground";
	public const string APPLICATION_DID_BECOME_ACTIVE 			= "applicationDidBecomeActive";
	public const string APPLICATION_DID_RECEIVE_MEMORY_WARNING 	= "applicationDidReceiveMemoryWarning";
	public const string APPLICATION_WILL_RESIGN_ACTIVE 	        = "applicationWillResignActive";
	public const string APPLICATION_WILL_TERMINATE 	            = "applicationWillTerminate";



	public const string DATA_SEPARATOR = "|";
	public const string END_OF_LINE = "endofline";

	private static bool _IsInited = false;


	
	[DllImport ("__Internal")]
	private static extern void _initIOSNative(string appId);
	
	
    //--------------------------------------
	//  NATIVE FUNCTIONS
	//--------------------------------------
	
	
	[DllImport ("__Internal")]
	private static extern void _showRateUsPopUp(string title, string message, string rate, string remind, string declined);
	
	[DllImport ("__Internal")]
	private static extern void _showDialog(string title, string message, string yes, string no);
	
	[DllImport ("__Internal")]
	private static extern void _showMessage(string title, string message, string ok);

	[DllImport ("__Internal")]
	private static extern void _dismissCurrentAlert();


	
    //--------------------------------------
	//  MARKET
	//--------------------------------------
	
	[DllImport ("__Internal")]
	private static extern void _loadStore(string ids);
	
	[DllImport ("__Internal")]
	private static extern void _restorePurchases();
	
	
	[DllImport ("__Internal")]
	private static extern void _buyProduct(string id);

	[DllImport ("__Internal")]
	private static extern void _verifyLastPurchase(string url);

	
	private static IOSNative _instance = null;

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------
	
	void Awake() {

		gameObject.name = "IOSNative";

		DontDestroyOnLoad(gameObject);
	}

	void Start() {

		if(APP_ID.Equals("REPLACE_WITH_YOUT_APPLICATION_ID")) {
			Debug.LogError ("App ID is empty");
		}

		if(Application.platform == RuntimePlatform.IPhonePlayer && !_IsInited) {
			Debug.Log("IOSNative inited with appId: " + APP_ID);
			_IsInited = true;
			_initIOSNative(APP_ID);
			applicationDidBecomeActive();

		}

	}
	

	public void Init() {
		//use to init IOSNative
	}

	
	public static IOSNative instance {
		get {
			if(_instance == null) {
				GameObject go =  new GameObject("IOSNative");
				_instance =  go.AddComponent<IOSNative>();


			}
			
			return _instance;
		}
	}


	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	public static void dismissCurrentAlert() {
		CheckState();
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_dismissCurrentAlert();
		}	


	}
	

	
	
	public static void showRateUsPopUP(string title, string message, string rate, string remind, string declined) {
		CheckState();
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_showRateUsPopUp(title, message, rate, remind, declined);
		}	
	}
	
	
	public static void showDialog(string title, string message) {
		showDialog(title, message, "Yes", "No");
	}
	
	public static void showDialog(string title, string message, string yes, string no) {
		CheckState();
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_showDialog(title, message, yes, no);
		}	
	}
	
	
	public static void showMessage(string title, string message) {
		showMessage(title, message, "Ok");
	}
	
	public static void showMessage(string title, string message, string ok) {
		CheckState();
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_showMessage(title, message, ok);
		}	
	}
	
	public static void loadStore(string ids) {
		CheckState();
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_loadStore(ids);
		}
	}
	
	public static void buyProduct(string id) {
		CheckState();
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_buyProduct(id);
		}
	}
	
	public static void restorePurchases() {
		CheckState();
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_restorePurchases();
		}
	}

	public static void verifyLastPurchase(string url) {
		CheckState();
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_verifyLastPurchase(url);
		}
	}
	

	
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------


	private void applicationDidEnterBackground() {
		dispatch(APPLICATION_DID_ENTER_BACKGROUND);
	}

	private void applicationDidBecomeActive() {
		dispatch(APPLICATION_DID_BECOME_ACTIVE);
	}

	private void applicationDidReceiveMemoryWarning() {
		dispatch(APPLICATION_DID_RECEIVE_MEMORY_WARNING);
	}


	private void applicationWillResignActive() {
		dispatch (APPLICATION_WILL_RESIGN_ACTIVE);
	}


	private void applicationWillTerminate() {
		dispatch (APPLICATION_WILL_TERMINATE);
	}
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------

	private static void CheckState() {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			if(_instance == null) {
				IOSNative.instance.Init();
			}
		}	
	}
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------


}
