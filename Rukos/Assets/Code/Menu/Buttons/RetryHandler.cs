using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

internal class RetryHandler : ButtonComponents, IPointerDownHandler
{
    [SerializeField] private LetFirstBall letFirstBall;
    internal List<GameObject> balls;
    [SerializeField] private Button volumeButton, retryButton, homeButton;
    [SerializeField] private Image volumeImg, retryImg, homeImg;
    [SerializeField] private PauseHandler pauseHandler;
    private Image spriteParent;
    internal delegate IEnumerator ReturnMenuAction(CanvasType type);
    internal static event ReturnMenuAction OnReturnMenu;
    internal bool retryClicked = false;

    public override Button Button1 { get { return volumeButton; } set { volumeButton = value; } }
    public override Button Button2 { get { return retryButton; } set { retryButton = value; } }
    public override Button Button3 { get { return homeButton; } set { homeButton = value; } }
    public override Image Img1 { get { return volumeImg; } set { volumeImg = value; } }
    public override Image Img2 { get { return retryImg; } set { retryImg = value; } }
    public override Image Img3 { get { return homeImg; } set { homeImg = value; } }
    internal delegate IEnumerator ReturnGameAction(CanvasType type);
    internal static event ReturnGameAction OnReturnGame;
    internal bool doNotIncreaseColors = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        retryClicked = true;
        //doNotIncreaseColors = true;
        balls = new List<GameObject>();
        this.gameObject.GetComponentInParent<Image>().enabled = false;

        spriteParent = GetComponentInParent<Image>();
        base.Start();
        base.DeactivateComponents();

        if (OnReturnGame != null)
        {
            StartCoroutine(OnReturnGame(CanvasType.InGameView));
        }
        PermanentFunctions.instance.score = 0;
        PermanentFunctions.instance.maxlives = 3;

        //balls.AddRange(GameObject.FindGameObjectsWithTag("Ball"));
        //foreach (GameObject ball in balls)
        //{
        //    Destroy(ball);
        //}

        letFirstBall.countBalls = 0;
        letFirstBall.firstBallUnleashed = false;
        Application.targetFrameRate = 30;
        pauseHandler.pauseIsNotClicked = true;
        letFirstBall.inGameRoundsTemp = 0;
        Time.timeScale = 0;
        letFirstBall.ballCountPerStage = 2;
    }
}
