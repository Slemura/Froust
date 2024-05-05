using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Froust.EntryPoint.UIMediators;
using Froust.Level;
using Froust.Level.Model;
using Froust.Runtime.PersistentData;
using Froust.Runtime.Screens;
using RpDev.Services.UI;
using RpDev.Services.UI.Mediators;
using RpDev.Services.AsyncStateMachine.Abstractions;
using RpDev.Services.GenericFactories.VContainer;
using UniRx;
using UnityEngine;

namespace Froust.EntryPoint.States
{
    public class GameplayState : StateBase
    {
        private readonly IUIService _iuiService;
        private readonly UserDataHandler _userDataHandler;
        private readonly IPlainClassFactory _plainClassFactory;
        private readonly IRootStatesHandler _rootStatesHandler;
        private readonly UIMediatorFactory _uiMediatorFactory;
        private readonly GameAudioHandler _gameAudioHandler;
        private readonly GameplayResourcesModel _gameplayResourcesModel;
        private readonly Stack<IDisposable> _disposables = new();

        private GameplayEcsStartup _ecsGameplay;
        private GamePlayScreen _gameplayScreen;
        private GamePlayScreenMediator _gameplayScreenMediator;

        public GameplayState(IUIService iuiService, 
            UserDataHandler userDataHandler,
            IPlainClassFactory plainClassFactory,
            IRootStatesHandler rootStatesHandler,
            UIMediatorFactory uiMediatorFactory,
            GameAudioHandler gameAudioHandler,
            GameplayResourcesModel gameplayResourcesModel)
        {
            _iuiService = iuiService;
            _userDataHandler = userDataHandler;
            _plainClassFactory = plainClassFactory;
            _rootStatesHandler = rootStatesHandler;
            _uiMediatorFactory = uiMediatorFactory;
            _gameAudioHandler = gameAudioHandler;
            _gameplayResourcesModel = gameplayResourcesModel;
        }

        public override async UniTask OnEnter(CancellationToken cancellationToken)
        {
            CreateEcsGameplay();
            await ShowGameplayScreen();
            LaunchEcsGameplay().Forget();
        }

        public override async UniTask OnExit(CancellationToken cancellationToken)
        {
            await _gameplayScreen.FadeOutAsync(cancellationToken);
        }

        public override void Dispose()
        {
            while (_disposables.Count > 0)
                _disposables.Pop()?.Dispose();
         
            _iuiService.DestroyScreen(_gameplayScreen);
            
            _gameplayScreenMediator = null;
            _ecsGameplay = null;
        }

        private async UniTask ShowGameplayScreen()
        {
            _gameplayScreen = await _iuiService.SpawnScreen<GamePlayScreen>();
            _gameplayScreenMediator = _uiMediatorFactory.Create<GamePlayScreenMediator, GamePlayScreen>(_gameplayScreen);
            _gameplayScreenMediator.Initialize();
            _gameplayScreenMediator.AddGameplayModel(_ecsGameplay.GetGameplayModel());
            
            _disposables.Push(_gameplayScreenMediator);
            
            await _gameplayScreen.FadeInAsync();
        }

        private void CreateEcsGameplay()
        {
            _ecsGameplay = new GameplayEcsStartup();
            _ecsGameplay.AddResourcesModel(_gameplayResourcesModel);
            _ecsGameplay.AddAudioHandler(_gameAudioHandler);
            _ecsGameplay.Init();
            
            _disposables.Push(_ecsGameplay);
        }

        private async UniTask LaunchEcsGameplay()
        {
            _disposables.Push(Observable.EveryUpdate().Subscribe(_ => Tick()));
            _disposables.Push(Observable.EveryFixedUpdate().Subscribe(_ => FixedTick()));

            var gameplayResult = await _ecsGameplay.Start();
            var secondsInGameplay = (int)gameplayResult.GameplayTime;

            _userDataHandler.TrySetBestScore(secondsInGameplay, gameplayResult.DeadEnemyCountReadOnly.Value);

            _rootStatesHandler.GoToGameOverState(new GameOverStatePayload(secondsInGameplay,
                gameplayResult.DeadEnemyCountReadOnly.Value));
        }

        private void Tick() => _ecsGameplay?.Update();
        private void FixedTick() => _ecsGameplay?.FixedUpdate();
    }
}