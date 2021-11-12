using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Sphere _sphere;
    [SerializeField] private float _offset;

    private float _positionX;

    private void Awake()
    {
        transform.position = new Vector3(_offset + _sphere.transform.position.x, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        _positionX = _offset + _sphere.transform.position.x;

        transform.position = new Vector3(_positionX, transform.position.y, transform.position.z);
    }
}
