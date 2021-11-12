using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    [SerializeField] protected float Speed;
    [SerializeField] protected float MinIdleTime;
    [SerializeField] protected float MaxIdleTime;
    [SerializeField] protected Vector3 StartPosition = Vector3.zero;
    [SerializeField] protected Vector3 TargetPosition;

    protected float IdleTime;
    protected bool IsActivated;
    protected IEnumerator Coroutine;
    protected bool CoroutineIsActive = false;

    protected void Awake()
    {
        SetObstacleParametres();
    }

    protected void OnDisable()
    {
        if (CoroutineIsActive == true)
        {
            StopCoroutine(Coroutine);
            CoroutineIsActive = false;
        }

        SetObstacleParametres();
    }

    private void Update()
    {
        if (IsActivated == false)
            IdleTime -= Time.deltaTime;

        if (IdleTime <= 0 && IsActivated == false)
        {
            ActivateTrapHintAnimation();
        }
    }

    protected abstract void ActivateTrapHintAnimation();

    protected abstract void SetObstacleParametres();
}
