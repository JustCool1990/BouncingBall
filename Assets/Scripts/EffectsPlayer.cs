using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameModeSwitcher))]
public class EffectsPlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private Sphere _sphere;

    private void OnEnable()
    {
        _sphere.Died += OnDied;
    }

    private void OnDisable()
    {
        _sphere.Died -= OnDied;
    }

    private void OnDied()
    {
        _explosionEffect.Play();
    }
}