using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3Int playerStartPosition;
    public SpriteRenderer spriteRenderer;
    public Sprite openedSprite;
    public Sprite closedSprite;

    Vector3Int _gridPosition;
    public Vector3Int GridPosition => _gridPosition;

    public bool IsOpened { get; private set; }

    public void Start()
    {
        _gridPosition = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
    }

    public void Initialize()
    {
        SetOpenState(false);
    }

    public void SetOpenState(bool state)
    {
        IsOpened = state;
        spriteRenderer.sprite = IsOpened ? openedSprite : closedSprite;
    }
}
