using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class GameoverController : MenusComponents
{
    private const float timeToScaleObj = 1f;


    [SerializeField] internal GameObject shapes, fittings, tubes, shadow, intersection;
    [SerializeField] private Button volumeButton, retryButton, homeButton;
    [SerializeField] private Image volumeImg, retryImg, homeImg;
    [SerializeField] private LetFirstBall leftFirstBall;
    //private SpriteRenderer spriteText;
    internal List<GameObject> balls;

    public override GameObject Obj1 { get { return shapes; } set { shapes = value; } }
    public override GameObject Obj2 { get { return fittings; } set { fittings = value; } }
    public override GameObject Obj3 { get { return tubes; } set { tubes = value; } }
    public override GameObject Obj4 { get { return shadow; } set { shadow = value; } }
    public override Button Button1 { get { return volumeButton; } set { volumeButton = value; } }
    public override Button Button2 { get { return retryButton; } set { retryButton = value; } }
    public override Button Button3 { get { return homeButton; } set { homeButton = value; } }
    public override Image Img1 { get => volumeImg; set => volumeImg = value; }
    public override Image Img2 { get => retryImg; set => retryImg = value; }
    public override Image Img3 { get => homeImg; set => homeImg = value; }

    internal delegate IEnumerator GameOverMenuAction(float time, Button button1, Button button2);
    internal static event GameOverMenuAction OnAppearGameOver;

    private bool shouldLerp = false;
    private void OnEnable()
    {
        OnAppearGameOver += PermanentFunctions.instance.ScaleOverTime;

        StopAllCoroutines();

        base.DeactivateObjects();
        base.ActivateComponents();

        //loadingObject.SetActive(false);
        //intersection.SetActive(false);
        //stopObject.SetActive(false);
        balls = new List<GameObject>();
        //Button2.transform.localScale = new Vector3(1.7f, 0, 1.7f);

        shouldLerp = true;

        leftFirstBall.inGameRoundsTemp = 0;
        PermanentFunctions.instance.roundsCount = 0;
        leftFirstBall.inGameRoundsTemp = PermanentFunctions.instance.roundsCount;

        balls.AddRange(GameObject.FindGameObjectsWithTag("Ball"));
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<SpriteRenderer>().enabled = false;
        }
        intersection.SetActive(false);
        Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 0;
    }
    private void OnDisable()
    {
        OnAppearGameOver -= PermanentFunctions.instance.ScaleOverTime;
    }

    private void Update()
    {
        if (shouldLerp)
        {
            StartCoroutine(OnAppearGameOver(timeToScaleObj, Button2, Button3));
        }
    }

   
}
