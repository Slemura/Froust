using System.Threading;
using Cysharp.Threading.Tasks;
using RpDev.Services.AsyncStateMachine.Data;

namespace RpDev.Services.AsyncStateMachine.Abstractions
{
    public interface IStateMachineHook
    {
        UniTask OnBeforeEnter(HookEnterPayload exitPayload, CancellationToken cancellationToken);
        UniTask OnAfterEnter(HookEnterPayload exitPayload, CancellationToken cancellationToken);

        UniTask OnBeforeExit(HookExitPayload exitPayload, CancellationToken cancellationToken);
        UniTask OnAfterExit(HookExitPayload exitPayload, CancellationToken cancellationToken);
    }
}