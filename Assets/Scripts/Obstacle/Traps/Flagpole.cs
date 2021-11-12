using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flagpole : Obstacle
{
    private Flag[] _flagsTransform;
    private Animator[] _flagsAnimators;
    private Quaternion _endRotation;
    private Quaternion _startRotation;
    private int _openFlagIndex = 0;
    private int _openFlagCounter = 0;

    private new void Awake()
    {
        _flagsTransform = GetComponentsInChildren<Flag>();
        _flagsAnimators = GetComponentsInChildren<Animator>();
        _endRotation = Quaternion.Euler(TargetPosition);
        _startRotation = Quaternion.Euler(StartPosition);

        base.Awake();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _flagsTransform.Length; i++)
        {
            _flagsTransform[i].HintAnimationEnded += OnHintAnimationEnded;
        }
    }

    private new void OnDisable()
    {
        for (int i = 0; i < _flagsTransform.Length; i++)
        {
            _flagsTransform[i].HintAnimationEnded -= OnHintAnimationEnded;
        }

        base.OnDisable();

        foreach (var flag in _flagsTransform)
        {
            CheckFlagsRotation(flag);
            CheckFlagsLocalScale(flag);
        }

        _openFlagCounter = 0;
    }

    protected override void ActivateTrapHintAnimation()
    {
        if (_openFlagCounter == 0)
        {
            _openFlagCounter++;
            _openFlagIndex = Random.Range(0, _flagsTransform.Length);

            IsActivated = true;
            _flagsAnimators[_openFlagIndex].SetTrigger(AnimatorTrapController.States.Activate);
        }
        else
        {
            IsActivated = true;
            _flagsAnimators[_openFlagIndex].SetTrigger(AnimatorTrapController.States.Activate);

            _openFlagIndex = ChooseSecondObstacle(_openFlagIndex);

            _flagsAnimators[_openFlagIndex].SetTrigger(AnimatorTrapController.States.Activate);
        }
    }

    protected override void SetObstacleParametres()
    {
        IdleTime = Random.Range(MinIdleTime, MaxIdleTime);
        IsActivated = false;
    }

    private void CheckFlagsRotation(Flag flag)
    {
        if (flag.transform.localRotation != _startRotation)
            flag.ResetRotation(_startRotation);
    }

    private void CheckFlagsLocalScale(Flag flag)
    {
        if (flag.transform.localScale != Vector3.one)
            flag.ResetScale();
    }

    private void OnHintAnimationEnded(Flag flag)
    {
        ActivateTrap(flag);
    }

    private void ActivateTrap(Flag flag)
    {
        Coroutine = ChangeRotation(flag, flag.transform.localRotation == _startRotation ? _endRotation : _startRotation);
        StartCoroutine(Coroutine);
    }

    private IEnumerator ChangeRotation(Flag flag, Quaternion rotation)
    {
        CoroutineIsActive = true;

        while (flag.transform.localRotation != rotation)
        {
            flag.transform.localRotation = Quaternion.RotateTowards(flag.transform.localRotation, rotation, Speed * Time.deltaTime);
            yield return null;
        }

        SetObstacleParametres();
        CoroutineIsActive = false;
    }

    private int ChooseSecondObstacle(int index)
    {
        switch (index)
        {
            case 0:
            default:
                return Random.Range(index + 1, _flagsTransform.Length);

            case 1:
                return Random.Range(0, index + 1) < index ? 0 : index + 1;

            case 2:
                return Random.Range(0, index);
        }
    }
}
