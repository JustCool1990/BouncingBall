using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : Initable<Sphere, PlatformGenerator, TrapsProvider>
{
    [SerializeField] private PlatformType _platformType;
    [SerializeField] private Transform _platformStartPoint;
    [SerializeField] private Transform _platformEndPoint;
    [SerializeField, Range(1, 5)] private int _capacity;
    [SerializeField] private Ground _ground;

    public Ground Ground => _ground;
    public PlatformType PlatformType => _platformType;
    public Transform PlatformStartPoint => _platformStartPoint;
    public Transform PlatformEndPoint => _platformEndPoint;

    public int Capacity => _capacity;
}

public enum PlatformType
{
    Safe,
    Coins,
    Traps,
    ChooseColor
}
