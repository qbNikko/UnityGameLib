using System;
using NUnit.Framework;

namespace UnityGameLib.Attribute
{
    [AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class DictionaryAttribute: PropertyAttribute
    {
        public string key;
        public string value;
    }
}