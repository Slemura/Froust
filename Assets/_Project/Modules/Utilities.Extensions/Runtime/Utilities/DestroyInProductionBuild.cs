using UnityEngine;

namespace RpDev.Utilities
{
    public class DestroyInProductionBuild : MonoBehaviour
    {
        private void Start()
        {
            if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
                return;

            if (Debug.isDebugBuild == false)
                Destroy(gameObject);
        }
    }
}