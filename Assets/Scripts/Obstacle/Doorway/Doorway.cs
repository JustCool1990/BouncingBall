using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Doorway : MonoBehaviour
{
    [SerializeField] private List<ColorAtribute> _colorAtributes;

    private ChooseColorPlatform _chooseColorPlatform;
    private ColorAtribute _colorAtribute;

    public event UnityAction<ColorAtribute> MaterialSelected;
    public event UnityAction Opening;

    private void Awake()
    {
        _chooseColorPlatform = GetComponentInParent<ChooseColorPlatform>();
    }

    private void OnEnable()
    {
        _chooseColorPlatform.EnteredSphere += OnEnteredSphere;
    }

    private void OnDisable()
    {
        _chooseColorPlatform.EnteredSphere -= OnEnteredSphere;
    }

    private void OnEnteredSphere()
    {
        SelectColorAtribute();
    }

    private void SelectColorAtribute()
    {
        _colorAtribute = _colorAtributes[Random.Range(0, _colorAtributes.Count)];
        MaterialSelected?.Invoke(_colorAtribute);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Sphere sphere))
        {
            if(sphere.ColorType == _colorAtribute.ColorType)
            {
                Opening?.Invoke();
            }
        }
    }
}
