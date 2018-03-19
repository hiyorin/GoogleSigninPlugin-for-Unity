using UnityEngine;
using UnityEditor;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif
using UnityEditor.Callbacks;
using System;
using System.IO;
using Google;

public class GoogleSignInPostProcessBuild
{
	[PostProcessBuild]
	private static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
	{
		if (buildTarget != BuildTarget.iOS)
		{
			return;
		}

		GoogleSettings settings = SettingsUtility.Load<GoogleSettings>();

        UpdateCocoaPods();
        UpdatePlist(buildPath, settings);
	}

    /// <summary>
    /// CocoaPodsを更新する
    /// Google/SignInをCocoaPodsに追加する
    /// </summary>
    private static void UpdateCocoaPods()
    {
        Type iosResolver = VersionHandler.FindClass(
            "Google.IOSResolver", "Google.IOSResolver");
        if (iosResolver == null)
        {
            return;
        }

        VersionHandler.InvokeStaticMethod(
            iosResolver, "AddPod",
            new object[] { "Google/SignIn" },
            namedArgs: null//new Dictionary<string, object> (){}
        );
    }

    /// <summary>
    /// plistの更新をする
    /// </summary>
    /// <param name="buildPath"></param>
    /// <param name="settings"></param>
    private static void UpdatePlist(string buildPath, GoogleSettings settings)
    {
#if UNITY_IOS
        PlistDocument plist = new PlistDocument();
        string filePath = Path.Combine(buildPath, "Info.plist");
        plist.ReadFromFile(filePath);

        PlistElement bundleUrlTypes = null;
        if (!plist.root.values.TryGetValue("CFBundleURLTypes", out bundleUrlTypes))
        {
            bundleUrlTypes = plist.root.CreateArray("CFBundleURLTypes");
        }

        // set url scheme
        PlistElementDict urlSchemeDict = bundleUrlTypes.AsArray().AddDict();
        urlSchemeDict.SetString("CFBundleTypeRole", "Editor");
        urlSchemeDict.CreateArray("CFBundleURLSchemes").AddString(settings.IOS.URLScheme);

        // set bundle id
        PlistElementDict bundleIdDict = bundleUrlTypes.AsArray().AddDict();
        bundleIdDict.SetString("CFBundleTypeRole", "Editor");
#if UNITY_5_6_OR_NEWER
        bundleIdDict.CreateArray("CFBundleURLSchemes").AddString(Application.identifier);
#else
        bundleIdDict.CreateArray("CFBundleURLSchemes").AddString(Application.bundleIdentifier);
#endif
		plist.WriteToFile(filePath);
#endif
    }
}
