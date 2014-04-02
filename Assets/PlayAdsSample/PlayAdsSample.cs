using UnityEngine;
using System.Collections;

public class PlayAdsSample : MonoBehaviour 
{
	private bool playAdsSDKReady = false;
	
	#region PlayAdsSDK AppID & Secret
	
#if UNITY_EDITOR
	private const string appId = "";
	private const string secret = "";
#elif UNITY_IPHONE
	private const string appId = "1002785";
	private const string secret = "b0ac12b5a1523089124b475364a85e7decacfa765bc7dd4465306ad516922b69";
#elif UNITY_ANDROID
	private const string appId = "78";
	private const string secret = "c44fd878e20100ce934b7ae8c09c15d101920258620ea315624f51c7d94cea32";
#endif
	
	#endregion
	
	void Start () 
	{	
		PlayAdsSDK.SDKReady += PlayAds_SDKReady;
		PlayAdsSDK.Initialize (appId, secret);
	}
	
	void OnGUI ()
	{
//		if(GUI.Button(new Rect(Screen.width * 0.1f, Screen.height * 0.1f, Screen.width * 0.8f, Screen.height * 0.1f), "Get Version"))
//		{
//			this.PlayAds_GetVersion();
//		}
//		
//		// DISABLE BUTTONS WHILE PLAYADSSDK IS NOT READY
//		if(!this.playAdsSDKReady)
//		{
//			return;
//		}
//		
//		if(GUI.Button(new Rect(Screen.width * 0.1f, Screen.height * 0.3f, Screen.width * 0.8f, Screen.height * 0.1f), "Show Interstitial (Synchronous)"))
//		{
//			this.PlayAds_ShowSyncInterstitial();
//		}
//		
//		if(GUI.Button(new Rect(Screen.width * 0.1f, Screen.height * 0.5f, Screen.width * 0.8f, Screen.height * 0.1f), "Show Interstitial (Asynchronous)"))
//		{
//			this.PlayAds_ShowAsyncInterstitial();
//		}
	}
	
	/// <summary>
	/// Register callback & ask the native SDK for the version
	/// </summary>
	private void PlayAds_GetVersion()
	{
		PlayAdsSDK.SDKVersion += PlayAds_SDKVersion;
		
		PlayAdsSDK.GetVersion();
	}
	
	/// <summary>
	/// Shows a synchronoys interstitial (blocking the screen)
	/// </summary>
	private void PlayAds_ShowSyncInterstitial()
	{
		PlayAdsSDK.InterstitialShown 	+= PlayAds_InterstitialShown;
		PlayAdsSDK.InterstitialFailed 	+= PlayAds_InterstitialFailed;
		PlayAdsSDK.InterstitialClosed 	+= PlayAds_InterstitialClosed;
			
		PlayAdsSDK.ShowInterstitial(PlayAdsInterstitialType.Smart);
	}
	
	/// <summary>
	/// Shows an asynchronoys interstitial (blocking the screen only when the interstitial is fully loaded)
	/// </summary>
	private void PlayAds_ShowAsyncInterstitial()
	{
		PlayAdsSDK.InterstitialReady += PlayAds_InterstitialReady;

		PlayAdsSDK.PrepareInterstitial(PlayAdsInterstitialType.Smart);
	}
	
	#region PlayAdsSDK Callbacks
	
	#region Generic SDK Callbacks
	
	private void PlayAds_SDKReady()
	{
		//From this point you can start wotking with interstitials
		
		this.playAdsSDKReady = true;
		
		Debug.Log("*PlayAds * - SDK is Ready");
	}
	
	private void PlayAds_SDKVersion(string version)
	{
		PlayAdsSDK.SDKVersion -= PlayAds_SDKVersion;
		Debug.Log("* PlayAds * - SDK Version: " + version);
	}
	
	#endregion
	
	#region Interstitial callbacks
	
	/// <summary>
	/// This method is only needed for ASYNCHRONOUS interstitial mode
	/// </summary>
	private void PlayAds_InterstitialReady()
	{
		PlayAdsSDK.InterstitialReady 	-= PlayAds_InterstitialReady;
		
		Debug.Log("*PlayAds * - InterstitialReady");
		
		PlayAdsSDK.InterstitialShown 	+= PlayAds_InterstitialShown;
		PlayAdsSDK.InterstitialFailed	+= PlayAds_InterstitialFailed;
		PlayAdsSDK.InterstitialClosed 	+= PlayAds_InterstitialClosed;
		
		PlayAdsSDK.ShowPreparedInterstitial();
	}
	
	private void PlayAds_InterstitialShown()
	{
		PlayAdsSDK.InterstitialShown -= PlayAds_InterstitialShown;
		
		//Do whatever is needed when the ad is shown (and the app is going to PAUSE state)
		
		Debug.Log("*PlayAds * - InterstitialShown");
	}
	
	private void PlayAds_InterstitialFailed(string errorMessage)
	{
		//After this callback the SDK will automatically raise Close event
		//Use this method for debug/logging purposes
		
		Debug.Log("*PlayAds * - InterstitialFailed: " + errorMessage);
	}
	
	private void PlayAds_InterstitialClosed()
	{
		PlayAdsSDK.InterstitialFailed -= PlayAds_InterstitialFailed;
		PlayAdsSDK.InterstitialClosed -= PlayAds_InterstitialClosed;
		
		//Do whatever is needed when the app is returned
		var otherGameObject = GameObject.Find("iAdContainer");
		iAdUseExample other = (iAdUseExample) otherGameObject.GetComponent(typeof(iAdUseExample));
		other.showBanner();

		Debug.Log("*PlayAds * - InterstitialClosed");
	}
	
	#endregion
	
	#endregion
}
