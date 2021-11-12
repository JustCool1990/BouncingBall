using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;
using UnityEngine.Events;

public class GameOverScreen : Screen
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _backMenuButton;

    public event UnityAction RestartGame;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(Restart);
        _backMenuButton.onClick.AddListener(ExitToMainMenu);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(Restart);
        _backMenuButton.onClick.RemoveListener(ExitToMainMenu);
    }

    private void Restart()
    {
        RestartGame?.Invoke();
    }

    private void ExitToMainMenu()
    {
        Menu.Load();
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
