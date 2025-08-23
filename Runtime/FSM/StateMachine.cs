using System;
using System.Collections.Generic;
using UnityGameLib.Collection;
using UnityGameLib.Component.Utils;

namespace UnityGameLib.FSM
{
    public class StateMachine
    {
        private Dictionary<string, IState> _statesDictionary;
        private Dictionary<Type, IState> _statesTypeDictionary;
        private DisposeContainer  _disposableContainer;
        private IState _currentState;

        public StateMachine()
        {
            _disposableContainer.AddDisposable(CollectionPool.GetDictionary(out _statesTypeDictionary));
            _disposableContainer.AddDisposable(CollectionPool.GetDictionary(out _statesTypeDictionary));
        }

        public StateMachine RegisterState(IState state)
        {
            _statesDictionary.Add(state.Name(), state);
            _statesTypeDictionary.Add(state.GetType(), state);
            state.StateMachine = this;
            return this;
        }
        
        public void SwitchState(Type state)
        {
            if (_statesTypeDictionary.TryGetValue(state, out var newState))
            {
                _currentState?.Exit();
                _currentState = newState;
                _currentState.Enter();
            }
        }
        
        public void SwitchState(string state)
        {
            if (_statesDictionary.TryGetValue(state, out var newState))
            {
                _currentState?.Exit();
                _currentState = newState;
                _currentState.Enter();
            }
        }

        public void HandleEvent(string eventName)
        {
            _currentState?.HandleEvent(eventName);
        }

        public void Update()
        {
            _currentState?.Update();
        }

        public void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }
    }
}