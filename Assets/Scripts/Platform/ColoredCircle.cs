using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColoredCircle : MonoBehaviour
{
    [SerializeField] private ColorAtribute _colorAtribute;

    private MeshRenderer _meshRenderer;

    public ColorAtribute ColorAtribute => _colorAtribute;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetColorAtribute(ColorAtribute colorAtribute)
    {
        _colorAtribute = colorAtribute;
        _meshRenderer.material = colorAtribute.Material;
    }
}
