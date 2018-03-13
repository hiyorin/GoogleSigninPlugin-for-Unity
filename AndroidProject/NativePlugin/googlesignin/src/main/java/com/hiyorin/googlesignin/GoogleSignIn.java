package com.hiyorin.googlesignin;

import android.app.Activity;
import android.app.Application;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;

import com.google.android.gms.auth.api.Auth;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.auth.api.signin.GoogleSignInResult;
import com.google.android.gms.auth.api.signin.GoogleSignInStatusCodes;
import com.google.android.gms.common.api.GoogleApiClient;
import com.unity3d.player.UnityPlayer;

public class GoogleSignIn implements Application.ActivityLifecycleCallbacks {

    private static final int RC_SIGN_IN = 9001;

    private GoogleApiClient client = null;
    private GoogleSignInAccount accont = null;
    private  boolean isDebugMode = false;

    public String getEmail() { return accont != null ? accont.getEmail() : ""; }
    public String getId() { return accont != null ? accont.getId() : ""; }
    public String getIdToken() { return accont != null ? accont.getIdToken() : ""; }
    public String getServerAuthCode() { return accont != null ? accont.getServerAuthCode() : ""; }
    public String getDisplayName() { return accont != null ? accont.getDisplayName() : ""; }

    public void initialize() {

        UnityPlayer.currentActivity.getApplication().registerActivityLifecycleCallbacks(this);
    }

    public void dispose() {
        UnityPlayer.currentActivity.getApplication().unregisterActivityLifecycleCallbacks(this);
    }

    public void signIn(String clientId,
                       boolean requestEmail,
                       boolean requestId,
                       boolean requestIdToken,
                       boolean requestServerAuthCode,
                       boolean requestProfile)
    {
        Log.d(GoogleSignInPlugin.TAG, String.format("signIn clientId:%s, requestEmail:%b, requestId:%b, requestIdToken:%b, requestServerAuthCode:%b, requestProfile:%b",
                clientId, requestEmail, requestId, requestIdToken, requestServerAuthCode, requestProfile));

        // intialize
        signOut();

        // sign in options
        GoogleSignInOptions.Builder builder = new GoogleSignInOptions.Builder(GoogleSignInOptions.DEFAULT_SIGN_IN);
        if (requestEmail) builder.requestEmail();
        if (requestId) builder.requestId();
        if (requestIdToken) builder.requestIdToken(clientId);
        if (requestServerAuthCode) builder.requestServerAuthCode(clientId);
        if (requestProfile) builder.requestProfile();
        GoogleSignInOptions options = builder.build();

        Activity currentActivity = UnityPlayer.currentActivity;
        client = new GoogleApiClient.Builder(currentActivity)
                .addApi(Auth.GOOGLE_SIGN_IN_API, options)
                .build();

        Intent signInIntent = Auth.GoogleSignInApi.getSignInIntent(client);
        currentActivity.startActivityForResult(signInIntent, RC_SIGN_IN);
    }

    public void signOut() {
        logDebug("signOut", "");
        if (client != null)
        {
            if (client.isConnected()) {
                Auth.GoogleSignInApi.signOut(client);
                client.disconnect();
            }
            client = null;
        }
    }

    public void setDebugMode(boolean isEnable)
    {
        logDebug("setDebugMode", String.format("isEnable:%b", isEnable));
        isDebugMode = isEnable;
    }

    private void onSignInResult(GoogleSignInResult result) {
        if (result.isSuccess()) {
            logDebug("onSignInResult", "successed");
            accont = result.getSignInAccount();
            client.connect();
            UnityPlayer.UnitySendMessage(GoogleSignInPlugin.TAG, GoogleSignInPlugin.SIGN_IN_SUCCESSED_CALLBACK, "");
        } else if (result.getStatus().getStatusCode() == GoogleSignInStatusCodes.SIGN_IN_CANCELLED) {
            logDebug("onSignInResult", "user cancel");
            UnityPlayer.UnitySendMessage(GoogleSignInPlugin.TAG, GoogleSignInPlugin.SIGN_IN_USER_CANCEL_CALLBACK, "");
        } else {
            logError("onSignInResult", String.format("failed %s", result.getStatus().toString()));
            UnityPlayer.UnitySendMessage(GoogleSignInPlugin.TAG, GoogleSignInPlugin.SIGN_IN_FAILED_CALLBACK, result.getStatus().toString());
        }
    }

    private void logDebug(String method, String message) {
        if (isDebugMode) {
            Log.d(GoogleSignInPlugin.TAG, String.format("%s %s", method, message));
        }
    }

    private void logError(String method, String message) {
        if (isDebugMode) {
            Log.e(GoogleSignInPlugin.TAG, String.format("%s %s", method, message));
        }
    }

    /**
     * Call Activity lifecycle onActivityResult
     * @param requestCode
     * @param resultCode
     * @param data
     */
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        logDebug("onActivityResult", String.format("requestCode:%d, resultCode:%d", requestCode, resultCode));
        if (requestCode == RC_SIGN_IN) {
            GoogleSignInResult result = Auth.GoogleSignInApi.getSignInResultFromIntent(data);
            onSignInResult(result);
        }
    }

    @Override
    public void onActivityCreated(Activity activity, Bundle savedInstanceState) {

    }

    @Override
    public void onActivityStarted(Activity activity) {
        logDebug("onActivityStarted", "");
        if (client != null) {
            client.connect();
        }
    }

    @Override
    public void onActivityResumed(Activity activity) {

    }

    @Override
    public void onActivityPaused(Activity activity) {

    }

    @Override
    public void onActivityStopped(Activity activity) {
        logDebug("onActivityStopped", "");
        if (client != null && client.isConnected()) {
            client.disconnect();
        }
    }

    @Override
    public void onActivitySaveInstanceState(Activity activity, Bundle outState) {

    }

    @Override
    public void onActivityDestroyed(Activity activity) {

    }
}
