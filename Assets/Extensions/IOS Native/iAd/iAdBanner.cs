using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class iAdBanner : EventDispatcherBase {


	[DllImport ("__Internal")]
	private static extern void _IADCreateBannerAd(int gravity,  int id);
	
	[DllImport ("__Internal")]
	private static extern void _IADCreateBannerAdPos(int x, int y, int id);
	
	[DllImport ("__Internal")]
	private static extern void _IADShowAd(int id);
	
	[DllImport ("__Internal")]
	private static extern void _IADHideAd(int id);



	private bool _IsLoaded = false;
	private bool _IsOnScreen = false;
	private bool firstLoad = true;
	
	private bool _ShowOnLoad = true;

	private int _id;
	private TextAnchor _anchor;
	


	public iAdBanner(TextAnchor anchor,  int id) {
		_id = id;
		_anchor = anchor;
		
		
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_IADCreateBannerAd(gravity, _id);
		}
		
		
		
	}
	
	public iAdBanner(int x, int y,  int id) {
		_id = id;
		
		
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_IADCreateBannerAdPos(x, y, _id);
		}
		
		
	}


	public void Hide() { 
		if(!_IsOnScreen) {
			return;
		}
		
		_IsOnScreen = false;
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_IADHideAd(_id);
		}
	}
	
	
	public void Show() { 
		
		if(_IsOnScreen) {
			return;
		}
		
		_IsOnScreen = true;
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_IADShowAd(_id);
		}
	}

	
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------


	public int id {
		get {
			return _id;
		}
	}
	

	
	
	public bool IsLoaded {
		get {
			return _IsLoaded;
		}
	}
	
	public bool IsOnScreen {
		get {
			return _IsOnScreen;
		}
	}
	
	public bool ShowOnLoad {
		get {
			return _ShowOnLoad;
		} 
		
		set {
			_ShowOnLoad = value;
		}
	}
	
	public TextAnchor anchor {
		get {
			return _anchor;
		}
	}
	

	public int gravity {
		get {
			switch(_anchor) {
			case TextAnchor.LowerCenter:
				return IOSGravity.BOTTOM | IOSGravity.CENTER;
			case TextAnchor.LowerLeft:
				return IOSGravity.BOTTOM | IOSGravity.LEFT;
			case TextAnchor.LowerRight:
				return IOSGravity.BOTTOM | IOSGravity.RIGHT;
				
			case TextAnchor.MiddleCenter:
				return IOSGravity.CENTER;
			case TextAnchor.MiddleLeft:
				return IOSGravity.CENTER | IOSGravity.LEFT;
			case TextAnchor.MiddleRight:
				return IOSGravity.CENTER | IOSGravity.RIGHT;
				
			case TextAnchor.UpperCenter:
				return IOSGravity.TOP | IOSGravity.CENTER;
			case TextAnchor.UpperLeft:
				return IOSGravity.TOP | IOSGravity.LEFT;
			case TextAnchor.UpperRight:
				return IOSGravity.TOP | IOSGravity.RIGHT;
			}
			
			return IOSGravity.TOP;
		}
	}


	


	public void didFailToReceiveAdWithError() {
		dispatch(iAdEvent.FAIL_TO_RECEIVE_AD);
	}
	
	
	public void bannerViewDidLoadAd() {
		_IsLoaded = true;
		if(ShowOnLoad && firstLoad) {
			Show();
			firstLoad = false;
		}

		dispatch(iAdEvent.AD_LOADED);
	}
	

	public void bannerViewWillLoadAd(){
		dispatch(iAdEvent.AD_VIEW_LOADED);
	}
	
	
	public void bannerViewActionDidFinish(){
		dispatch(iAdEvent.AD_VIEW_FINISHED);
	}
		
}

