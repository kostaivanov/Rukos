using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class ReleaseController : MonoBehaviour
{
    [SerializeField] private InstructionsController instructionsController;

    private CollectSquares collectSquares;
    private SwitchSquares switchSquares;
    internal bool ballIsInside = false;

    [SerializeField] private Button releaseLeft, releaseRight, releaseUp, releaseDown;
    private CollectBalls collectBalls;

    private int currentIndex = 0;
    private MoveBallController moveBallController;
    private RotateSquare rotateSquare;
    private GenerateNewShapes generateNewshapes;
    private bool squaresAssigned;

    [SerializeField] private AudioSource squaresAS;
    [SerializeField] private AudioClip releaseSound;

    // Start is called before the first frame update
    private void Start()
    {
        squaresAssigned = false;

        collectSquares = GetComponent<CollectSquares>();
        switchSquares = GetComponent<SwitchSquares>();

        releaseLeft.onClick.AddListener(ReleaseBallLeft);
        releaseRight.onClick.AddListener(ReleaseBallRight);
        releaseUp.onClick.AddListener(ReleaseBallUp);
        releaseDown.onClick.AddListener(ReleaseBallDown);
        rotateSquare = GetComponent<RotateSquare>();
        generateNewshapes = GetComponent<GenerateNewShapes>();
        //currentIndex = switchSquares.indexSquare;
        currentIndex = 0;
    }

    private void Update()
    {
        if (collectBalls == null && squaresAssigned == false && collectSquares.squares != null)
        {
            collectBalls = collectSquares.squares[currentIndex].GetComponent<CollectBalls>();
            squaresAssigned = true;
        }

        if (switchSquares.indexSquare != currentIndex)
        {
            collectBalls = null;
            currentIndex = switchSquares.indexSquare;
            collectBalls = collectSquares.squares[currentIndex].GetComponent<CollectBalls>();
        }

        if (generateNewshapes.shapesAreDestroyed == true)
        {
            currentIndex = 0;
            //collectSquares = squaresCollection.GetComponent<CollectSquares>();
            collectSquares = GetComponent<CollectSquares>();
            if (collectBalls == null)
            {
                collectBalls = collectSquares.squares[currentIndex].GetComponent<CollectBalls>();
            }
        }
    }

    private void ReleaseBallLeft()
    {
        if (currentIndex != 0 && collectBalls != null && collectBalls.balls.Count != 0)
        {
            PlayReleaseSound();
            foreach (var topka in collectBalls.balls)
            {
                //if (topka != null && topka.transform.position.x < collectSquares.squares[currentIndex].transform.position.x - 0.5f)
                //{
                //    //ball = topka;
                //    moveBallController = topka.GetComponent<MoveBallController>();
                //    if (moveBallController.readyToRotate == true && moveBallController.youCanRelease_Left == true && rotateSquare.rotating == false)
                //    {

                //        StartCoroutine(WaitAfterRelease(topka, Vector2.left));
                //        return;
                //    }

                //}
                moveBallController = topka.GetComponent<MoveBallController>();
                if (moveBallController.readyToRotate == true && moveBallController.youCanRelease_Left == true && rotateSquare.rotating == false)
                {

                    StartCoroutine(WaitAfterRelease(topka, Vector2.left));
                    return;
                }
            }

            //ball = collectBalls.balls[collectBalls.balls.Count - 1];

            
        }
    }

    private void ReleaseBallRight()
    {
        if (collectBalls != null && collectBalls.balls.Count != 0 && currentIndex == switchSquares.indexSquare)
        {
            PlayReleaseSound();
            foreach (var topka in collectBalls.balls)
            {
                //Debug.Log("topka  " + topka.name + " - " + topka.gameObject.transform.position.y);
                //Debug.Log("kvadrat  " + $"{collectSquares.squares[switchSquares.indexSquare].transform.position.y - 0.5f}");

                moveBallController = topka.GetComponent<MoveBallController>();
                //if (topka != null && topka.transform.position.x > collectSquares.squares[currentIndex].transform.position.x + 0.5f)
                //{
                //    Debug.Log("topka  " + topka.name + " - " + topka.gameObject.transform.position.y);
                //    if (moveBallController.readyToRotate == true && moveBallController.youCanRelease_Right == true && rotateSquare.rotating == false)
                //    {
                //        moveBallController.youCanRelease_Right = false;
                //        StartCoroutine(WaitAfterRelease(topka, Vector2.right));
                //        return;
                //    }
                    
                //}
                if (moveBallController.readyToRotate == true && moveBallController.youCanRelease_Right == true && rotateSquare.rotating == false)
                {
                    instructionsController.tutBallReleasedRight = true;
                    //Debug.Log("topka  " + topka.name + " - " + topka.gameObject.transform.position.y);
                    moveBallController.youCanRelease_Right = false;
                    StartCoroutine(WaitAfterRelease(topka, Vector2.right));
                    return;
                }
            }

            //ball = collectBalls.balls[collectBalls.balls.Count - 1];

            
        }
    }

    private void ReleaseBallUp()
    {
        if (collectBalls != null && collectBalls.balls.Count != 0)
        {
            PlayReleaseSound();
            foreach (var topka in collectBalls.balls)
            {
                moveBallController = topka.GetComponent<MoveBallController>();

                //if (topka.transform.position.y > collectSquares.squares[switchSquares.indexSquare].transform.position.y + 0.5f)
                //{
                //    //ball = topka;
                //    if (topka.GetComponent<MoveBallController>().youCanRelease_Up == true && rotateSquare.rotating == false)
                //    {
                //        StartCoroutine(WaitAfterRelease(topka, Vector2.up));
                //        return;
                //    }
                    
                //}
                if (topka.GetComponent<MoveBallController>().youCanRelease_Up == true && rotateSquare.rotating == false)
                {
                    StartCoroutine(WaitAfterRelease(topka, Vector2.up));
                    return;
                }
            }

            //ball = collectBalls.balls[collectBalls.balls.Count - 1];

           
        }
    }

    private void ReleaseBallDown()
    {
        if (collectBalls != null && collectBalls.balls.Count != 0)
        {
            PlayReleaseSound();
            foreach (var topka in collectBalls.balls)
            {
                moveBallController = topka.GetComponent<MoveBallController>();
                //Debug.Log("topka  " + topka.gameObject.transform.position.y);
                //Debug.Log("kvadrat  " + $"{collectSquares.squares[switchSquares.indexSquare].transform.position.y - 0.5f}");
                //if (topka != null && topka.transform.position.y < collectSquares.squares[switchSquares.indexSquare].transform.position.y - 0.5f)
                //{
                    
                //    if (topka.GetComponent<MoveBallController>().youCanRelease_Down == true && rotateSquare.rotating == false)
                //    {
                //        StartCoroutine(WaitAfterRelease(topka, Vector2.down));
                //        return;
                //    }
                    
                //}
                if (topka.GetComponent<MoveBallController>().youCanRelease_Down == true && rotateSquare.rotating == false)
                {
                    instructionsController.tutBallReleasedDown = true;

                    StartCoroutine(WaitAfterRelease(topka, Vector2.down));
                    return;
                }
            }

            //ball = collectBalls.balls[collectBalls.balls.Count - 1];

            
        }
    }

    private IEnumerator WaitAfterRelease(GameObject ball, Vector3 directionPushBall)
    {
        MoveBallController moveBallController = ball.GetComponentInParent<MoveBallController>();

        ball.GetComponentInParent<Rigidbody2D>().velocity = Vector3.zero;
        moveBallController.isInside = false;

        SetBallNewDirection(directionPushBall, moveBallController);

        moveBallController.currentVacuumHole.SetActive(false);
        ball.GetComponentInParent<Rigidbody2D>().AddForce(directionPushBall * 700f);
        moveBallController.canMove = true;


        moveBallController.collidersAreFound = false;
        moveBallController.readyToRotate = false;


        collectBalls.balls.Remove(ball);
        collectBalls.count--;

        yield return new WaitForSecondsRealtime(0.5f);
        if (ball != null)
        {
            moveBallController.currentVacuumHole.SetActive(true);
        }
    }
    public void PlayReleaseSound()
    {
        squaresAS.PlayOneShot(releaseSound);
    }

    private static void SetBallNewDirection(Vector3 directionPushBall, MoveBallController moveBallController)
    {
        if (directionPushBall == Vector3.left)
        {
            moveBallController.direction = Vector3.left;
            moveBallController.directionName = "Left";
        }
        else if (directionPushBall == Vector3.right)
        {
            moveBallController.direction = Vector3.right;
            moveBallController.directionName = "Right";
        }
        else if (directionPushBall == Vector3.up)
        {
            moveBallController.direction = Vector3.up;
            moveBallController.directionName = "Up";
        }
        else if (directionPushBall == Vector3.down)
        {
            moveBallController.direction = Vector3.down;
            moveBallController.directionName = "Down";
        }
    }
}
