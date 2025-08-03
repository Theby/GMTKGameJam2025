using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreditsScreen : MonoBehaviour
{
    InputAction _backAction;

    public event Action OnBack;

    void Awake()
    {
        _backAction = InputSystem.actions.FindAction("Back");
        _backAction.started += Back;
    }

    void OnEnable()
    {
        _backAction.Enable();
    }

    void OnDisable()
    {
        _backAction.Disable();
    }

    void Back(InputAction.CallbackContext ctx)
    {
        AudioManager.instance.BackUISfx();
        OnBack?.Invoke();
    }
}
