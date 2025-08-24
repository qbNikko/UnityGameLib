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
        private Dictionary<Type, Dictionary<string,object>> _namedDictionary;

        private DIService()
        {
            _disposableContainer = new DisposeContainer();
            _disposableContainer.AddDisposable(CollectionPool.GetDictionary(out _singletonDictionary));
            _disposableContainer.AddDisposable(CollectionPool.GetDictionary(out _multiDictionary));
            _disposableContainer.AddDisposable(CollectionPool.GetDictionary(out _namedDictionary));
            
        }

        private static DIService Instance
        {
            get
            {
                if (_instance == null) _instance = new DIService();
                return _instance;
            }
        }

        public static IDisposable RegisterSingleton<T>(T instance) where T : class
        {
            return Instance.RegisterSingleton_(instance);
        }
        
        public static IDisposable RegisterNamed<T>(string name, T instance) where T : class
        {
            return Instance.RegisterNamed_(name, instance);
        }

        public static IDisposable RegisterMulti<T>(T instance) where T : class
        {
            return Instance.RegisterMulti_(instance);
        }

        public static bool GetSingleton<T>(out T instance) where T : class
        {
            return Instance.GetSingleton_(out instance);
        }
        
        public static bool GetAll<T>(out ReadOnlyCollection<T> instance) where T:class
        {
            return Instance.GetAll_(out instance);
        }
        
        public static bool GetNamed<T>(string name, out T instance) where T : class
        {
            return Instance.GetNamed_(name, out instance);
        }

        public IDisposable RegisterSingleton_<T>(T instance) where T:class
        {
            Type type = typeof(T);
            if(_singletonDictionary.ContainsKey(type)) throw new Exception("Singleton "+type+"object already registered");
            _singletonDictionary.Add(type, instance);
            return DisposeObjectHandler.Create(() => _singletonDictionary.Remove(type));
        }
        
        public IDisposable RegisterNamed_<T>(string name, T instance) where T:class
        {
            Type type = typeof(T);
            Dictionary<string, object> namedDictionary;
            if (!_namedDictionary.TryGetValue(type, out namedDictionary))
            {
                _disposableContainer.AddDisposable(CollectionPool.GetDictionary(out namedDictionary));
                _namedDictionary.Add(type,namedDictionary);
            }
            if (namedDictionary.ContainsKey(name)) throw new Exception("Object "+type+":"+name+" already registered");
            namedDictionary.Add(name, instance);
            return DisposeObjectHandler.Create(() => namedDictionary.Remove(name));
        }
        
        public IDisposable RegisterMulti_<T>(T instance) where T:class
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

        public bool GetSingleton_<T>(out T instance) where T:class
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
        
        public bool GetAll_<T>(out ReadOnlyCollection<T> instance) where T:class
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
        
        public bool GetNamed_<T>(string name, out T instance) where T:class
        {
            Type type = typeof(T);
            Dictionary<string, object> namedDictionary;
            if (_namedDictionary.TryGetValue(type, out namedDictionary))
            {
                if (namedDictionary.TryGetValue(name, out object value))
                {
                    instance = value as T;
                    return true;
                }
            }
            instance = null;
            return false;
        }
    }
}