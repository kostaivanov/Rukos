using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CanvasManager : MonoBehaviour
{
    private List<CanvasSetType> canvasSetTypeList;
    private CanvasSetType lastActiveCanvas;

    private void Awake()
    {
        canvasSetTypeList = GetComponentsInChildren<CanvasSetType>().ToList();
        canvasSetTypeList.ForEach(x => x.gameObject.SetActive(false));

        SwitchCanvas(CanvasType.MainMenu);
    }

    internal void SwitchCanvas(CanvasType canvasType)
    {
        if (lastActiveCanvas != null)
        {
            lastActiveCanvas.gameObject.SetActive(false);
        }

        CanvasSetType desiredCanvas = canvasSetTypeList.Find(x => x.canvasType == canvasType);

        if (desiredCanvas != null)
        {
            desiredCanvas.gameObject.SetActive(true);
            lastActiveCanvas = desiredCanvas;
        }
        else
        {
            Debug.LogWarning("The main menu canvas was not found!");
        }
    }
}
