using System;
using System.Collections.Generic;
using Froust.Level.Model;
using Froust.Runtime.Screens;
using Froust.UI.Handlers;
using RpDev.Services.GenericFactories.VContainer;
using RpDev.Services.UI.Mediators;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Froust.EntryPoint.UIMediators
{
    public class GamePlayScreenMediator : UIMediatorBase<GamePlayScreen>, IDisposable, IInitializable
    {
        private readonly Stack<IDisposable> _disposables = new();

        private IPlainClassFactory _plainClassFactory;
        private MusicButtonHandler _musicButtonHandler;
        private SoundButtonHandler _soundButtonHandler;
        
        private float _gameplayTime;
        
        [Inject]
        private void AddDependencies(IPlainClassFactory plainClassFactory)
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

        public void AddGameplayModel(IReadonlyGameplayModel gameplayModel)
        {
            _disposables.Push(gameplayModel.IceDriftLevelReadOnly.Subscribe(UpdateSinkLevel));
            _disposables.Push(gameplayModel.StartLevelObservable.Subscribe(_ => AddTimeTick()));
        }

        public void Dispose()
        {
            while (_disposables.Count > 0)
                _disposables.Pop().Dispose();
        }

        private void AddTimeTick()
        {
            _disposables.Push(Observable.EveryUpdate().Subscribe(Tick));
        }

        private void UpdateSinkLevel(float sinkLevelNormalize)
        {
            View.WarningOverlay.UpdateWarningLevel(sinkLevelNormalize);
        }

        private void Tick(long _)
        {
            _gameplayTime += Time.deltaTime;
            View.LevelTimeCounterView.UpdateTimeInLevel(Mathf.RoundToInt(_gameplayTime));
        }
    }
}