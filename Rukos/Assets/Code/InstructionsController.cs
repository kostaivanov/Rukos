using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsController : MonoBehaviour
{
    [SerializeField] private GameObject shapes, rotateInstructions, switchRightInstructions, pushRightInstruction, pushDownInstruction, homeButtonObj;
    private MoveBallController tutorialMoveBallScript;
    internal bool rotateInstr_ON, switchedToRight, tutBallReleasedRight, tutBallReleasedDown;
    [SerializeField] private LetFirstBall letFirstBallScript;
    [SerializeField] private SwitchSquares switchSquaresScript;
    [SerializeField] private GoTutorial goTutorialScript;
    internal bool homeButton_ON;

    // Start is called before the first frame update
    void Start()
    {
        rotateInstr_ON = false;
        switchedToRight = false;
        tutBallReleasedRight = false;
        //homeButtonObj.SetActive(false);
        homeButton_ON = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(shapes.transform.GetChild(3).GetComponent<Animator>().GetBool("PutBrokenSprite"));
        if (letFirstBallScript.tutorialMoveBallScript != null)
        {
            if (letFirstBallScript.tutorialMoveBallScript.isInside == true && rotateInstr_ON == false && FindWhichShapeIsTheBall() == switchSquaresScript.indexSquare && letFirstBallScript.tutorialMoveBallScript.youCanRelease_Right == false)
            {
                if (pushRightInstruction.activeSelf == true)
                {
                    pushRightInstruction.SetActive(false);
                }
                rotateInstructions.SetActive(true);
                rotateInstr_ON = true;
            }

            else if (letFirstBallScript.tutorialMoveBallScript.isInside == true && letFirstBallScript.tutorialMoveBallScript.youCanRelease_Right == true && rotateInstr_ON == true && FindWhichShapeIsTheBall() == switchSquaresScript.indexSquare)
            {
                if (pushDownInstruction.activeSelf == true)
                {
                    pushDownInstruction.SetActive(false);
                }
                rotateInstructions.SetActive(false);
                pushRightInstruction.SetActive(true);
                rotateInstr_ON = false;
            }
            else  if(FindWhichShapeIsTheBall() != switchSquaresScript.indexSquare && rotateInstructions.activeSelf == true)
            {
                rotateInstructions.SetActive(false);
                rotateInstr_ON = false;

            }

            if (FindWhichShapeIsTheBall() != switchSquaresScript.indexSquare && pushRightInstruction.activeSelf == true)
            {
                pushRightInstruction.SetActive(false);
                rotateInstr_ON = false;

            }
            else if(FindWhichShapeIsTheBall() == switchSquaresScript.indexSquare && pushRightInstruction.activeSelf == false && letFirstBallScript.tutorialMoveBallScript.youCanRelease_Right == true)
            {
                pushRightInstruction.SetActive(true);
            }

            if (tutBallReleasedRight == true)
            {
                tutBallReleasedRight = false;
                pushRightInstruction.SetActive(false);
                switchRightInstructions.SetActive(true);
            }

            if (switchSquaresScript.indexSquare == 0 && shapes.transform.GetChild(1).gameObject.GetComponent<CollectBalls>().count == 1)
            {
                switchRightInstructions.SetActive(true);
            }
            else
            {
                switchRightInstructions.SetActive(false);
            }

            if (switchedToRight == true)
            {
                switchRightInstructions.SetActive(false);
                switchedToRight = false;
            }
            if (letFirstBallScript.tutorialMoveBallScript.isInside == true && letFirstBallScript.tutorialMoveBallScript.youCanRelease_Down && rotateInstr_ON == true && letFirstBallScript.tutorialMoveBallScript.currentVacuumHole.transform.parent.GetSiblingIndex() == (1))
            {
                if (pushRightInstruction.activeSelf == true)
                {
                    pushRightInstruction.SetActive(false);
                }
                rotateInstructions.SetActive(false);
                pushDownInstruction.SetActive(true);
                rotateInstr_ON = false;
            }

            if (tutBallReleasedDown == true || FindWhichShapeIsTheBall() != switchSquaresScript.indexSquare)
            {
                tutBallReleasedDown = false;
                pushDownInstruction.SetActive(false);
            }
            else if (FindWhichShapeIsTheBall() == switchSquaresScript.indexSquare && letFirstBallScript.tutorialMoveBallScript.youCanRelease_Down == false)
            {
                pushDownInstruction.SetActive(false);
            }

           

        }

        if (goTutorialScript.clicked == true)
        {
            if (shapes.transform.GetChild(3).GetComponent<Animator>().GetBool("PutBrokenSprite") == true && homeButton_ON == false)
            {
                homeButtonObj.SetActive(true);
                homeButton_ON = true;
            }
        }
    }

    private int FindWhichShapeIsTheBall()
    {
        foreach (Transform transform in shapes.transform)
        {
            if (transform.gameObject.GetComponent<CollectBalls>().count == 1)
            {
                return transform.GetSiblingIndex();
            }
        }
        return -1;
    }
}
