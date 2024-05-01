using UnityEngine;

namespace RpDev.Utilities
{
    public static class AndroidAppMinimizer
    {
        public static void Minimize()
        {
            if (Application.platform != RuntimePlatform.Android) return;

            var player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var activity = player.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call<bool>("moveTaskToBack", true);
        }
    }
}