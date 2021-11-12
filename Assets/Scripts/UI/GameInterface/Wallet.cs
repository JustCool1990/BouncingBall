using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private Sphere _sphere;
    [SerializeField] private TMP_Text _value;
    [SerializeField] private GameModeSwitcher _gameModeSwitcher;

    private int _wallet = 0;

    private void OnEnable()
    {
        _sphere.CoinCollected += OnCoinCollected;
        _gameModeSwitcher.GameBegin += OnGameBegin;
    }

    private void OnDisable()
    {
        _sphere.CoinCollected -= OnCoinCollected;
        _gameModeSwitcher.GameBegin -= OnGameBegin;
    }

    private void OnCoinCollected()
    {
        _wallet++;
        _value.text = _wallet.ToString();
    }

    private void OnGameBegin()
    {
        ResetCoins();
    }

    private void ResetCoins()
    {
        _wallet = 0;
        _value.text = _wallet.ToString();
    }
}
