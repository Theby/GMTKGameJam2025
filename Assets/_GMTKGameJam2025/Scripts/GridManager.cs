using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap wallTilemap;
    public Tilemap goalTilemap;
    public List<Box> boxes;

    public int goals;

    public bool IsCompleted { get; private set; }

    public event Action OnGridCompleted;

    public void Initialize(Stage stage, int loopIndex)
    {
        wallTilemap = stage.wallTilemaps[loopIndex];
        goalTilemap = stage.goalTilemaps[loopIndex];

        boxes = new List<Box>();
        for (int i = 0; i < loopIndex + 1; i++)
        {
            var stageBoxes = stage.boxes[i].GetComponentsInChildren<Box>().ToList();
            boxes.AddRange(stageBoxes);
        }

        goals = stage.goals[loopIndex];
        IsCompleted = false;
    }

    public bool IsWall(Vector3Int position)
    {
        return wallTilemap.HasTile(position);
    }

    public bool IsGoal(Vector3Int position)
    {
        return goalTilemap.HasTile(position);
    }

    public Box TryGetBox(Vector3Int position)
    {
        var box = boxes.FirstOrDefault(b => b.transform.position == position);
        return box;
    }

    public bool CheckCompleted()
    {
        int inGoal = 0;
        foreach (var box in boxes)
        {
            var isGoal = IsGoal(box.GridPosition);
            if (!isGoal) 
                continue;
            
            inGoal++;
        }

        IsCompleted = inGoal == goals;

        if (IsCompleted)
        {
            OnGridCompleted?.Invoke();
        }

        return IsCompleted;
    }
}
