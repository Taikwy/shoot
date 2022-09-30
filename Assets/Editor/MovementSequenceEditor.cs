using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MovementSequence), true)]
public class MovementSequenceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MovementSequence sequence = (MovementSequence)target;
        EditorGUILayout.LabelField("Sequence Buttons", EditorStyles.boldLabel);
        if(GUILayout.Button("Set component refs"))
        {
            sequence.SetComponentsEditor();
        }
    }
}