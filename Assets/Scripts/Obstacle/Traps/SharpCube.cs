using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SharpCube : Obstacle
{
    private Animator _animator;
    private Trap _cube;
    private Vector3 _upperPosition;

    private new void Awake()
    {
        _animator = GetComponent<Animator>();
        _cube = GetComponentInChildren<Trap>();
        _upperPosition = TargetPosition;

        base.Awake();
    }

    private new void OnDisable()
    {
        CheckLocalScale();

        base.OnDisable();
    }

    public void ActivateTrap()
    {
        Coroutine = ChangePosition(TargetPosition);
        StartCoroutine(Coroutine);
    }

    protected override void ActivateTrapHintAnimation()
    {
        IsActivated = true;
        _animator.SetTrigger(AnimatorTrapController.States.Activate);
    }

    protected override void SetObstacleParametres()
    {
        IdleTime = Random.Range(MinIdleTime, MaxIdleTime);
        TargetPosition = _cube.transform.localPosition == StartPosition ? _upperPosition : StartPosition;
        IsActivated = false;
    }

    private void CheckLocalScale()
    {
        if (_cube.transform.localScale != Vector3.one)
            _cube.transform.localScale = Vector3.one;
    }

    private IEnumerator ChangePosition(Vector3 targetPosition)
    {
        CoroutineIsActive = true;

        while (_cube.transform.localPosition != targetPosition)
        {
            _cube.transform.localPosition = Vector3.MoveTowards(_cube.transform.localPosition, targetPosition, Speed * Time.deltaTime);
            yield return null;
        }

        SetObstacleParametres();
        CoroutineIsActive = false;
    }
}
