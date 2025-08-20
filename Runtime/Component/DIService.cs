using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine.Pool;
using UnityEngine.UI;
using UnityGameLib.Collection;
using UnityGameLib.Component.Utils;

namespace UnityGameLib.Component
{
    public class DIService
    {
        private static DIService _instance;
        private DisposeContainer  _disposableContainer;
        private Dictionary<Type, object> _singletonDictionary;
        private Dictionary<Type, List<object>> _multiDictionary;

        private DIService()
        {
            _disposableContainer = new DisposeContainer();
            _disposableContainer.AddDisposable(CollectionPool.GetDictionary(out _singletonDictionary));
            _disposableContainer.AddDisposable(CollectionPool.GetDictionary(out _multiDictionary));
        }

        public static DIService Instance
        {
            get
            {
                if (_instance == null) _instance = new DIService();
                return _instance;
            }
        }

        public IDisposable RegisterSingleton<T>(T instance) where T:class
        {
            Type type = typeof(T);
            if(_singletonDictionary.ContainsKey(type)) throw new Exception("Singleton "+type+"object already registered");
            _singletonDictionary.Add(type, instance);
            return DisposeObjectHandler.Create(() => _singletonDictionary.Remove(type));
        }
        
        public IDisposable RegisterMulti<T>(T instance) where T:class
        {
            Type type = typeof(T);
            List<object> list;
            if (!_multiDictionary.TryGetValue(type,out list))
            {
                _disposableContainer.AddDisposable(CollectionPool.GetList(out list));
                _multiDictionary.Add(type,list);
            }
            list.Add(instance);
            return DisposeObjectHandler.Create(() => list.Remove(instance));
        }

        public bool GetSingleton<T>(out T instance) where T:class
        {
            Type type = typeof(T);
            if (_singletonDictionary.TryGetValue(type, out object value))
            {
                instance = value as T;
                return true;
            }
            instance = null;
            return false;
        }
        
        public bool GetAll<T>(out ReadOnlyCollection<T> instance) where T:class
        {
            Type type = typeof(T);
            if (_multiDictionary.TryGetValue(type, out List<object> value))
            {
                instance = new ReadOnlyCollection<T>(value.Cast<T>().ToList());
                return true;
            }
            instance = null;
            return false;
        }
        
        
    }
}