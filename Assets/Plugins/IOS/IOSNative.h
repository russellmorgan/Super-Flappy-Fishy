//
//  IOSNative.h
//
//  Created by Osipov Stanislav on 1/11/13.
//
//

#import <Foundation/Foundation.h>

@interface IOSNative : NSObject


+ (void) setAppId:(NSString*)appId;
+ (NSString *) getAppId;
- (void)showRateUsPopUp: (NSString *) title message: (NSString*) msg b1: (NSString*) b1 b2: (NSString*) b2 b3: (NSString*) b3;

+ (void) dismissCurrentAlert;
+ (void) unregisterAllertView;

@end
