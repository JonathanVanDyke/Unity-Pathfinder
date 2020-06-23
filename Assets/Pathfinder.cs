using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

    [SerializeField] Waypoint start, finish;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;
    Waypoint searchCenter;
    List<Waypoint> path = new List<Waypoint>(); 

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            CalculatePath();
        }
        return path;
    }

    private void CalculatePath()
    {
        LoadBlocks();
        SetStartAndEndColor();
        BreadthFirstSearch();
        CreatePath();
    }

    private void CreatePath()
    {
        path.Add(finish);

        Waypoint previous = finish.exploredFrom;
        while (previous != start)
        {
            path.Add(previous);
            previous = previous.exploredFrom;
        }

        // add start waypint
        path.Add(start);
        // reverse the list
        path.Reverse();
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(start);
        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            StopIfEndFound();
            ExploreNeighbors();
            searchCenter.isExplored = true;
        }
        print("Finish pathfinding");
    }

    private void StopIfEndFound()
    {
        if (searchCenter == finish)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbors()
    {
        if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
            if (grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];

        if (!neighbour.isExplored || queue.Contains(neighbour))
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
        }
    }

    private void SetStartAndEndColor()
    {
        start.SetColorTop(Color.green);
        finish.SetColorTop(Color.red);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if (!grid.ContainsKey(gridPos))
            {
                grid.Add(waypoint.GetGridPos(), waypoint);
            }
            else
            {
                Debug.LogWarning("Overlapping block " + waypoint);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
