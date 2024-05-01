using System;
using Cysharp.Threading.Tasks;
using Froust.Level.Model;
using Froust.Level.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Froust.Level
{
    public sealed class GameplayEcsStartup : IDisposable
    {
        private EcsWorld _world;
        private IEcsSystems _updateSystems;
        private IEcsSystems _fixedUpdateSystems;
        private GameplayModel _gameplayModel;

        private UniTaskCompletionSource<GameplayModel> _gameplayModelCompletionSource;
        private IGameplayResourcesModel _gameplayResourcesModel;
        private GameAudioHandler _audioHandler;

        public void AddResourcesModel(IGameplayResourcesModel gameplayResourcesModel)
        {
            _gameplayResourcesModel = gameplayResourcesModel;
        }

        public void AddAudioHandler(GameAudioHandler audioHandler)
        {
            _audioHandler = audioHandler;
        }

        public void Init()
        {
            _world = new EcsWorld();

            _gameplayModel = new GameplayModel
            {
                StartLevelTime = DateTime.UtcNow
            };
        }

        public IReadonlyGameplayModel GetGameplayModel()
        {
            return _gameplayModel;
        }

        public async UniTask<IReadonlyGameplayModel> Start()
        {
            _gameplayModelCompletionSource = new UniTaskCompletionSource<GameplayModel>();

            _updateSystems = new EcsSystems(_world);
            _updateSystems
                .Add(new InitLevelDataModelSystem())
                .Add(new InitCameraDataSystem())
                .Add(new InitGroundSystem())
                .Add(new InitPlayerSystem())
                .Add(new InitEnemySystem())
                .Add(new InitPlayerViewSystem())
                .Add(new InitSnowdriftSystem())
                .Add(new KeyboardInputSystem())
                .Add(new ScreenJoystickInputSystem())
                .Add(new ChangeEnemySpawnDifficultySystem())
                .Add(new SnowdriftPositionGeneratorSystem())
                .Add(new CreateSnowdriftViewSystem())
                .Add(new RisingSnowdriftSystem())
                .Add(new CreateEnemyViewSystem())
                .Add(new DestroySnowdriftSystem())
                .Add(new GroundSinkSystem())
                .Add(new UpdateCameraPositionSystem())
                .Add(new EndLevelAnimationSystem())
                .Add(new EndGameSystem())
                .Add(new OnStartLevelSystem())
                #if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                #endif
                .Inject(_world)
                .Inject(_audioHandler)
                .Inject(_gameplayModel)
                .Inject(_gameplayResourcesModel.LevelView)
                .Inject(_gameplayResourcesModel.EnemyConfiguration)
                .Inject(_gameplayResourcesModel.PlayerConfiguration)
                .Inject(_gameplayResourcesModel.GroundConfiguration)
                .Inject(_gameplayResourcesModel.CameraConfiguration)
                .Init();

            _fixedUpdateSystems = new EcsSystems(_world);
            _fixedUpdateSystems
                .Add(new CharactersCollideSystem())
                .Add(new EnemyDisableNavMeshSystem())
                .Add(new PlayerPushBackSystem())
                .Add(new PushSystem())
                .Add(new EnemyStopPushSystem())
                
                .Add(new PlayerMovementSystem())
                .Add(new EnemyMovementSystem())
                .Add(new EnemyOutOfBoundariesCheckSystem())
                .Add(new PlayerOutOfBoundariesCheckSystem())
                .Add(new PlayerAnimationSystem())
                .Add(new EnemyDestroySystem())
                .Inject(_world)
                .Inject(_gameplayModel)
                .Inject(_audioHandler)
                .Inject(_gameplayResourcesModel.LevelView)
                .Inject(_gameplayResourcesModel.EnemyConfiguration)
                .Inject(_gameplayResourcesModel.GroundConfiguration)
                .Inject(_gameplayResourcesModel.PlayerConfiguration)
                .Init();

            return await _gameplayModelCompletionSource.Task;
        }

        public void Update()
        {
            if (_gameplayModel.IsGameEnded) return;
            _updateSystems?.Run();
        }

        public void FixedUpdate()
        {
            if (_gameplayModel.IsGameEnded)
            {
                _gameplayModelCompletionSource.TrySetResult(_gameplayModel);
                return;
            }

            _fixedUpdateSystems?.Run();
        }

        public void Dispose()
        {
            if (_updateSystems != null)
            {
                _updateSystems.Destroy();
                _updateSystems = null;
            }

            if (_fixedUpdateSystems != null)
            {
                _fixedUpdateSystems.Destroy();
                _fixedUpdateSystems = null;
            }

            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}