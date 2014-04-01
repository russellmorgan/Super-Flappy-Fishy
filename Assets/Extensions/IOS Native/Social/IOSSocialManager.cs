////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////


using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class IOSSocialManager : EventDispatcher {


	[DllImport ("__Internal")]
	private static extern void _twPost(string text);
	
	[DllImport ("__Internal")]
	private static extern void _twPostWithMedia(string text, string encodedMedia);

	[DllImport ("__Internal")]
	private static extern void _fbPost(string text);
	
	[DllImport ("__Internal")]
	private static extern void _fbPostWithMedia(string text, string encodedMedia);

	private static IOSSocialManager _instance = null;


	
	public const string TWITTER_POST_FAILED  = "twitter_post_failed";
	public const string TWITTER_POST_SUCCESS = "twitter_post_success";
	
	public const string FACEBOOK_POST_FAILED  = "facebook_post_failed";
	public const string FACEBOOK_POST_SUCCESS = "facebook_post_success";


	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	void Awake() {
		DontDestroyOnLoad(gameObject);
	}



	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	public void TwitterPost(string text) {
		TwitterPost(text, null);
	}


	public void TwitterPost(string text, Texture2D texture) {
		if(texture == null) {
			if(Application.platform == RuntimePlatform.IPhonePlayer) {
				_twPost(text);
			}
		} else {
			byte[] val = texture.EncodeToPNG();
			string bytesString = System.Convert.ToBase64String (val);

			if(Application.platform == RuntimePlatform.IPhonePlayer) {
				_twPostWithMedia(text, bytesString);
			}
		}

	}


	public void FacebookPost(string text) {
		FacebookPost(text, null);
	}
	
	public void FacebookPost(string text, Texture2D texture) {
		if(texture == null) {
			if(Application.platform == RuntimePlatform.IPhonePlayer) {
				_fbPost(text);
			}
		} else {
			byte[] val = texture.EncodeToPNG();
			string bytesString = System.Convert.ToBase64String (val);
			
			if(Application.platform == RuntimePlatform.IPhonePlayer) {
				_fbPostWithMedia(text, bytesString);
			}
		}
	}
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------

	public static IOSSocialManager instance  {
		get {
			if(_instance == null) {
				GameObject go =  new GameObject("IOSSocialManager");
				_instance = go.AddComponent<IOSSocialManager>();
			}

			return _instance;
		}
	}
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------

	private void OnTwitterPostFailed() {
		dispatch(TWITTER_POST_FAILED);
	}

	private void OnTwitterPostSuccess() {
		dispatch(TWITTER_POST_SUCCESS);
	}

	private void OnFacebookPostFailed() {
		dispatch(FACEBOOK_POST_FAILED);
	}
	
	private void OnFacebookPostSuccess() {
		dispatch(FACEBOOK_POST_SUCCESS);
	}
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------


	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------

}
