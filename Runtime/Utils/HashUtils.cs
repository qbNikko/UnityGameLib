using System;
using System.Linq;
using System.Reflection;

namespace UnityGameLib.Utils
{
    public static class HashUtils
    {
        public static string HashOfType(this Type type)
        {
            if (type != null) return type.Assembly.GetName().Name + "$" + type.FullName;
            return null;
        }
        

        public static Type ParseType(string hash)
        {
            if (string.IsNullOrEmpty(hash)) return null;
            var arr = hash.Split('$');
            var tp = ParseType(arr.GetOrDefault(0,string.Empty),
                arr.GetOrDefault(1,string.Empty));
            return tp;
        }
        
        public static Type ParseType(string assembName, string typeName)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.GetName().Name == assembName || assembly.FullName == assembName)
                {
                    foreach (Type t in assembly.GetTypes())
                    {
                        if (t.FullName == typeName) return t;
                    }
                    break;
                }
            }
            return null;
        }
    }
}