using System.Threading;
using Cysharp.Threading.Tasks;
using RpDev.Extensions.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace RpDev.Services.UI
{
	[RequireComponent(typeof(Canvas))]
	[RequireComponent(typeof(CanvasScaler))]
	[RequireComponent(typeof(GraphicRaycaster))]
	[RequireComponent(typeof(CanvasGroup))]
	public abstract class UIScreen : MonoBehaviour
	{
		[SerializeField, Min(0)] protected float _fadeDuration = 0.25f;

		private CanvasGroup _canvasGroup;

		protected virtual void Awake ()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
		}

		public void FadeIn ()
		{
			_canvasGroup.FadeIn(_fadeDuration);
		}

		public void FadeOut ()
		{
			_canvasGroup.FadeOut(_fadeDuration);
		}

		public async UniTask FadeInAsync (CancellationToken cancellationToken = default)
		{
			await _canvasGroup.FadeInAsync(_fadeDuration)
			                  .AttachExternalCancellation(cancellationToken);
		}

		public virtual async UniTask FadeOutAsync (CancellationToken cancellationToken = default)
		{
			await _canvasGroup.FadeOutAsync(_fadeDuration)
			                  .AttachExternalCancellation(cancellationToken);
		}
	}
}
