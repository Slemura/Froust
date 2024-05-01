using Froust.EntryPoint.States;

namespace Froust.EntryPoint
{
    public interface IRootStatesHandler
    {
        void GoToMainMenuState();
        void GoToGameplayState();
        void GoToGameOverState(GameOverStatePayload gameOverStatePayload);
    }
}