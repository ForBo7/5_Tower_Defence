using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class SnapEditor : MonoBehaviour
{
    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        SnapWaypointToGrid();
        LabelWaypoint();
    }

    private void SnapWaypointToGrid()
    {
        int gridSize = waypoint.GetGridSize();

        transform.position = new Vector3(waypoint.GetGridPosition().x, 0f, waypoint.GetGridPosition().y);
    }

    private void LabelWaypoint()
    {
        TextMesh textMesh;
        int gridSize = waypoint.GetGridSize();
        textMesh = GetComponentInChildren<TextMesh>();
        string cubeLabel = "(" + waypoint.GetGridPosition().x / gridSize + ", " + waypoint.GetGridPosition().y / gridSize + ")";
        textMesh.text = cubeLabel;
        gameObject.name = "Cube " + cubeLabel;
    }
}
