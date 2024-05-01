using System;

namespace RpDev.Services.AsyncStateMachine.Data
{
    public struct HookExitPayload
    {
        public readonly Type CurrentType;
        public readonly Type TargetType;

        public HookExitPayload(Type currentType, Type targetType)
        {
            TargetType = targetType;
            CurrentType = currentType;
        }
    }
}