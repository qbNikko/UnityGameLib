﻿using System;
using UnityEngine.Pool;

namespace UnityGameLib.Component.Pool
{
    public class ObjectPoolManager<T> :IDisposable where T : class, IPooledObject
    {
        ObjectPool<T> _pool;
        Action<IPooledObject> _releaseAction;

        public ObjectPoolManager(int maxPoolSize, int defaultPoolSize, bool sizeCheck, Func<T> onCreate)
        {
            _pool = new ObjectPool<T>(
                ()=>OnCreate(onCreate),
                i => OnGet(i),
                i => OnRelease(i),
                i => OnDestroy(i),
                sizeCheck, defaultPoolSize, maxPoolSize
            );
            _releaseAction = (i) => _pool.Release((T)i);
        }
        
        public T Get()=>_pool.Get();
        public void Release(T obj) => _pool.Release(obj);

        private void OnDestroy(T pooledObject)
        {
            pooledObject.Dispose();
        }

        private void OnRelease(T pooledObject)
        {
            pooledObject.Release();
        }

        private void OnGet(T pooledObject)
        {
            pooledObject.Reset();
        }
        private T OnCreate(Func<T> onCreate)
        {
            T pooledObject = onCreate.Invoke();
            pooledObject.Initialize();
            pooledObject.SetRealisePoolAction(_releaseAction);
            return pooledObject;
        }

        public void Dispose()
        {
            _pool.Dispose();
        }
    }
}