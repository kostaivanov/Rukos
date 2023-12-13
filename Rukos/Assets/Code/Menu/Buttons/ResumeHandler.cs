using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ResumeHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private CanvasManager canvasManager;
    [SerializeField] private PauseHandler pauseHandler;
    internal bool resumed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        pauseHandler.pauseIsNotClicked = true;
        Time.timeScale = 1;
        Application.targetFrameRate = 60;
        canvasManager.SwitchCanvas(CanvasType.InGameView);
        resumed = true;
    }
}
