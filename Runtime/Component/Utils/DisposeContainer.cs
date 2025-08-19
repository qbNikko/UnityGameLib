using System;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityGameLib.Collection;

namespace UnityGameLib.Component.Utils
{
    public class DisposeContainer : IDisposable
    {
        private List<IDisposable> disposables;
        private List<Action> disposableActions;
        private PooledObject<List<IDisposable>> disposablePool;
        private PooledObject<List<Action>> disposableActionsPool;
        
        public void AddDisposable(IDisposable disposable)
        {
            if (disposables == null)
                disposablePool = CollectionPool.GetList(out disposables);
            disposables.Add(disposable);
        }
        
        public void AddDisposableAction(Action action)
        {
            if (disposableActions == null)
                disposableActionsPool = CollectionPool.GetList(out disposableActions);
            disposableActions.Add(action);
        }
        public void Dispose()
        {
            disposableActions.ForEach(a=>
            {
                try
                {
                    a.Invoke();
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogError(e.Message);
                }
            });
        }
    }
}