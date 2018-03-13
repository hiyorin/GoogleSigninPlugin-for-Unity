#if UNITY_ANDROID
using UnityEngine;

/// <summary>
/// Androidの Google Sign-In 関連プラグイン
/// </summary>
public class GoogleSignInPluginForAndroid : GoogleSignInPlugin.Interface
{
    private const string ClassName = "com.hiyorin.googlesignin.GoogleSignInPlugin";

    private void Start()
    {
        using (AndroidJavaClass plugin = new AndroidJavaClass(ClassName))
        {
            plugin.CallStatic("initialize");
        }
    }

    private void OnDestroy()
    {
        using (AndroidJavaClass plugin = new AndroidJavaClass(ClassName))
        {
            plugin.CallStatic("dispose");
        }
    }

    /// <summary>
    /// EMail（リクエストすると取得できる）
    /// </summary>
    public override string Email
    {
        get
        {
            using (AndroidJavaClass plugin = new AndroidJavaClass(ClassName))
            {
                return plugin.CallStatic<string>("getEmail");
            }
        }
    }

    /// <summary>
    /// ID（リクエストすると取得できる）
    /// </summary>
    public override string Id
    {
        get
        {
            using (AndroidJavaClass plugin = new AndroidJavaClass(ClassName))
            {
                return plugin.CallStatic<string>("getId");
            }
        }
    }

    /// <summary>
    /// ID Token（リクエストすると取得できる）
    /// </summary>
    public override string IdToken
    {
        get
        {
            using (AndroidJavaClass plugin = new AndroidJavaClass(ClassName))
            {
                return plugin.CallStatic<string>("getIdToken");
            }
        }
    }

    /// <summary>
    /// Server Auth Code（リクエストすると取得できる）
    /// </summary>
    public override string ServerAuthCode
    {
        get
        {
            using (AndroidJavaClass plugin = new AndroidJavaClass(ClassName))
            {
                return plugin.CallStatic<string>("getServerAuthCode");
            }
        }
    }

    /// <summary>
    /// Display Name（リクエストすると取得できる）
    /// </summary>
    public override string DisplayName
    {
        get
        {
            using (AndroidJavaClass plugin = new AndroidJavaClass(ClassName))
            {
                return plugin.CallStatic<string>("getDisplayName");
            }
        }
    }

    /// <summary>
    /// サインイン
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

        using (AndroidJavaClass plugin = new AndroidJavaClass(ClassName))
        {
            plugin.CallStatic("signIn", clientId, requestEmail, requestId, requestIdToken, requestServerAuthCode, requestProfile);
        }
    }

    /// <summary>
    /// サインアウト
    /// </summary>
    public override void SignOut()
    {
        using (AndroidJavaClass plugin = new AndroidJavaClass(ClassName))
        {
            plugin.CallStatic("signOut");
        }
    }

	/// <summary>
	/// デバッグモードの設定
	/// </summary>
	/// <param name="isEnable"></param>
	public override void SetDebugMode(bool isEnable)
	{
        using (AndroidJavaClass plugin = new AndroidJavaClass(ClassName))
        {
            plugin.CallStatic("setDebugMode", isEnable);
        }
    }

    /// <summary>
    /// Androids the activity result.
    /// </summary>
    /// <param name="requestCode">Request code.</param>
    /// <param name="resultCode">Result code.</param>
    /// <param name="data">Data.</param>
    public override void AndroidActivityResult(int requestCode, int resultCode, AndroidJavaObject data)
    {
        using (AndroidJavaClass plugin = new AndroidJavaClass(ClassName))
        {
            plugin.CallStatic("onActivityResult", requestCode, resultCode, data);
        }
    }
}
#endif