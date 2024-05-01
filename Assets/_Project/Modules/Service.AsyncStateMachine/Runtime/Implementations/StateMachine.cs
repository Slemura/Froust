using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using RpDev.Services.AsyncStateMachine.Abstractions;
using RpDev.Services.AsyncStateMachine.Data;
using RpDev.Services.AsyncStateMachine.Data.Exceptions;

namespace RpDev.Services.AsyncStateMachine
{
    public sealed class StateMachine<TTrigger> : IStateMachine, IDisposable
        where TTrigger : Enum
    {
        private readonly Dictionary<Type, StateConfiguration> _configurations;
        private readonly IStateFactory _factory;
        private readonly HashSet<IStateMachineHook> _hooks = new HashSet<IStateMachineHook>();
        
        private Type _initialStateType;

        private bool _isStarted;
        
        private Action<IState> _onStateChanged;

        private IState CurrentState { get; set; }

        public StateMachine(IStateFactory plainClassFactory)
        {
            _factory = plainClassFactory;
            _configurations = new Dictionary<Type, StateConfiguration>();
        }

        /// <summary>
        /// Set initial state of the state machine that will be called when the state machine is started.
        /// </summary>
        /// <typeparam name="T">Type of the state</typeparam>
        /// <returns></returns>
        public StateMachine<TTrigger> SetInitialState<T>() where T : IState
        {
            Configure<T>();

            _initialStateType = typeof(T);

            return this;
        }
        
        public void Dispose()
        {
            CurrentState?.Dispose();
        }

        /// <summary>
        /// Runs the state machine.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public async UniTask Start(CancellationToken cancellationToken)
        {
            if (_isStarted)
                throw new RuntimeStateMachineException($"'{GetType().Name}' is already started.");

            _isStarted = true;
            await Enter(_initialStateType, cancellationToken);
        }

        /// <summary>
        /// Runs the state machine with payload if initial state requires payload.
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TPayload">Payload that will be passed to the initial state</typeparam>
        public async UniTask Start<TPayload>(TPayload payload, CancellationToken cancellationToken)
        {
            if (_isStarted)
                throw new RuntimeStateMachineException($"'{GetType().Name}' is already started.");
            
            _isStarted = true;
            await Enter(_initialStateType, payload, cancellationToken);
        }

        /// <summary>
        /// Stops the state machine with dispose of current state.
        /// State machine will be rolled back to the initial state
        /// </summary>
        /// <param name="cancellationToken"></param>
        public async UniTask Stop(CancellationToken cancellationToken)
        {
            await ExitCurrentState(null, cancellationToken);

            CurrentState = null;

            _isStarted = false;
        }
        
        public void SubscribeOnStateChanged(Action<IState> callback)
        {
            _onStateChanged += callback;
        }

        public void UnsubscribeOnStateChanged(Action<IState> callback)
        {
            _onStateChanged = callback;
        }

        /// <summary>
        /// Allow to configure new state inside the state machine
        /// </summary>
        /// <typeparam name="T">State type</typeparam>
        /// <returns></returns>
        public StateConfiguration Configure<T>() where T : IState
        {
            if (_configurations.TryGetValue(typeof(T), out var configuration))
                return configuration;

            configuration = new StateConfiguration();
            _configurations.Add(typeof(T), configuration);

            return configuration;
        }

        /// <summary>
        /// Start transition to the new state
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="cancellationToken"></param>
        public async UniTask Fire(TTrigger trigger, CancellationToken cancellationToken)
        {
            await FireInternal(trigger, cancellationToken);
        }

        /// <summary>
        /// Start transition to the new state with payload if state requires payload
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="payload"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TPayload"></typeparam>
        public async UniTask Fire<TPayload>(TTrigger trigger, TPayload payload, CancellationToken cancellationToken)
        {
            await FireInternal(trigger, payload, cancellationToken);
        }

        /// <summary>
        /// Adds a hook to the state machine that will be called on every state change.
        /// </summary>
        /// <param name="hook">Implementation of the hook</param>
        /// <exception cref="InvalidOperationException">Will be thrown if such already attached. Duplications not allowed</exception>
        public void AddHook(IStateMachineHook hook)
        {
            if (_hooks.Contains(hook))
                throw new InvalidOperationException(
                    $"This hook already attached to the state machine {hook.GetType()}");

            _hooks.Add(hook);
        }

        /// <summary>
        /// Removes a hook from the state machine.
        /// Second and consecutive calls for the same hook will be ignored.
        /// </summary>
        /// <param name="hook">Implementation of the hook</param>
        public void RemoveHook(IStateMachineHook hook)
        {
            if (_hooks.Contains(hook))
                _hooks.Remove(hook);
        }
        
        private async UniTask Enter(Type stateType, CancellationToken cancellationToken)
        {
            await ExitCurrentState(stateType, cancellationToken);

            var state = _factory.Create(stateType);

            CurrentState = (IState) state;

            var pureState = (IPureState) CurrentState;

            foreach (var stateMachineHook in _hooks)
                await stateMachineHook.OnBeforeEnter(new HookEnterPayload(stateType), cancellationToken);
            
            await pureState.OnEnter(cancellationToken);
            
            foreach (var stateMachineHook in _hooks)
                await stateMachineHook.OnAfterEnter(new HookEnterPayload(stateType), cancellationToken);

            _onStateChanged?.Invoke(CurrentState);
        }

        private async UniTask Enter<TPayload>(Type stateType, TPayload payload, CancellationToken cancellationToken)
        {
            await ExitCurrentState(stateType, cancellationToken);

            var state = _factory.Create(stateType);

            var payloadState = (IPayloadedState<TPayload>) state;

            CurrentState = payloadState;

            foreach (var stateMachineHook in _hooks)
                await stateMachineHook.OnBeforeEnter(new HookEnterPayload(stateType), cancellationToken);
            
            await payloadState.OnEnter(payload, cancellationToken);
            
            foreach (var stateMachineHook in _hooks)
                await stateMachineHook.OnAfterEnter(new HookEnterPayload(stateType), cancellationToken);

            _onStateChanged?.Invoke(CurrentState);
        }

        private async UniTask ExitCurrentState(Type stateType, CancellationToken cancellationToken)
        {
            if (CurrentState != null)
            {
                foreach (var stateMachineHook in _hooks)
                {
                    await stateMachineHook.OnBeforeExit(new HookExitPayload(CurrentState.GetType(), stateType),
                        cancellationToken);
                }

                await CurrentState.OnExit(cancellationToken);
                
                foreach (var stateMachineHook in _hooks)
                {
                    await stateMachineHook.OnAfterExit(new HookExitPayload(CurrentState.GetType(), stateType),
                        cancellationToken);
                }

                CurrentState.Dispose();
            }
        }

        private async UniTask FireInternal(TTrigger trigger, CancellationToken cancellationToken)
        {
            var type = VerifyAndReturnStateType(trigger);

            if (type == null)
                return;
            
            await Enter(type, cancellationToken);
        }

        private async UniTask FireInternal<TPayload>(TTrigger trigger, TPayload payload,
            CancellationToken cancellationToken)
        {
            var type = VerifyAndReturnStateType(trigger);

            if (type == null)
                return;
            
            await Enter(type, payload, cancellationToken);
        }

        private Type VerifyAndReturnStateType(TTrigger trigger)
        {
            if (CurrentState == null)
            {
                throw new RuntimeStateMachineException(
                    $"State Machine '{GetType().Name}' is not initialized. Use '{nameof(Start)}' method");
            }

            if (_configurations.TryGetValue(CurrentState.GetType(), out var configuration) == false)
            {
                var stateName = CurrentState.GetType().Name;
                throw new ArgumentException(
                    $"State Machine '{GetType().Name}' has no configuration for '{stateName}' state.");
            }

            if (configuration.TryGetTargetStateType(trigger, out var nextStateType) == false)
            {
                var stateName = CurrentState.GetType().Name;
                var triggerName = $"{typeof(TTrigger).Name}.{Enum.GetName(typeof(TTrigger), trigger)}";
                throw new ArgumentException($"State '{stateName}' is not configured for '{triggerName}' trigger.");
            }

            return nextStateType;
        }

        public sealed class StateConfiguration
        {
            private readonly Dictionary<TTrigger, Type> _stateTypeByTrigger;

            public StateConfiguration()
            {
                _stateTypeByTrigger = new Dictionary<TTrigger, Type>();
            }

            /// <summary>
            /// Allow transition to the new state by trigger and state type
            /// </summary>
            /// <param name="trigger">Trigger that will "trigger" transition</param>
            /// <typeparam name="T">Type of the new state</typeparam>
            /// <returns>Configuration for the same state</returns>
            /// <exception cref="ArgumentException"></exception>
            public StateConfiguration AllowTransition<T>(TTrigger trigger)
                where T : IState
            {
                if (_stateTypeByTrigger.ContainsKey(trigger))
                {
                    var stateName = typeof(T).Name;
                    var triggerName = $"{typeof(TTrigger).Name}.{Enum.GetName(typeof(TTrigger), trigger)}";
                    throw new ArgumentException(
                        $"State '{stateName}' is already configured for '{triggerName}' trigger.");
                }

                _stateTypeByTrigger.Add(trigger, typeof(T));

                return this;
            }

            public bool TryGetTargetStateType(TTrigger trigger, out Type stateType)
            {
                return _stateTypeByTrigger.TryGetValue(trigger, out stateType);
            }
        }
    }
}