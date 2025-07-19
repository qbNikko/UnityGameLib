using System.Linq;
using UnityEngine;

namespace UnityGameLib.Extension
{
    public static class GameObjectExtensions
    {
        public static void DestroyChildren(this GameObject t)
        {
            t.transform.Cast<Transform>().ToList().ForEach(c => Object.Destroy(c.gameObject));
        }

        public static void DestroyChildrenImmediate(this GameObject t)
        {
            t.transform.Cast<Transform>().ToList().ForEach(c => Object.DestroyImmediate(c.gameObject));
        }
    }
}