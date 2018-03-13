//
//  GoogleSignInController.h
//  Unity-iPhone
//
//  Created by MacPro2017 on 2017/05/22.
//
//

#import <Foundation/Foundation.h>

@interface GoogleSignInController : NSObject

- (id)initWithClientId:(NSString*)clientId;
- (NSString*)getEmail;
- (NSString*)getId;
- (NSString*)getIdToken;
- (NSString*)getServerAuthCode;
- (NSString*)getDisplayName;
- (void)signIn:(NSString*)clientId;
- (void)signOut;
- (void)setDebugMode:(bool)enable;

@end
