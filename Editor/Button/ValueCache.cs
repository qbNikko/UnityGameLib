﻿#if UNITY_EDITOR
using System.Collections.Generic;

namespace UnityGameLib.Editor.Button
{
    public class ValueCache
    {
        public static Dictionary<int, object> valueCache = new Dictionary<int, object>();
    }
}
#endif