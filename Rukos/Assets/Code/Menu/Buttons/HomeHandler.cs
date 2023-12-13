using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
internal class HomeHandler : ButtonComponents, IPointerDownHandler
{
    [SerializeField] private LetFirstBall letFirstBall;
    [SerializeField] private InstructionsController instructionsScript;
    [SerializeField] private GoTutorial goTutorialScript;

    [SerializeField] internal GameObject fittings, shapes, shadow;
    internal List<GameObject> balls;
    [SerializeField] private List<SpriteRenderer> tubes;
    [SerializeField] private SpriteRenderer intersection;

    [SerializeField] private Button volumeButton, resumeButton, homeButton, loadButton, tutorialButton, switchLeftButton, switchRightButton, switchUpButton, switchDownButton, pushLeftButton, pushRightButton, pushUpButton, pushDownButton, rotateButton;
    [SerializeField] private Image volumeImg, resumeImg, homeImg, loadImg, tutorialImg, switchLeftImg, switchRightImg, switchUpImg, switchDownImg, pushLeftImg, pushRightImg, pushUpImg, pushDownImg, rotateImg;
    [SerializeField] private PauseHandler pauseHandler;
    //private Image spriteParent;
    internal delegate IEnumerator ReturnMenuAction(CanvasType type);
    internal static event ReturnMenuAction OnReturnMenu;

    public override Button Button1 { get { return volumeButton; } set { volumeButton = value; } }
    public override Button Button2 { get { return resumeButton; } set { resumeButton = value; } }
    public override Button Button3 { get { return homeButton; } set { homeButton = value; } }
    public override Image Img1 { get { return volumeImg; } set { volumeImg = value; } }
    public override Image Img2 { get { return resumeImg; } set { resumeImg = value; } }
    public override Image Img3 { get { return homeImg; } set { homeImg = value; } }

    public void OnPointerDown(PointerEventData eventData)
    {
        balls = new List<GameObject>();
        this.gameObject.GetComponentInParent<Image>().enabled = false;

        //if (spriteParent != null)
        //{
        //    spriteParent.enabled = false;
        //}

        if (OnReturnMenu != null)
        {
            StartCoroutine(OnReturnMenu(CanvasType.MainMenu));
        }
        //spriteParent = GetComponentInParent<Image>();
        base.Start();
        base.DeactivateComponents();
       
        balls.AddRange(GameObject.FindGameObjectsWithTag("Ball"));
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
        Debug.Log(this.gameObject.transform.parent.name);
        PermanentFunctions.instance.score = 0;
        letFirstBall.countBalls = 0;
        letFirstBall.firstBallUnleashed = false;
        Application.targetFrameRate = 30;
        pauseHandler.pauseIsNotClicked = true;
        instructionsScript.homeButton_ON = false;

        loadButton.enabled = true;
        loadImg.enabled = true;
        tutorialButton.enabled = true;
        tutorialImg.enabled = true;

        if (goTutorialScript.clicked == true)
        {
            fittings.SetActive(false);
            goTutorialScript.clicked = false;
            shadow.SetActive(false);
            intersection.enabled = false;

            switchLeftButton.enabled = false;
            switchRightButton.enabled = false;
            switchUpButton.enabled = false;
            switchDownButton.enabled = false;
            pushLeftButton.enabled = false;
            pushRightButton.enabled = false;
            pushUpButton.enabled = false;
            pushDownButton.enabled = false;
            rotateButton.enabled = false;

            switchLeftImg.enabled = false;
            switchRightImg.enabled = false;
            switchUpImg.enabled = false;
            switchDownImg.enabled = false;
            pushLeftImg.enabled = false;
            pushRightImg.enabled = false;
            pushUpImg.enabled = false;
            pushDownImg.enabled = false;
            rotateImg.enabled = false;

            foreach (SpriteRenderer tube in tubes)
            {
                tube.enabled = false;
            }

            foreach (Transform shape in shapes.transform)
            {
                foreach (Transform shapeChild in shape.transform)
                {
                    if (shapeChild.gameObject.tag == "ExplosionSprite")
                    {
                        shapeChild.GetComponent<SpriteRenderer>().enabled = false;
                    }
                    if (shapeChild.gameObject.tag == "SquareLight")
                    {
                        shapeChild.gameObject.SetActive(false);
                    }
                    shape.GetComponent<SpriteRenderer>().enabled = false;
                }
            }

        }
        Time.timeScale = 0;
    }
}
