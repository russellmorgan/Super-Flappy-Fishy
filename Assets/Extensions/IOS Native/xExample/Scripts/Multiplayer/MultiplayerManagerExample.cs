////////////////////////////////////////////////////////////////////////////////
//  
// @module <module_name>
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class MultiplayerManagerExample : MonoBehaviour {

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	void Awake() {


		IOSNative.instance.Init();

		GameCenterManager.init();

		GameCenterMultiplayer.instance.addEventListener (GameCenterMultiplayer.PLAYER_CONNECTED, OnGCPlayerConnected);
		GameCenterMultiplayer.instance.addEventListener (GameCenterMultiplayer.PLAYER_DISCONNECTED, OnGCPlayerDisconnected);

		GameCenterMultiplayer.instance.addEventListener (GameCenterMultiplayer.MATCH_STARTED, OnGCMatchStart);
		GameCenterMultiplayer.instance.addEventListener (GameCenterMultiplayer.DATA_RECIVED, OnGCDataRecived);
	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	void OnGUI() {
		if(GUI.Button(new Rect(170, 70, 150, 50), "Find Match")) {
			GameCenterMultiplayer.instance.FindMatch (2, 2);
		}

		if(GUI.Button(new Rect(170, 130, 150, 50), "Send Data to All")) {
			string msg = "hello world";
			System.Text.UTF8Encoding  encoding = new System.Text.UTF8Encoding();
			byte[] data = encoding.GetBytes(msg);
			GameCenterMultiplayer.instance.SendDataToAll (data, GameCenterDataSendType.RELIABLE);
		}


		if(GUI.Button(new Rect(170, 190, 150, 50), "Send to Player")) {
			string msg = "hello world";
			System.Text.UTF8Encoding  encoding = new System.Text.UTF8Encoding();
			byte[] data = encoding.GetBytes(msg);

			GameCenterMultiplayer.instance.sendDataToPlayers (data, GameCenterDataSendType.RELIABLE, GameCenterMultiplayer.instance.match.playerIDs[0]);
		}

		if(GUI.Button(new Rect(170, 250, 150, 50), "Disconnect")) {
			GameCenterMultiplayer.instance.disconnect ();
		}


		//turn based
	/*	if(GUI.Button(new Rect(330, 70, 150, 50), "Trun Based Match")) {
			GameCenterMultiplayer.instance.FindTurnBasedMatch (2, 2);
		} */

	}
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------

	private void OnGCPlayerConnected(CEvent e) {
		string playerID = e.data as string;
		IOSNative.showMessage ("Player Connected", "playerid: " + playerID);
	}

	private void OnGCPlayerDisconnected(CEvent e) {
		string playerID = e.data as string;
		IOSNative.showMessage ("Player Disconnected", "playerid: " + playerID);
	}

	private void OnGCMatchStart(CEvent e) {
		GameCenterMatchData match = e.data as GameCenterMatchData;

		IOSNative.showMessage ("OnMatchStart", "let's playe now \n  Other player count: " + match.playerIDs.Count);
	}

	private void OnGCDataRecived(CEvent e) {
		GameCenterDataPackage package = e.data as GameCenterDataPackage;

		System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
		string str = enc.GetString(package.buffer);

		IOSNative.showMessage ("Data recived", "player ID: " + package.playerID + " \n " + "data: " + str);

	}
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------

}
