//
//  SocialGate.m
//  Unity-iPhone
//
//  Created by lacost on 2/15/14.
//
//

#import "SocialGate.h"

@implementation SocialGate

static SocialGate *_sharedInstance;


+ (id)sharedInstance {
    
    if (_sharedInstance == nil)  {
        _sharedInstance = [[self alloc] init];
    }
    
    return _sharedInstance;
}


-(void) twitterPostWithMedia:(NSString *)status media:(NSString *)media {
    NSLog(@"twitterPostWithMedia");
    
    NSData *imageData = [[NSData alloc] initWithBase64Encoding:media];
    UIImage *image = [[UIImage alloc] initWithData:imageData];
    
    SLComposeViewController *tweetSheet = [SLComposeViewController composeViewControllerForServiceType:SLServiceTypeTwitter];
    [tweetSheet setInitialText:status];
    [tweetSheet addImage:image];
    
    UIViewController *vc =  UnityGetGLViewController();
    
    [vc presentViewController:tweetSheet animated:YES completion:nil];
    
    tweetSheet.completionHandler = ^(SLComposeViewControllerResult result) {
        switch(result) {
                //  This means the user cancelled without sending the Tweet
            case SLComposeViewControllerResultCancelled:
                NSLog(@"Tweet message was cancelled");
                UnitySendMessage("IOSSocialManager", "OnTwitterPostFailed", [Unity3d NSStringToChar:@""]);
                break;
                //  This means the user hit 'Send'
            case SLComposeViewControllerResultDone:
                NSLog(@"Done pressed successfully");
                UnitySendMessage("IOSSocialManager", "OnTwitterPostSuccess", [Unity3d NSStringToChar:@""]);
                break;
        }
    };
    
}

- (void) twitterPost:(NSString *)status {
     NSLog(@"twitterPost");
    
    SLComposeViewController *twSheet = [SLComposeViewController  composeViewControllerForServiceType:SLServiceTypeTwitter];
    [twSheet setInitialText:status];
    
    
    UIViewController *vc =  UnityGetGLViewController();
    
    [vc presentViewController:twSheet animated:YES completion:nil];
    
    twSheet.completionHandler = ^(SLComposeViewControllerResult result) {
        switch(result) {
                //  This means the user cancelled without sending the Tweet
            case SLComposeViewControllerResultCancelled:
                NSLog(@"Tweet message was cancelled");
                UnitySendMessage("IOSSocialManager", "OnTwitterPostFailed", [Unity3d NSStringToChar:@""]);
                break;
                //  This means the user hit 'Send'
            case SLComposeViewControllerResultDone:
                NSLog(@"Done pressed successfully");
                UnitySendMessage("IOSSocialManager", "OnTwitterPostSuccess", [Unity3d NSStringToChar:@""]);
                break;
        }
    };
}


- (void) fbPost:(NSString *)status {
    
     NSLog(@"fbPost");
    
    SLComposeViewController *fbSheet = [SLComposeViewController  composeViewControllerForServiceType:SLServiceTypeFacebook];
    [fbSheet setInitialText:status];
    
    
    UIViewController *vc =  UnityGetGLViewController();
    
    [vc presentViewController:fbSheet animated:YES completion:nil];
    
    fbSheet.completionHandler = ^(SLComposeViewControllerResult result) {
        switch(result) {
                //  This means the user cancelled without sending the Tweet
            case SLComposeViewControllerResultCancelled:
                NSLog(@"Facebook message was cancelled");
                UnitySendMessage("IOSSocialManager", "OnFacebookPostFailed", [Unity3d NSStringToChar:@""]);
                break;
                //  This means the user hit 'Send'
            case SLComposeViewControllerResultDone:
                NSLog(@"Facebook pressed successfully");
                UnitySendMessage("IOSSocialManager", "OnFacebookPostSuccess", [Unity3d NSStringToChar:@""]);
                break;
        }
    };

}

-(void) fbPostWithMedia:(NSString *)status media:(NSString *)media {
     NSLog(@"fbPostWithMedia");
    
    NSData *imageData = [[NSData alloc] initWithBase64Encoding:media];
    UIImage *image = [[UIImage alloc] initWithData:imageData];
    
    SLComposeViewController *fbSheet = [SLComposeViewController composeViewControllerForServiceType:SLServiceTypeFacebook];
    [fbSheet setInitialText:status];
    [fbSheet addImage:image];
    
    UIViewController *vc =  UnityGetGLViewController();
    
    [vc presentViewController:fbSheet animated:YES completion:nil];
    
    fbSheet.completionHandler = ^(SLComposeViewControllerResult result) {
        switch(result) {
                //  This means the user cancelled without sending the Tweet
            case SLComposeViewControllerResultCancelled:
                NSLog(@"Tweet message was cancelled");
                UnitySendMessage("IOSSocialManager", "OnFacebookPostFailed", [Unity3d NSStringToChar:@""]);
                break;
                //  This means the user hit 'Send'
            case SLComposeViewControllerResultDone:
                NSLog(@"Done pressed successfully");
                UnitySendMessage("IOSSocialManager", "OnFacebookPostSuccess", [Unity3d NSStringToChar:@""]);
                break;
        }
    };

}


extern "C" {
    

    

    
    void _twPost(char* text) {
        NSString *status = [Unity3d charToNSString:text];
        [[SocialGate sharedInstance] twitterPost:status];
    }
    
    
    
    void _twPostWithMedia(char* text, char* encodedMedia) {
        NSString *status = [Unity3d charToNSString:text];
        NSString *media = [Unity3d charToNSString:encodedMedia];
        
        [[SocialGate sharedInstance] twitterPostWithMedia:status media:media];
    }
    
    
    
    void _fbPost(char* text) {
        NSString *status = [Unity3d charToNSString:text];
        [[SocialGate sharedInstance] fbPost:status];
    }
    
    
    void _fbPostWithMedia(char* text, char* encodedMedia) {
        
        NSString *status = [Unity3d charToNSString:text];
        NSString *media = [Unity3d charToNSString:encodedMedia];
        
        [[SocialGate sharedInstance] fbPostWithMedia:status media:media];
    }
    
    
}



@end
