using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AutorsSlider : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed;

    private Slider _slider;
    private AutorsScreen _autorsScreen;
    private IEnumerator _coroutine;
    private bool _coroutineIsActive = false;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _autorsScreen = GetComponentInParent<AutorsScreen>();
        ResetSliderValue();
    }

    private void OnEnable()
    {
        _autorsScreen.Opened += OnOpened;
        _autorsScreen.BackMainMenuScreenButtonClick += OnBackMainMenuScreenButtonClick;
    }

    private void OnDisable()
    {
        _autorsScreen.Opened -= OnOpened;
        _autorsScreen.BackMainMenuScreenButtonClick -= OnBackMainMenuScreenButtonClick;
    }

    private void OnOpened()
    {
        _coroutine = ShowAutors();
        StartCoroutine(_coroutine);
    }

    private IEnumerator ShowAutors()
    {
        _coroutineIsActive = true;

        while(_slider.value < _slider.maxValue)
        {
            _slider.value += _scrollSpeed * Time.deltaTime;
            yield return null;
        }

        _coroutineIsActive = false;
    }

    private void OnBackMainMenuScreenButtonClick()
    {
        CheckShowCoroutine();
        ResetSliderValue();
    }

    private void CheckShowCoroutine()
    {
        if(_coroutineIsActive == true)
        {
            DisableCoroutine();
        }
    }

    private void DisableCoroutine()
    {
        StopCoroutine(_coroutine);
        _coroutineIsActive = false;
    }

    private void ResetSliderValue()
    {
        _slider.value = 0;
    }
}
