using System.Threading;
using Cysharp.Threading.Tasks;
using Froust.Runtime.PersistentData;
using Froust.Runtime.Screens;
using RpDev.Services.UI;
using RpDev.Services.AsyncStateMachine.Abstractions;
using RpDev.Services.UI.Mediators;

namespace Froust.EntryPoint.States
{
    public class GameOverState : StateBase<GameOverStatePayload>
    {
        private readonly IUIService _iuiService;
        private readonly IRootStatesHandler _rootStatesHandler;
        private readonly UserDataHandler _userDataHandler;
        private readonly UIMediatorFactory _uiMediatorFactory;
        private GameOverScreen _gameOverScreen;
        private GameOverStatePayload _payload;
        private GameOverScreenMediator _gameOverScreenMediator;

        public GameOverState(IUIService iuiService, IRootStatesHandler rootStatesHandler,
            UserDataHandler userDataHandler,
            UIMediatorFactory uiMediatorFactory)
        {
            _iuiService = iuiService;
            _rootStatesHandler = rootStatesHandler;
            _userDataHandler = userDataHandler;
            _uiMediatorFactory = uiMediatorFactory;
        }

        public override async UniTask OnEnter(GameOverStatePayload payload, CancellationToken cancellationToken)
        {
            _payload = payload;
            await ShowGameOverScreen();
            AddScreenHandlers();
        }

        public override async UniTask OnExit(CancellationToken cancellationToken)
        {
            await _gameOverScreen.FadeOutAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _gameOverScreen.OnStartGameClicked -= GoToGameState;
            _gameOverScreenMediator.Dispose();
            _iuiService.DestroyScreen(_gameOverScreen);
        }

        private async UniTask ShowGameOverScreen()
        {
            _gameOverScreen = await _iuiService.SpawnScreen<GameOverScreen>();
            _gameOverScreen.SetupInfo(_payload.SecondsInLevel, _userDataHandler.LevelTimeScore);
            _gameOverScreenMediator = _uiMediatorFactory.Create<GameOverScreenMediator, GameOverScreen>(_gameOverScreen);
            _gameOverScreenMediator.Initialize();
            
            await _gameOverScreen.FadeInAsync();
        }

        private void AddScreenHandlers()
        {
            _gameOverScreen.OnStartGameClicked += GoToGameState;
        }

        private void GoToGameState()
        {
            _rootStatesHandler.GoToGameplayState();
        }
    }
}