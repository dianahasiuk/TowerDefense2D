using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public static WaypointPath Instance;
    public Transform[] waypoints;

    void Awake() => Instance = this;

    public Transform GetWaypoint(int index) => waypoints[index];
    public int GetWaypointCount() => waypoints.Length;

    void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length < 2) return;
        Gizmos.color = Color.red;
        for (int i = 0; i < waypoints.Length - 1; i++)
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
    }
}