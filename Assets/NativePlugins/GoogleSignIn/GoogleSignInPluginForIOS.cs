#if UNITY_IOS
using System.Runtime.InteropServices;

/// <summary>
/// Google Sign-In plugin for iOS
/// </summary>
public class GoogleSignInPluginForIOS : GoogleSignInPlugin.Interface
{
    [DllImport("__Internal")]
    private static extern string _GetEmail_GoogleSignIn();
    [DllImport("__Internal")]
    private static extern string _GetId_GoogleSignIn();
    [DllImport("__Internal")]
    private static extern string _GetIdToken_GoogleSignIn();
    [DllImport("__Internal")]
    private static extern string _GetServerAuthCode_GoogleSignIn();
    [DllImport("__Internal")]
    private static extern string _GetDisplayName_GoogleSignIn();
    [DllImport("__Internal")]
    private static extern void _SignIn_GoogleSignIn(string clientId);
    [DllImport("__Internal")]
    private static extern void _SignOut_GoogleSignIn();
    [DllImport("__Internal")]
    private static extern void _SetDebugMode_GoogleSignIn(bool isEnable);

    /// <summary>
    /// EMail.
    /// </summary>
    public override string Email { get { return _GetEmail_GoogleSignIn(); } }

    /// <summary>
    /// ID.
    /// </summary>
    public override string Id { get { return _GetId_GoogleSignIn(); } }

    /// <summary>
    /// ID Token.
    /// </summary>
    public override string IdToken { get { return _GetIdToken_GoogleSignIn(); } }

    /// <summary>
    /// Server Auth Code.
    /// </summary>
    public override string ServerAuthCode { get { return _GetServerAuthCode_GoogleSignIn(); } }

    /// <summary>
    /// Display Name.
    /// </summary>
    public override string DisplayName { get { return _GetDisplayName_GoogleSignIn(); } }

    /// <summary>
    /// Sign-in
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="clientSecret"></param>
    /// <param name="requestEmail"></param>
    /// <param name="requestId"></param>
    /// <param name="requestIdToken"></param>
    /// <param name="requestServerAuthCode"></param>
    /// <param name="requestProfile"></param>
    public override void SignIn(string clientId, string clientSecret, bool requestEmail, bool requestId, bool requestIdToken, bool requestServerAuthCode, bool requestProfile)
    {
    	_SignIn_GoogleSignIn(clientId);
    }

    /// <summary>
    /// Sign-out
    /// </summary>
    public override void SignOut()
    {
        _SignOut_GoogleSignIn();
    }

    /// <summary>
    /// Debug mode setting.
    /// </summary>
    /// <param name="isEnable"></param>
    public override void SetDebugMode(bool isEnable)
    {
        _SetDebugMode_GoogleSignIn(isEnable);
    }

    /// <summary>
    /// Androids the activity result.
    /// </summary>
    /// <param name="requestCode">Request code.</param>
    /// <param name="resultCode">Result code.</param>
    /// <param name="data">Data.</param>
    public override void AndroidActivityResult(int requestCode, int resultCode, AndroidJavaObject data)
    {
        
    }
}
#endif