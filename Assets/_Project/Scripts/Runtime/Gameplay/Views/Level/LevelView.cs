using Froust.Level.Camera;
using Froust.Services;
using RpDev.ThirdParty.Input.Joysticks.Base;
using UnityEngine;

namespace Froust.Level.Configurations
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private LevelPointsInfo _levelPoints;
        [SerializeField] private LevelPointsInfo _enemyMovePoints;

        [Header("Ground")] [SerializeField] private Transform _ground;
        [Header("Water")] [SerializeField] private Transform _water;
        [Header("Camera")] [SerializeField] private Transform _cameraSinkRoot;
        
        [SerializeField] private CameraShake _cameraShake;
        [SerializeField] private Transform _cameraAdjustmentRoot;
        [SerializeField] private Transform _gameContainer;
        [SerializeField] private Joystick _screenJoystick;
        [SerializeField] private ParticleSystem _iceFoeSplashParticles;

        public LevelPointsInfo LevelPoints => _levelPoints;
        public LevelPointsInfo EnemyMovePoints => _enemyMovePoints;
        public Transform Ground => _ground;
        public Transform CameraSinkRoot => _cameraSinkRoot;
        public Transform CameraAdjustmentRoot => _cameraAdjustmentRoot;
        public Transform Water => _water;
        public Transform GameContainer => _gameContainer;
        public CameraShake CameraShake => _cameraShake;
        public ParticleSystem IceFoeSplashParticles => _iceFoeSplashParticles;
        public IPlayerInput ScreenJoystick => _screenJoystick;
    }
}