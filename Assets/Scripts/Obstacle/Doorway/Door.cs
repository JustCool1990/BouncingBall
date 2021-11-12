using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Door : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _openRotatePosition;

    private MeshRenderer _meshRenderer;
    private Quaternion _endRotation;
    private Quaternion _startRotation = Quaternion.Euler(Vector3.zero);
    private Doorway _doorway;
    private IEnumerator _coroutine;
    private bool _coroutineIsActive = false;

    private void Awake()
    {
        _doorway = GetComponentInParent<Doorway>();
        _meshRenderer = GetComponent<MeshRenderer>();

        _endRotation = Quaternion.Euler(_openRotatePosition);
    }

    private void OnEnable()
    {
        _doorway.Opening += OnOpening;
        _doorway.MaterialSelected += OnMaterialSelected;
    }

    private void OnDisable()
    {
        _doorway.Opening -= OnOpening;
        _doorway.MaterialSelected -= OnMaterialSelected;

        if (_coroutineIsActive)
        {
            StopCoroutine(_coroutine);

            _coroutineIsActive = false;
        }

        ResetRotation();
    }

    public void OnMaterialSelected(ColorAtribute colorAtribute)
    {
        _meshRenderer.material = colorAtribute.Material;
    }

    private void OnOpening()
    {
        _coroutine = Open();
        StartCoroutine(_coroutine);
    }

    private IEnumerator Open()
    {
        _coroutineIsActive = true;

        while (transform.localRotation != _endRotation)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _endRotation, _speed * Time.deltaTime);
            yield return null;
        }

        _coroutineIsActive = false;
    }

    private void ResetRotation()
    {
        if(transform.localRotation != _startRotation)
            transform.localRotation = _startRotation;
    }
}
