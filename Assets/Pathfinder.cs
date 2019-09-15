using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startingPoint;
    [SerializeField] Waypoint endingPoint;

    bool isExecuting = true;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right,
    };

    // Start is called before the first frame update
    void Start()
    {
        LoadBlocks();
        Pathfind();
    }

    private void Pathfind()
    {
        queue.Enqueue(startingPoint);

        while(queue.Count > 0 && isExecuting)
        {
            var searchCenter = queue.Dequeue();
            print("Searching from" + searchCenter.name);
            HaltIfEndFound(searchCenter);
            ExploreNeighbors(searchCenter);
            searchCenter.isExplored = true;
        }
        print("Pathfinding finished?");
    }

    private void HaltIfEndFound(Waypoint searchCenter)
    {
        if (searchCenter == endingPoint)
        {
            isExecuting = false;
            print("Stopping search. Start and end points are same.");
        }
    }

    private void ExploreNeighbors(Waypoint searchCenter)
    {
        if (!isExecuting)
        {
            return; // since the while loop only checks it condintion once it loops back to the very top.
        }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = searchCenter.GetGridPosition() + direction;
            try
            {
                QueueNewNeighbors(neighborCoords);
            }
            catch
            {
                // do nothing for now
            }
        }
    }

    private void QueueNewNeighbors(Vector2Int neighborCoords)
    {
        Waypoint neighbor = grid[neighborCoords];
        if (!neighbor.isExplored)
        {
            grid[neighborCoords].SetTopColor(Color.magenta);
            queue.Enqueue(neighbor);
            print("Queueing " + neighbor);
        }
        
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
