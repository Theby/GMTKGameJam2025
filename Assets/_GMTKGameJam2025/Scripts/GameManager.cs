using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Stage> stages;
    public GridManager gridManager;
    public Player player;
    public ScreenShake screenShake;
    public float shakeDuration;
    public float shakeMagnitude;
    public CanvasManager canvasManager;

    int _currentStageIndex = 0;
    int _currentLoopIndex = 0;

    bool _firstTime = true;

    void Awake()
    {
        AudioManager.instance.Initialize();

        canvasManager.Initialize();
        canvasManager.pauseScreen.OnContinue += ContinueHandler;
        canvasManager.pauseScreen.OnExit += ExitGameHandler;

        foreach (var stage in stages)
        {
            stage.SaveOriginalBoxPositions();
        }

        player.OnExitRoom += ExitRoomHandler;
        player.OnPause += PauseHandler;
        gridManager.OnGridCompleted += GridCompletedHandler;

        canvasManager.OnResetStage += OnResetStageHandler;
        canvasManager.OnResetGame += OnRestartGameHandler;

        ShowPauseScreen();
    }

    void ShowPauseScreen()
    {
        AudioManager.instance.PlayUIMusic();
        canvasManager.ShowPauseScreen();

        HideAllStages();
        player.gameObject.SetActive(false);
    }

    void ShowGameScreen()
    {
        AudioManager.instance.PlayGameMusic();
        canvasManager.ShowGameScreen();

        if (_firstTime)
        {
            _firstTime = false;
            ShowStage(_currentStageIndex, _currentLoopIndex);
        }
        else
        {
            var stage = stages[_currentStageIndex];
            stage.gameObject.SetActive(true);

            player.gameObject.SetActive(true);
        }
    }

    void ShowYouWinScreen()
    {
        HideAllStages();
        player.gameObject.SetActive(false);

        canvasManager.ShowYouWinScreen();
    }

    void ShowStage(int stageIndex, int loopIndex)
    {
        HideAllStages();
        player.gameObject.SetActive(true);

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

    void RestartGame()
    {
        _currentStageIndex = 0;
        _currentLoopIndex = 0;

        foreach (var stage in stages)
        {
            stage.ResetAllBoxes();
        }

        ShowStage(_currentStageIndex, _currentLoopIndex);
    }

    void GridCompletedHandler()
    {
        StartCoroutine(StageCompletedRoutine());
    }

    IEnumerator StageCompletedRoutine()
    {
        yield return new WaitForSeconds(0.3f);

        AudioManager.instance.PlayLevelCompleteSfx();

        yield return new WaitForSeconds(0.6f);

        var stage = stages[_currentStageIndex];
        stage.exitDoor.SetOpenState(true);

        AudioManager.instance.PlayDoorOpenSfx();
        screenShake.TriggerShake(shakeDuration, shakeMagnitude);
    }

    void ExitRoomHandler()
    {
        AudioManager.instance.LoadNewStageSfx();

        _currentStageIndex++;
        _currentStageIndex %= stages.Count;
        _currentLoopIndex = _currentStageIndex == 0 ? _currentLoopIndex + 1 : _currentLoopIndex;

        if (_currentLoopIndex >= 3)
        {
            ShowYouWinScreen();
        }
        else
        {
            ShowStage(_currentStageIndex, _currentLoopIndex);
        }
    }

    void OnResetStageHandler()
    {
        RestartStage();
    }

    void OnRestartGameHandler()
    {
        RestartGame();
    }

    void ContinueHandler()
    {
        ShowGameScreen();
    }

    void ExitGameHandler()
    {
        Application.Quit();
    }

    void PauseHandler()
    {
        ShowPauseScreen();
    }
}