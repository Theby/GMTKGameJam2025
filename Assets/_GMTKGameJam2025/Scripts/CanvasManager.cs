using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public PauseScreen pauseScreen;
    public YouWinScreen youWinScreen;
    public ControlsScreen controlsScreen;
    public CreditsScreen creditsScreen;
    public GameObject gameScreen;
    public Image resetStageImage;
    public TextMeshProUGUI resetStageText;
    public Image resetGameImage;
    public TextMeshProUGUI resetGameText;
    public TextMeshProUGUI stageCounter;

    public event Action OnResetStage;
    public event Action OnResetGame;

    Vector2 _targetSize;
    Sequence _resetStageSequence = null;
    Sequence _resetGameSequence = null;

    public void Awake()
    {
        pauseScreen.OnControls += ShowControlsScreen;
        pauseScreen.OnCredits += ShowCreditsScreen;
        controlsScreen.OnBack += ShowPauseScreen;
        creditsScreen.OnBack += ShowPauseScreen;
    }

    public void Initialize()
    {
        var resetStageInput = InputSystem.actions.FindAction("ResetStage");
        resetStageInput.started += ResetStageStarted;
        resetStageInput.canceled += ResetStageCanceled;

        var resetGameInput = InputSystem.actions.FindAction("ResetGame");
        resetGameInput.started += ResetGameStarted;
        resetGameInput.canceled += ResetGameCanceled;

        _targetSize = new Vector2(Screen.width, 100.0f);

        HideResetStageWidget();
        HideResetGameWidget();
    }

    public void ShowPauseScreen()
    {
        HideAllScreens();
        pauseScreen.gameObject.SetActive(true);
        pauseScreen.Initialize();
    }

    public void ShowYouWinScreen()
    {
        HideAllScreens();
        youWinScreen.gameObject.SetActive(true);
    }

    public void ShowControlsScreen()
    {
        HideAllScreens();
        controlsScreen.gameObject.SetActive(true);
    }

    public void ShowCreditsScreen()
    {
        HideAllScreens();
        creditsScreen.gameObject.SetActive(true);
    }

    public void ShowGameScreen()
    {
        HideAllScreens();
        gameScreen.SetActive(true);
    }

    public void UpdateStageUI(int stageIndex, int loopIndex)
    {
        stageCounter.SetText($"{stageIndex + 1}-{loopIndex + 1}");
    }

    void HideAllScreens()
    {
        pauseScreen.gameObject.SetActive(false);
        youWinScreen.gameObject.SetActive(false);
        controlsScreen.gameObject.SetActive(false);
        creditsScreen.gameObject.SetActive(false);
        gameScreen.SetActive(false);
    }

    void ResetStageStarted(InputAction.CallbackContext ctx)
    {
        if (!gameScreen.activeSelf)
        {
            return;
        }

        if (_resetGameSequence != null)
        {
            return;
        }

        _resetStageSequence?.Kill();

        resetStageImage.rectTransform.sizeDelta = new Vector2(0.0f, resetStageImage.rectTransform.sizeDelta.y);
        var color = resetStageImage.color;
        color.a = 0.0f;
        resetStageImage.color = color;

        _resetStageSequence = DOTween.Sequence();
        var sizeTween = resetStageImage.rectTransform.DOSizeDelta(_targetSize, 1.0f);
        var alphaTween = resetStageImage.DOFade(1.0f, 0.8f);

        _resetStageSequence.Insert(0, sizeTween);
        _resetStageSequence.Insert(0, alphaTween);

        _resetStageSequence.OnComplete(() =>
        {
            _resetStageSequence = null;
            HideResetStageWidget();
            OnResetStage?.Invoke();
        });

        resetStageText.gameObject.SetActive(true);
    }

    void ResetStageCanceled(InputAction.CallbackContext ctx)
    {
        _resetStageSequence?.Kill();
        _resetStageSequence = null;

        HideResetStageWidget();
    }

    void HideResetStageWidget()
    {
        resetStageImage.rectTransform.sizeDelta = new Vector2(0.0f, resetStageImage.rectTransform.sizeDelta.y);
        var color = resetStageImage.color;
        color.a = 0.0f;
        resetStageImage.color = color;

        resetStageText.gameObject.SetActive(false);
    }

    void ResetGameStarted(InputAction.CallbackContext ctx)
    {
        if (!gameScreen.activeSelf)
        {
            return;
        }

        if (_resetStageSequence != null)
        {
            return;
        }

        _resetGameSequence?.Kill();

        resetGameImage.rectTransform.sizeDelta = new Vector2(0.0f, resetGameImage.rectTransform.sizeDelta.y);
        var color = resetGameImage.color;
        color.a = 0.0f;
        resetGameImage.color = color;

        _resetGameSequence = DOTween.Sequence();
        var sizeTween = resetGameImage.rectTransform.DOSizeDelta(_targetSize, 1.0f);
        var alphaTween = resetGameImage.DOFade(1.0f, 0.8f);

        _resetGameSequence.Insert(0, sizeTween);
        _resetGameSequence.Insert(0, alphaTween);

        _resetGameSequence.OnComplete(() =>
        {
            _resetGameSequence = null;
            HideResetGameWidget();
            OnResetGame?.Invoke();
        });

        resetGameText.gameObject.SetActive(true);
    }

    void ResetGameCanceled(InputAction.CallbackContext ctx)
    {
        _resetGameSequence?.Kill();
        _resetGameSequence = null;

        HideResetGameWidget();
    }

    void HideResetGameWidget()
    {
        resetGameImage.rectTransform.sizeDelta = new Vector2(0.0f, resetGameImage.rectTransform.sizeDelta.y);
        var color = resetGameImage.color;
        color.a = 0.0f;
        resetGameImage.color = color;

        resetGameText.gameObject.SetActive(false);
    }
}
