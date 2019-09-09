using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SnapEditor : MonoBehaviour
{
    [SerializeField] [Range(1f, 20f)] int gridSize;

    void Update()
    {
        float gridSizeF = (float)gridSize;
        Vector3 snapPos;
        snapPos.x = Mathf.RoundToInt(transform.position.x / gridSizeF) * gridSizeF;
        snapPos.z = Mathf.RoundToInt(transform.position.z / gridSizeF) * gridSizeF;

        transform.position = new Vector3(snapPos.x, 0f, snapPos.z);
    }
}
