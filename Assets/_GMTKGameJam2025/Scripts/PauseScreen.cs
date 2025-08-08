using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScreen : MonoBehaviour
{
    public RectTransform selector;
    public List<RectTransform> buttonsSelectPosition;

    public event Action OnContinue;
    public event Action OnControls;
    public event Action OnCredits;
    public event Action OnExit;

    int _selectorIndex;

    InputAction _moveAction;
    InputAction _selectAction;

    void Awake()
    {
        _moveAction = InputSystem.actions.FindAction("MenuMove");
        _moveAction.started += Move;

        _selectAction = InputSystem.actions.FindAction("Select");
        _selectAction.started += Select;
    }

    void OnEnable()
    {
        _moveAction.Enable();
        _selectAction.Enable();
    }

    void OnDisable()
    {
        _moveAction.Disable();
        _selectAction.Disable();
    }

    public void Initialize()
    {
        _selectorIndex = 0;
        _selectorIndex = SetSelector(_selectorIndex);
    }

    void Move(InputAction.CallbackContext ctx)
    {
        Vector2 moveValue = ctx.ReadValue<Vector2>();
        if (moveValue.y > 0.0f)
        {
            _selectorIndex--;
            AudioManager.instance.MoveUISfx();
        }
        else if (moveValue.y < 0.0f)
        {
            _selectorIndex++;
            AudioManager.instance.MoveUISfx();
        }

        _selectorIndex = SetSelector(_selectorIndex);
    }

    int SetSelector(int index)
    {
        index = Mathf.Clamp(index, 0, buttonsSelectPosition.Count - 1);
        selector.position = buttonsSelectPosition[index].position;

        return index;
    }

    void Select(InputAction.CallbackContext ctx)
    {
        switch (_selectorIndex)
        {
            case 0:
                OnContinue?.Invoke();
                break;
            case 1:
                OnControls?.Invoke();
                break;
            case 2:
                OnCredits?.Invoke();
                break;
            case 3:
                OnExit?.Invoke();
                break;
        }

        AudioManager.instance.PressUISfx();
    }
}
