using Cysharp.Threading.Tasks;
using Froust.EntryPoint.States;
using RpDev.Services.AsyncStateMachine;
using UnityEngine;
using VContainer.Unity;

namespace Froust.EntryPoint
{
    public class AppCore : IInitializable, IRootStatesHandler
    {
        private readonly StateMachine<GameRootStateTrigger> _stateMachine;

        public AppCore(StateMachine<GameRootStateTrigger> stateMachine)
        {
            Application.targetFrameRate = 60;
            _stateMachine = stateMachine;
        }

        public void Initialize()
        {
            DefineStateMachine();
            
            _stateMachine.Start(default).Forget();
        }
        
        public void GoToMainMenuState()
        {
            _stateMachine.Fire(GameRootStateTrigger.MainMenu, default).Forget();
        }
        
        public void GoToGameplayState()
        {
            _stateMachine.Fire(GameRootStateTrigger.Gameplay, default).Forget();
        }
        
        public void GoToGameOverState(GameOverStatePayload gameOverStatePayload)
        {
            _stateMachine.Fire(GameRootStateTrigger.GameOver, gameOverStatePayload, default).Forget();
        }
        
        private void DefineStateMachine()
        {
            _stateMachine.SetInitialState<BootstrapState>();
            
            _stateMachine.Configure<BootstrapState>()
                .AllowTransition<MainMenuState>(GameRootStateTrigger.MainMenu);
            
            _stateMachine.Configure<MainMenuState>()
                .AllowTransition<GameplayState>(GameRootStateTrigger.Gameplay);
            
            _stateMachine.Configure<GameplayState>()
                .AllowTransition<GameOverState>(GameRootStateTrigger.GameOver);
            
            _stateMachine.Configure<GameOverState>()
                .AllowTransition<GameplayState>(GameRootStateTrigger.Gameplay);
        }
    }
}
