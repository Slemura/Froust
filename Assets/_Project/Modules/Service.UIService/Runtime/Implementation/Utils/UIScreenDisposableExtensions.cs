using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace RpDev.Services.UI.Utils
{
    public static class UIScreenDisposableExtensions
    {
        public static T LinkDestroyTo<T>(this T self, ICollection<IDisposable> disposables, IUIService iuiService)
            where T : UIScreen
        {
            disposables.Add(new UIScreenDestroyDisposable<T>(self, iuiService));
            return self;
        }

        private sealed class UIScreenDestroyDisposable<T> : IDisposable where T : UIScreen
        {
            private readonly T _uiScreen;
            private readonly IUIService _iuiService;

            public UIScreenDestroyDisposable(T uiScreen, IUIService iuiService)
            {
                _uiScreen = uiScreen;
                _iuiService = iuiService;
            }

            public void Dispose()
            {
                DisposeInternal().Forget();
            }

            private async UniTask DisposeInternal()
            {
                await _uiScreen.FadeOutAsync();
                _iuiService.DestroyScreen(_uiScreen);
            }
        }
    }
}