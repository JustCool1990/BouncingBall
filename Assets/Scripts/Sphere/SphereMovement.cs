using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    [SerializeField] private GameModeSwitcher _gameModeSwitcher;
    [SerializeField] private float _speed;

    private Vector3 _startPosition;
    private bool _gameStarted = false;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void OnEnable()
    {
        _gameModeSwitcher.GameBegin += OnGameBegin;
        _gameModeSwitcher.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _gameModeSwitcher.GameBegin -= OnGameBegin;
        _gameModeSwitcher.GameOver -= OnGameOver;
    }

    private void FixedUpdate()
    {
        if(_gameStarted == true)
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }

    private void OnGameBegin()
    {
        transform.position = _startPosition;
        _gameStarted = true;
    }

    private void OnGameOver()
    {
        _gameStarted = false;
    }
}
