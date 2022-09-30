using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EnemyPath), true)]
public class EnemyPathEditor : Editor
{
    // SerializedObject path;

    // private void OnEnable(){
    //     path = serializedObject.FindProperty();
    // }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemyPath enemyPath = (EnemyPath)target;

        // EditorGUILayout.LabelField("Path Segment Buttons", EditorStyles.boldLabel);
        if(GUILayout.Button("Populate Segment List"))
        {
            enemyPath.PopulateSegmentsEditor();
        }
        if(GUILayout.Button("Mirror Path X"))
        {
            enemyPath.MirrorXEditor();
        }
        if(GUILayout.Button("Mirror Path Y"))
        {
            enemyPath.MirrorYEditor();
        }
    }
}