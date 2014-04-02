#include <iostream>

#ifdef __cplusplus
extern "C"
{
	void PlayAdsSDKInitialize(const char* appID, const char* secret, const char* instanceName);
	void PlayAdsSDKShowInterstitial(const char* type);
    void PlayAdsSDKPrepareInterstitial(const char* type);
    void PlayAdsSDKShowPreparedInterstitial();
    void PlayAdsSDKGetVersion();
}
#endif