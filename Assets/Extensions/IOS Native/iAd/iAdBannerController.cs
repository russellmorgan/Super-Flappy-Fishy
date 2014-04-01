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

public class iAdBannerController : EventDispatcher {


	[DllImport ("__Internal")]
	private static extern void _IADDestroyBanner(int id);


	[DllImport ("__Internal")]
	private static extern void _IADStartInterstitialAd();

	[DllImport ("__Internal")]
	private static extern void _IADLoadInterstitialAd();

	[DllImport ("__Internal")]
	private static extern void _IADShowInterstitialAd();


	private static int _nextId = 0;
	private static iAdBannerController _instance;

	private Dictionary<int, iAdBanner> _banners; 


	
	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	void Awake() {
		_banners =  new Dictionary<int, iAdBanner>();
		DontDestroyOnLoad(gameObject);
	}


	public static iAdBannerController instance {

		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType(typeof(iAdBannerController)) as iAdBannerController;
				if (_instance == null) {
					_instance = new GameObject ("iAdBannerController").AddComponent<iAdBannerController> ();
				}
			}

			return _instance;

		}

	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	public iAdBanner CreateAdBanner(TextAnchor anchor)  {

		
		iAdBanner bannner = new iAdBanner(anchor, nextId);
		_banners.Add(bannner.id, bannner);
		
		return bannner;
		
	}
	
	
	public iAdBanner CreateAdBanner(int x, int y)  {

		iAdBanner bannner = new iAdBanner(x, y, nextId);
		_banners.Add(bannner.id, bannner);
		
		return bannner;
	}


	public void DestroyBanner(int id) {
		if(_banners != null) {
			if(_banners.ContainsKey(id)) {
				_banners.Remove(id);
				
				if(Application.platform == RuntimePlatform.IPhonePlayer) {
					_IADDestroyBanner(id);
				}
			}
		}
	}


	public void StartInterstitialAd() {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_IADStartInterstitialAd();
		}
	}
	
	public void LoadInterstitialAd() {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_IADLoadInterstitialAd();
		}
	}
	
	public void ShowInterstitialAd() {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_IADShowInterstitialAd();
		}
	}

	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------

	public static int nextId {
		get {
			_nextId++;
			return _nextId;
		}
	}



	
	public iAdBanner GetBanner(int id) {
		if(_banners.ContainsKey(id)) {
			return _banners[id];
		} else {
			Debug.LogWarning("Banner id: " + id.ToString() + " not found");
			return null;
		}
	}
	
	
	
	public List<iAdBanner> banners {
		get {
			
			List<iAdBanner> allBanners =  new List<iAdBanner>();
			if(_banners ==  null) {
				return allBanners;
			}
			
			foreach(KeyValuePair<int, iAdBanner> entry in _banners) {
				allBanners.Add(entry.Value);
			}
			
			return allBanners;
			
			
		}
	}

	//--------------------------------------
	//  EVENTS
	//--------------------------------------

	private void didFailToReceiveAdWithError(string bannerID) {
		int id = System.Convert.ToInt32(bannerID);
		iAdBanner banner = GetBanner(id) as iAdBanner;
		if(banner != null) {
			banner.didFailToReceiveAdWithError();
		}

	}


	private void bannerViewDidLoadAd(string bannerID) {
		int id = System.Convert.ToInt32(bannerID);
		iAdBanner banner = GetBanner(id) as iAdBanner;
		if(banner != null) {
			banner.bannerViewDidLoadAd();
		}
	}


	private void bannerViewWillLoadAd(string bannerID){
		int id = System.Convert.ToInt32(bannerID);
		iAdBanner banner = GetBanner(id) as iAdBanner;
		if(banner != null) {
			banner.bannerViewWillLoadAd();
		}
	}


	private void bannerViewActionDidFinish(string bannerID){
		int id = System.Convert.ToInt32(bannerID);
		iAdBanner banner = GetBanner(id) as iAdBanner;
		if(banner != null) {
			banner.bannerViewActionDidFinish();
		}
	}



	private void interstitialdidFailWithError(string data) {
		dispatch(iAdEvent.INTERSTITIAL_DID_FAIL_WITH_ERROR);
	}

	private void interstitialAdWillLoad(string data) {
		dispatch(iAdEvent.INTERSTITIAL_AD_WILL_LOAD);
	}

	private void interstitialAdDidLoad(string data) {
		dispatch(iAdEvent.INTERSTITIAL_AD_DID_LOAD);
	}

	private void interstitialAdActionDidFinish(string data) {
		dispatch(iAdEvent.INTERSTITIAL_AD_ACTION_DID_FINISH);
	}






	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------

}
