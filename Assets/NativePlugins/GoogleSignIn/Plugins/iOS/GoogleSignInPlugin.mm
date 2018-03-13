//
//  GoogleSignInController.h
//  Unity-iPhone
//
//  Created by MacPro2017 on 2017/05/22.
//
//

#import <Foundation/Foundation.h>
#include "GoogleSignInController.h"

GoogleSignInController* googleSignInController = nil;
GoogleSignInController* GetGoogleSignInController() {
    if (googleSignInController == nil) {
        googleSignInController = [[GoogleSignInController alloc] initWithClientId:@""];
    }
    return googleSignInController;
}

const char* _AllocReturnString_GoogleSignIn(NSString* srcString) {
    if (srcString == nil) {
        return NULL;
    }
    
    const char *str = [srcString UTF8String];
    char* retStr = (char*)malloc(strlen(str) + 1);
    strcpy(retStr, str);
    return retStr;
}

extern "C"
{
    const char* _GetEmail_GoogleSignIn() {
        return _AllocReturnString_GoogleSignIn([GetGoogleSignInController() getEmail]);
    }
    
    const char* _GetId_GoogleSignIn() {
        return _AllocReturnString_GoogleSignIn([GetGoogleSignInController() getId]);
    }
    
    const char* _GetIdToken_GoogleSignIn() {
        return _AllocReturnString_GoogleSignIn([GetGoogleSignInController() getIdToken]);
    }
    
    const char* _GetServerAuthCode_GoogleSignIn() {
        return _AllocReturnString_GoogleSignIn([GetGoogleSignInController() getServerAuthCode]);
    }
    
    const char* _GetDisplayName_GoogleSignIn() {
        return _AllocReturnString_GoogleSignIn([GetGoogleSignInController() getDisplayName]);
    }
    
    void _SignIn_GoogleSignIn(const char* clientId) {
        NSString* clientIdString = [NSString stringWithUTF8String:clientId];
        [GetGoogleSignInController() signIn:clientIdString];
    }
    
    void _SignOut_GoogleSignIn() {
       [GetGoogleSignInController() signOut];
    }
    
    void _SetDebugMode_GoogleSignIn(bool enable) {
        [GetGoogleSignInController() setDebugMode:enable];
    }
}
