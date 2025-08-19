using UnityEditor;
using UnityEngine;
using UnityGameLib.Reactive;

namespace UnityGameLib.Editor.Reactive
{
    [CustomPropertyDrawer(typeof(ReactiveProperty<int>))]
    public class ReactivePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty findPropertyRelative = property.FindPropertyRelative("_value");
            EditorGUI.PropertyField(position, findPropertyRelative, GUIContent.none);
        }
    }
}