using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RpDev.Services.UI;
using UnityEngine;

namespace Froust.Runtime.Screens {
    
    public class LoadingScreen : UIScreen {
        
        [SerializeField] private float _minimumDisplayTime;

        private float _displayTime;

        private void Update()
        {
            _displayTime += Time.deltaTime;
        }

        public override async UniTask FadeOutAsync(CancellationToken cancellationToken = default)
        {
            if(_displayTime < _minimumDisplayTime) {
                await UniTask.Delay(TimeSpan.FromSeconds(_minimumDisplayTime - _displayTime), cancellationToken: cancellationToken)
                             .ContinueWith(() => base.FadeOutAsync(cancellationToken));
            }

            await base.FadeOutAsync(cancellationToken);
        }
    }
}