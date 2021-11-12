using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuScreen : Screen
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _autorsScreenButton;
    [SerializeField] private Button _exitGameButton;

    public event UnityAction StartGameButtonClick;
    public event UnityAction AutorsScreenButtonClick;
    public event UnityAction ExitScreenButtonClick;

    private void OnEnable()
    {
        _startGameButton.onClick.AddListener(StartGame);
        _autorsScreenButton.onClick.AddListener(OpenAutorsScreen);
        _exitGameButton.onClick.AddListener(ExitGame);
    }

    private void OnDisable()
    {
        _startGameButton.onClick.RemoveListener(StartGame);
        _autorsScreenButton.onClick.RemoveListener(OpenAutorsScreen);
        _exitGameButton.onClick.RemoveListener(ExitGame);
    }

    private void StartGame()
    {
        StartGameButtonClick?.Invoke();
    }

    private void OpenAutorsScreen()
    {
        AutorsScreenButtonClick?.Invoke();
    }

    private void ExitGame()
    {
        ExitScreenButtonClick?.Invoke();
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
}
