//
//  GoogleSignInController.m
//  Unity-iPhone
//
//  Created by MacPro2017 on 2017/05/22.
//
//

#import <Foundation/Foundation.h>
#import "GoogleSignInController.h"

#import <GoogleSignIn/GoogleSignIn.h>
#include "UnityInterface.h"
#import "AppDelegateListener.h"

// GoogleSignIn TAG
#define TAG                         "GoogleSignInPlugin"
#define GOOGLE_SIGN_IN_SUCCESSED    "OnSignInSuccessed"
#define GOOGLE_SIGN_IN_FAILED       "OnSignInFailed"
#define GOOGLE_SIGN_IN_USER_CANCEL  "OnSignInUserCancel"

@interface GoogleSignInController()<AppDelegateListener, GIDSignInDelegate, GIDSignInUIDelegate>
@property (nonatomic) BOOL isDebugMode;
- (void)log:(NSString*)method msg:(NSString*)msg;
@end

@implementation GoogleSignInController

- (id) initWithClientId:(NSString*)clientId {
    self = [self init];
    self.isDebugMode = false;
    [GIDSignIn sharedInstance].clientID = clientId;
    [GIDSignIn sharedInstance].shouldFetchBasicProfile = YES;
    [GIDSignIn sharedInstance].delegate = self;
    [GIDSignIn sharedInstance].uiDelegate = self;
    UnityRegisterAppDelegateListener(self);
    return self;
}

#pragma mark AppDelegateListener

- (void)onOpenURL:(NSNotification*)notification {
    [self log:@"AppDelegateListener.onOpenURL" msg:@""];
    NSURL* url = notification.userInfo[@"url"];
    NSString* sourceApplication = notification.userInfo[@"sourceApplication"];
    id annotation = notification.userInfo[@"annotation"];
    [[GIDSignIn sharedInstance] handleURL:url sourceApplication:sourceApplication annotation:annotation];
}

#pragma mark GIDSignInDelegate

- (void)signIn:(GIDSignIn *)signIn didSignInForUser:(GIDGoogleUser *)user withError:(NSError *)error {
    [self log:@"GIDSignInDelegate.singIn" msg:[NSString stringWithFormat:@"didSignInForUser error:%@", error]];
    if (error == nil)
    {
        UnitySendMessage(TAG, GOOGLE_SIGN_IN_SUCCESSED, "");
    }
    else if (error.code == kGIDSignInErrorCodeCanceled)
    {
        UnitySendMessage(TAG, GOOGLE_SIGN_IN_USER_CANCEL, "");
    }
    else
    {
        UnitySendMessage(TAG, GOOGLE_SIGN_IN_FAILED, [[NSString stringWithFormat:@"%@", error] UTF8String]);
    }
}

- (void)signIn:(GIDSignIn *)signIn didDisconnectWithUser:(GIDGoogleUser *)use withError:(NSError *)error {
    [self log:@"GIDSignInDelegate.signIn" msg:[NSString stringWithFormat:@"didDisconnectWithUser error:%@", error]];
    UnitySendMessage(TAG, GOOGLE_SIGN_IN_FAILED, [[NSString stringWithFormat:@"%@", error] UTF8String]);
}

#pragma mark GIDSignInUIDelegate

- (void)signInWillDispatch:(GIDSignIn *)signIn error:(NSError *)error {
    [self log:@"GIDSignInUIDelegate.signInWillDispatch" msg:[NSString stringWithFormat:@"error:%@", error]];
    if (error != nil)
    {
        UnitySendMessage(TAG, GOOGLE_SIGN_IN_FAILED, [[NSString stringWithFormat:@"%@", error] UTF8String]);
    }
}

- (void)signIn:(GIDSignIn *)signIn presentViewController:(UIViewController *)viewController {
    [self log:@"GIDSignInUIDelegate.signIn" msg:@"present"];
    [UnityGetGLViewController() presentViewController:viewController animated:YES completion:nil];
}

- (void)signIn:(GIDSignIn *)signIn dismissViewController:(UIViewController *)viewController {
    [self log:@"GIDSignInUIDelegate.sign" msg:@"dismiss"];
    [UnityGetGLViewController() dismissViewControllerAnimated:YES completion:nil];
}

#pragma mark GoogleSignInController

- (NSString*)getEmail {
//    [self log:@"getEmal" msg:[GIDSignIn sharedInstance].currentUser.profile.email];
    return [GIDSignIn sharedInstance].currentUser.profile.email;
}

- (NSString*)getId {
//    [self log:@"getId" msg:[GIDSignIn sharedInstance].currentUser.userID];
    return [GIDSignIn sharedInstance].currentUser.userID;
}

- (NSString*)getIdToken {
//    [self log:@"getIdToekn" msg:[GIDSignIn sharedInstance].currentUser.authentication.idToken];
    return [GIDSignIn sharedInstance].currentUser.authentication.idToken;
}

- (NSString*)getServerAuthCode {
//    [self log:@"getServerAuthCode" msg:[GIDSignIn sharedInstance].currentUser.serverAuthCode];
    return [GIDSignIn sharedInstance].currentUser.serverAuthCode;
}

- (NSString*)getDisplayName {
//    [self log:@"getDisplayName" msg:[GIDSignIn sharedInstance].currentUser.profile.name];
    return [GIDSignIn sharedInstance].currentUser.profile.name;
}

- (void)signIn:(NSString*)clientId {
    [self log:@"signIn" msg:clientId];
    [GIDSignIn sharedInstance].clientID = clientId;
    [[GIDSignIn sharedInstance] signIn];
}

- (void)signOut {
    [self log:@"signOut" msg:@""];
    [[GIDSignIn sharedInstance] signOut];
}

- (void)setDebugMode:(bool)enable {
    [self log:@"setDebug" msg:[NSString stringWithFormat:@"enable:%d", enable]];
    self.isDebugMode = enable;
}

- (void)log:(NSString*)method msg:(NSString*)msg {
    if (self.isDebugMode) {
        NSLog(@"%s %@ %@", TAG, method, msg);
    }
}

@end
