using System;
using RpDev.Services.AsyncStateMachine.Abstractions;
using VContainer;

namespace RpDev.Services.AsyncStateMachine.Implementations
{
    public class StateFactory : IStateFactory
    {
        private readonly IObjectResolver _container;

        public StateFactory(IObjectResolver container)
        {
            _container = container;
        }

        public T Create<T>() where T : IState
        {
            return _container.Resolve<T>();
        }

        public object Create(Type type)
        {
            return _container.Resolve(type);
        }
    }
}