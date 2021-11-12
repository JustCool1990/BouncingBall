using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public abstract class Screen : MonoBehaviour
{
    [SerializeField] private float _apperanceSpeed;

    protected CanvasGroup CanvasGroup;

    protected void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    public abstract void Open();
    public abstract void Close();

    protected IEnumerator ChangeScreenAlpha()
    {
        while (CanvasGroup.alpha < 1)
        {
            CanvasGroup.alpha += _apperanceSpeed * Time.deltaTime;
            yield return null;
        }

        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;
    }
}
