using UnityEngine;

namespace Common.PluginWrappers {

    public class Toaster {
        public static Toaster instance = new Toaster();
        private static AndroidJavaObject currentActivity;
        private static AndroidJavaObject context;
        private static AndroidJavaObject toast;
        public static void Initialize() {
#if UNITY_ANDROID&&!UNITY_EDITOR
            AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
#endif
        }

        public static void ShowToast(string message) {
#if UNITY_ANDROID&&!UNITY_EDITOR
            currentActivity.Call
            (
                "runOnUiThread",
                new AndroidJavaRunnable(() => {
                    AndroidJavaClass Toast
                        = new AndroidJavaClass("android.widget.Toast");
                    AndroidJavaObject javaString
                        = new AndroidJavaObject("java.lang.String", message);
                    toast = Toast.CallStatic<AndroidJavaObject>
                    (
                        "makeText",
                        context,
                        javaString,
                        Toast.GetStatic<int>("LENGTH_SHORT")
                    );
                    toast.Call("show");
                })
            );
#endif
        }

        public static void CancelToast() {
#if UNITY_ANDROID&&!UNITY_EDITOR
            currentActivity.Call("runOnUiThread",
                new AndroidJavaRunnable(() => {
                    if (toast != null) toast.Call("cancel");
                }));
#endif
        }
    }

}