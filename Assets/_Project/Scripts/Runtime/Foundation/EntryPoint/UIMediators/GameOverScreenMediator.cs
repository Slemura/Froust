using System;
using System.Collections.Generic;
using Froust.Runtime.Screens;
using Froust.UI.Handlers;
using RpDev.Services.GenericFactories.VContainer;
using RpDev.Services.UI.Mediators;
using VContainer;
using VContainer.Unity;

namespace Froust.EntryPoint
{
    public class GameOverScreenMediator : UIMediatorBase<GameOverScreen>, IInitializable, IDisposable
    {
        private readonly Stack<IDisposable> _disposables = new();
        private IPlainClassFactory _plainClassFactory;
        private MusicButtonHandler _musicButtonHandler;
        private SoundButtonHandler _soundButtonHandler;
        
        [Inject]
        public void SetDependencies(IPlainClassFactory plainClassFactory)
        {
            _plainClassFactory = plainClassFactory;
        }
        
        public void Initialize()
        {
            _musicButtonHandler = _plainClassFactory.Create<MusicButtonHandler>();
            _musicButtonHandler.AddMusicButtonView(View.MusicButton);
            
            _soundButtonHandler = _plainClassFactory.Create<SoundButtonHandler>();
            _soundButtonHandler.AddSoundButtonView(View.SoundButton);
            
            _disposables.Push(_soundButtonHandler);
            _disposables.Push(_musicButtonHandler);
        }

        public void Dispose()
        {
            while (_disposables.Count > 0)
            {
                _disposables.Pop().Dispose();
            }
        }
    }
}
