using RpDev.Services.AsyncStateMachine.Abstractions;
using VContainer;

namespace RpDev.Services.AsyncStateMachine.Tools
{
    public static class StateVContainerExtensions
    {
        public static void BindState<TState>(this IContainerBuilder container) where TState : StateBase
        {
            container.Register<TState>(Lifetime.Transient);
        }

        public static void BindState<TAbstractState, TState>(this IContainerBuilder container)
            where TAbstractState : StateBase where TState : TAbstractState
        {
            container.Register<TAbstractState, TState>(Lifetime.Transient);
        }
    }
}