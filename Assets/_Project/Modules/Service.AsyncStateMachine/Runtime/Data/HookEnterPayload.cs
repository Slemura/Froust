using System;

namespace RpDev.Services.AsyncStateMachine.Data
{
    public struct HookEnterPayload
    {
        public readonly Type TargetType;

        public HookEnterPayload(Type targetType)
        {
            TargetType = targetType;
        }
    }
}