using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityGameLib.Attribute;
using Object = UnityEngine.Object;

namespace UnityGameLib.Editor.Button
{
    [CustomEditor(typeof(Object), true)]
    [CanEditMultipleObjects]
    public class ButtonPropertyDrawer : UnityEditor.Editor
    {
        public readonly List<Button> Buttons = new List<Button>();
        private void OnEnable()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var methods = target.GetType().GetMethods(flags);

            foreach (MethodInfo method in methods)
            {
                var buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();

                if (buttonAttribute == null)
                    continue;

                Buttons.Add(Button.Create(method, buttonAttribute));
            }
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            foreach (Button button in Buttons)
            {
                button.Draw(targets);
            }
        }
    }
}