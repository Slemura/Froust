using System.Threading;
using Cysharp.Threading.Tasks;
using Froust.Runtime.Screens;
using RpDev.Services.UI;
using RpDev.Services.AsyncStateMachine.Abstractions;
using RpDev.Services.UI.Mediators;

namespace Froust.EntryPoint.States
{
    public class MainMenuState : StateBase
    {
        private readonly IUIService _iuiService;
        private readonly IRootStatesHandler _rootStatesHandler;
        private readonly UIMediatorFactory _uiMediatorFactory;
        private readonly LoadingScreen _loadingScreen;
        
        private MainMenuScreen _mainMenuScreen;
        private MainMenuScreenMediator _mainMenuScreenMediator;

        public MainMenuState(IUIService iuiService,
            IRootStatesHandler rootStatesHandler,
            UIMediatorFactory uiMediatorFactory,
            LoadingScreen loadingScreen)
        {
            _iuiService = iuiService;
            _rootStatesHandler = rootStatesHandler;
            _uiMediatorFactory = uiMediatorFactory;
            _loadingScreen = loadingScreen;
        }
        
        public override async UniTask OnEnter(CancellationToken cancellationToken)
        {
            await InitMainMenu();
            AddScreenHandlers();
        }
        
        public override async UniTask OnExit(CancellationToken cancellationToken)
        {
            await _mainMenuScreen.FadeOutAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _mainMenuScreen.OnStartGameClicked -= GoToGameState;
            _iuiService.DestroyScreen(_mainMenuScreen);
            _mainMenuScreenMediator.Dispose();
        }
        
        private async UniTask InitMainMenu()
        {
            _mainMenuScreen = await _iuiService.SpawnScreen<MainMenuScreen>();
            _mainMenuScreenMediator = _uiMediatorFactory.Create<MainMenuScreenMediator, MainMenuScreen>(_mainMenuScreen);
            _mainMenuScreenMediator.Initialize();
            
            _mainMenuScreen.FadeInAsync().Forget();
            _loadingScreen.FadeOutAsync().Forget();
        }

        private void AddScreenHandlers()
        {
            _mainMenuScreen.OnStartGameClicked += GoToGameState;
        }

        private void GoToGameState()
        {
            _rootStatesHandler.GoToGameplayState();
        }
    }
}
