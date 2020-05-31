﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class CubeEditor : MonoBehaviour
{
    [SerializeField][Range(1f, 20f)] float gridSize = 10;

    TextMesh textMesh;

    private void Start()
    {
        textMesh = GetComponentInChildren<TextMesh>();
    }

    void Update()
    {
        Vector3 snapPos;

        snapPos.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize; // round causes binary switch
        snapPos.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;

        textMesh.text = snapPos.x / gridSize + "," + snapPos.z / gridsize;

        transform.position = new Vector3(snapPos.x, 0f, snapPos.z);
    }
}