using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

internal class PlayHandler : ButtonComponents, IPointerDownHandler
{
    [SerializeField] private MenuController menuController;
    [SerializeField] private GameObject loadingObject, shapes, pause;
    [SerializeField] private Button volumeButton, playButton, RecordsButton, exitButton, loadButton, tutorialButton, switchLeftButton, switchRightButton, switchUpButton, switchDownButton, pushLeftButton, pushRightButton, pushUpButton, pushDownButton, rotateButton;
    [SerializeField] private Image volumeImg, playImg, RecordsImg, exitImg, loadImg, tutorialImg, switchLeftImg, switchRightImg, switchUpImg, switchDownImg, pushLeftImg, pushRightImg, pushUpImg, pushDownImg, rotateImg;

    public override Button Button1 { get { return volumeButton; } set { volumeButton = value; } }
    public override Button Button2 { get { return playButton; } set { playButton = value; } }
    public override Button Button3 { get { return RecordsButton; } set { RecordsButton = value; } }
    public override Button Button4 { get { return exitButton; } set { exitButton = value; } }

    public override Image Img1 { get { return volumeImg; } set { volumeImg = value; } }
    public override Image Img2 { get { return playImg; } set { playImg = value; } }
    public override Image Img3 { get { return RecordsImg; } set { RecordsImg = value; } }
    public override Image Img4 { get { return exitImg; } set { exitImg = value; } }


    internal bool playWasClicked = false;
    [SerializeField] private GenerateNewShapes generateNewShapes;
    [SerializeField] private RetryHandler retryHandler;
    private void Awake()
    {
        HomeHandler.OnReturnMenu += LoadingScene;
        RetryHandler.OnReturnGame += LoadingScene;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        base.Start();
        base.DeactivateComponents();

        StartCoroutine(LoadingScene(CanvasType.InGameView));
        playWasClicked = true;
    }

    internal IEnumerator LoadingScene(CanvasType type)
    {
        GameObject gameOver = GameObject.FindGameObjectWithTag("GameOver");
        if (gameOver != null)
        {
            gameOver.GetComponentInParent<Image>().enabled = false;
        }

        if (retryHandler.retryClicked == false)
        {
            ResetShapesValues();
            string[] fullNamePreviousShape = shapes.transform.GetChild(shapes.GetComponent<SwitchSquares>().indexSquare).name.Split(new char[] { '_', }, System.StringSplitOptions.RemoveEmptyEntries);
            string nameOfPreviousShape = fullNamePreviousShape[fullNamePreviousShape.Length - 1];
            shapes.GetComponent<CollectSquares>().ReturnOriginalColor(shapes.transform.GetChild(shapes.GetComponent<SwitchSquares>().indexSquare).gameObject, nameOfPreviousShape);

            string[] fullNameFirstShape = shapes.transform.GetChild(0).gameObject.name.Split(new char[] { '_', }, System.StringSplitOptions.RemoveEmptyEntries);

            string nameOfFirstShape = fullNameFirstShape[fullNameFirstShape.Length - 1];
            shapes.GetComponent<SwitchSquares>().indexSquare = 0;
            shapes.GetComponent<CollectSquares>().FlashLightTheBox(shapes.transform.GetChild(0).gameObject, nameOfFirstShape);
        }

        Button4.enabled = false;
        Img4.enabled = false;
        loadButton.enabled = false;
        loadImg.enabled = false;
        tutorialButton.enabled = false;
        tutorialImg.enabled = false;

        switchLeftButton.enabled = true;
        switchRightButton.enabled = true;
        switchUpButton.enabled = true;
        switchDownButton.enabled = true;
        pushLeftButton.enabled = true;
        pushRightButton.enabled = true;
        pushUpButton.enabled = true;
        pushDownButton.enabled = true;
        rotateButton.enabled = true;

        switchLeftImg.enabled = true;
        switchRightImg.enabled = true;
        switchUpImg.enabled = true;
        switchDownImg.enabled = true;
        pushLeftImg.enabled = true;
        pushRightImg.enabled = true;
        pushUpImg.enabled = true;
        pushDownImg.enabled = true;
        rotateImg.enabled = true;

        loadingObject.SetActive(true);
        pause.SetActive(true);

        yield return new WaitForSecondsRealtime(2f);

        if (retryHandler.retryClicked == true)
        {
            generateNewShapes.GenerateNewShapesFunction(retryHandler.gameObject);
            retryHandler.doNotIncreaseColors = true;
        }

        
        menuController.tubes.SetActive(true);
        menuController.fittings.SetActive(true);
        menuController.shapes.SetActive(true);
        menuController.intersection.SetActive(true);
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        base.canvasManager.SwitchCanvas(type);
        loadingObject.SetActive(false);        
    }

    private void ResetShapesValues()
    {
        foreach (Transform shape in shapes.transform)
        {
            string[] name = shape.gameObject.name.Split(new char[] { '_', '(', ')' }, System.StringSplitOptions.RemoveEmptyEntries);

            CollectBalls collectBalls = shape.GetComponent<CollectBalls>();
            shape.gameObject.GetComponent<Animator>().SetBool("PutBrokenSprite", false);
            collectBalls.added = false;
            collectBalls.count = 0;
            collectBalls.squareIsFull = false;
            collectBalls.sameColor = 0;
            collectBalls.checkedIfSquareIsFull = false;
            collectBalls.balls.Clear();
            if (name[name.Length - 1] == "Polygon" && shape.rotation.eulerAngles.z != 0)
            {
                shape.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
