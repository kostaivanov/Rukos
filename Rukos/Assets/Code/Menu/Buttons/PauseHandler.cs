using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

internal class PauseHandler : ButtonComponents, IPointerDownHandler
{
    [SerializeField] private Button volumeButton, resumeButton, homeButton;
    [SerializeField] private Image volumeImg, resumeImg, homeImg;

    internal bool pauseIsNotClicked = true;

    public override Button Button1 { get { return volumeButton;  } set { volumeButton = value;  } } 
    public override Button Button2 { get { return resumeButton; } set { resumeButton = value; } }
    public override Button Button3 { get { return homeButton; } set { homeButton = value; } }
    public override Image Img1 { get { return volumeImg; } set { volumeImg = value; } }
    public override Image Img2 { get { return resumeImg; } set { resumeImg = value; } }
    public override Image Img3 { get { return homeImg; } set { homeImg = value; } }

    public void OnPointerDown(PointerEventData eventData)
    {
        pauseIsNotClicked = false;
        base.Start();
        base.ActivateComponents();
        base.canvasManager.SwitchCanvas(CanvasType.PauseMenu);
        Time.timeScale = 0;
        Application.targetFrameRate = 30;
    }
}
