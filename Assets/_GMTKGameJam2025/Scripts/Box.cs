using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class Box : MonoBehaviour
{
    public float moveSpeed = 0.2f;
    public GridManager gridManager;

    Vector3Int _gridPosition;
    public Vector3Int GridPosition => _gridPosition;

    public void Initialize()
    {
        var spawnPosition = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
        SetPosition(spawnPosition);
    }

    public void SetPosition(Vector3Int position)
    {
        _gridPosition = position;
        transform.position = _gridPosition;
    }

    public TweenerCore<Vector3, Vector3, VectorOptions> Move(Vector3Int direction)
    {
        if (gridManager.IsCompleted && gridManager.IsGoal(_gridPosition))
            return null;

        var nextPosition = _gridPosition + direction;
        if (gridManager.IsWall(nextPosition))
            return null;

        if (gridManager.IsDoor(nextPosition))
            return null;

        if (gridManager.TryGetBox(nextPosition) != null)
            return null;

        _gridPosition = nextPosition;
        return transform.DOMove(nextPosition, moveSpeed);
    }
}
