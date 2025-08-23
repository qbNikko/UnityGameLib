using System;

namespace UnityGameLib.Component.Utils
{
    public interface IEventSubscriber : IDisposable
    {
        public void Subscribe();

        public void Unsubscribe();
    }
}