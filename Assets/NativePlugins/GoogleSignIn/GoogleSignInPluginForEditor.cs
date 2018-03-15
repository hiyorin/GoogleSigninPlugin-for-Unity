#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

/// <summary>
/// Google Sign-In plugin for UnityEditor
/// </summary>
public class GoogleSignInPluginForEditor : GoogleSignInPlugin.Interface
{
    /// OAuth2 URL.
    private const string AuthUrl = "https://accounts.google.com/o/oauth2/v2/auth";
    /// OAuth2 get token URL.
    private const string AuthTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
    /// OAuth2 profile.
    private const string AuthProfileUrl = "https://www.googleapis.com/oauth2/v1/userinfo";
    /// OAuth2 scope.
    private const string AuthScope = "openid email profile";
    /// OAuth redirect_uri for Desktop fixed value.
    private const string AuthRedirectUri = "urn:ietf:wg:oauth:2.0:oob";

    /// <summary>
    /// EMail.
    /// </summary>
    public override string Email { get { return string.Empty; } }

    /// <summary>
    /// ID.
    /// </summary>
    public override string Id { get { return string.Empty; } }

    /// <summary>
    /// ID Token.
    /// </summary>
    public override string IdToken { get { return _idToken; } }

    /// <summary>
    /// Server Auth Code.
    /// </summary>
    public override string ServerAuthCode { get { return string.Empty; } }

    /// <summary>
    /// Display Name.
    /// </summary>
    public override string DisplayName { get { return _displayName; } }

    private string _clientId = string.Empty;
    private string _clientSecret = string.Empty;
    private bool _isAuthing = false;
    private string _authorizationCode = string.Empty;
    private string _idToken = string.Empty;
    private string _displayName = string.Empty;

    private void OnGUI()
    {
        if (!_isAuthing) return;
        GUI.depth = 0;
        Rect rect = new Rect(0.0f, 0.0f, Screen.width, Screen.height / 12.0f);
        _authorizationCode = GUI.TextField(rect, _authorizationCode);
        rect.y += rect.height;
        if (GUI.Button(rect, "Execute"))
        {
            StartCoroutine(GetTokenWithProfile(_clientId, _clientSecret, _authorizationCode));
            _isAuthing = false;
        }
    }

    private IEnumerator GetTokenWithProfile(string clientId, string clientSecret, string authorizationCode)
    {
        string accessToken = string.Empty;

        // get token
        WWWForm tokenData = GeneratePostData(clientId, clientSecret, authorizationCode);
        using (UnityWebRequest request = UnityWebRequest.Post(AuthTokenUrl, tokenData))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return Send(request);
            if (IsError(request))
            {
                SendMessage("OnSignInFailed", request.error);
                yield break;
            }

            Dictionary<string, object> jsonDic = Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;
            _idToken = jsonDic["id_token"] as string;
            accessToken = jsonDic["access_token"] as string;
        }
        
        // get profile
        string profileData = GenerateGetData(accessToken);
        using (UnityWebRequest request = UnityWebRequest.Get(AuthProfileUrl + profileData))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return Send(request);
            if (IsError(request))
            {
                SendMessage("OnSignInResult", request.error);
                yield break;
            }
            Debug.Log(request.downloadHandler.text);
            Dictionary<string, object> jsonDic = Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;
            _displayName = jsonDic["name"] as string;
        }

        SendMessage("OnSignInSuccessed");
    }
    

    private WWWForm GeneratePostData(string clientId, string clientSecret, string authorizationCode)
    {
        WWWForm form = new WWWForm();
        form.AddField("code", authorizationCode);
        form.AddField("client_id", clientId);
        form.AddField("client_secret", clientSecret);
        form.AddField("redirect_uri", AuthRedirectUri);
        form.AddField("grant_type", "authorization_code");
        form.AddField("access_type", "offline");
        return form;
    }

    private string GenerateGetData(string accessToken)
    {
        return string.Format("?access_token={0}", accessToken);
    }

    private IEnumerator Send(UnityWebRequest request)
    {
#if UNITY_2017
        yield return request.SendWebRequest();
#else
        yield return request.Send();
#endif
    }

    private bool IsError(UnityWebRequest request)
    {
#if UNITY_2017
        return request.isNetworkError;
#else
        return request.isError;
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
    public override void SignIn(string clientId, string clientSecret, bool requestEmail, bool requestId, bool requestIdToken, bool requestServerAuthCode, bool requestProfile)
    {
        string url = string.Format("{0}?response_type=code&client_id={1}&scope={2}&redirect_uri={3}&access_type=offline",
            AuthUrl,
            clientId,
            WWW.EscapeURL(AuthScope),
            AuthRedirectUri);
        Application.OpenURL(url);
        _clientId = clientId;
        _clientSecret = clientSecret;
        _isAuthing = true;
    }

    /// <summary>
    /// Sign-out
    /// </summary>
    public override void SignOut() { }

	/// <summary>
    /// Debug mode setting.
	/// </summary>
	/// <param name="isEnable"></param>
	public override void SetDebugMode(bool isEnable)
	{
		
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