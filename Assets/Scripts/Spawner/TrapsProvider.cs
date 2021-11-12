using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrapsProvider : MonoBehaviour
{
    [SerializeField] private GameModeSwitcher _gameModeSwitcher;
    [SerializeField] private List<Obstacle> _trapPrefabs;
    [SerializeField] private int _capacity;
    [SerializeField] private Transform _container;

    private Vector3 _disablePosition = new Vector3(-0.5f, 0);
    private PlatformGenerator _roadGenerator;
    private Camera _camera;
    private List<Obstacle> _pool = new List<Obstacle>();

    private void Awake()
    {
        _roadGenerator = GetComponentInParent<PlatformGenerator>();
        Initialize();
    }

    private void Initialize()
    {
        _camera = Camera.main;

        for (int i = 0; i < _trapPrefabs.Count; i++)
        {
            for (int j = 0; j < _capacity; j++)
            {
                Obstacle trap = Instantiate(_trapPrefabs[i], _container.transform);
                trap.gameObject.SetActive(false);
                _pool.Add(trap);
            }
        }
    }

    private void OnEnable()
    {
        _gameModeSwitcher.GameBegin += OnGameBegin;
        _roadGenerator.ChooseColorPlatformShowed += OnChooseColorPlatformSpawned;
    }

    private void OnDisable()
    {
        _gameModeSwitcher.GameBegin -= OnGameBegin;
        _roadGenerator.ChooseColorPlatformShowed -= OnChooseColorPlatformSpawned;
    }

    public bool TryGetObject(out Obstacle result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return result != null;
    }

    public void DisableObjectAbroadScreen()
    {
        Vector3 disablePoint = _camera.ViewportToWorldPoint(_disablePosition);

        foreach (var trap in _pool)
        {
            if (CheckActiveTrap(trap))
            {
                DisableObject(trap, disablePoint);
            }
        }
    }

    private void OnGameBegin()
    {
        foreach (var trap in _pool)
        {
            if (CheckActiveTrap(trap))
            {
                DisableObject(trap);
            }
        }

        ShuffleTrapsPool();
    }

    private void OnChooseColorPlatformSpawned()
    {
        ShuffleTrapsPool();
    }

    private bool CheckActiveTrap(Obstacle obstacle)
    {
        return obstacle.gameObject.activeSelf == true;
    }

    private void DisableObject(Obstacle obstacle, Vector2 disablePoint)
    {
        if (Mathf.Abs(obstacle.transform.position.x) < disablePoint.x)
            obstacle.gameObject.SetActive(false);
    }
    
    private void DisableObject(Obstacle obstacle)
    {
        obstacle.gameObject.SetActive(false);
    }

    private void ShuffleTrapsPool()
    {
        for (int i = _pool.Count - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i);

            var tmp = _pool[j];
            _pool[j] = _pool[i];
            _pool[i] = tmp;
        }
    }
}
