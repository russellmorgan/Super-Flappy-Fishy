////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using System;

public class GameCenterPlayerTemplate {

	private string _playerId;
	private string _displayName;


	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	public GameCenterPlayerTemplate (string pId, string pName) {
		_playerId = pId;
		_displayName = pName;
	}


	//--------------------------------------
	// GET / SET
	//--------------------------------------

	public string playerId {
		get {
			return _playerId;
		}
	}


	public string displayName {
		get {
			return _displayName;
		}
	}
}


