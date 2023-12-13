using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateSquare : MonoBehaviour
{
    [SerializeField] private GameObject squaresCollection;
    private CollectSquares collectSquares;
    private SwitchSquares switchSquares;
    private GenerateNewShapes generateNewshapes;

    [SerializeField] private Button rotateButton;
    private float speed = 0.3f;
    private CollectBalls collectBalls;
    private int currentIndex = 0;
    internal bool rotating = false;
    internal bool rotateIsClicked = false;
    private bool squaresAssigned;

    [SerializeField] private AudioSource squaresAS;
    [SerializeField] private AudioClip rotateSound;

    private void Start()
    {
        squaresAssigned = false;

        switchSquares = GetComponent<SwitchSquares>();
        //collectSquares = squaresCollection.GetComponent<CollectSquares>();
        collectSquares = GetComponent<CollectSquares>();
        generateNewshapes = GetComponent<GenerateNewShapes>();

        rotateButton.onClick.AddListener(RotateTheSquare);

        currentIndex = 0;
    }

    public void PlayRotateSound()
    {
        squaresAS.PlayOneShot(rotateSound);
    }

    private void Update()
    {
        if (collectBalls == null && squaresAssigned == false && collectSquares.squares != null)
        {
            collectBalls = collectSquares.squares[currentIndex].GetComponent<CollectBalls>();
            squaresAssigned = true;
        }

        //Debug.Log("collectBalls" + $" {collectBalls == null}  - " + currentIndex + " -  " + generateNewshapes.shapesAreDestroyed);
        if (generateNewshapes.shapesAreDestroyed == true)
        {
            //collectSquares = squaresCollection.GetComponent<CollectSquares>();
            collectSquares = GetComponent<CollectSquares>();
            if (collectBalls == null)
            {
                collectBalls = collectSquares.squares[currentIndex].GetComponent<CollectBalls>();
            }
        }

        if (switchSquares.indexSquare != currentIndex && collectSquares.squares.Count == 6)
        {
            collectBalls = null;
            //Debug.Log(switchSquares.indexSquare + " - " + collectSquares.squares.Count);
            if (collectSquares.squares[switchSquares.indexSquare] != null)
            {
                collectBalls = collectSquares.squares[switchSquares.indexSquare].GetComponent<CollectBalls>();
                currentIndex = switchSquares.indexSquare;
            }
        }
    }

    public void RotateTheSquare()
    {
        rotateIsClicked = true;
        PlayRotateSound();
        if (collectSquares == null)
        {
            collectSquares = squaresCollection.GetComponent<CollectSquares>();
            if (collectBalls == null)
            {
                collectBalls = collectSquares.squares[currentIndex].GetComponent<CollectBalls>();
            }
            //collectSquares != null && 
        }
        if (collectSquares.squares[switchSquares.indexSquare].tag == "SquareClockWise")
        {
            //Debug.Log(collectBalls.balls.Count);
            string[] name = collectSquares.squares[switchSquares.indexSquare].name.Split(new char[] { '_' }, System.StringSplitOptions.RemoveEmptyEntries);
            string nameOfFigure = name[name.Length - 1];
            //
            //if (collectBalls != null && collectBalls.balls.Count != 0)
            //{
            //foreach (var topka in collectBalls.balls)
            //{
            //if (topka != null && topka.GetComponent<MoveBallController>().readyToRotate == true)
            // {
            if (StartsWith(nameOfFigure, "P"))
            {
                //StartCoroutine(RotateMe(Vector3.back * 45, speed, collectSquares.squares[switchSquares.indexSquare]));
                StartCoroutine(Rotate(Vector3.back, 45, collectSquares.squares[switchSquares.indexSquare], 0.1f));
            }
            else
            {
                //StartCoroutine(RotateMe(Vector3.back * 90, speed, collectSquares.squares[switchSquares.indexSquare]));
                StartCoroutine(Rotate(Vector3.back, 90, collectSquares.squares[switchSquares.indexSquare], 0.2f));
            }
            //StartCoroutine(RotateMe(Vector3.back * 90, speed, collectSquares.squares[switchSquares.indexSquare]));
            // topka.GetComponent<MoveBallController>().readyToRotate = false;
            //}
            //}

            //}

        }

        if (collectSquares.squares[switchSquares.indexSquare].tag == "SquareCounterClockWise")
        {
            string[] name = collectSquares.squares[switchSquares.indexSquare].name.Split(new char[] { '_', }, System.StringSplitOptions.RemoveEmptyEntries);
            string nameOfFigure = name[name.Length - 1];
            //foreach (var topka in collectBalls.balls)
            //{
            //if (topka.GetComponent<MoveBallController>().readyToRotate == true)
            //{
            if (StartsWith(nameOfFigure, "P"))
            {
                //StartCoroutine(RotateMe(Vector3.forward * 45, speed, collectSquares.squares[switchSquares.indexSquare]));
                StartCoroutine(Rotate(Vector3.forward, 45, collectSquares.squares[switchSquares.indexSquare], 0.1f));
            }
            else
            {
                //StartCoroutine(RotateMe(Vector3.forward * 90, speed, collectSquares.squares[switchSquares.indexSquare]));
                StartCoroutine(Rotate(Vector3.forward, 90, collectSquares.squares[switchSquares.indexSquare], 0.2f));
            }

            //}
            //}            
        }

        if (collectBalls != null && collectBalls.balls.Count != 0)
        {
            //Debug.Log(collectBalls.balls.Count);
            foreach (var topka in collectBalls.balls)
            {
                if (topka != null && topka.GetComponent<MoveBallController>() != null)
                {
                    topka.GetComponent<MoveBallController>().foundColliders.Clear();
                    topka.GetComponent<MoveBallController>().collidersAreFound = false;
                    topka.GetComponent<MoveBallController>().collidersAdded = false;

                }
                //Debug.Log(topka.GetComponent<MoveBallController>() == null);
            }

            //ball = collectBalls.balls[collectBalls.balls.Count - 1];
        }
    }

    public bool StartsWith(string value, string currentChar)
    {
        return value.StartsWith(currentChar, true, null);
    }

    //private IEnumerator RotateMe(Vector3 byAngles, float timeSpeed, GameObject square)
    //{
    //    rotating = true;

    //    rotateButton.interactable = false;
    //    var currentAngle = square.transform.rotation;
    //    var toAngle = Quaternion.Euler(square.transform.eulerAngles + byAngles);

    //    for (var t = 0f; t < 1; t += Time.fixedDeltaTime / timeSpeed)
    //    {
    //        square.transform.rotation = Quaternion.Slerp(currentAngle, toAngle, t);
    //        yield return null;
    //    }
    //    rotateIsClicked = false;
    //    square.transform.rotation = toAngle;
    //    rotateButton.interactable = true;
    //    rotating = false;
    //}

    //float duration = 0.2f

    private IEnumerator Rotate(Vector3 axis, float angle, GameObject square, float duration)
    {
        rotating = true;
        rotateButton.interactable = false;
        Quaternion from = square.transform.rotation;
        Quaternion to = square.transform.rotation;

        to *= Quaternion.Euler(axis * angle);
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            if (square != null)
            {
                square.transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
        rotateIsClicked = false;
        square.transform.rotation = to;
        rotateButton.interactable = true;
        rotating = false;
    }
}
