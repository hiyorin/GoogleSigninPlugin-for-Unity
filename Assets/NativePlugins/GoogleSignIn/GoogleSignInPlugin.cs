using UnityEngine;
using System.Collections;

/// <summary>
/// Google Sign-In plugin
/// </summary>
public class GoogleSignInPlugin : SingletonMonoBehaviour<GoogleSignInPlugin>
{
    /// <summary>
    /// Interface per platform.
    /// </summary>
    public abstract class Interface : MonoBehaviour
    {
        /// <summary>
        /// EMail.
        /// </summary>
        public abstract string Email { get; }

        /// <summary>
        /// ID.
        /// </summary>
        public abstract string Id { get; }

        /// <summary>
        /// ID Token.
        /// </summary>
        public abstract string IdToken { get; }

        /// <summary>
        /// Server Auth Code.
        /// </summary>
        public abstract string ServerAuthCode { get; }

        /// <summary>
        /// Display Name.
        /// </summary>
        public abstract string DisplayName { get; }

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
        public abstract void SignIn(string clientId, string clientSecret, bool requestEmail, bool requestId, bool requestIdToken, bool requestServerAuthCode, bool requestProfile);

        /// <summary>
        /// Sign-out
        /// </summary>
		public abstract void SignOut();

		/// <summary>
        /// Debug mode setting.
		/// </summary>
		public abstract void SetDebugMode(bool isEnable);

        /// <summary>
        /// Androids the activity result.
        /// </summary>
        /// <param name="requestCode">Request code.</param>
        /// <param name="resultCode">Result code.</param>
        /// <param name="data">Data.</param>
        public abstract void AndroidActivityResult(int requestCode, int resultCode, AndroidJavaObject data);
    }

    private Interface _interface = null;

    public bool IsConnecting { private set; get; }
    public bool IsSignIn { get { return string.IsNullOrEmpty(Error); } }
    public string Error { private set; get; }

    /// <summary>
    /// EMail.
    /// You can get it if requested at sign in.
    /// </summary>
    public string Email { get { return _interface.Email; } }

    /// <summary>
    /// ID.
    /// You can get it if requested at sign in.
    /// </summary>
    public string Id { get { return _interface.Id; } }

    /// <summary>
    /// ID Token.
    /// You can get it if requested at sign in.
    /// </summary>
    public string IdToken { get { return _interface.IdToken; } }

    /// <summary>
    /// Server Auth Code.
    /// You can get it if requested at sign in.
    /// </summary>
    public string ServerAuthCode { get { return _interface.ServerAuthCode; } }

    /// <summary>
    /// Display Name.
    /// You can get it if requested at sign in.
    /// </summary>
    public string DisplayName { get { return _interface.DisplayName; } }

    /// <summary>
    /// Called at initialization
    /// </summary>
    protected override void OnInitialize()
    {
        base.OnInitialize();
        _interface =
#if UNITY_EDITOR
            gameObject.AddComponent<GoogleSignInPluginForEditor>();
#elif UNITY_ANDROID
            gameObject.AddComponent<GoogleSignInPluginForAndroid>();
#elif UNITY_IOS
            gameObject.AddComponent<GoogleSignInPluginForIOS>();
#endif
    }

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
    /// <returns>CoroutineEnumerator</returns>
    public IEnumerator SignIn(string clientId, string clientSecret, bool requestEmail, bool requestId, bool requestIdToken, bool requestServerAuthCode, bool requestProfile)
    {
        _interface.SignIn(clientId, clientSecret, requestEmail, requestId, requestIdToken, requestServerAuthCode, requestProfile);
        IsConnecting = true;
        yield return new WaitUntil(() => !IsConnecting);
    }

    /// <summary>
    /// Sign-out
    /// </summary>
    public void SignOut()
    {
        _interface.SignOut();
    }

    /// <summary>
    /// Debug mode setting.
    /// </summary>
    /// <param name="isEnable"></param>
    public void SetDebugMode(bool isEnable)
    {
	    _interface.SetDebugMode (isEnable);
    }

    /// <summary>
    /// Androids the activity result.
    /// </summary>
    /// <param name="requestCode">Request code.</param>
    /// <param name="resultCode">Result code.</param>
    /// <param name="data">Data.</param>
    public void AndroidActivityResult(int requestCode, int resultCode, AndroidJavaObject data)
    {
        _interface.AndroidActivityResult(requestCode, resultCode, data);
    }

    /// <summary>
    /// Received UnitySendMessage
    /// SignInが成功したときに呼ばれます
    /// </summary>
    private void OnSignInSuccessed()
    {
        IsConnecting = false;
        Error = null;
    }

    /// <summary>
    /// Received UnitySendMessage
    /// SignInが失敗したときに呼ばれます
    /// </summary>
    /// <param name="message"></param>
    private void OnSignInFailed(string message)
    {
        IsConnecting = false;
        Error = message;
    }

    /// <summary>
    /// Received UnitySendMessage
    /// SignInがユーザーによりに呼ばれます	
    /// </summary>
    private void OnSignInUserCancel()
    {
    	IsConnecting = false;
    	Error = "User Cancel";
    }
}
