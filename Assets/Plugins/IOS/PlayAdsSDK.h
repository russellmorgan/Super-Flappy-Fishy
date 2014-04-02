//
//  PlayAdsSDK.h
//  PlayAdsSDK V2 1.9.1 b3865
//
//  Created by the AppLift iOS Team on 2/27/13.
//  Copyright (c) 2013 AppLift. All rights reserved.
//

#import <Foundation/Foundation.h>

// Types and Contstants
//================================

typedef enum {
    PlayAdsInterstitialTypeGift         = 0,
    PlayAdsInterstitialTypeScratch      = 1,
    PlayAdsInterstitialTypeGamesList    = 2,
    PlayAdsInterstitialTypeSlotMachine  = 3,
    PlayAdsInterstitialTypeSmart        = 4,
    PlayAdsInterstitialTypeCoverFlow    = 5,
    PlayAdsInterstitialTypeMemory       = 6,
    PlayAdsInterstitialTypeLight        = 7
} PlayAdsInterstitialType;

extern NSString * const kPlayAdsErrorTypeNetwork;
extern NSString * const kPlayAdsErrorTypeInterstitialTypeNotActive;
extern NSString * const kPlayAdsErrorTypeResourceNotAvailable;



// SDK Delegate
//================================

@protocol PlayAdsSDKDelegate <NSObject>

@optional

- (void)playAdsSDKReady;
- (void)playAdsInterstitialReady;
- (void)playAdsInterstitialDidShown;
- (void)playAdsInterstitialDidFailWithError:(NSError*)error;
- (void)playAdsInterstitialDidClose;

@end



// SDK Interface
//================================

@interface PlayAdsSDK : NSObject

// Boostrap
// To be able to capture the "playAdsSDKReady" notification implemet:
// + (NSString*)startPlayAdsSDKForApp:(NSString *)appId
//                        secretToken:(NSString *)secretToken
//                           delegate:(id<PlayAdsSDKDelegate>)delegate;

+ (void)startPlayAdsSDKForApp:(NSString *)appId
                  secretToken:(NSString *)secretToken;

+ (void)startPlayAdsSDKForApp:(NSString *)appId
                  secretToken:(NSString *)secretToken
                     delegate:(id<PlayAdsSDKDelegate>)delegate;

+ (void)deallocPlayAdsSDK;


// Delegation pattern based implementation
//================================

// Loads the specific ad type
+ (void)prepareInterstitialOfType:(PlayAdsInterstitialType)type
                         delegate:(id<PlayAdsSDKDelegate>)delegate;

// The ad type will be choosen randomly from the APD
+ (void)prepareInterstitialWithDelegate:(id<PlayAdsSDKDelegate>)delegate;

// Shows the prepared ad after -(void)playAdsSDKReady; is called
+ (void)showPreparedInterstitial;


// Direct implementation
//================================

// Shows the specific ad type
+ (void)showInterstitialOfType:(PlayAdsInterstitialType)type
                      delegate:(id<PlayAdsSDKDelegate>)delegate;

// Shows an ad choosen randomly from the APD
+ (void)showInterstitialWithDelegate:(id<PlayAdsSDKDelegate>)delegate;


// Helper methods
//================================

+ (void)resetPrecachedGames;
+ (void)resetImageCache;
+ (NSString*)versionString;
+ (UIViewController*)currentInterstitial;

@end
