using Froust.Level.Configurations;

namespace Froust.Level.Model
{
    public interface IGameplayResourcesModel
    {
        EnemyConfiguration EnemyConfiguration { get; }
        GroundConfiguration GroundConfiguration { get; }
        CameraConfiguration CameraConfiguration { get; }
        PlayerConfiguration PlayerConfiguration { get; }
        LevelView LevelView { get; }
    }

    public class GameplayResourcesModel : IGameplayResourcesModel
    {
        private EnemyConfiguration _enemyConfiguration;
        private GroundConfiguration _groundConfiguration;
        private CameraConfiguration _cameraConfiguration;
        private PlayerConfiguration _playerConfiguration;
        private LevelView _levelView;

        public EnemyConfiguration EnemyConfiguration => _enemyConfiguration;
        public GroundConfiguration GroundConfiguration => _groundConfiguration;
        public CameraConfiguration CameraConfiguration => _cameraConfiguration;
        public PlayerConfiguration PlayerConfiguration => _playerConfiguration;
        public LevelView LevelView => _levelView;

        public void AddResources(EnemyConfiguration enemyConfiguration,
            GroundConfiguration groundConfiguration,
            CameraConfiguration cameraConfiguration,
            PlayerConfiguration playerConfiguration,
            LevelView levelView)
        {
            _enemyConfiguration = enemyConfiguration;
            _groundConfiguration = groundConfiguration;
            _cameraConfiguration = cameraConfiguration;
            _playerConfiguration = playerConfiguration;
            _levelView = levelView;
        }
    }
}