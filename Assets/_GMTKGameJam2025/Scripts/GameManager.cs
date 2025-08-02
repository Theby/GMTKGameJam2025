using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public List<Stage> stages;
    public GridManager gridManager;
    public Player player;

    int _currentStageIndex = 0;
    int _currentLoopIndex = 0;
    
    InputAction _resetInput;

    void Awake()
    {
        ShowStage(_currentStageIndex, _currentLoopIndex);

        player.OnExitRoom += ExitRoomHandler;
        gridManager.OnGridCompleted += GridCompletedHandler;
        
        _resetInput = InputSystem.actions.FindAction("Reset");
        _resetInput.started += OnResetHandler;
    }

    void ShowStage(int stageIndex, int loopIndex)
    {
        HideAllStages();
        var stage = stages[stageIndex];
        stage.gameObject.SetActive(true);

        stage.Initialize(loopIndex);
        gridManager.Initialize(stage, loopIndex);

        player.Initialize(gridManager.enterDoor.playerStartPosition);
    }

    void HideAllStages()
    {
        foreach (var stage in stages)
        {
            stage.gameObject.SetActive(false);
        }
    }

    void RestartStage()
    {
        var stage = stages[_currentStageIndex];
        stage.exitDoor.SetOpenState(false);

        gridManager.Reset();
        player.Initialize(gridManager.enterDoor.playerStartPosition);
    }

    void GridCompletedHandler()
    {
        var stage = stages[_currentStageIndex];
        stage.exitDoor.SetOpenState(true);
    }

    void ExitRoomHandler()
    {
        _currentStageIndex++;
        _currentStageIndex %= stages.Count;
        _currentLoopIndex = _currentStageIndex == 0 ? _currentLoopIndex + 1 : _currentLoopIndex;

        Debug.Log($"{_currentStageIndex}_{_currentLoopIndex}");
        ShowStage(_currentStageIndex, _currentLoopIndex);
    }

    void OnResetHandler(InputAction.CallbackContext obj)
    {
        RestartStage();
    }
}