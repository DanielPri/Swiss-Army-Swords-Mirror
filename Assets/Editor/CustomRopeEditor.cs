using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GenerateRope))]
public class CustomRopeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("HOW TO USE: Place this script on an empty object, then give it a rope segment prefab. Indicate amount of segments you want and click generate.", MessageType.Info);
        DrawDefaultInspector();
        
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

