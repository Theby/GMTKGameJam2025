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
    public Door enterDoor;
    public Door exitDoor;

    public int goals;

    public bool IsCompleted { get; private set; }

    public event Action OnGridCompleted;
    
    List<Vector3Int> boxInitialPositions = new List<Vector3Int>();

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

        InitializeBoxes();

        goals = stage.goals[loopIndex];

        enterDoor = stage.enterDoor;
        exitDoor = stage.exitDoor;

        IsCompleted = false;
    }

    void InitializeBoxes()
    {
        boxInitialPositions = new List<Vector3Int>();
        foreach (var box in boxes)
        {
            box.Initialize();
            boxInitialPositions.Add(box.GridPosition);
        }
    }

    public void Reset()
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            var box = boxes[i];
            var boxInitialPosition = boxInitialPositions[i];
            
            box.SetPosition(boxInitialPosition);
        }
        
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

    public bool IsDoor(Vector3Int position)
    {
        return enterDoor.transform.position == position
               || IsExitDoor(position);
    }

    public bool IsExitDoor(Vector3Int position)
    {
        return exitDoor.transform.position == position;
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

        bool isCompleted = inGoal == goals;

        if (!IsCompleted && isCompleted)
        {
            OnGridCompleted?.Invoke();
        }

        IsCompleted = isCompleted;
        return IsCompleted;
    }
}
