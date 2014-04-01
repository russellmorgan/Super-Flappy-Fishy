////////////////////////////////////////////////////////////////////////////////
//  
// @module <module_name>
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class iCloudUseExample : MonoBehaviour {

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	private float v = 1.1f;

	void Awake() {
		IOSNative.instance.Init();


		//initialize icloud and listed for events
		iCloudManager.instance.init ();
	

		iCloudManager.instance.addEventListener (iCloudManager.CLOUD_INITIALIZED, OnInit);
		iCloudManager.instance.addEventListener (iCloudManager.CLOUD_INITIALIZE_FAILED, OnInitFailed);
		iCloudManager.instance.addEventListener (iCloudManager.CLOUD_DATA_CHANGED, OnDataChanged);

		iCloudManager.instance.addEventListener (iCloudManager.CLOUD_DATA_RECEIVE, OnDataReceive);
	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	void OnGUI() {
		if(GUI.Button(new Rect(170, 70, 150, 50), "Set String")) {
			iCloudManager.instance.setString ("TestStringKey", "Hello World");
		}

		if(GUI.Button(new Rect(170, 130, 150, 50), "Get String")) {
			iCloudManager.instance.requestDataForKey ("TestStringKey");
		}




		if(GUI.Button(new Rect(330, 70, 150, 50), "Set Float")) {
			v += 1.1f;
			iCloudManager.instance.setFloat ("TestFloatKey", v);
		}

		if(GUI.Button(new Rect(330, 130, 150, 50), "Get Float")) {
			iCloudManager.instance.requestDataForKey ("TestFloatKey");
		}


		if(GUI.Button(new Rect(490, 70, 150, 50), "Set Bytes")) {
			string msg = "hello world";
			System.Text.UTF8Encoding  encoding = new System.Text.UTF8Encoding();
			byte[] data = encoding.GetBytes(msg);
			iCloudManager.instance.setData ("TestByteKey", data);
		}

		if(GUI.Button(new Rect(490, 130, 150, 50), "Get Bytes")) {
			iCloudManager.instance.requestDataForKey ("TestByteKey");
		}

		if(GUI.Button(new Rect(170, 500, 150, 50), "TestConnection")) {
			Application.LoadLevel("Pear-To-Pear example game");
		}

	}
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------

	private void OnInit() {
		IOSNative.showMessage("iCloud", "Initialization Sucsess!");
	}

	private void OnInitFailed() {
		IOSNative.showMessage("iCloud", "Initialization Failed!");
	}

	private void OnDataChanged() {
		IOSNative.showMessage("iCloud", "Cloud Data Was Changed On Other Device");
	}

	private void OnDataReceive(CEvent e) {
		iCloudData data = e.data as iCloudData;
		if(data.IsEmpty) {
			IOSNative.showMessage(data.key, "data is empty");
		} else {
			IOSNative.showMessage(data.key, data.stringValue);
		}


	}
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------

}
