using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{

    TextMesh textMesh;
    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
        textMesh = GetComponentInChildren<TextMesh>();
    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
        transform.position = new Vector3(
            waypoint.GetGridPos().x, 
            0f,
            waypoint.GetGridPos().y
        );
    }

    void UpdateLabel()
    {
        int gridSize = waypoint.GetGridSize();
        string labelText = 
            waypoint.GetGridPos().x / gridSize + 
            "," + 
            waypoint.GetGridPos().y / gridSize;
        textMesh.text = labelText;
        gameObject.name = labelText;
    }
}
