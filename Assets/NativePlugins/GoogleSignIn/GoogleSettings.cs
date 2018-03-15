using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Google settings.
/// </summary>
public class GoogleSettings : ScriptableObject
{
    [Serializable]
    public class Platform
    {
        [SerializeField]
        private string _clientId = string.Empty;
        [SerializeField]
        private string _clientSecret = string.Empty;
        [SerializeField]
        private string _urlScheme = string.Empty;

        public string ClientId { get { return _clientId; } }

        public string ClientSecret { get { return _clientSecret; } }

        public string URLScheme { get { return _urlScheme; } }
    }

#pragma warning disable 414
    [SerializeField]
    private Platform _android = null;
    [SerializeField]
    private Platform _ios = null;
    [SerializeField]
    private Platform _editor = null;
#pragma warning restore 414

    /// <summary>
    /// Platform settings.
    /// </summary>
    public Platform Target
    {
        get
        {
#if UNITY_EDITOR
            return _editor;
#elif UNITY_ANDROID
            return _android;
#elif UNITY_IOS
            return _ios;
#else
            Debug.LogErrorFormat("This platform is not supported.");
            return null;
#endif
        }
    }
    
    /// <summary>
    /// Android
    /// </summary>
    public Platform Android { get { return _android; } }
    
    /// <summary>
    /// iOS
    /// </summary>
    public Platform IOS { get { return _ios; } }
    
    /// <summary>
    /// Editor
    /// </summary>
    public Platform Editor { get { return _editor; } }
    
    #if UNITY_EDITOR
    [MenuItem("Settings/Google")]
    private static void OnCreateSettings()
    {
        SettingsUtility.CreateSettings<GoogleSettings>();
    }
#endif
}
