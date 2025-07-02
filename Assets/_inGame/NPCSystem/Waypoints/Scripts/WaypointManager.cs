using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance { get; private set; }

    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Transform exitPoint;

    [Header("Gizmo Settings")]
    [SerializeField] private Color waypointColor = Color.blue;
    [SerializeField] private float waypointSphereRadius = 0.25f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public bool IsOnNavMesh(Vector3 position)
    {
        NavMeshHit hit;
        return NavMesh.SamplePosition(position, out hit, 1f, NavMesh.AllAreas);
    }
    public Transform GetRandomWaypoint()
    {
        if (waypoints == null || waypoints.Length == 0) return null;
        return waypoints[Random.Range(0, waypoints.Length)];
    }

    public Transform GetExitPoint() => exitPoint;

    private void OnDrawGizmos()
    {
        
        if (waypoints != null)
        {
            Gizmos.color = waypointColor;
            foreach (Transform waypoint in waypoints)
            {
                if (waypoint != null)
                {
                    Gizmos.DrawSphere(waypoint.position, waypointSphereRadius);
                }
            }
        }

        
        if (exitPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(exitPoint.position, waypointSphereRadius * 1.5f); 
            Gizmos.DrawLine(exitPoint.position, exitPoint.position + Vector3.up * 0.5f); 
        }
    }
}

