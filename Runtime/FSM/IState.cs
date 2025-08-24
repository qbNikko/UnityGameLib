using System.Collections.Generic;

namespace UnityGameLib.FSM
{
    public interface IState
    {
        public string Name();
        public StateMachine StateMachine { get; internal set; }

        public void Enter(Dictionary<string,object> parameters = null) {}
        
        public void Exit() {}
        
        public void Update() { }
        
        public void FixedUpdate() {}
        void HandleEvent(string eventName) {}
    }
    
    public abstract class State : IState
    {
        private StateMachine _stateMachine;

        public string Name()
        {
            throw new System.NotImplementedException();
        }

        StateMachine IState.StateMachine
        {
            get => _stateMachine;
            set => _stateMachine = value;
        }
    }
}