using System.Threading;
using Cysharp.Threading.Tasks;
using Froust.Level.Configurations;
using Froust.Level.Model;
using RpDev.Services.AssetProvider.Abstractions;
using RpDev.Services.AsyncStateMachine.Abstractions;
using RpDev.Services.AudioService;
using UnityEngine;

namespace Froust.EntryPoint.States
{
    public class BootstrapState : StateBase
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IRootStatesHandler _rootStatesHandler;
        private readonly GameAudioHandler _gameAudioHandler;
        private readonly GameplayResourcesModel _gameplayResourcesModel;

        private PlayerConfiguration _playerConfiguration;
        private GroundConfiguration _groundConfiguration;
        private CameraConfiguration _cameraConfiguration;
        private EnemyConfiguration _enemyConfiguration;
        private LevelView _levelView;

        public BootstrapState(IAssetProvider assetProvider, IRootStatesHandler rootStatesHandler,
            GameAudioHandler gameAudioHandler,
            GameplayResourcesModel gameplayResourcesModel)
        {
            _assetProvider = assetProvider;
            _rootStatesHandler = rootStatesHandler;
            _gameAudioHandler = gameAudioHandler;
            _gameplayResourcesModel = gameplayResourcesModel;
        }

        public override async UniTask OnEnter(CancellationToken cancellationToken)
        {
            await UniTask.WhenAll(LoadPlayerConfiguration(),
                LoadGroundConfiguration(),
                LoadCameraConfiguration(),
                LoadEnemyConfiguration(),
                LoadLevelView(),
                LoadAudioPacks());

            _gameplayResourcesModel.AddResources(_enemyConfiguration, _groundConfiguration, _cameraConfiguration, _playerConfiguration, _levelView);
            _rootStatesHandler.GoToMainMenuState();
        }

        private async UniTask LoadPlayerConfiguration()
        {
            _playerConfiguration = await _assetProvider.LoadAsset<PlayerConfiguration>("PlayerConfiguration", default);
        }

        private async UniTask LoadGroundConfiguration()
        {
            _groundConfiguration = await _assetProvider.LoadAsset<GroundConfiguration>("GroundConfiguration", default);
        }

        private async UniTask LoadCameraConfiguration()
        {
            _cameraConfiguration = await _assetProvider.LoadAsset<CameraConfiguration>("CameraConfiguration", default);
        }

        private async UniTask LoadEnemyConfiguration()
        {
            _enemyConfiguration = await _assetProvider.LoadAsset<EnemyConfiguration>("EnemyConfiguration", default);
        }

        private async UniTask LoadLevelView()
        {
            var levelViewPrototype = await _assetProvider.LoadAsset<GameObject>("LevelView", default);
            _levelView = Object.Instantiate(levelViewPrototype).GetComponent<LevelView>();
        }

        private async UniTask LoadAudioPacks()
        {
            var audioPackLibrary = await _assetProvider.LoadAsset<AudioPackLibrary>("AudioPackLibrary", default);
            _gameAudioHandler.AddAudioLibrary(audioPackLibrary);
        }

        public override UniTask OnExit(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        public override void Dispose()
        {
            _playerConfiguration = null;
            _groundConfiguration = null;
            _cameraConfiguration = null;
            _enemyConfiguration = null;
            _levelView = null;
        }
    }
}