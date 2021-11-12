using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Sphere : MonoBehaviour
{
    [SerializeField] private GameModeSwitcher _gameModeSwitcher;

    private Collider _collider;
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private ColorType _colorType = ColorType.Green;
    private Material _standartMaterial;

    public ColorType ColorType => _colorType;
    public event UnityAction ColorChanged;
    public event UnityAction CoinCollected;
    public event UnityAction Died;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _standartMaterial = _meshRenderer.material;
    }

    private void OnEnable()
    {
        _gameModeSwitcher.GameBegin += OnGameBegin;
    }

    private void OnDisable()
    {
        _gameModeSwitcher.GameBegin -= OnGameBegin;
    }

    private void OnGameBegin()
    {
        _collider.enabled = true;
        _rigidbody.isKinematic = false;
        _meshRenderer.enabled = true;
        ResetColor();
    }

    private void ResetColor()
    {
        _meshRenderer.material = _standartMaterial;
        _colorType = ColorType.Green;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ColoredCircle colorCircle))
        {
            _meshRenderer.material = colorCircle.ColorAtribute.Material;
            _colorType = colorCircle.ColorAtribute.ColorType;

            ColorChanged?.Invoke();
        }
        else if (other.TryGetComponent(out Coin coin))
        {
            if(_colorType == coin.ColorType)
            {
                coin.gameObject.SetActive(false);

                CoinCollected?.Invoke();
            }
        }
        else if(other.TryGetComponent(out Trap trap))
        {
            _collider.enabled = false;
            _rigidbody.isKinematic = true;
            _meshRenderer.enabled = false;

            Died?.Invoke();
        }
    }
}
