using UnityEngine;
using UnityEngine.InputSystem;

public class YouWinScreen : MonoBehaviour
{
    InputAction _backAction;

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
        Application.Quit();
    }
}
