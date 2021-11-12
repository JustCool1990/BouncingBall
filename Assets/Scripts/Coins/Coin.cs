using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Coin : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    public ColorType ColorType { get; private set; }

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetColorAtribute(ColorAtribute colorAtribute)
    {
        ColorType = colorAtribute.ColorType;
        _meshRenderer.material = colorAtribute.Material;
    }
}
