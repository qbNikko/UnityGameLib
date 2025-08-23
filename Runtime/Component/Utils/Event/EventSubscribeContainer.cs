using System;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityGameLib.Collection;

namespace UnityGameLib.Component.Utils
{
    public class EventSubscribeContainer : IEventSubscriber
    {
        private List<IEventSubscriber> _subscribers;
        private PooledObject<List<IEventSubscriber>> _subscribersPool;
        public EventSubscribeContainer()
        {
            _subscribersPool = CollectionPool.GetList(out _subscribers);
        }

        public void AddSubscriber(IEventSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void Subscribe()
        {
            _subscribers.ForEach(subscriber => subscriber.Subscribe());
        }

        public void Unsubscribe()
        {
            _subscribers.ForEach(subscriber => subscriber.Unsubscribe());
        }

        public void Dispose()
        {
            Unsubscribe();
        }
    }
}