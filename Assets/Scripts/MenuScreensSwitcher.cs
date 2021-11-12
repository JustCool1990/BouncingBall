using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class MenuScreensSwitcher : MonoBehaviour
{
    [SerializeField] private MainMenuScreen _mainMenuScreen;
    [SerializeField] private AutorsScreen _autorsScreen;
    [SerializeField] private ExitGameScreen _exitGameScreen;

    private void Awake()
    {
        _mainMenuScreen.Open();
    }

    private void OnEnable()
    {
        _mainMenuScreen.StartGameButtonClick += OnStartGameButtonClick;
        _mainMenuScreen.AutorsScreenButtonClick += OnAutorsScreenButtonClick;
        _mainMenuScreen.ExitScreenButtonClick += OnExitScreenButtonClick;
        _autorsScreen.BackMainMenuScreenButtonClick += OnBackMainMenuScreenButtonClick;
        _exitGameScreen.ResumeButtonClick += OnResumeButtonClick;
        _exitGameScreen.ExitGameButtonClick += OnExitGameButtonClick;
    }

    private void OnDisable()
    {
        _mainMenuScreen.StartGameButtonClick -= OnStartGameButtonClick;
        _mainMenuScreen.AutorsScreenButtonClick -= OnAutorsScreenButtonClick;
        _mainMenuScreen.ExitScreenButtonClick -= OnExitScreenButtonClick;
        _autorsScreen.BackMainMenuScreenButtonClick -= OnBackMainMenuScreenButtonClick;
        _exitGameScreen.ResumeButtonClick -= OnResumeButtonClick;
        _exitGameScreen.ExitGameButtonClick -= OnExitGameButtonClick;
    }

    private void OnStartGameButtonClick()
    {
        _mainMenuScreen.Close();

        Game.Load();
    }

    private void OnAutorsScreenButtonClick()
    {
        _mainMenuScreen.Close();
        _autorsScreen.Open();
    }

    private void OnExitScreenButtonClick()
    {
        _mainMenuScreen.Close();
        _exitGameScreen.Open();
    }

    private void OnBackMainMenuScreenButtonClick()
    {
        _autorsScreen.Close();
        _mainMenuScreen.Open();
    }

    private void OnResumeButtonClick()
    {
        _exitGameScreen.Close();
        _mainMenuScreen.Open();
    }

    private void OnExitGameButtonClick()
    {
        Application.Quit();
    }
}
