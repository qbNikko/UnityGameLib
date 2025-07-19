using UnityEngine;

namespace UnityGameLib.Attribute
{
    [System.AttributeUsageAttribute(System.AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
    public class ResourceListAttribute : PropertyAttribute
    {
        public string Path;
        public string Pattern;
    }
}