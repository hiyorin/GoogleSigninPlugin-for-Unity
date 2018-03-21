using UnityEngine;
using System;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GoogleSignIn.Utility
{
    /// <summary>
    /// ScriptableObject setting
    /// </summary>
    public static class SettingsUtility
    {
    	/// <summary>
    	/// Load ScriptableObject
    	/// </summary>
    	public static T Load<T>() where T:ScriptableObject
    	{
    		return Resources.Load<T>(GetFileName<T>());
    	}

    	/// <summary>
    	/// Aync load ScriptableObject
    	/// </summary>
    	public static IEnumerator LoadAsync<T>(Action<T> onResult) where T:ScriptableObject
    	{
    		yield return ResourcesUtility.LoadAsync<T>(GetFileName<T>(), onResult);
    	}

    	private static string GetFileName<T>()
    	{
    		return typeof(T).Name;
    	}

#if UNITY_EDITOR
    	public static void CreateSettings<T>() where T:ScriptableObject
    	{
            string assets = "Assets";
            string resources = "Resources";
            string resourcesPath = string.Format("{0}/{1}", assets, resources);
            string savePath = string.Format("{0}/{1}.asset", resourcesPath, GetFileName<T>());

            if (!AssetDatabase.IsValidFolder(resourcesPath))
            {
                AssetDatabase.CreateFolder(assets, resources);
            }

    		T settings = AssetDatabase.LoadAssetAtPath<T>(savePath);
    		if (settings == null)
    		{
    			settings = ScriptableObject.CreateInstance<T>();
    			AssetDatabase.CreateAsset(settings, savePath);
    			AssetDatabase.Refresh();
    		}
    		Selection.activeObject = settings;
    	}
#endif
    }
}