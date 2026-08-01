using System;
using System.Collections.Generic;

namespace _Project.CodeBase.Gameplay.StateMachine
{
    public abstract class StateMachine
    {
        private readonly Dictionary<Type, IExitableState> _registeredStates;
        private IExitableState _currentState;

        public StateMachine()
        {
            _registeredStates = new Dictionary<Type, IExitableState>();
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState newState = ChangeState<TState>();
            newState.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPaylodedState<TPayload>
        {
            TState newState = ChangeState<TState>();
            newState.Enter(payload);
        }

        public void RegisterState<TState>(TState state) where TState : IExitableState
        {
            _registeredStates.Add(typeof(TState), state);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            if (_currentState != null)
                _currentState.Exit();

            TState state = GetState<TState>();
            _currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _registeredStates[typeof(TState)] as TState;
        }
    }
}