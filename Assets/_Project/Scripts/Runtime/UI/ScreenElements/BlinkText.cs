using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Froust.Runtime.UI
{
    public class BlinkText : MonoBehaviour
    {
        #region Set in Inspector

        [SerializeField, Range(0, 1)] private float from;
        [SerializeField, Range(0, 1)] private float to = 1;
        [SerializeField, Min(0)] private float speed = 1;

        #endregion Set in Inspector

        private TMP_Text _graphics;

        private void Awake()
        {
            _graphics = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            _graphics.DOFade(to, speed)
                .From(from)
                .SetSpeedBased()
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }
    }
}