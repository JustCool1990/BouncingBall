using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameModeSwitcher : MonoBehaviour
{
    [SerializeField] private Sphere _sphere;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private GameInterface _gameInterface;

    private Vector3 _gravityVector = new Vector3(0, -25, 0);

    public event UnityAction GameBegin;
    public event UnityAction GameOver;

    private void Awake()
    {
        Physics.gravity = _gravityVector;
    }

    private void Start()
    {
        OnRestartGame();
    }

    private void OnEnable()
    {
        _sphere.Died += OnDied;
        _gameOverScreen.RestartGame += OnRestartGame;
    }

    private void OnDisable()
    {
        _sphere.Died -= OnDied;
        _gameOverScreen.RestartGame -= OnRestartGame;
    }

    private void OnDied()
    {
        _gameInterface.CloseInterface();
        _gameOverScreen.Open();

        GameOver?.Invoke();
    }

    private void OnRestartGame()
    {
        _gameOverScreen.Close();
        _gameInterface.OpenInterface();

        GameBegin?.Invoke();
    }
}
