using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class SnapEditor : MonoBehaviour
{
    [SerializeField] [Range(1f, 20f)] int gridSize;

    TextMesh textMesh;

    void Update()
    {
        Vector3 snapPos;
        float gridSizeF = (float)gridSize;

        snapPos.x = Mathf.RoundToInt(transform.position.x / gridSizeF) * gridSizeF;
        snapPos.z = Mathf.RoundToInt(transform.position.z / gridSizeF) * gridSizeF;

        transform.position = new Vector3(snapPos.x, 0f, snapPos.z);

        textMesh = GetComponentInChildren<TextMesh>();

        string cubeLabel = "(" + snapPos.x / gridSizeF + ", " + snapPos.z / gridSizeF + ")";
        textMesh.text = cubeLabel;

        gameObject.name = "Cube " + cubeLabel;
    }
}
