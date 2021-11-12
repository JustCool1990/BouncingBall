using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorAtribute
{
    [SerializeField] private ColorType _colorType;
    [SerializeField] private Material _material;

    public Material Material => _material;
    public ColorType ColorType => _colorType;
}

public enum ColorType
{
    Green,
    Yellow,
    Red
}
