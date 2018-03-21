using UnityEngine;
using System;
using System.Collections;

namespace GoogleSignIn.Utility
{
    public static class ResourcesUtility
    {
        public static IEnumerator LoadAsync<T>(string path, Action<T> onResult)
            where T:UnityEngine.Object
        {
            ResourceRequest request = Resources.LoadAsync<T>(path);
            yield return request;
            SystemUtility.SafeCall(onResult, request.asset as T);
        }
    }
}
