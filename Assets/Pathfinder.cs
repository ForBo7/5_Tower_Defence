using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startingPoint;
    [SerializeField] Waypoint endingPoint;

    bool isExecuting = true;

    Waypoint searchCenter;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();

    List<Waypoint> path = new List<Waypoint>();

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right,
        new Vector2Int(1, 1), // up right
        new Vector2Int(-1, 1), // up left
        new Vector2Int(-1, 1), // down right
        new Vector2Int(-1, -1) // down left
    };

    public List<Waypoint> GetPath()
    {
        LoadBlocks();
        BreadthFirstSearch();
        FormPath();
        return path;
    }



    private void FormPath()
    {
        path.Add(endingPoint);
        Waypoint previousPoint = endingPoint.exploredFrom;
        while (previousPoint != startingPoint)
        {
            path.Add(previousPoint);
            previousPoint = previousPoint.exploredFrom;
        }
        path.Add(startingPoint);
        path.Reverse();
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startingPoint);

        while(queue.Count > 0 && isExecuting)
        {
            searchCenter = queue.Dequeue();
            HaltIfEndFound();
            ExploreNeighbors();
            searchCenter.isExplored = true;
        }
    }

    private void HaltIfEndFound()
    {
        if (searchCenter == endingPoint)
        {
            isExecuting = false;
            print("Stopping search. Start and end points are same.");
        }
    }

    private void ExploreNeighbors()
    {
        if (!isExecuting)
        {
            return; // since the while loop only checks it condintion once it loops back to the very top.
        }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = searchCenter.GetGridPosition() + direction;
            if(grid.ContainsKey(neighborCoords))
            {
                QueueNewNeighbors(neighborCoords);
            }
        }
    }

    private void QueueNewNeighbors(Vector2Int neighborCoords)
    {
        Waypoint neighbor = grid[neighborCoords];
        if (!neighbor.isExplored && !queue.Contains(neighbor))
        {
            queue.Enqueue(neighbor);
            neighbor.exploredFrom = searchCenter;
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
