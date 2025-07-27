using System;

namespace UnityGameLib.Reactive
{
    public interface IReactiveObservable<T> : IObservable<T>, IDisposable
    {
    }
}