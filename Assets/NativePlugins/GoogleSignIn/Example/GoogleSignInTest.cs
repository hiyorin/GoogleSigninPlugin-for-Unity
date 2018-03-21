using UnityEngine;

namespace GoogleSignIn
{
    public class GoogleSignInTest : MonoBehaviour
    {
        [SerializeField]
        private string _clientId = string.Empty;
        [SerializeField]
        private string _clientSecret = string.Empty;
        [SerializeField]
        private bool _requestEmail = false;
        [SerializeField]
        private bool _requestId = false;
        [SerializeField]
        private bool _requestIdToken = false;
        [SerializeField]
        private bool _requestServerAuthCode = false;
        [SerializeField]
        private bool _requestProfile = false;

        private void Start()
        {
            GoogleSignInPlugin.Instance.SetDebugMode(true);
        }

        private void OnGUI()
        {
            Rect rect = new Rect(0.0f, 0.0f, Screen.width, Screen.height / 12.0f);
            GUI.TextArea(rect, "Email : " + GoogleSignInPlugin.Instance.Email);
            rect.y += rect.height;
            GUI.TextArea(rect, "Id : " + GoogleSignInPlugin.Instance.Id);
            rect.y += rect.height;
            GUI.TextArea(rect, "IdToken : " + GoogleSignInPlugin.Instance.IdToken);
            rect.y += rect.height;
            GUI.TextArea(rect, "ServerAuthCode : " + GoogleSignInPlugin.Instance.ServerAuthCode);
            rect.y += rect.height;
            GUI.TextArea(rect, "DisplayName : " + GoogleSignInPlugin.Instance.DisplayName);
            rect.y += rect.height;
            if (GUI.Button(rect, "SignIn"))
            {
                StartCoroutine(GoogleSignInPlugin.Instance.SignIn(_clientId, _clientSecret, _requestEmail, _requestId, _requestIdToken, _requestServerAuthCode, _requestProfile));
            }
            rect.y += rect.height;
            if (GUI.Button(rect, "SignOut"))
            {
                GoogleSignInPlugin.Instance.SignOut();
            }   
        }
    }
}
