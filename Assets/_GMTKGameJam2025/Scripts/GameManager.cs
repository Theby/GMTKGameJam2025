using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Stage> stages;
    public GridManager gridManager;
    public Player player;

    int _currentStageIndex = 0;
    int _currentLoopIndex = 0;

    void Awake()
    {
        ShowStage(_currentStageIndex, _currentLoopIndex);

        gridManager.OnGridCompleted += GridCompletedHandler;
    }

    void ShowStage(int stageIndex, int loopIndex)
    {
        HideAllStages();
        var stage = stages[stageIndex];
        stage.gameObject.SetActive(true);

        stage.Initialize(loopIndex);
        gridManager.Initialize(stage, loopIndex);

        player.Initialize(Vector3Int.zero); //TODO give correct one
    }

    void HideAllStages()
    {
        foreach (var stage in stages)
        {
            stage.gameObject.SetActive(false);
        }
    }

    void GridCompletedHandler()
    {
        _currentStageIndex++;
        _currentStageIndex %= stages.Count;
        _currentLoopIndex = _currentStageIndex == 0 ? _currentLoopIndex + 1 : _currentLoopIndex;

        Debug.Log($"{_currentStageIndex}_{_currentLoopIndex}");
        ShowStage(_currentStageIndex, _currentLoopIndex);
    }
}