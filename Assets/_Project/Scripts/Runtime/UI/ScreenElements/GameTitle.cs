using DG.Tweening;
using UnityEngine;


namespace Froust.Runtime.Screens
{
	public class GameTitle : MonoBehaviour
	{
		private void Start ()
		{
			transform.DORotate(new Vector3(0, 0, -1), 1f)
			         .SetEase(Ease.InOutSine)
			         .SetLoops(-1, LoopType.Yoyo)
			         .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
		}
	}
}