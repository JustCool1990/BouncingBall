using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ExitGameScreen : Screen
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _exitGameButton;

    public event UnityAction ResumeButtonClick;
    public event UnityAction ExitGameButtonClick;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(BackMainMenuScreen);
        _exitGameButton.onClick.AddListener(ExitGame);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(BackMainMenuScreen);
        _exitGameButton.onClick.RemoveListener(ExitGame);
    }

    public override void Close()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
    }

    public override void Open()
    {
        StartCoroutine(ChangeScreenAlpha());
    }

    private void BackMainMenuScreen()
    {
        ResumeButtonClick?.Invoke();
    }

    private void ExitGame()
    {
        ExitGameButtonClick?.Invoke();
    }
}
