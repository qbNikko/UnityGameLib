using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityGameLib.Attribute;

namespace UnityGameLib.Editor.Button
{
    public class ButtonWithoutParameters : Button
    {
        public ButtonWithoutParameters(bool async,  MethodInfo method, ButtonAttribute buttonAttribute)
        {
            this._async = async;
            this._method = method;
            this._buttonAttribute = buttonAttribute;
        }
        
        protected override void DrawInternal(IEnumerable<object> targets)
        {
            if (!GUILayout.Button(GetMethodName()))
                return;
            InvokeMethod(targets);
        }

        protected override void InvokeMethod(IEnumerable<object> targets)
        {
            foreach (object obj in targets)
            {
                _method.Invoke(obj, null);
            }
        }
    }
}