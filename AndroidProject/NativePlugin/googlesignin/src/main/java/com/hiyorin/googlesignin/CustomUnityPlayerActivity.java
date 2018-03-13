package com.hiyorin.googlesignin;

import android.content.Intent;
import com.unity3d.player.UnityPlayerActivity;

public class CustomUnityPlayerActivity extends UnityPlayerActivity {

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        GoogleSignInPlugin.onActivityResult(requestCode, resultCode, data);
        super.onActivityResult(requestCode, resultCode, data);
    }
}
