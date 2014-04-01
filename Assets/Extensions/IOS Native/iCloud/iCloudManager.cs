////////////////////////////////////////////////////////////////////////////////
//  
// @module <module_name>
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class iCloudManager : EventDispatcher {

	public const string CLOUD_INITIALIZED	        = "store_kit_initialized";
	public const string CLOUD_INITIALIZE_FAILED	    = "cloud_initialize_failed";

	public const string CLOUD_DATA_CHANGED	        = "cloud_data_changed";
	public const string CLOUD_DATA_RECEIVE       = "cloud_data_receive";

	[DllImport ("__Internal")]
	private static extern void _initCloud();

	[DllImport ("__Internal")]
	private static extern void _setString(string key, string val);

	[DllImport ("__Internal")]
	private static extern void _setDouble(string key, float val);
	
	[DllImport ("__Internal")]
	private static extern void _setData(string key, string val);

	[DllImport ("__Internal")]
	private static extern void _requestDataForKey(string key);


	private static iCloudManager _instance;

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	public static iCloudManager instance {

		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType(typeof(iCloudManager)) as iCloudManager;
				if (_instance == null) {
					_instance = new GameObject ("iCloudManager").AddComponent<iCloudManager> ();
				}
			}

			return _instance;

		}

	}


	public void init() {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_initCloud ();
		}
	}

	public void setString(string key, string val) {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_setString(key, val);
		}
	}

	public void setFloat(string key, float val) {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_setDouble(key, val);
		}
	}

	public void setData(string key, byte[] val) {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {

			string b = "";
			int len = val.Length;
			for(int i = 0; i < len; i++) {
				if(i != 0) {
					b += ",";
				}

				b += val[i].ToString();
			}

			_setData(key, b);
		}
	}

	public void requestDataForKey(string key) {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_requestDataForKey(key);
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

	private void OnCloudInit() {
		dispatch (CLOUD_INITIALIZED);
	}

	private void OnCloudInitFail() {
		dispatch (CLOUD_INITIALIZE_FAILED);
	}

	private void OnCloudDataChanged() {
		dispatch (CLOUD_DATA_CHANGED);
	}


	private void OnCloudData(string array) {
		string[] data;
		data = array.Split("|" [0]);

		iCloudData package = new iCloudData (data[0], data [1]);

		dispatch (CLOUD_DATA_RECEIVE, package);
	}

	private void OnCloudDataEmpty(string array) {
		string[] data;
		data = array.Split("|" [0]);

		iCloudData package = new iCloudData (data[0], "null");

		dispatch (CLOUD_DATA_RECEIVE, package);
	}



	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------

}
