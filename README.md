# GoogleSigninPlugin
A set of tools for Unity to allow handling Google Sign-in for Android and iOS.

# Install
GoogleSigninPlugin.unitypackage

# Settings
#### Create settings file
* Menu/Settings/Google

#### Android
* Setting ClientId and ClientSecret to GoogleSettings.asset

#### iOS
* Setting ClientId and UrlScheme to GoogleSettings.asset

#### Editor
* Setting ClientId and ClientSecret to GoogleSettings.asset

# Usage
#### Example: Sign-in
Call the "SignIn" method and check the items you want to get.
```cs
public void Example()
{
  StartCoroutine(GoogleSigninPlugin.Instance.SignIn(
    requestEmail:true,
    requestId:true,
    requestIdToken:true,
    requestServerAuthCode:true,
    requestProfile:true));
}
```
