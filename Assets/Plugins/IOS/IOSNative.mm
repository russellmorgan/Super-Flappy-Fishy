//
//  IOSNative.m
//
//  Created by Osipov Stanislav on 1/11/13.
//
//

#import "IOSNative.h"
#import "Unity3d.h"

#import "RatePopUPDelegate.h"
#import "PopUPDelegate.h"


#import "InAppPurchaseManager.h"

@implementation IOSNative

static NSString *_appId;


- (id) init
{
	self = [super init];
	if(self!= NULL) {
        [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(applicationDidBecomeActive:)   name:UIApplicationDidBecomeActiveNotification object:nil];
        [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(applicationWillResignActive:) name:UIApplicationWillResignActiveNotification object:nil];
        [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(applicationDidEnterBackground:) name:UIApplicationDidEnterBackgroundNotification object:nil];
        [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(applicationWillTerminate:) name:UIApplicationWillTerminateNotification object:nil];
        [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(applicationDidReceiveMemoryWarning:) name:UIApplicationDidReceiveMemoryWarningNotification object:nil];
        
    }
	return self;
}



-(void)dealloc {
    if (_appId != nil)
        [_appId release];
    
    
    [[NSNotificationCenter defaultCenter] removeObserver:self];
    [super dealloc];
}



+ (void) setAppId:(NSString *)appId {
    
    if (_appId != nil) {
        [_appId release];
        _appId = nil;
    }
    _appId = appId;
    [_appId retain];
}

+ (NSString *) getAppId {
    return _appId;
}


+ (void) sendEvent: (NSString* ) event {
    UnitySendMessage("IOSNative", [Unity3d NSStringToChar:event], [Unity3d NSStringToChar:@"null"]);
}


- (void)applicationDidBecomeActive:(NSNotification *)notification {
    [IOSNative sendEvent:@"applicationDidBecomeActive"];
}


- (void) applicationWillResignActive:(NSNotification *)notification {
    [IOSNative sendEvent:@"applicationWillResignActive"];
}

- (void) applicationDidEnterBackground:(NSNotification *)notification {
    [IOSNative sendEvent:@"applicationDidEnterBackground"];
}

- (void) applicationWillTerminate:(NSNotification *)notification {
    [IOSNative sendEvent:@"applicationWillTerminate"];
}

- (void) applicationDidReceiveMemoryWarning:(NSNotification *)notification {
    [IOSNative sendEvent:@"applicationDidReceiveMemoryWarning"];
}


 static UIAlertView* _currentAllert =  nil;



+ (void) unregisterAllertView {
    if(_currentAllert != nil) {
        [_currentAllert release];
        _currentAllert = nil;
    }
}

+(void) dismissCurrentAlert {
    if(_currentAllert != nil) {
        [_currentAllert dismissWithClickedButtonIndex:0 animated:YES];
        [_currentAllert release];
        _currentAllert = nil;
    }
}

- (void) showRateUsPopUp: (NSString *) title message: (NSString*) msg b1: (NSString*) b1 b2: (NSString*) b2 b3: (NSString*) b3 {
    
    UIAlertView *alert = [[UIAlertView alloc] init];
    [alert setTitle:title];
    [alert setMessage:msg];
    [alert setDelegate: [[RatePopUPDelegate alloc] init]];
    
    [alert addButtonWithTitle:b1];
    [alert addButtonWithTitle:b2];
    [alert addButtonWithTitle:b3];
    
    [alert show];
    
    _currentAllert = alert;
 
}


- (void) showDialog: (NSString *) title message: (NSString*) msg yesTitle:(NSString*) b1 noTitle: (NSString*) b2{
    
    UIAlertView *alert = [[UIAlertView alloc] init];
    [alert setTitle:title];
    [alert setMessage:msg];
    [alert setDelegate: [[PopUPDelegate alloc] init]];
    [alert addButtonWithTitle:b1];
    [alert addButtonWithTitle:b2];
    [alert show];
    
    _currentAllert = alert;
    
}


- (void) showMessage: (NSString *) title message: (NSString*) msg okTitle:(NSString*) b1 {
    
    UIAlertView *alert = [[UIAlertView alloc] init];
    [alert setTitle:title];
    [alert setMessage:msg];
    [alert setDelegate: [[PopUPDelegate alloc] init]];
    [alert addButtonWithTitle:b1];
    [alert show];

    _currentAllert = alert;
}





static IOSNative *ios = NULL;

extern "C" {
    void _initIOSNative (char* appId)  {
        ios = [[IOSNative alloc] init];
        [IOSNative setAppId:[Unity3d charToNSString:appId ]];
    }
    
    
    //--------------------------------------
	//  NATIVE FUNCTIONS
	//--------------------------------------
    
    void _showRateUsPopUp(char* title, char* message, char* b1, char* b2, char* b3) {
        [ios showRateUsPopUp:[Unity3d charToNSString:title] message:[Unity3d charToNSString:message] b1:[Unity3d charToNSString:b1] b2:[Unity3d charToNSString:b2] b3:[Unity3d charToNSString:b3]];
    }
    
    
    void _showDialog(char* title, char* message, char* yes, char* no) {
        [ios showDialog:[Unity3d charToNSString:title] message:[Unity3d charToNSString:message] yesTitle:[Unity3d charToNSString:yes] noTitle:[Unity3d charToNSString:no]];
    }
    
    void _showMessage(char* title, char* message, char* ok) {
        [ios showMessage:[Unity3d charToNSString:title] message:[Unity3d charToNSString:message] okTitle:[Unity3d charToNSString:ok]];
    }
    
    void _dismissCurrentAlert() {
        [IOSNative dismissCurrentAlert];
    }
    
    //--------------------------------------
	//  MARKET
	//--------------------------------------
    
    void _loadStore(char * productsId) {
        
        NSString* str = [Unity3d charToNSString:productsId];
        NSArray *items = [str componentsSeparatedByString:@","];
        
        for(NSString* s in items) {
            [[InAppPurchaseManager instance] addProductId:s];
        }
        
        [[InAppPurchaseManager instance] loadStore];
    }
    
    void _buyProduct(char * productId) {
        [[InAppPurchaseManager instance] buyProduct:[Unity3d charToNSString:productId]];
    }
    
    void _restorePurchases() {
        [[InAppPurchaseManager instance] restorePurchases];
    }
    
    
    void _verifyLastPurchase(char* url) {
        [[InAppPurchaseManager instance] verifyLastPurchase:[Unity3d charToNSString:url]];
    }
    
    
    
    
}



@end
