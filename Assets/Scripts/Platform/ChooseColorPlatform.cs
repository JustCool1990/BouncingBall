using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChooseColorPlatform : Platform
{
    [SerializeField] private List<ColorAtribute> _colorAtributes;

    private ColoredCircle[] _colorCubes;

    public event UnityAction EnteredSphere;

    private void Awake()
    {
        _colorCubes = GetComponentsInChildren<ColoredCircle>();
        ShuffleColorAtributes();
    }

    protected override void OnInitedEnable()
    {
        Sphere.ColorChanged += OnColorChanged;
        RoadGenerator.ChooseColorPlatformShowed += OnChooseColorPlatformShowed;
        RoadGenerator.ChooseColorPlatformHided += OnChooseColorPlatformHided;
    }

    protected override void OnInitedDisable()
    {
        Sphere.ColorChanged -= OnColorChanged;
        RoadGenerator.ChooseColorPlatformShowed -= OnChooseColorPlatformShowed;
        RoadGenerator.ChooseColorPlatformHided -= OnChooseColorPlatformHided;
    }

    public void SetCirclesColorAtributes()
    {
        for (int i = 0; i < _colorCubes.Length; i++)
        {
            _colorCubes[i].SetColorAtribute(_colorAtributes[i]);
        }
    }

    private void OnColorChanged()
    {
        SetActiveCircles(false);
    }

    private void SetActiveCircles(bool value)
    {
        foreach (var cube in _colorCubes)
        {
            cube.gameObject.SetActive(value);
        }
    }

    private void OnChooseColorPlatformShowed()
    {
        SetActiveCircles(true);
        SetCirclesColorAtributes();
    }

    private void OnChooseColorPlatformHided()
    {
        ShuffleColorAtributes();
    }

    private void ShuffleColorAtributes()
    {
        for (int i = _colorAtributes.Count - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i);

            var tmp = _colorAtributes[j];
            _colorAtributes[j] = _colorAtributes[i];
            _colorAtributes[i] = tmp;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Sphere sphere))
        {
            EnteredSphere?.Invoke();
        }
    }
}
