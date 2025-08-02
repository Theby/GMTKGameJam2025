using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 0.2f;
    public GridManager gridManager;

    public event Action OnExitRoom;

    bool _isMoving;
    InputAction _moveAction;
    Vector3Int _gridPosition;

    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _moveAction.started += Move;
    }

    public void Initialize(Vector3Int startingPosition)
    {
        _gridPosition = startingPosition;
        transform.position = startingPosition;
    }

    void Move(InputAction.CallbackContext ctx)
    {
        if (_isMoving)
            return;

        Vector2 moveValue = ctx.ReadValue<Vector2>();
        Vector3Int direction = Vector3Int.zero;
        if (moveValue.y > 0.0f)
        {
            direction = Vector3Int.up;
        }
        else if (moveValue.y < 0.0f)
        {
            direction = Vector3Int.down;
        }
        else if (moveValue.x < 0.0f)
        {
            direction = Vector3Int.left;
        }
        else if (moveValue.x > 0.0f)
        {
            direction = Vector3Int.right;
        }

        if (direction != Vector3Int.zero)
        {
            TryMove(direction);
        }
    }

    void TryMove(Vector3Int direction)
    {
        var nextPosition = _gridPosition + direction;
        if (gridManager.IsWall(nextPosition))
            return;

        if (gridManager.IsDoor(nextPosition))
        {
            if (gridManager.IsExitDoor(nextPosition) && gridManager.exitDoor.IsOpened)
            {
                OnExitRoom?.Invoke();
            }

            return;
        }

        var box = gridManager.TryGetBox(nextPosition);
        var tween = box == null
            ? transform.DOMove(nextPosition, moveSpeed)
            : box.Move(direction);

        _gridPosition = box == null ? nextPosition : _gridPosition;
        _isMoving = tween != null;
        tween?.OnComplete(MovementCompletedHandler);
    }

    void MovementCompletedHandler()
    {
        gridManager.CheckCompleted();
        _isMoving = false;
    }
}