////////////////////////////////////////////////////////////////////////////////
//  
// @module <module_name>
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class NetwrokManager  {

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------



	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	public static void send(BasePackage pack) {
		GameCenterMultiplayer.instance.SendDataToAll (pack.getBytes(), GameCenterDataSendType.RELIABLE);
	}


	
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
