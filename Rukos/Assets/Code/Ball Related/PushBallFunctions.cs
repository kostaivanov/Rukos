using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBallFunctions
{
    internal void CheckBallDirectionAndPushOposite(GameObject ballObejct, GameObject otherobject)
    {
        MoveBallController moveBallController = ballObejct.GetComponentInParent<MoveBallController>();
        //Debug.Log("Topka poziciq - " + otherObject.transform.position.y + " - " + "Vacuum Dupka poziciq - " + thisGameobject.transform.position.y);
        if (ballObejct.transform.position.x < otherobject.transform.position.x && otherobject.transform.position.x - ballObejct.transform.position.x > 0.5f)
        {
            //Debug.Log("Left");
            moveBallController.directionName = "Left";
            PushBallOppositeDirection(Vector2.left, ballObejct.gameObject);
        }
        else if (ballObejct.transform.position.x > otherobject.transform.position.x && ballObejct.transform.position.x - otherobject.transform.position.x > 0.5f)
        {
            //Debug.Log("Right");
            moveBallController.directionName = "Right";
            PushBallOppositeDirection(Vector2.right, ballObejct.gameObject);
        }
        else if (ballObejct.transform.position.y < otherobject.transform.position.y && otherobject.transform.position.y - ballObejct.transform.position.y > 0.5f)
        {
            //Debug.Log("Down");
            moveBallController.directionName = "Down";
            PushBallOppositeDirection(Vector2.down, ballObejct.gameObject);
        }
        else if (ballObejct.transform.position.y > otherobject.transform.position.y && ballObejct.transform.position.y - otherobject.transform.position.y > 0.5f)
        {
            //Debug.Log("UP");
            moveBallController.directionName = "Up";
            PushBallOppositeDirection(Vector2.up, ballObejct.gameObject);
        }
    }

    internal void PushBallOppositeDirection(Vector2 direction, GameObject needlessBall)
    {
        needlessBall.GetComponentInParent<Rigidbody2D>().AddForce(direction * 100f);
        needlessBall.GetComponentInParent<MoveBallController>().isInside = false;
        //Debug.Log("pushvash oposite li we mursha - " + needlessBall.transform.parent.name);
    }
}
