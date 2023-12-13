using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsCheckpointController : MonoBehaviour
{
    [SerializeField] private GameObject stopObject;
    private MoveBallController currentMoveBallController;
    private CollectSquares collectSquares;
    private GameObject currentSquare;
    private VacuumController firstVacuumController;
    private List<GameObject> listHoles;
    internal bool ballIsUnleashed;
    private RotateSquare rotateSquare;
    private LetFirstBall leftFirstBall;
    private GameObject currentBall;
    private GameObject previousAndAlreadyMovingBall;
    private bool currentBallIsSelectedFirstTime = false;
    private bool currentBallIsSelectedNextTimes = false;
    // Start is called before the first frame update
    //void Start()
    //{
    //    listHoles = new List<GameObject>();
    //    collectSquares = GetComponent<CollectSquares>();
    //    rotateSquare = GetComponent<RotateSquare>();
    //    currentSquare = collectSquares.squares[0];
    //    ballIsUnleashed = false;
    //    leftFirstBall = stopObject.GetComponentInParent<LetFirstBall>();

    //    foreach (Transform hole in currentSquare.transform)
    //    {
    //        listHoles.Add(hole.gameObject);
    //        if (hole.transform.position.x < currentSquare.transform.position.x - 0.5f)
    //        {
    //            firstVacuumController = hole.GetComponent<VacuumController>();
    //        }
    //    }
    //}

    // Update is called once per frame
    //void Update()
    //{
    //    if (leftFirstBall.currentBallOnLine != null && currentBallIsSelectedFirstTime == false)
    //    {
    //        currentBall = leftFirstBall.currentBallOnLine;
    //        currentBallIsSelectedFirstTime = true;
    //    }

    //    if (currentBall != leftFirstBall.currentBallOnLine && currentBallIsSelectedNextTimes == false)
    //    {
    //        currentBall = leftFirstBall.currentBallOnLine;
    //        currentBallIsSelectedNextTimes = true;
    //    }

    //    if (currentBall != null && leftFirstBall.currentBallOnLine != null)
    //    {
    //        Debug.Log("current ball -> " + currentBall.name);
    //    }

    //    //if (rotateSquare.rotateIsClicked == true)
    //    //{

    //    //    foreach (GameObject hole in listHoles)
    //    //    {
    //    //        if (hole.transform.position.x < currentSquare.transform.position.x - 0.5f)
    //    //        {
    //    //            firstVacuumController = hole.GetComponent<VacuumController>();
    //    //            if (firstVacuumController.ball == null)
    //    //            {
    //    //                ballIsUnleashed = false;
    //    //            }
    //    //        }
    //    //    }
    //    //}
    //    //firstVacuumController.ball != null && ballIsUnleashed == false &&
    //    if (leftFirstBall.ballThatIsMoving != null)
    //    {
    //        if (leftFirstBall.ballThatIsMoving.GetComponent<MoveBallController>().canMove == false && ballIsUnleashed == false)
    //        {
    //            StartCoroutine(WaitUntilStart());
    //        }
    //    }

    //}


    //internal IEnumerator WaitUntilStart()
    //{
    //    //ballIsUnleashed = true;

    //    if (currentBall == leftFirstBall.currentBallOnLine && currentBall.GetComponent<MoveBallController>().isInside == false && leftFirstBall.ballThatIsMoving.GetComponent<MoveBallController>().isInside == true)
    //    {
    //        Debug.Log("current ball -> " + currentBall.name);
    //        MoveBallController moveballController = leftFirstBall.currentBallOnLine.GetComponent<MoveBallController>();
    //        moveballController.canMove = true;
    //        leftFirstBall.currentBallOnLine.GetComponent<CircleCollider2D>().isTrigger = true;
    //        previousAndAlreadyMovingBall = leftFirstBall.ballThatIsMoving;
    //        ballIsUnleashed = true;
    //        Debug.Log("moving ball -> " + previousAndAlreadyMovingBall.name);
    //        //stopObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
    //    }

    //    yield return new WaitForSecondsRealtime(0.5f);
    //    //Debug.Log(leftFirstBall.ballThatIsMoving.GetComponent<SpriteRenderer>().sprite.name);
    //    //stopObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
    //    previousAndAlreadyMovingBall.GetComponent<CircleCollider2D>().isTrigger = false;
    //    //currentBallIsSelectedFirstTime = false;
    //    currentBallIsSelectedNextTimes = false;
    //}
}
