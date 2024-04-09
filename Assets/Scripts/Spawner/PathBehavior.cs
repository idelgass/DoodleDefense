using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathBehavior : MonoBehaviour
{
    [SerializeField] private Vector3[] waypoints;
    public Vector3[] Waypoints => waypoints;


    public Vector3 GetWaypointPos(int index){
        // Don't want other classes to have to add the offset, do it here
        return transform.position + waypoints[index];
    }

    private void OnDrawGizmos()
    {
        // Draw wire sphere over each point and connect to next with a line
        // Adding transform.position so they move when PathObject moves
        for(int i = 0; i < waypoints.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(center:waypoints[i] + transform.position, radius:0.5f);

            if(i < waypoints.Length - 1)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(from:waypoints[i] + transform.position, to:waypoints[i + 1] + transform.position);
            }
        }
    }
}
