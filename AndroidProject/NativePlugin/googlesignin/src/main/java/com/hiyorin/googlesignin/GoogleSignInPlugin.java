package com.hiyorin.googlesignin;

import android.content.Intent;

public class GoogleSignInPlugin {

    public static final String TAG = "GoogleSignInPlugin";
    public static final String SIGN_IN_SUCCESSED_CALLBACK = "OnSignInSuccessed";
    public static final String SIGN_IN_FAILED_CALLBACK = "OnSignInFailed";
    public static final String SIGN_IN_USER_CANCEL_CALLBACK = "OnSignInUserCancel";

    private static GoogleSignIn instance = null;
    private static GoogleSignIn Instance(){
        if (instance == null) instance = new GoogleSignIn();
        return instance;
    }

    public static void initialize() {
        Instance().initialize();
    }

    public static void dispose() {
        Instance().dispose();
    }

    public static void signIn(String clientId,
                              boolean requestEmail,
                              boolean requestId,
                              boolean requestIdToken,
                              boolean requestServerAuthCode,
                              boolean requestProfile) {
        Instance().signIn(clientId, requestEmail, requestId, requestIdToken, requestServerAuthCode, requestProfile);
    }

    public static void signOut() {
        Instance().signOut();
    }

    public static void setDebugMode(boolean isEnable) { Instance().setDebugMode(isEnable); }

    public static void onActivityResult(int requestCode, int resultCode, Intent data) {
        Instance().onActivityResult(requestCode, resultCode, data);
    }

    public static String getEmail() { return Instance().getEmail(); }
    public static String getId() { return Instance().getId(); }
    public static String getIdToken() { return Instance().getIdToken(); }
    public static String getServerAuthCode() { return Instance().getServerAuthCode(); }
    public static String getDisplayName() { return Instance().getDisplayName(); }
}
