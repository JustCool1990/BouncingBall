using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Flag : MonoBehaviour
{
    public event UnityAction<Flag> HintAnimationEnded;

    public void ResetRotation(Quaternion rotation)
    {
        transform.localRotation = rotation;
    }

    public void ResetScale()
    {
        transform.localScale = Vector3.one;
    }

    public void ActivateHintAnimation()
    {
        HintAnimationEnded?.Invoke(this);
    }
}
