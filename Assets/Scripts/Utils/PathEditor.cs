using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathBehavior))]
public class PathEditor : Editor
{
    // PathBehavior pathBehavior{
    //     get {return target as PathBehavior;}
    // }
    PathBehavior pathBehavior => target as PathBehavior;
    

    private void OnSceneGUI()
    {
        // Add handles and a visible index to each waypoint
        // Remember need to add position as offset so they move when PathObject moves
        for(int i = 0; i < pathBehavior.Waypoints.Length; i++)
        {
            EditorGUI.BeginChangeCheck();
            
            // Adding handle
            Vector3 currentPointPos = pathBehavior.Waypoints[i] + pathBehavior.transform.position;
            Vector3 newPointPos = Handles.FreeMoveHandle(position:currentPointPos, 
                size:0.7f, snap:new Vector3(0.25f, 0.25f, 0.25f), capFunction: Handles.SphereHandleCap);
            
            // Adding visible index
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.fontSize = 16;
            textStyle.normal.textColor = Color.green;
            Vector3 textAllignment = new Vector3(0.45f, -0.45f, 0f);
            Handles.Label(position:pathBehavior.Waypoints[i] + pathBehavior.transform.position + textAllignment,
                text:$"{i}", style: textStyle);

            EditorGUI.EndChangeCheck();

            if(EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(objectToUndo: pathBehavior, name:"Movable Handles");
                pathBehavior.Waypoints[i] = newPointPos - pathBehavior.transform.position;
            }
        }
    }
}
