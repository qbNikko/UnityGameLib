using System;
using UnityEngine;

namespace UnityGameLib.Component.Pool
{
    public class MbPooledObject : MonoBehaviour, IPooledObject
    {
        private Action<IPooledObject> _realisePoolAction;
        
        public virtual void Reset()
        {
            gameObject.SetActive(true);
        }

        public virtual void Release()
        {
            gameObject.SetActive(false);
        }

        public virtual void Dispose()
        {
            Destroy(gameObject);
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