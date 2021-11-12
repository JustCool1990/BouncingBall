using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody), typeof(GroundCheck))]
public class SphereJump : MonoBehaviour
{
    [SerializeField] private JumpSlider _jumpSlider;
    
    private GroundCheck _groundCheck;
    private Rigidbody _rigidbody;
    private bool _canJump = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _groundCheck = GetComponent<GroundCheck>();
    }

    private void OnEnable()
    {
        _jumpSlider.Jumping += OnJumping;
        _groundCheck.Grounded += OnGrounded;
    }

    private void OnDisable()
    {
        _jumpSlider.Jumping -= OnJumping;
        _groundCheck.Grounded -= OnGrounded;
    }
    
    private void OnJumping(float value)
    {
        if(_canJump)
        {
            _rigidbody.AddForce(Vector3.up * value, ForceMode.Impulse);
            _canJump = false;
        }
    }

    private void OnGrounded()
    {
        _canJump = true;
    }
}
