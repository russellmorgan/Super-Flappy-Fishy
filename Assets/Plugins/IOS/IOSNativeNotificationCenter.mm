//
//  IOSNativeNotificationCenter.m
//  Unity-iPhone
//
//  Created by lacost on 11/9/13.
//
//

#import "Unity3d.h"
#import "IOSNativeNotificationCenter.h"

@implementation IOSNativeNotificationCenter


static IOSNativeNotificationCenter *sharedHelper = nil;

+ (IOSNativeNotificationCenter *) sharedInstance {
    if (!sharedHelper) {
        sharedHelper = [[IOSNativeNotificationCenter alloc] init];
        
    }
    return sharedHelper;
}

-(void) scheduleNotification:(int)time message:(NSString *)messgae sound:(bool *)sound {
    
    
    UILocalNotification* localNotification = [[UILocalNotification alloc] init];
    localNotification.fireDate = [NSDate dateWithTimeIntervalSinceNow:time];
    localNotification.alertBody = messgae;
    localNotification.timeZone = [NSTimeZone defaultTimeZone];
    
    
    if(sound) {
        localNotification.soundName = UILocalNotificationDefaultSoundName;
    }
    
    
    [[UIApplication sharedApplication] scheduleLocalNotification:localNotification];

}



-(void) showNotificationBanner:(NSString *)title message:(NSString *)messgae {
    [GKNotificationBanner showBannerWithTitle:title message:messgae completionHandler:^{}];
}

- (void) cancelNotifications {
    [[UIApplication sharedApplication] cancelAllLocalNotifications];
}



@end

extern "C" {
    
    void _cancelNotifications() {
        [[IOSNativeNotificationCenter sharedInstance] cancelNotifications];
    }
    
    
    void _scheduleNotification (int time, char* messgae, bool* sound)  {
        [[IOSNativeNotificationCenter sharedInstance] scheduleNotification:time message:[Unity3d charToNSString:messgae] sound:sound];
    }
    
    void _showNotificationBanner (char* title, char* messgae)  {
        [[IOSNativeNotificationCenter sharedInstance] showNotificationBanner:[Unity3d charToNSString:title] message:[Unity3d charToNSString:messgae]];
    }
    
}
