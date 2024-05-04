using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Froust.Runtime.UI
{
	public class SnowmanUI : MonoBehaviour
	{
		[SerializeField] private float _minInterval = 2f;
		[SerializeField] private float _maxInterval = 8f;

		private const float MinPunch = 0.0f;
		private const float MaxPunch = 0.1f;

		private static Vector3 MinPunchScale => new Vector3(MinPunch, MinPunch, MinPunch);
		private static Vector3 MaxPunchScale => new Vector3(MaxPunch, MaxPunch, MaxPunch);

		private void Start ()
		{
			transform.DORotate(new Vector3(0, 0, -3), 1f)
			         .SetRelative(true)
			         .SetEase(Ease.InOutSine)
			         .SetLoops(-1, LoopType.Yoyo)
			         .SetLink(gameObject, LinkBehaviour.KillOnDestroy);


			StartCoroutine(AnimateWithRandomInterval());
		}

		private IEnumerator AnimateWithRandomInterval ()
		{
			while (true)
			{
				float randomInterval = Random.Range(_minInterval, _maxInterval);

				yield return new WaitForSeconds(randomInterval);

				transform.DOPunchScale(GetRandomPunchScale(), 1.5f, 3)
				         .SetRelative(true)
				         .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
			}
			// ReSharper disable once IteratorNeverReturns
		}

		private Vector3 GetRandomPunchScale ()
		{
			float sign = Mathf.Sign(transform.localScale.x);

			float x = Random.Range(MinPunchScale.x, MaxPunchScale.x) * sign;
			float y = Random.Range(MinPunchScale.y, MaxPunchScale.y);
			float z = Random.Range(MinPunchScale.z, MaxPunchScale.z);

			return new Vector3(x, y, z);
		}
	}
}
