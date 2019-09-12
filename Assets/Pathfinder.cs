using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startingPoint;
    [SerializeField] Waypoint endingPoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    // Start is called before the first frame update
    void Start()
    {
        LoadBlocks();
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            bool isOverLapping = grid.ContainsKey(waypoint.GetGridPosition());
            if (isOverLapping)
            {
                Debug.LogWarning("Overlapping block being skipped at " + waypoint);
            }
            else
            {
                grid.Add(waypoint.GetGridPosition(), waypoint);
                ColorStartAndEndPoints();
            }
        }
    }

    private void ColorStartAndEndPoints()
    {
        startingPoint.SetTopColor(Color.grey);
        endingPoint.SetTopColor(Color.cyan);
    }
}
