#import "PlayAdsSDKWrapper.h"
#import "PlayAdsSDK.h"

extern void UnityPause(bool pause);

@interface PlayAdsSDKBridge : NSObject <PlayAdsSDKDelegate>

@property (nonatomic, retain) NSString  *instanceName;
@property (nonatomic, assign) BOOL      isReadyRaised;

+ (void)startPlayAdsSDKForApp:(NSString*)appicationID secretToken:(NSString*)applicationSecret;
+ (void)showInterstitialOfType:(PlayAdsInterstitialType)intType;
+ (void)prepareInterstitialOfType:(PlayAdsInterstitialType)intType;
+ (void)showPreparedInterstitial;
+ (NSString*)getVersion;

@end

static PlayAdsSDKBridge *_sharedInstance;

@implementation PlayAdsSDKBridge

+ (PlayAdsSDKBridge*)sharedInstance
{
    if(_sharedInstance == nil)
    {
        _sharedInstance = [[PlayAdsSDKBridge alloc] init];
    }
    return _sharedInstance;
}

+ (void)startPlayAdsSDKForApp:(NSString*)applicationID secretToken:(NSString*)applicationSecret
{
    [PlayAdsSDK startPlayAdsSDKForApp:applicationID secretToken:applicationSecret delegate:[PlayAdsSDKBridge sharedInstance]];
}

+ (void)showInterstitialOfType:(PlayAdsInterstitialType)type
{
    [PlayAdsSDK showInterstitialOfType:type delegate:[PlayAdsSDKBridge sharedInstance]];
}

+ (void)prepareInterstitialOfType:(PlayAdsInterstitialType)type
{
    [PlayAdsSDK prepareInterstitialOfType:type delegate:[PlayAdsSDKBridge sharedInstance]];
}

+ (void)showPreparedInterstitial
{
    [PlayAdsSDK showPreparedInterstitial];
}

+ (NSString*)getVersion
{
    return [PlayAdsSDK versionString];
}

- (id)init
{
    self = [super init];
    if (self)
    {
        self.isReadyRaised = NO;
    }
    return self;
}

- (void)dealloc
{
    [_instanceName release];
    [super dealloc];
}

- (void)playAdsSDKReady
{
    UnitySendMessage([self.instanceName UTF8String], "SDKReadyCallback", "");
}

- (void)playAdsInterstitialReady
{
    if(self.isReadyRaised)
    {
        return;
    }
    
    self.isReadyRaised = YES;
    UnitySendMessage([self.instanceName UTF8String], "InterstitialReadyCallback", "");
}

- (void)playAdsInterstitialDidShown
{
    UnitySendMessage([self.instanceName UTF8String], "InterstitialShownCallback", "");
    
    [self playAdsInterstitialReady];
    
    UnityPause(true);
}

- (void)playAdsInterstitialDidFailWithError:(NSError*)error
{
    UnityPause(false);
    UnitySendMessage([self.instanceName UTF8String], "InterstitialFailedCallback", [[NSString stringWithFormat:@"%@", [error localizedDescription]] UTF8String]);
}

- (void)playAdsInterstitialDidClose
{
    UnityPause(false);
    self.isReadyRaised = NO;
    UnitySendMessage([self.instanceName UTF8String], "InterstitialClosedCallback", "");
}

- (void)playAdsVersion
{
    UnitySendMessage([self.instanceName UTF8String], "PlayAdsVersionCallback", [[NSString stringWithFormat:@"%@", [PlayAdsSDKBridge getVersion]] UTF8String]);
}

@end


#ifdef __cplusplus

extern "C"
{
    void PlayAdsSDKInitialize(const char* appID, const char* secret, const char* instanceName)
    {
        NSString *applicationID = [NSString stringWithCString:appID encoding:nil];
    	NSString *applicationSecret = [NSString stringWithCString:secret encoding:nil];
        NSString *unityInstanceName = [NSString stringWithCString:instanceName encoding:nil];
        
        [PlayAdsSDKBridge sharedInstance].instanceName = unityInstanceName;
        [PlayAdsSDKBridge startPlayAdsSDKForApp:applicationID secretToken:applicationSecret];
    }
    
	void PlayAdsSDKShowInterstitial(const char* type)
    {
        NSString *intersitialType = [NSString stringWithCString:type encoding:nil];
        
        if ([intersitialType isEqualToString:@"Smart"])
        {
            [PlayAdsSDKBridge showInterstitialOfType:PlayAdsInterstitialTypeSmart];
        }
    }
    
    void PlayAdsSDKPrepareInterstitial(const char* type)
    {
        NSString *intersitialType = [NSString stringWithCString:type encoding:nil];
        
        if ([intersitialType isEqualToString:@"Smart"])
        {
            [PlayAdsSDKBridge prepareInterstitialOfType:PlayAdsInterstitialTypeSmart];
        }
    }
    
    void PlayAdsSDKShowPreparedInterstitial()
    {
        [PlayAdsSDKBridge showPreparedInterstitial];
    }

    void PlayAdsSDKGetVersion()
    {
        [[PlayAdsSDKBridge sharedInstance] playAdsVersion];
    }
}

#endif