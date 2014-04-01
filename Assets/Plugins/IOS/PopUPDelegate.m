//
//  RatePopUPDelegate.m
//
//  Created by Osipov Stanislav on 1/12/13.
//
//

#import "PopUPDelegate.h"
#import "Unity3d.h"
#import "IOSNative.h"

@implementation PopUPDelegate

- (void)alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger)buttonIndex {
    [IOSNative unregisterAllertView];
    UnitySendMessage("IOSPopUp", "onPopUpCallBack",  [Unity3d NSIntToChar:buttonIndex]);
}


@end
