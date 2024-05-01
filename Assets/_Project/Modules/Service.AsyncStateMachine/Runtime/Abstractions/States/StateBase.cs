using System.Threading;
using Cysharp.Threading.Tasks;

namespace RpDev.Services.AsyncStateMachine.Abstractions
{
    public abstract class StateBase : IPureState
    {
        public abstract UniTask OnEnter(CancellationToken cancellationToken);

        public abstract UniTask OnExit(CancellationToken cancellationToken);

        public abstract void Dispose();
    }

    public abstract class StateBase<TPayload> : StateBase, IPayloadedState<TPayload>
    {
        public abstract UniTask OnEnter(TPayload payload, CancellationToken cancellationToken);

        public sealed override UniTask OnEnter(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }
    }
}