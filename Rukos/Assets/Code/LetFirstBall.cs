using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetFirstBall : MonoBehaviour
{
    internal int ballCountPerStage = 2;

    [SerializeField] private GameObject ball, startPositionObj, hitPoints;
    [SerializeField] private Transform[] ballsPositions = new Transform[3];

    private GameObject clone_Explosion, cloneTutorial_0, cloneTutorial_1, cloneTutorial_2, cloneTutorial_3;
    [SerializeField] private Sprite[] colorSprites;
    internal bool firstBallUnleashed = false;
    internal GameObject currentBallOnLine, ballThatIsMoving;
    internal int countBalls = 0;
    private bool counted = false;
    private bool cloned = false;
    private bool thisBallAlreadyCounted = false;
    [SerializeField] private GenerateNewShapes generateNewshapes;
    internal int inGameRoundsTemp;
    private GameObject previousAndAlreadyMovingBall;
    internal bool ballIsUnleashed = false;
    [SerializeField] private PlayHandler playHandler;
    [SerializeField] private PauseHandler pauseHandler;
    [SerializeField] private ResumeHandler resumeHandler;
    [SerializeField] private RetryHandler retryhandler;
    [SerializeField] private GoTutorial tutorialHandler;
    internal MoveBallController tutorialMoveBallScript;

    internal bool passing = false;
    internal bool ballIsOut = false;
    [SerializeField] private LayerMask ballLayer;
    private Collider2D[] hitColliders;
    private const float searchCircleRadius = 0.125f;
    private bool letGoBall = true;
    [SerializeField] private GameObject ballPosCheck;

    private void OnEnable()
    {
        inGameRoundsTemp = PermanentFunctions.instance.roundsCount;

        //InGameViewController.OnClickedPlay += WaitUntilStart;
    }

    private void OnDisable()
    {
        //InGameViewController.OnClickedPlay -= WaitUntilStart;
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        
        if (currentBallOnLine != otherObject.gameObject && counted == true)
        {
            counted = false;
        }

        if (otherObject.tag == "Ball" && firstBallUnleashed == false)
        {
            StartCoroutine(WaitUntilStart(otherObject));
            //Debug.Log("how many times");
            firstBallUnleashed = true;
        }

        //if (otherObject.tag == "Ball" && otherObject.transform.position.y > this.gameObject.transform.position.y)
        //{
        //    otherObject.gameObject.GetComponent<MoveBallController>().directionName = "Up";
        //    countBalls--;
        //}
        if (otherObject.tag == "Ball" && counted == false)
        {
            currentBallOnLine = otherObject.gameObject;
            countBalls++;
            thisBallAlreadyCounted = false;
            counted = true;
            //otherObject.GetComponent<MoveBallController>().canMove = false;
            //otherObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

    }
    private void OnTriggerStay2D(Collider2D otherObject)
    {
        hitColliders = Physics2D.OverlapCircleAll(ballPosCheck.transform.position, searchCircleRadius, ballLayer);

        //if (hitColliders != null)
        //{
        //    foreach (Collider2D item in hitColliders)
        //    {
        //        Debug.Log(item.gameObject.name);
        //    }
        //}

        if (resumeHandler.resumed == true && hitColliders != null && pauseHandler.pauseIsNotClicked == true)
        {
            Collider2D topBallCollider;
            foreach (Collider2D collider in hitColliders)
            {
                if (collider.gameObject.transform.position.y > ballPosCheck.gameObject.transform.position.y)
                {
                    topBallCollider = collider;
                    if (currentBallOnLine != topBallCollider)
                    {
                        letGoBall = false;
                        resumeHandler.resumed = false;
                    }
                }

            }
        }
        else
        {
            letGoBall = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(ballPosCheck.transform.position, searchCircleRadius);

    }

    private void OnTriggerExit2D(Collider2D otherObject)
    {
        if (otherObject.tag == "Ball")
        {
            ballThatIsMoving = otherObject.gameObject;
            ballThatIsMoving.GetComponent<CircleCollider2D>().isTrigger = false;
            //Debug.Log("ko praim sq tuka, koq topka e we? - " + otherObject.gameObject.name);

        }

        ballIsOut = true;

        if (thisBallAlreadyCounted == false && PermanentFunctions.instance.roundsCount > 0)
        {
            float speed = otherObject.gameObject.GetComponent<MoveBallController>().moveSpeed;
            speed += PermanentFunctions.instance.roundsCount * PermanentFunctions.instance.increaseTheSpeed;

          

            otherObject.gameObject.GetComponent<MoveBallController>().moveSpeed = speed;
            thisBallAlreadyCounted = true;
        }

    }

    private void Update()
    {
        //if (currentBallOnLine != null)
        //{
        //    Debug.Log("Current ball - " + currentBallOnLine.name);

        //}
        //if (ballThatIsMoving != null)
        //{
        //    Debug.Log("Moving ball - " + ballThatIsMoving.name);

        //}
        //if (currentBallOnLine != null && ballThatIsMoving != null)
        //{
        //    Debug.Log("current ball - " + currentBallOnLine.name + " ==  dvijeshta topka - " + ballThatIsMoving.name);
        //}
        if (tutorialHandler.tutorialWasClicked == true)
        {

            tutorialHandler.tutorialWasClicked = false;
            Debug.Log("tutorial");
            CreateTutorialBalls();

        }

        if (playHandler.playWasClicked == true)
        {
            Debug.Log("play handler rabbotiii");

            StartCoroutine(WaitUntilStart());
            playHandler.playWasClicked = false;
        }

        if (countBalls == 4 && cloned == false)
        {
            //Debug.Log("kogato doide vreme da puska novi topki dokato igraesh");

            countBalls = 0;
            //clone_Explosion = Instantiate(ball, startPositionObj.transform.position, Quaternion.identity);
          
            StartCoroutine(WaitUntilStart());

            cloned = true;
        }
       
        if (inGameRoundsTemp != PermanentFunctions.instance.roundsCount)
        {
            if (ballCountPerStage < colorSprites.Length)
            {
                //Debug.Log("kogato minesh nivoto ");
                ballCountPerStage++;
            }
            countBalls = 0;
            StartCoroutine(WaitUntilStart());
            inGameRoundsTemp = PermanentFunctions.instance.roundsCount;
            firstBallUnleashed = false;
        }

        if (retryhandler.doNotIncreaseColors == true)
        {
            //Debug.Log("kogato dadaesh retry we maika mu da eba");

            //countBalls = 0;
            StartCoroutine(WaitUntilStart());
            //inGameRoundsTemp = PermanentFunctions.instance.roundsCount;
            //firstBallUnleashed = false;
            retryhandler.doNotIncreaseColors = false;
        }

        if (hitPoints.activeSelf == true && currentBallOnLine != null && ballThatIsMoving != null && ballThatIsMoving.GetComponent<MoveBallController>().isInside == true && ballIsUnleashed == false)
        {
            StartCoroutine(WaitUntilLetTheBallGo(currentBallOnLine));
            ballIsUnleashed = true;
            Debug.Log("asdasdasdasdasdasd wat ?");
        }

    }

    private IEnumerator WaitUntilStart(Collider2D ball)
    {
        yield return new WaitUntil(() => pauseHandler.pauseIsNotClicked);

       // passing = true;
        ball.GetComponent<MoveBallController>().canMove = true;
        ball.GetComponent<CircleCollider2D>().isTrigger = true;

        //otherObject.enabled = false;
        while (pauseHandler.pauseIsNotClicked == false)
        {
            yield return new WaitForSecondsRealtime(0.6f);
            ball.GetComponent<CircleCollider2D>().isTrigger = false;

            //passing = false;

        }
    }

    private IEnumerator WaitUntilStart()
    {
        //Debug.Log("clone please");

        clone_Explosion = Instantiate(ball, startPositionObj.transform.position, Quaternion.identity);
       
        clone_Explosion.GetComponent<SpriteRenderer>().sprite = colorSprites[Random.Range(0, ballCountPerStage)];
        yield return new WaitForSeconds(2f);
        clone_Explosion = Instantiate(ball, startPositionObj.transform.position, Quaternion.identity);

        clone_Explosion.GetComponent<SpriteRenderer>().sprite = colorSprites[Random.Range(0, ballCountPerStage)];
        yield return new WaitForSeconds(2f);
        clone_Explosion = Instantiate(ball, startPositionObj.transform.position, Quaternion.identity);

        clone_Explosion.GetComponent<SpriteRenderer>().sprite = colorSprites[Random.Range(0, ballCountPerStage)];
        yield return new WaitForSeconds(2f);
        clone_Explosion = Instantiate(ball, startPositionObj.transform.position, Quaternion.identity);

        clone_Explosion.GetComponent<SpriteRenderer>().sprite = colorSprites[Random.Range(0, ballCountPerStage)];
        //clone_Explosion.GetComponent<SpriteRenderer>().sprite = colorSprites[Random.Range(0, colorSprites.Length)];
        //InGameViewController.OnClickedPlay -= WaitUntilStart;

        cloned = false;
    }

    internal IEnumerator WaitUntilLetTheBallGo(GameObject currentBall)
    {
        yield return new WaitUntil(() => pauseHandler.pauseIsNotClicked);
      
        if (ballIsOut == true && letGoBall == true)
        {
            GameObject tempBallOnLine = currentBall;
            //passing = true;
            MoveBallController moveballController = tempBallOnLine.GetComponent<MoveBallController>();
            moveballController.canMove = true;
            tempBallOnLine.GetComponent<CircleCollider2D>().isTrigger = true;
            ballIsOut = false;
        }
        
        //previousAndAlreadyMovingBall = ballThatIsMoving;

        //Debug.Log("moving ball -> " + previousAndAlreadyMovingBall.name);
        //stopObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
     

        while (pauseHandler.pauseIsNotClicked == false)
        {
            yield return new WaitForSecondsRealtime(1f);
        }

        //Debug.Log(leftFirstBall.ballThatIsMoving.GetComponent<SpriteRenderer>().sprite.name);
        //stopObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
        if (ballThatIsMoving != null)
        {
            ballThatIsMoving.GetComponent<CircleCollider2D>().isTrigger = false;            
        }
        passing = false;
        //StopAllCoroutines();
        //ballIsUnleashed = false;
    }

    private void CreateTutorialBalls()
    {
        cloneTutorial_0 = Instantiate(ball, startPositionObj.transform.position, Quaternion.identity);
        cloneTutorial_0.GetComponent<SpriteRenderer>().sprite = colorSprites[0];
        tutorialMoveBallScript = cloneTutorial_0.GetComponent<MoveBallController>();


        cloneTutorial_1 = Instantiate(ball, ballsPositions[0].position, Quaternion.identity);
        cloneTutorial_1.GetComponent<SpriteRenderer>().sprite = colorSprites[0];

        cloneTutorial_2 = Instantiate(ball, ballsPositions[1].position, Quaternion.identity);
        cloneTutorial_2.GetComponent<SpriteRenderer>().sprite = colorSprites[0];

        cloneTutorial_3 = Instantiate(ball, ballsPositions[2].position, Quaternion.identity);
        cloneTutorial_3.GetComponent<SpriteRenderer>().sprite = colorSprites[0];
    }
}
