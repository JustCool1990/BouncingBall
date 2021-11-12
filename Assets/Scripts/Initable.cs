using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initable<T1, T2, T3> : MonoBehaviour
{
    public T1 Sphere { get; private set; }
    public T2 RoadGenerator { get; private set; }
    public T3 TrapsProvider { get; private set; }

    private bool _inited;

    public void Init(T1 model1, T2 model2, T3 model3)
    {
        if (_inited)
            throw new InvalidOperationException();

        Sphere = model1;
        RoadGenerator = model2;
        TrapsProvider = model3;
        _inited = true;
        enabled = true;
    }

    private void OnEnable()
    {
        if (_inited == false)
        {
            enabled = false;
            return;
        }

        OnInitedEnable();
    }

    private void OnDisable()
    {
        if (_inited)
            OnInitedDisable();
    }

    protected virtual void OnInitedEnable() { }
    protected virtual void OnInitedDisable() { }
}
