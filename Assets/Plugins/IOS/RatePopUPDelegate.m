//
//  RatePopUPDelegate.m
//
//  Created by Osipov Stanislav on 1/12/13.
//
//

#import "RatePopUPDelegate.h"
#import "Unity3d.h"
#import "IOSNative.h"

@implementation RatePopUPDelegate


NSString *templateReviewURL = @"itms-apps://ax.itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id=APP_ID";

NSString* templateReviewURLIOS7  = @"itms-apps://itunes.apple.com/app/idAPP_ID";

NSString *const kAppiraterRatedCurrentVersion		= @"kAppiraterRatedCurrentVersion";

- (void)alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger)buttonIndex {
    
    if(buttonIndex == 0) {
        [RatePopUPDelegate rateApp];
    }
    
    [IOSNative unregisterAllertView];
    UnitySendMessage("IOSRateUsPopUp", "onPopUpCallBack",  [Unity3d NSIntToChar:buttonIndex]);
}


+ (void)rateApp {
#if TARGET_IPHONE_SIMULATOR
    NSLog(@"APPIRATER NOTE: iTunes App Store is not supported on the iOS simulator. Unable to open App Store page.");
#else
    NSUserDefaults *userDefaults = [NSUserDefaults standardUserDefaults];
    
	// this URL Scheme should work in the iOS 6 App Store in addition to older stores
    NSString *appId = [IOSNative getAppId];
    
    
    
    NSString *reviewURL;
    NSArray *vComp = [[UIDevice currentDevice].systemVersion componentsSeparatedByString:@"."];
    
    if ([[vComp objectAtIndex:0] intValue] >= 7) {
        reviewURL = [templateReviewURLIOS7 stringByReplacingOccurrencesOfString:@"APP_ID" withString:[NSString stringWithFormat:@"%@", appId]];
    }  else {
        reviewURL = [templateReviewURL stringByReplacingOccurrencesOfString:@"APP_ID" withString:[NSString stringWithFormat:@"%@", appId]];
        
    }
    
	
    [userDefaults setBool:YES forKey:kAppiraterRatedCurrentVersion];
    [userDefaults synchronize];
    [[UIApplication sharedApplication] openURL:[NSURL URLWithString:reviewURL]];
#endif
}

@end
