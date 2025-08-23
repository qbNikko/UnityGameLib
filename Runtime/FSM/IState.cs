namespace UnityGameLib.FSM
{
    public interface IState
    {
        public string Name();
        public StateMachine StateMachine { get; internal set; }

        public void Enter() {}
        
        public void Exit() {}
        
        public void Update() { }
        
        public void FixedUpdate() {}
        void HandleEvent(string eventName) {}
    }
}