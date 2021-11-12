using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoorHint : MonoBehaviour
{
    [SerializeField] private float _showTime;
    [SerializeField] private Doorway _doorway;
    [SerializeField] private Image _hintPanel;
    [SerializeField] private Image _hintImage;
    [SerializeField] private TMP_Text _hintText;

    private float _lastShowTime;
    private IEnumerator _coroutine;
    private bool _coroutineIsActive = false;

    private void Start()
    {
        ResetShowTime();
    }

    private void OnEnable()
    {
        _doorway.MaterialSelected += OnMaterialSelected;
    }

    private void OnDisable()
    {
        _doorway.MaterialSelected -= OnMaterialSelected;

        if (_coroutineIsActive)
        {
            StopCoroutine(_coroutine);

            _coroutineIsActive = false;
        }
    }

    private void OnMaterialSelected(ColorAtribute colorAtribute)
    {
        _coroutine = ShowDoorColorHint(colorAtribute);
        StartCoroutine(_coroutine);
    }

    private IEnumerator ShowDoorColorHint(ColorAtribute colorAtribute)
    {
        _coroutineIsActive = true;
        _hintPanel.gameObject.SetActive(true);
        _hintText.text = $"{WriteHintText(colorAtribute)} door ahead ->";
        SetMaterial(colorAtribute);

        while (_lastShowTime > 0)
        {
            _lastShowTime -= Time.deltaTime;
            yield return null;
        }

        _coroutineIsActive = false;
        ResetShowTime();
    }

    private string WriteHintText(ColorAtribute colorAtribute)
    {
        return colorAtribute.ColorType.ToString();
    }

    private void SetMaterial(ColorAtribute colorAtribute)
    {
        _hintImage.color = DetermineHintColor(colorAtribute.ColorType);
    }

    private Color DetermineHintColor(ColorType colorType)
    {
        switch(colorType)
        {
            default:
            case ColorType.Green: return Color.green;

            case ColorType.Yellow: return Color.yellow;

            case ColorType.Red: return Color.red;
        }
    }

    private void ResetShowTime()
    {
        _hintPanel.gameObject.SetActive(false);
        _lastShowTime = _showTime;
    }
}
