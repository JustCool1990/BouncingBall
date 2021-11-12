using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class GameInterface : MonoBehaviour
{
    private CanvasGroup _InterfaceCanvas;

    private void Awake()
    {
        _InterfaceCanvas = GetComponent<CanvasGroup>();
    }

    public void OpenInterface()
    {
        _InterfaceCanvas.alpha = 1;
        _InterfaceCanvas.interactable = true;
        _InterfaceCanvas.blocksRaycasts = true;
    }

    public void CloseInterface()
    {
        _InterfaceCanvas.alpha = 0;
        _InterfaceCanvas.interactable = false;
        _InterfaceCanvas.blocksRaycasts = false;
    }
}