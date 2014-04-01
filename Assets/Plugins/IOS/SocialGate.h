//
//  SocialGate.h
//  Unity-iPhone
//
//  Created by lacost on 2/15/14.
//
//

#import <Foundation/Foundation.h>
#import <Accounts/Accounts.h>
#import <Social/Social.h>

#include "iPhone_View.h"
#include "Unity3d.h"

@interface SocialGate : NSObject
    + (id) sharedInstance;

- (void) twitterPost:(NSString*)status;
- (void) twitterPostWithMedia:(NSString*)status media: (NSString*) media;


- (void) fbPost:(NSString*)status;
- (void) fbPostWithMedia:(NSString*)status media: (NSString*) media;
@end
