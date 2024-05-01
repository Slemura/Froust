using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace RpDev.Services.AsyncStateMachine.Abstractions
{
    public interface IState : IDisposable
    {
        UniTask OnExit(CancellationToken cancellationToken);
    }

    public interface IPureState : IState
    {
        UniTask OnEnter(CancellationToken cancellationToken);
    }

    public interface IPayloadedState<in TPayload> : IState
    {
        UniTask OnEnter(TPayload payload, CancellationToken cancellationToken);
    }
}