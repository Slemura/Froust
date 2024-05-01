namespace RpDev.ThirdParty.Input.Joysticks.Base
{
    public interface IPlayerInput
    {
        bool IsActive { get; }
        float Horizontal { get; }
        float Vertical { get; }
        float Magnitude { get; } // TODO what
        void ResetController();
        void ResumeController();
    }
}