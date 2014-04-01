//
//  iAdBannerObject.m
//  Unity-iPhone
//
//  Created by lacost on 2/11/14.
//
//

#import "iAdBannerObject.h"

@implementation iAdBannerObject


- (void) dealloc
{
    [self bannerView].delegate = nil;
    [[self bannerView] release];
    
    
   // [[self bannerView] dealloc];
    [super dealloc];
}



-(void) InitBanner:(int)bannerId  {
    NSNumber *n = [NSNumber numberWithInt:bannerId];
    [self setBid:n];
    
    
    bool IsLandscape;
    UIInterfaceOrientation orientation = [UIApplication sharedApplication].statusBarOrientation;
    if(orientation == UIInterfaceOrientationLandscapeLeft || orientation == UIInterfaceOrientationLandscapeRight) {
        IsLandscape = true;
    } else {
        IsLandscape = false;
    }
    
    NSLog(@"IsLandscape %i", IsLandscape);
    
    
    
    [self setBannerView:[[ADBannerView alloc] initWithFrame:CGRectZero]];
    
    if(IsLandscape) {
         [self bannerView].currentContentSizeIdentifier = ADBannerContentSizeIdentifierLandscape;
    } else {
         [self bannerView].currentContentSizeIdentifier = ADBannerContentSizeIdentifierPortrait;
    }
   
 
    [self bannerView].delegate = self;
    [[self bannerView] setBackgroundColor:[UIColor clearColor]];
   
    
  }




- (void) CreateBannerAdPos:(int)x y:(int)y  bannerId:(int)bannerId {
    
    [self InitBanner:bannerId];
    
    [self bannerView].frame = CGRectMake(x,
                                         y,
                                         [self bannerView].frame.size.width,
                                         [self bannerView].frame.size.height);
    
    

    UIViewController *vc =  UnityGetGLViewController();
    [[vc view] addSubview:[self bannerView]];
    [self bannerView].hidden = true;
    
}


-(void) CreateBanner:(int)gravity  bannerId:(int)bannerId {
    
    [self InitBanner:bannerId];
    
    
    float x = 0.0;
    float y = 0.0;
    

    
    if(gravity == 83) {
        y = [self GetH: 2];
    }
    
    if(gravity == 81) {
        x = [self GetW:1];
        y = [self GetH: 2];
        
    }
    
    if(gravity == 85) {
        x = [self GetW:2];
        y = [self GetH: 2];
        
    }
    
    
    if(gravity == 51) {
        //ziros
    }
    
    if(gravity == 49) {
        x = [self GetW:1];
        
    }
    
    if(gravity == 53) {
        x = [self GetW:2];
    }
    
    if(gravity == 19) {
        y = [self GetH: 1];
    }
    
    if(gravity == 17) {
        x = [self GetW:1];
        y = [self GetH: 1];
        
    }
    
    if(gravity == 21) {
        x = [self GetW:2];
        y = [self GetH: 1];
    }
    
   
    [self bannerView].frame = CGRectMake(x,y,
                                         [self bannerView].frame.size.width,
                                         [self bannerView].frame.size.height);
    
    
    UIViewController *vc =  UnityGetGLViewController();
    [[vc view] addSubview:[self bannerView]];
    [self bannerView].hidden = true;

}

- (float) GetW: (int) p {
    UIViewController *vc =  UnityGetGLViewController();
    
    bool IsLandscape;
    UIInterfaceOrientation orientation = [UIApplication sharedApplication].statusBarOrientation;
    if(orientation == UIInterfaceOrientationLandscapeLeft || orientation == UIInterfaceOrientationLandscapeRight) {
        IsLandscape = true;
    } else {
        IsLandscape = false;
    }
    
    CGFloat w;
    if(IsLandscape) {
        w = vc.view.frame.size.height;
    } else {
        w = vc.view.frame.size.width;
    }
    
    if(p == 1) {
        return  (w - [self bannerView].frame.size.width) / 2;
    }
    
    if(p == 2) {
        return  w - [self bannerView].frame.size.width;
    }
    
    return 0.0;
    
    
}

- (float) GetH: (int) p {
    UIViewController *vc =  UnityGetGLViewController();
    
    bool IsLandscape;
    UIInterfaceOrientation orientation = [UIApplication sharedApplication].statusBarOrientation;
    if(orientation == UIInterfaceOrientationLandscapeLeft || orientation == UIInterfaceOrientationLandscapeRight) {
        IsLandscape = true;
    } else {
        IsLandscape = false;
    }
    
    CGFloat h;
    if(IsLandscape) {
        h = vc.view.frame.size.width;
    } else {
        h = vc.view.frame.size.height;
    }
    
    if(p == 1) {
        return  (h - [self bannerView].frame.size.height) / 2;
    }
    
    if(p == 2) {
        return  h - [self bannerView].frame.size.height;
    }
    
    return 0.0;
    
    
    
}


-(void) HideAd {
    [[self bannerView] removeFromSuperview];
    
}

-(void) ShowAd {
    [self bannerView].hidden = false;
    UIViewController *vc =  UnityGetGLViewController();
    [[vc view] addSubview:[self bannerView]];
    
}

#pragma mark GADInterstitialDelegate implementation


-(void)bannerView:(ADBannerView *)banner didFailToReceiveAdWithError:(NSError *)error{
    if(error != nil) {
         NSLog(@"didFailToReceiveAdWithError %@", error.description);
    }
    
    UnitySendMessage("iAdBannerController", "didFailToReceiveAdWithError", [[[self bid] stringValue] UTF8String]);
}

-(void)bannerViewDidLoadAd:(ADBannerView *)banner{
    NSLog(@"bannerViewDidLoadAd");
    UnitySendMessage("iAdBannerController", "bannerViewDidLoadAd", [[[self bid] stringValue] UTF8String]);
}
-(void)bannerViewWillLoadAd:(ADBannerView *)banner{
     NSLog(@"bannerViewWillLoadAd");
    
    UnitySendMessage("iAdBannerController", "bannerViewWillLoadAd", [[[self bid] stringValue] UTF8String]);
}
-(void)bannerViewActionDidFinish:(ADBannerView *)banner{
     NSLog(@"bannerViewActionDidFinish");
    
    UnitySendMessage("iAdBannerController", "bannerViewActionDidFinish", [[[self bid] stringValue] UTF8String]);
}




@end
