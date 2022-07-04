using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectInfo)), CanEditMultipleObjects]
public class ObjectInfoEditor : Editor
{
    public SerializedProperty info;
     void OnEnable () {
         info = serializedObject.FindProperty("info");
     }
     
     public override void OnInspectorGUI() {
         serializedObject.Update ();
         info.stringValue = EditorGUILayout.TextArea(info.stringValue, GUILayout.MaxHeight(200) );
         serializedObject.ApplyModifiedProperties ();
     }
}
