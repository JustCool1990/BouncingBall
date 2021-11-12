using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinsPlatform : Platform
{
    [SerializeField] private float _leftOffsetPosition;
    [SerializeField] private float _rightOffsetPosition;


    private CoinsContainer[] _coinsContainers;
    private bool _spawnCoinsOnceTime = true;

    private void Awake()
    {
        _coinsContainers = GetComponentsInChildren<CoinsContainer>();
    }

    protected override void OnInitedEnable()
    {
        RoadGenerator.CoinsPlatformShowed += OnCoinsPlatformSpawned;
    }

    protected override void OnInitedDisable()
    {
        RoadGenerator.CoinsPlatformShowed -= OnCoinsPlatformSpawned;

        _spawnCoinsOnceTime = true;
    }

    private void OnCoinsPlatformSpawned()
    {
        if (_spawnCoinsOnceTime == true)
        {
            _spawnCoinsOnceTime = false;
            SetCoinsContainersOnPositions();
            SetCoinContainersColorAtributes();
        }
    }

    private void SetCoinsContainersOnPositions()
    {
        for (int i = 0; i < _coinsContainers.Length; i++)
        {
            float randomPositionX = Random.Range(_leftOffsetPosition, _rightOffsetPosition);
            _coinsContainers[i].transform.localPosition = new Vector3(randomPositionX, _coinsContainers[i].transform.localPosition.y, _coinsContainers[i].transform.localPosition.z);
        }
    }

    private void SetCoinContainersColorAtributes()
    {
        for (int i = 0; i < _coinsContainers.Length; i++)
        {
            _coinsContainers[i].ShuffleColorAtributes();
            _coinsContainers[i].SetCoinsColorAtributes();
        }
    }
}
