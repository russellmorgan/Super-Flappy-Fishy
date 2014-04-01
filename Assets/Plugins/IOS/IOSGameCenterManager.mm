#import "IOSGameCenterManager.h"
#import "Unity3d.h"

@implementation IOSGameCenterManager

- (id)init {
    self = [super init];
    if (self) {
        curentView =  NULL;
        requestedLeaderBordId = NULL;
        isAchievementsWasLoaded = FALSE;
        
        gameCenterManager = [[GameCenterManager alloc] init];
        NSLog(@"IOSGameCenterManager inited");
    }
    
    return self;
}



- (void)dealloc {
    [super dealloc];
}




- (void) reportScore: (int) score forCategory: (NSString*) category {
    NSLog(@"reportScore: ");
    NSLog(@"NEW HI SCORE : %i", score);
    NSLog(@"category %@", category);
    
    GKScore *scoreReporter = [[[GKScore alloc] initWithCategory:category] autorelease];
    scoreReporter.value = score;
    [scoreReporter reportScoreWithCompletionHandler: ^(NSError *error) {
        if (error != nil) {
            NSLog(@"got error while score sibmiting");
        } else {
            NSLog(@"new hing score sumbited succsess: %i", score);
        }
        
    }];
}


-(void) resetAchievements {
    [gameCenterManager resetAchievements];
}

-(void) submitAchievement:(double)percentComplete identifier:(NSString *)identifier  notifayComplete: (BOOL) notifayComplete {
    [gameCenterManager submitAchievement:identifier percentComplete:percentComplete notifayComplete:notifayComplete];
}

- (void) propouseAuth {
    UIAlertView *alert = [[UIAlertView alloc] init];
    [alert setTitle:@"Game Center is disabled"];
    [alert setMessage:@"Do you whant to login now?"];
    [alert setDelegate:self];
    [alert addButtonWithTitle:@"Yes"];
    [alert addButtonWithTitle:@"No"];
    [alert show];
    [alert release];
    
    
    
}

- (void) showLeaderBoard:(NSString *)leaderBoradrId {
    
    requestedLeaderBordId = leaderBoradrId;
    [requestedLeaderBordId retain];
    
    GKLocalPlayer *localPlayer = [GKLocalPlayer localPlayer];
    if(localPlayer.isAuthenticated) {
        [self showLeaderBoardPopUp];
    } else {
        [self propouseAuth];
    }
}


- (void) showAchievements {
    
    GKLocalPlayer *localPlayer = [GKLocalPlayer localPlayer];
    if(localPlayer.isAuthenticated) {
        [self showAchievementsPopUp];
    } else {
        [self propouseAuth];
    }
}

- (void) showAchievementsPopUp {
    
    GKAchievementViewController *achievements = [[GKAchievementViewController alloc] init];
    if(achievements != NULL) {
        
        achievements.achievementDelegate = self;
        
        CGSize screenSize = [[UIScreen mainScreen] bounds].size;
        UIWindow* window = [UIApplication sharedApplication].keyWindow;
        
        UIViewController *glView2 = [[UIViewController alloc] init];
        curentView = glView2.view;
        [window addSubview: glView2.view];
        
        [glView2 presentViewController: achievements animated: YES completion:nil];
        
        achievements.view.transform = CGAffineTransformMakeRotation(0.0f);
        [achievements.view setCenter:CGPointMake(screenSize.width/2, screenSize.height/2)];
        achievements.view.bounds = CGRectMake(0, 0, screenSize.width, screenSize.height);
        
    }
}

- (void) showLeaderBoardsPopUp {
    GKGameCenterViewController *leaderboardController = [[GKGameCenterViewController alloc] init];
    
    
    NSLog(@"showLeaderBoardsPopUp");
    leaderboardController.gameCenterDelegate = self;
    
    
    CGSize screenSize = [[UIScreen mainScreen] bounds].size;
    UIWindow* window = [UIApplication sharedApplication].keyWindow;
    
    
    UIViewController *glView2 = [[UIViewController alloc] init];
    curentView = glView2.view;
    [window addSubview: glView2.view];
    [glView2 presentViewController: leaderboardController animated: YES completion:nil];
    leaderboardController.view.transform = CGAffineTransformMakeRotation(0.0f);
    [leaderboardController.view setCenter:CGPointMake(screenSize.width/2, screenSize.height/2)];
    leaderboardController.view.bounds = CGRectMake(0, 0, screenSize.width, screenSize.height);
    
    
}

- (void) showLeaderBoardPopUp {
    
    GKGameCenterViewController *leaderboardController = [[GKGameCenterViewController alloc] init];
    if (leaderboardController != NULL)
    {
        NSLog(@"requested LB: %@", requestedLeaderBordId);
        
        leaderboardController.leaderboardCategory = requestedLeaderBordId;
        leaderboardController.leaderboardTimeScope = GKLeaderboardTimeScopeWeek;
        leaderboardController.gameCenterDelegate = self;
        
        
        CGSize screenSize = [[UIScreen mainScreen] bounds].size;
        UIWindow* window = [UIApplication sharedApplication].keyWindow;
        
        
        UIViewController *glView2 = [[UIViewController alloc] init];
        curentView = glView2.view;
        [window addSubview: glView2.view];
        [glView2 presentViewController: leaderboardController animated: YES completion:nil];
        leaderboardController.view.transform = CGAffineTransformMakeRotation(0.0f);
        [leaderboardController.view setCenter:CGPointMake(screenSize.width/2, screenSize.height/2)];
        leaderboardController.view.bounds = CGRectMake(0, 0, screenSize.width, screenSize.height);
        
        [requestedLeaderBordId release];
        
    }
    
    requestedLeaderBordId = NULL;
}


- (void)gameCenterViewControllerDidFinish:(GKGameCenterViewController *)vc {
    [self dismissGameCenterView:vc];
}


- (void)achievementViewControllerDidFinish:(GKAchievementViewController *)vc; {
    [self dismissGameCenterView:vc];
}


-(void) dismissGameCenterView: (GKGameCenterViewController *)vc {
    if(curentView != NULL) {
        [curentView removeFromSuperview];
    }
    
    [vc dismissViewControllerAnimated:YES completion:nil];
    [vc.view.superview removeFromSuperview];
    [vc release];
}


- (void) authenticateLocalPlayer {
    NSLog(@"authenticateLocalPlayer call");
    
    
    if([self isGameCenterAvailable]){
        GKLocalPlayer *localPlayer = [GKLocalPlayer localPlayer];
        
        if(localPlayer.authenticated == NO) {
            [localPlayer authenticateWithCompletionHandler:^(NSError *error){
                if (localPlayer.isAuthenticated){
                    NSLog(@"PLAYER AUTHENICATED");
                    
                    NSMutableString * data = [[NSMutableString alloc] init];
                    
                    
                    [data appendString:localPlayer.playerID];
                    [data appendString:@","];
                    [data appendString:localPlayer.alias];
                    
                
                    
                    NSString *str = [[data copy] autorelease];
                    UnitySendMessage("GameCenterManager", "onAuthenticateLocalPlayer", [Unity3d NSStringToChar:str]);
                    
                    NSLog(@"PLAYER AUTHENICATED %@", localPlayer.alias);
                    
                    [[GCHelper sharedInstance] initNotificationHandler];
                    
                    if(!isAchievementsWasLoaded) {
                        [self loadAchievements];
                    }
                    
                } else {
                   
                    NSLog(@"Error: %@", error);
                    NSLog(@"PLAYER NOT AUTHENICATED");
                    UnitySendMessage("GameCenterManager", "onAuthenticationFailed", [Unity3d NSStringToChar:@""]);
                }
            }];
        } else {
            NSLog(@"Do nothing Player is already auntificated");
        }
    }
}
- (void) loadAchievements {
    [GKAchievement loadAchievementsWithCompletionHandler:^(NSArray *achievements, NSError *error) {
        if (error == nil) {
            isAchievementsWasLoaded = TRUE;
            NSMutableString * data = [[NSMutableString alloc] init];
            BOOL first = YES;
            for (GKAchievement* achievement in achievements) {
                
                
                if(!first) {
                    [data appendString:@","];
                }
                
                [data appendString:achievement.identifier];
                [data appendString:@","];
                
                NSLog(@"achievement.percentComplete:  %f", achievement.percentComplete);
                
                [data appendString:[NSString stringWithFormat:@"%f", achievement.percentComplete]];
                
                first = NO;
                
            }
            
            NSString *str = [[data copy] autorelease];
            UnitySendMessage("GameCenterManager", "onAchievementsLoaded", [Unity3d NSStringToChar:str]);
        }
    }];
}

- (void)alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger)buttonIndex {
    if (buttonIndex == 0) {
        [self authenticateLocalPlayer];
    } else if (buttonIndex == 1) {
        
    }
}



- (BOOL)isGameCenterAvailable {
    BOOL localPlayerClassAvailable = (NSClassFromString(@"GKLocalPlayer")) != nil;
    NSString *reqSysVer = @"4.1";
    NSString *currSysVer = [[UIDevice currentDevice] systemVersion];
    BOOL osVersionSupported = ([currSysVer compare:reqSysVer options:NSNumericSearch] != NSOrderedAscending);
    return (localPlayerClassAvailable && osVersionSupported);
}


-(void) retrieveScoreForLocalPlayerWithCategory:(NSString*)category
{
    GKLeaderboard *leaderboardRequest = [[[GKLeaderboard alloc] init] autorelease];
    leaderboardRequest.category = category;
    
    if (leaderboardRequest != nil) {
        NSMutableString * data = [[NSMutableString alloc] init];
        
        [leaderboardRequest loadScoresWithCompletionHandler:^(NSArray *scores, NSError *error){
            if (error != nil) {
                //Handle error <- don't care
            }  else {
                [data appendString:category];
                [data appendString:@","];
                [data appendString:[NSString stringWithFormat:@"%lld", leaderboardRequest.localPlayerScore.value]];
                
                NSString *str = [[data copy] autorelease];
                UnitySendMessage("GameCenterManager", "onLeaderBoardScore", [Unity3d NSStringToChar:str]);
                
                
                NSLog(@"Retrieved localScore:%lld",leaderboardRequest.localPlayerScore.value);
            }
        }];
    }
}



- (void)findMatchWithMinPlayers:(int)minPlayers maxPlayers:(int)maxPlayers {
    [[GCHelper sharedInstance] findMatchWithMinPlayers:minPlayers maxPlayers:maxPlayers];
}
 




@end


static IOSGameCenterManager *GCManager = NULL;

extern "C" {
    void _initGamaCenter ()  {
        GCManager = [[IOSGameCenterManager alloc] init];
        [GCManager authenticateLocalPlayer];
    }
    
    
    void _showLeaderBoard(char* leaderBoradrId) {
        [GCManager showLeaderBoard:[Unity3d charToNSString:leaderBoradrId]];
    }
    
    void _showLeaderBoards() {
        [GCManager showLeaderBoardsPopUp];
    }
    
    void _getLeadrBoardScore(char* leaderBoradrId) {
        [GCManager retrieveScoreForLocalPlayerWithCategory:[Unity3d charToNSString:leaderBoradrId]];
    }
    
    void _showAchievements() {
        [GCManager showAchievements];
    }
    
    
    
    
    void _reportScore(int score, char* leaderBoradrId) {
        [GCManager reportScore:score forCategory:[Unity3d charToNSString:leaderBoradrId]];
    }
    
    void _submitAchievement(float percents, char* achievementID, BOOL notifayComplete) {
        double v = (double) percents;
        [GCManager submitAchievement:v identifier:[Unity3d charToNSString:achievementID] notifayComplete:notifayComplete];
    }
    
    void _resetAchievements() {
        [GCManager resetAchievements];
    }
    
}

