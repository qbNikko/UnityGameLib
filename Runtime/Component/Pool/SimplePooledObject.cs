using System;

namespace UnityGameLib.Component.Pool
{
    public abstract class SimplePooledObject : IPooledObject
    {
        private Action<IPooledObject> _realisePoolAction;
        
        public virtual void Reset()
        {
        }

        public virtual void Release()
        {
        }

        public virtual void Dispose()
        {
        }

        public virtual void Initialize()
        {
        }

        void IPooledObject.SetRealisePoolAction(Action<IPooledObject> realisePoolAction)
        {
            _realisePoolAction = realisePoolAction;
        }

        public void ReleaseInPool()
        {
            _realisePoolAction?.Invoke(this);
        }

    }
}