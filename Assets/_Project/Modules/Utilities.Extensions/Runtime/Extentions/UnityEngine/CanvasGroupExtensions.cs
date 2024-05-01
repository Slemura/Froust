using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace RpDev.Extensions.Unity
{
    public static class CanvasGroupExtensions
    {
        public static CanvasGroup FadeIn(this CanvasGroup self, float duration)
        {
            self.DOKill();

            self.DOFade(1, duration)
                .From(0)
                .SetLink(self.gameObject, LinkBehaviour.KillOnDestroy);

            return self;
        }

        public static UniTask FadeInAsync(this CanvasGroup self, float duration)
        {
            if (self != null)
            {
                self.DOKill();

                return self.DOFade(1, duration)
                    .From(0)
                    .SetLink(self.gameObject, LinkBehaviour.KillOnDestroy)
                    .WithCancellation(self.GetCancellationTokenOnDestroy());
            }

            return default;
        }

        public static CanvasGroup FadeOut(this CanvasGroup self, float duration)
        {
            self.DOKill();

            self.DOFade(0, duration)
                .SetLink(self.gameObject, LinkBehaviour.KillOnDestroy);

            return self;
        }

        public static UniTask FadeOutAsync(this CanvasGroup self, float duration)
        {
            self.DOKill();

            return self.DOFade(0, duration)
                .SetLink(self.gameObject, LinkBehaviour.KillOnDestroy)
                .WithCancellation(self.GetCancellationTokenOnDestroy());
        }
    }
}