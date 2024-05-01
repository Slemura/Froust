using UnityEngine;
using UnityEngine.UI;

namespace Froust.Audio
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public class UIButtonSound : UIBehaviourSound<Button>
    {
        protected override void AddListener()
        {
            Component.onClick.AddListener(PlaySfx);
        }
    }
}