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

        transform.position = new Vector3(waypoint.GetGridPosition().x * gridSize, 0f, waypoint.GetGridPosition().y * gridSize);
    }

    private void LabelWaypoint()
    {
        TextMesh textMesh;
        textMesh = GetComponentInChildren<TextMesh>();
        string cubeLabel = "(" + waypoint.GetGridPosition().x+ ", " + waypoint.GetGridPosition().y+ ")";
        textMesh.text = cubeLabel;
        gameObject.name = "Cube " + cubeLabel;
    }
}
