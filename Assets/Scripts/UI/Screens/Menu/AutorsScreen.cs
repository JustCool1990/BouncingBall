using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class AutorsScreen : Screen
{
    [SerializeField] private Button _backPreviousScreenButton;

    private Animator _animator;

    public event UnityAction BackMainMenuScreenButtonClick;
    public event UnityAction Opened;

    private new void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _backPreviousScreenButton.onClick.AddListener(BackMainMenuScreen);
    }

    private void OnDisable()
    {
        _backPreviousScreenButton.onClick.RemoveListener(BackMainMenuScreen);
    }

    public override void Close()
    {
        _animator.SetTrigger(AnimatorScreenController.States.Disappear);
    }

    public override void Open()
    {
        _animator.SetTrigger(AnimatorScreenController.States.Appear);
    }

    public void TriggerOpenEvent()
    {
        Opened?.Invoke();
    }

    private void BackMainMenuScreen()
    {
        Close();
        BackMainMenuScreenButtonClick?.Invoke();
    }
}
