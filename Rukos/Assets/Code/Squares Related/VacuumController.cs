using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumController : MonoBehaviour
{
    //[SerializeField] private float magnetStrength;
    //[SerializeField] private float magnetRange;

    private Rigidbody2D ballRigidBody;
    //internal bool ballIsInside = false;
    internal Transform ball;
    private MoveBallController moveBallController;
    private SwitchSquares switchSquares;
    private CollectSquares collectSquares;

    //Transform magnet;
    //float radius = 5f;
    //float force = 100f;
    internal bool thereIsBall = false;
    private PushBallFunctions pushBallObject;

    private AudioSource squareAS;
    private AudioClip clashSound;

    private void Start()
    {
        switchSquares = this.transform.root.GetComponent<SwitchSquares>();
        collectSquares = this.transform.root.GetComponent<CollectSquares>();
        pushBallObject = new PushBallFunctions();
        squareAS = this.gameObject.transform.root.GetComponent<AudioSource>();
        clashSound = GetComponentInParent<CollectBalls>().clashSound;
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.tag == "BallChild")
        {
            //Debug.Log("koi kvadrat wyrtim ? " + this.transform.root.GetComponent<RotateSquare>().rotateIsClicked);
            if (ball != null)
            {
                moveBallController = otherObject.GetComponentInParent<MoveBallController>();
                moveBallController.isInside = false;
                PlayClashSound();
                pushBallObject.CheckBallDirectionAndPushOposite(otherObject.gameObject, this.gameObject);
                //CheckBallDirectionAndPushOposite(otherObject);
                Debug.Log("izpylqvan red 1");
            }
            else if (ball == null && this.transform.parent.GetSiblingIndex() == switchSquares.indexSquare && this.transform.root.GetComponent<RotateSquare>().rotateIsClicked == true && collectSquares.squares[switchSquares.indexSquare].GetComponent<CollectBalls>().balls.Count > 0)
            {
                pushBallObject.CheckBallDirectionAndPushOposite(otherObject.gameObject, this.gameObject);
                PlayClashSound();
                //CheckBallDirectionAndPushOposite(otherObject);
                Debug.Log("izpylqvan red 2");

            }
            else
            {
                moveBallController = otherObject.GetComponentInParent<MoveBallController>();
                ballRigidBody = otherObject.GetComponentInParent<Rigidbody2D>();
                //moveBallController = otherObject.GetComponentInParent<MoveBallController>();


                thereIsBall = true;
                ball = otherObject.transform;
                moveBallController.canMove = false;

                moveBallController.isInside = true;
                moveBallController.directionName = string.Empty;
                moveBallController.direction = Vector3.zero;
                ballRigidBody.Sleep();
                Debug.Log("izpylqvan red 3");

            }
        }

        //Vector3 magnetField = otherObject.gameObject.transform.position - this.transform.position;
        //float index = (radius - magnetField.magnitude) / radius;
        //ballRigidBody.AddForce(force * magnetField * index);

    }
    public void PlayClashSound()
    {
        squareAS.PlayOneShot(clashSound);
    }

    private void OnTriggerExit2D(Collider2D otherObject)
    {
        if (otherObject.tag != "BallChild")
        {
            ball = null;
            moveBallController = null;
            ballRigidBody = null;
            thereIsBall = false;
        }
    }
}
