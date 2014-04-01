////////////////////////////////////////////////////////////////////////////////
//  
// @module <module_name>
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;


public class ByteByffer  {

	private byte[] buffer;

	public int pointer = 0;
	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	public ByteByffer(byte[] buf) {
		buffer = buf;
	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	public int readInt() {
		int v =  System.BitConverter.ToInt32 (buffer, pointer);
		pointer += 4;
		return v;
	}

	public float readFloat() {
		float v =  System.BitConverter.ToSingle (buffer, pointer);
		pointer += 4;
		return v;
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
