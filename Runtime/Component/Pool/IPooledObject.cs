using System;
using UnityEngine;
using UnityGameLib.Interface;

namespace UnityGameLib.Component.Pool
{
    /**
     * Интерфейс объекта для пулинга
     */
    public interface IPooledObject : IDisposable, IInitialized
    {
        /**
         * Сброс состояния. При получении объекта из пула
         */
        void Reset();

        /**
         * Операция при возвращении объекта в пул
         */
        void Release();

        /**
         * Установка действия по возвращаению объекта в пул
         */
        void SetRealisePoolAction(Action<IPooledObject> realisePoolAction);

        /**
         * Возвращение объекта в пул
         */
        public void ReleaseInPool();
        
    }

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