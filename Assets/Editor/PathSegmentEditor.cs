using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PathSegment), true)]
public class PathSegmentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PathSegment pathScript = (PathSegment)target;
        // Undo.RecordObject(pathScript.gameObject, "Made some changes to myObject");

        EditorGUILayout.LabelField("Path Segment Buttons", EditorStyles.boldLabel);
        if(GUILayout.Button("Populate Points (for curves)"))
        {
            pathScript.PopulatePointsEditor();
        }
        if(GUILayout.Button("Clear Points (for curves)"))
        {
            pathScript.ResetPointsEditor();
        }
        if(GUILayout.Button("Toggle Point Visibility"))
        {
            pathScript.TogglePoints();
        }
        // PrefabUtility.RecordPrefabInstancePropertyModifications(pathScript.gameObject);
    }
}