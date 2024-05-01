using DG.Tweening;
using UnityEngine;

namespace Froust.Runtime.UI
{
    public class ScaleAnimationUI : MonoBehaviour
    {
        [SerializeField] private float _scale = 1.1f;
        [SerializeField] private float _speed = 1;

        private void Start()
        {
            transform.DOScale(Vector3.one * _scale, _speed)
                .SetEase(Ease.InOutSine)
                .SetSpeedBased(true)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }
    }
}