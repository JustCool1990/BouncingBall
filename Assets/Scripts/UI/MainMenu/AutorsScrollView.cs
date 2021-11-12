using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class AutorsScrollView : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private ScrollRect _scrollRect;

    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(ChangeScrollValue);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(ChangeScrollValue);
    }

    private void ChangeScrollValue(float value)
    {
        _scrollRect.horizontalNormalizedPosition = value;
    }
}
