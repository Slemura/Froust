using System;
using System.Collections.Generic;
using Froust.Runtime.PersistentData;
using Froust.Runtime.Screens;
using Froust.UI.Handlers;
using RpDev.Services.GenericFactories.VContainer;
using RpDev.Services.UI.Mediators;
using VContainer;
using VContainer.Unity;

namespace Froust.EntryPoint
{
    public class MainMenuScreenMediator : UIMediatorBase<MainMenuScreen>, IInitializable, IDisposable
    {
        private readonly Stack<IDisposable> _disposables = new();
        
        private IPlainClassFactory _plainClassFactory;
        private MusicButtonHandler _musicButtonHandler;
        private SoundButtonHandler _soundButtonHandler;
        
        private UserDataHandler _userData;

        [Inject]
        private void AddDependencies(UserDataHandler userData, IPlainClassFactory plainClassFactory)
        {
            _userData = userData;
            _plainClassFactory = plainClassFactory;
        }
        
        public void Initialize()
        {
            _musicButtonHandler = _plainClassFactory.Create<MusicButtonHandler>();
            _musicButtonHandler.AddMusicButtonView(View.MusicButton);
            
            _soundButtonHandler = _plainClassFactory.Create<SoundButtonHandler>();
            _soundButtonHandler.AddSoundButtonView(View.SoundButton);
            
            _disposables.Push(_musicButtonHandler);
            _disposables.Push(_soundButtonHandler);
            
            View.SetupInfo(_userData.LevelEnemyDefeatedScore);
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
