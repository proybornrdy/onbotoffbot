using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CreateFloorBlocks))]
public class CreateFloorBlocksEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CreateFloorBlocks myTarget = (CreateFloorBlocks)target;
        EditorGUI.LabelField(new Rect(3, 3, 10, 20), "Plane to convert");
        myTarget.plane = (GameObject)EditorGUILayout.ObjectField((Object)myTarget.plane, typeof(GameObject), true);
        myTarget.floorBlock = (GameObject)EditorGUILayout.ObjectField((Object)myTarget.floorBlock, typeof(GameObject), true);
        if (GUILayout.Button("Go"))
        {
            myTarget.Generate();
        }
    }
}


