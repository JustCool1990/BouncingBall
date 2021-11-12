using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class JumpSlider : MonoBehaviour
{
    [SerializeField] private GameModeSwitcher _gameModeSwitcher;
    [SerializeField] private float _fillSpeed;
    [SerializeField] private float _fillAccelerationFactor;
    [SerializeField] private Image _fill;
    [SerializeField] private float _fillingSpeed;
    [SerializeField] private Color _unfilledColor;
    [SerializeField] private Color _filledColor;

    private float _lowJumpForce = 11.1f, _mediumJumpForce = 15.8f, _highJumpForce = 19.5f;
    private float _lowJumpThreshold = 0.1f, _mediumJumpThreshold = 0.4f, _highJumpThreshold = 0.7f;
    private Slider _jumpSlider;
    private float _lastSpeed;
    private Color _targetColor;
    private float _targetProgress;
    private bool _gameStarted = false;

    public event UnityAction<float> Jumping;

    private void Awake()
    {
        _jumpSlider = GetComponentInChildren<Slider>();
        ResetSliderValues();
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
        {
            TouchCheck();
            UntouchCheck();
        }
    }

    private void TouchCheck()
    {
        if (Input.GetMouseButton(0))
        {
            _lastSpeed += Time.deltaTime * _fillAccelerationFactor;
            _jumpSlider.value += _lastSpeed * Time.deltaTime;
            ChangeColor();
        }
    }

    private void UntouchCheck()
    {
        if (Input.GetMouseButtonUp(0))
        {
            float jumpPower = SetJumpForce(_jumpSlider.value);

            if (jumpPower > 0)
                Jumping?.Invoke(jumpPower);

            ResetSliderValues();
        }
    }

    private float SetJumpForce(float sliderValue)
    {
        if (sliderValue < _lowJumpThreshold)
            return 0;
        else if(sliderValue < _mediumJumpThreshold)
            return _lowJumpForce;
        else if (sliderValue < _highJumpThreshold)
            return _mediumJumpForce;
        else
            return _highJumpForce;
    }

    private void ResetSliderValues()
    {
        _jumpSlider.value = 0;
        _lastSpeed = _fillSpeed;
    }

    private void ChangeColor()
    {
        _targetProgress = _jumpSlider.value / _jumpSlider.maxValue;
        _targetColor = Color.Lerp(_unfilledColor, _filledColor, _targetProgress);
        _fill.color = Color.Lerp(_fill.color, _targetColor, _fillingSpeed);
    }

    private void OnGameBegin()
    {
        _gameStarted = true;
    }

    private void OnGameOver()
    {
        _gameStarted = false;
    }
}
