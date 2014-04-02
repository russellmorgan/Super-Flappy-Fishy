using UnityEngine;
using System;
using System.Collections;

public enum PlayAdsInterstitialType
{
	Smart
#if UNITY_ANDROID
	, Light
#endif
};

public class PlayAdsSDK : MonoBehaviour
{
	private const string INSTANCE_NAME = "PlayAdsSDKInstance";
	
	#region --- PUBLIC ---
	
	#region -- CALLBACKS --
	
	public static event Action 			SDKReady;
	public static event Action 			InterstitialReady;
	public static event Action 			InterstitialShown;
	public static event Action 			InterstitialClosed;
	public static event Action 			InterstitialClicked;
	public static event Action<String> 	InterstitialFailed;
	public static event Action<String> 	SDKVersion;
	
	#endregion
	
	#region -- METHODS --
	
	public static void Initialize(string appID, string secret)
	{
		ensureInstance();
		PlayAdsSDK.PlayAdsSDKInitialize(appID, secret, INSTANCE_NAME);
	}
	
	public static void PrepareInterstitial(PlayAdsInterstitialType type)
	{
		ensureInstance();
		PlayAdsSDK.PlayAdsSDKPrepareInterstitial(type.ToString());
    }
	
	public static void ShowPreparedInterstitial()
	{
		ensureInstance();
		PlayAdsSDK.PlayAdsSDKShowPreparedInterstitial();
    }
	
	public static void ShowInterstitial(PlayAdsInterstitialType type)
	{
		ensureInstance();
		PlayAdsSDK.PlayAdsSDKShowInterstitial(type.ToString());
    }

    public static void GetVersion ()
    {
    	ensureInstance();
    	PlayAdsSDK.PlayAdsSDKGetVersion();
    }
	
	#endregion
	
	#endregion
	
	#region --- PRIVATE ---
	
	#region Singleton Instance
	private static PlayAdsSDK instance;
	private static void ensureInstance()
	{
		if(instance == null)
		{
			instance = FindObjectOfType(typeof(PlayAdsSDK) ) as PlayAdsSDK;
			if(instance == null)
			{
				instance = new GameObject(INSTANCE_NAME).AddComponent<PlayAdsSDK>();
			}
		}
	}
	private void Awake()
	{
		name = INSTANCE_NAME;
		DontDestroyOnLoad(transform.gameObject);
	}
	#endregion
	
	#region -- BRIDGES --
	
#if UNITY_EDITOR
	private static void PlayAdsSDKInitialize(string appId, string secret, string instanceName)
	{
		instance.SDKReadyCallback("");
	}
	
   	private static void PlayAdsSDKPrepareInterstitial(string type)
	{
		instance.InterstitialReadyCallback("");
	}
	
	private static void PlayAdsSDKShowPreparedInterstitial()
	{
		instance.InterstitialShownCallback("");
		instance.InterstitialClosedCallback("");
	}
	
	
	private static void PlayAdsSDKShowInterstitial(string type)
	{
		instance.InterstitialReadyCallback("");
		instance.InterstitialShownCallback("");
		instance.InterstitialClosedCallback("");
	}

	public static void PlayAdsSDKGetVersion ()
    {
    	instance.PlayAdsVersionCallback("");
    }
	
#elif UNITY_IPHONE
	
	[System.Runtime.InteropServices.DllImport("__Internal")]
   	private static extern void PlayAdsSDKInitialize(string appId, string secret, string instanceName);
	
	[System.Runtime.InteropServices.DllImport("__Internal")]
   	private static extern void PlayAdsSDKShowInterstitial(string type);
	
	[System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern void PlayAdsSDKPrepareInterstitial(string type);
	
	[System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern void PlayAdsSDKShowPreparedInterstitial();

	[System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern void PlayAdsSDKGetVersion();
	
#elif UNITY_ANDROID

	private static string LastFetchedType;
	
   	private static void PlayAdsSDKInitialize(string appId, string secret, string instanceName)
	{
		PlayAdsSDK.CallAndroidSDK("init", appId, secret);
	}
	
	private static void PlayAdsSDKShowInterstitial(string type)
	{
		PlayAdsSDK.CallAndroidSDK("show", type.ToString(), GetScreenOrientation());
	}
	
	private static void PlayAdsSDKPrepareInterstitial(string type)
	{
		LastFetchedType = type;
		PlayAdsSDK.CallAndroidSDK("cache", type.ToString(), GetScreenOrientation());
	}
	
	private static void PlayAdsSDKShowPreparedInterstitial()
	{
		if (LastFetchedType == null)
		{
			throw new Exception("PlayAdsSDK - ShowFetchedInterstitial called without waiting for InterstitialReady callback or calling FetchInterstitial before");
		} 
		else
		{
			PlayAdsSDK.PlayAdsSDKShowInterstitial(LastFetchedType);
		}
	}

	public static void PlayAdsSDKGetVersion ()
	{
		PlayAdsSDK.CallAndroidSDK("version");
	}
	
    private static AndroidJavaClass AndroidSDK;
    
    private static void CallAndroidSDK(string methodName, params object[] args)
    {
        if(AndroidSDK == null)
        {
        	AndroidSDK = new AndroidJavaClass("com.applift.playads.api.unity.UnityAPI");
        }
        AndroidSDK.CallStatic(methodName, args);
    }
    
    private static string GetScreenOrientation()
    {
    	string orientation = "Undefined";
     	switch(Screen.orientation)
     	{
     		case ScreenOrientation.Portrait: 			orientation = "Portrait";			break;
     		case ScreenOrientation.PortraitUpsideDown:	orientation = "PortraitUpsideDown"; break;
     		case ScreenOrientation.LandscapeLeft:		orientation = "LandscapeLeft"; 		break;
     		case ScreenOrientation.LandscapeRight: 		orientation = "LandscapeRight"; 	break;
     	}
        return orientation;
    }

#endif
	#endregion
	
	#region -- CALLBACKS --
	
	public void SDKReadyCallback(string message)
	{
		if(SDKReady != null)
		{
			SDKReady();
		}
	}
	
	public void InterstitialReadyCallback(string message)
	{
		if(InterstitialReady != null)
		{
			InterstitialReady();
		}
	}
	
	public void InterstitialShownCallback(string message)
	{
		if(InterstitialShown != null)
		{
			InterstitialShown();
		}
	}
	
	public void InterstitialClickedCallback(string message)
	{
		if(InterstitialClicked != null)
		{
			InterstitialClicked();
		}
	}
	
	public void InterstitialFailedCallback(string error)
	{
		if(InterstitialFailed != null)
		{
			InterstitialFailed(error);
		}
	}
	
	public void InterstitialClosedCallback(string message)
	{
		if(InterstitialClosed != null)
		{
			InterstitialClosed();
		}
	}

	public void PlayAdsVersionCallback(string version)
	{
		if(SDKVersion != null)
		{
			SDKVersion(version);
		}
	}
	
	#endregion
	
	#endregion
}
