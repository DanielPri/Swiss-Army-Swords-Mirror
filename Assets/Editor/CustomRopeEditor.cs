using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GenerateRope))]
public class CustomRopeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("Click button after setting parameters", MessageType.Info);

        GenerateRope myScript = (GenerateRope)target;
        if (GUILayout.Button("Generate Rope"))
        {
            myScript.createRopesQty();
        }

        if (GUILayout.Button("Reset"))
        {
            myScript.Reset();
        }
    }
}

