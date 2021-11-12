using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Spike : Obstacle
{
    [SerializeField] private float _hideSpeed;
    [SerializeField] private Transform _spearsPlatform;
    [SerializeField] private float _minSpearDepartureDistance;
    [SerializeField] private float _maxSpearDepartureDistance;

    private Animator _animator;
    private Trap _spears;
    private bool _isSpikeShowed = false;

    private new void Awake()
    {
        _animator = GetComponent<Animator>();
        _spears = GetComponentInChildren<Trap>();
        base.Awake();
    }

    private new void OnDisable()
    {
        CheckInitialState();

        base.OnDisable();
    }

    public void ActivateTrap()
    {
        Coroutine = ChangeTrapPosition(_spears.transform, TargetPosition, _isSpikeShowed == true ? Speed : _hideSpeed);
        StartCoroutine(Coroutine);
    }

    protected override void ActivateTrapHintAnimation()
    {
        IsActivated = true;

        if (_isSpikeShowed == false)
        {
            _isSpikeShowed = true;
            _animator.SetTrigger(AnimatorTrapController.States.Activate);
        }
        else
        {
            _isSpikeShowed = false;
            ActivateTrap();
        }
    }

    protected override void SetObstacleParametres()
    {
        IdleTime = Random.Range(MinIdleTime, MaxIdleTime);
        TargetPosition = _spears.transform.localPosition == StartPosition ? new Vector3(0, Random.Range(_minSpearDepartureDistance, _maxSpearDepartureDistance), 0) : StartPosition;
        IsActivated = false;
    }

    private void CheckInitialState()
    {
        if (_spears.transform.localPosition != Vector3.zero || _spearsPlatform.localScale != Vector3.one)
        {
            _spears.transform.localPosition = Vector3.zero;
            _spearsPlatform.localScale = Vector3.one;
            _isSpikeShowed = false;
        }
    }

    private IEnumerator ChangeTrapPosition(Transform startPosition, Vector3 targetPosition, float speed)
    {
        CoroutineIsActive = true;

        while (startPosition.localPosition != targetPosition)
        {
            startPosition.localPosition = Vector3.MoveTowards(startPosition.localPosition, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        SetObstacleParametres();
        CoroutineIsActive = false;
    }
}
