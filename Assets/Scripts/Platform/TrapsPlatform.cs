using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsPlatform : Platform
{
    [SerializeField] private Transform[] _trapPositions;

    private bool _spawnTrapsOnceTime = true;

    protected override void OnInitedEnable()
    {
        RoadGenerator.TrapPlatformShowed += OnTrapPlatformSpawned;
        RoadGenerator.TrapPlatformHided += OnTrapPlatformHided;
    }

    protected override void OnInitedDisable()
    {
        RoadGenerator.TrapPlatformShowed -= OnTrapPlatformSpawned;
        RoadGenerator.TrapPlatformHided -= OnTrapPlatformHided;

        _spawnTrapsOnceTime = true;
    }

    private void SetTrapsPositions()
    {
        for (int i = 0; i < _trapPositions.Length; i++)
        {
            if (TrapsProvider.TryGetObject(out Obstacle result))
            {
                result.transform.position = _trapPositions[i].transform.position;
                result.gameObject.SetActive(true);
            }
        }
    }

    private void OnTrapPlatformSpawned()
    {
        if(_spawnTrapsOnceTime == true)
        {
            _spawnTrapsOnceTime = false;
            SetTrapsPositions();
        }
    }

    private void OnTrapPlatformHided()
    {
        if (_spawnTrapsOnceTime == false)
        {
            DesableTraps();
        }
    }

    private void DesableTraps()
    {
        TrapsProvider.DisableObjectAbroadScreen();
    }
}
