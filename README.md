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
public IEnumerator Example()
{
  yield return GoogleSigninPlugin.Instance.SignIn(
    requestEmail:true,
    requestId:true,
    requestIdToken:true,
    requestServerAuthCode:true,
    requestProfile:true);
   
  Debug.Log(GoogleSigninPlugin.Instance.Email);
  Debug.Log(GoogleSigninPlugin.Instance.Id);
  Debug.Log(GoogleSigninPlugin.Instance.IdToken);
  Debug.Log(GoogleSigninPlugin.Instance.ServerAuthCode);
  Debug.Log(GoogleSigninPlugin.Instance.DisplayName);
}
```

#### Example: Sign-out
```cs
public void Example()
{
  GoogleSigninPlugin.Instance.SignOut();
}
```

# When using your own UnityPlayerActivity
Please pass the value of OnActivityResult of your Activity
```cs
public void Exapmle(int requestCode, int resultCode, AndroidJavaObject data)
{
  GoogleSigninPlugin.Instance.AndroidActivityResult(requestCode, resultCode, data);
}
```
