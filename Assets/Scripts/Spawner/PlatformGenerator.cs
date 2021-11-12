using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private GameModeSwitcher _gameModeSwitcher;
    [SerializeField] private Sphere _sphere;
    [SerializeField] private List<Platform> _platformPrefabs;
    [SerializeField] private int _chooseColorPlatformCount;
    [SerializeField] private Transform _platformsContainer;
    [SerializeField] private Transform _startRoad;

    private Vector3 _startRoadPosition;
    private TrapsProvider _trapsProvider;
    private Camera _camera;
    private List<Platform> _pool = new List<Platform>();
    private float _visibilityRange = 20;
    private int _platformCount = 0;
    private bool _gameStarted = false;
    private Vector3 _disablePosition = new Vector3(-0.6f, 0);

    public event UnityAction TrapPlatformShowed;
    public event UnityAction TrapPlatformHided;
    public event UnityAction ChooseColorPlatformShowed;
    public event UnityAction ChooseColorPlatformHided;
    public event UnityAction CoinsPlatformShowed;

    private void Awake()
    {
        _trapsProvider = GetComponentInChildren<TrapsProvider>();
        _startRoadPosition = _startRoad.position;
        Initialize();
    }

    private void Initialize()
    {
        _camera = Camera.main;

        for (int i = 0; i < _platformPrefabs.Count; i++)
        {
            for (int j = 0; j < _platformPrefabs[i].Capacity; j++)
            {
                Platform platform = Instantiate(_platformPrefabs[i], _platformsContainer.transform);
                platform.Init(_sphere, this, _trapsProvider);
                platform.gameObject.SetActive(false);

                _pool.Add(platform);
            }
        }
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

    private void Update()
    {
        if(_gameStarted == true)
            CheckingCoveredDistance();
    }

    private void OnGameBegin()
    {
        _startRoad.position = _startRoadPosition;
        _platformCount = 0;

        foreach (var platform in _pool)
        {
            if (CheckActivePlatform(platform))
            {
                DisableObject(platform);
            }
        }

        _gameStarted = true;
    }

    private void OnGameOver()
    {
        _gameStarted = false;
    }

    private void CheckingCoveredDistance()
    {
        if (_sphere.transform.position.x > (_startRoad.position.x - _visibilityRange))
        {
            SpawnPlatform();
            DisableObjectAbroadScreen();
        }
    }

    private void SpawnPlatform()
    {
        if(TryGetObject(out Platform result, ChoosePlatformType()))
        {
            result.transform.position = new Vector3(_startRoad.position.x - (result.PlatformStartPoint.localPosition.x * result.Ground.transform.localScale.x), transform.position.y, transform.position.z);
            result.gameObject.SetActive(true);

            PlatformSpawnNotify(result.PlatformType)?.Invoke();

            _startRoad.position = result.PlatformEndPoint.position;
            _platformCount++;
        }
    }

    private bool TryGetObject(out Platform result, PlatformType platformType)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false && p.PlatformType == platformType);
        
        return result != null;
    }

    private UnityAction PlatformSpawnNotify(PlatformType platformType)
    {
        switch(platformType)
        {
            case PlatformType.Traps: return TrapPlatformShowed;

            case PlatformType.ChooseColor: return ChooseColorPlatformShowed;

            case PlatformType.Coins: return CoinsPlatformShowed;

            default: return null;
        }
    }

    private UnityAction PlatformHideNotify(PlatformType platformType)
    {
        switch (platformType)
        {
            case PlatformType.Traps: return TrapPlatformHided;

            case PlatformType.ChooseColor: return ChooseColorPlatformHided;

            default: return null;
        }
    }

    private PlatformType ChoosePlatformType()
    {
        if (_platformCount == 0)
        {
            return PlatformType.Safe;
        }
        else if(_platformCount % _chooseColorPlatformCount == 0)
        {
            return PlatformType.ChooseColor;
        }
        else
        {
            return Random.Range(0, 2) == 0? PlatformType.Coins : PlatformType.Traps;
        }
    }

    private void DisableObjectAbroadScreen()
    {
        Vector3 disablePoint = _camera.ViewportToWorldPoint(_disablePosition);

        foreach (var platform in _pool)
        {
            if (CheckActivePlatform(platform))
            {
                DisableObject(platform, disablePoint);
            }
        }
    }

    private bool CheckActivePlatform(Platform platform)
    {
        return platform.gameObject.activeSelf == true;
    }

    private void DisableObject(Platform platform, Vector2 disablePoint)
    {
        if (Mathf.Abs(platform.transform.position.x) < disablePoint.x)
        {
            PlatformHideNotify(platform.PlatformType)?.Invoke();
            platform.gameObject.SetActive(false);
        }
    }

    private void DisableObject(Platform platform)
    {
        platform.gameObject.SetActive(false);
    }
}
