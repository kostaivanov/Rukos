using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBalls : MonoBehaviour
{
    private const int sameColorBallsCount2H = 1;
    private const int sameColorBallsCount3H = 2;
    private const int sameColorBallsCount4H = 3;


    internal List<GameObject> balls;
    internal bool added = false;
    //internal bool exit = false;
    internal int count = 0;
    internal bool squareIsFull;
    //internal bool firstIsIn;
    internal int sameColor = 0;

    internal bool checkedIfSquareIsFull = false;

    private AudioSource squaresAS;
    [SerializeField] internal AudioClip vacuumSound, clashSound;

    private void Start()
    {
        balls = new List<GameObject>();
        squareIsFull = false;

        squaresAS = GetComponentInParent<AudioSource>();
    }

    //private void Update()
    //{
    //    Debug.Log("same color count please - " + sameColor);
    //}

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.layer == LayerMask.NameToLayer("Balls"))
        {
            if (otherObject.tag == "Ball" && added == false && checkedIfSquareIsFull == false)
            {
                balls.Add(otherObject.gameObject);
                count++;
                added = true;
                PlayVacuumSound();
                //Debug.Log(otherObject.gameObject.name + " -  kvadrata sa kazva - " + this.gameObject.name);
                // StartCoroutine(WaitToOpenPosition());

                if (this.transform.GetSiblingIndex() == 0)
                {
                    GameObject stopObject = GameObject.FindGameObjectWithTag("StopObject");
                    LetFirstBall letFirstBall = stopObject.GetComponent<LetFirstBall>();
                    letFirstBall.ballIsUnleashed = false;
                }
            }
            added = false;
        }       
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
       
    //}
    private IEnumerator WaitToOpenPosition()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        added = false;
    }

    //private IEnumerator WaitSinceExit()
    //{
    //    yield return new WaitForSecondsRealtime(0.5f);
    //    exit = false;
    //}

    //private void OnTriggerExit2D(Collider2D otherObject)
    //{
    //    if (otherObject.tag == "Ball" && deleted == false)
    //    {
    //        balls.Remove(otherObject.gameObject);
    //        deleted = true;
    //    }
    //}
    public void PlayVacuumSound()
    {
        squaresAS.PlayOneShot(vacuumSound);
    }

    private void Update()
    {
        if (count == 2 && checkedIfSquareIsFull == false && sameColor != 3 && squareIsFull == false)
        {
            string[] name = this.gameObject.name.Split(new char[] { '_', }, System.StringSplitOptions.RemoveEmptyEntries);
            string nameOfFigure = name[name.Length - 2];

            if (StartsWith(nameOfFigure, "2"))
            {
                LoopBallCollectionAndCHeckIfFull(sameColorBallsCount2H);
            }
        }
        if (count == 3 && checkedIfSquareIsFull == false && sameColor != 3 && squareIsFull == false)
        {
            string[] name = this.gameObject.name.Split(new char[] { '_', }, System.StringSplitOptions.RemoveEmptyEntries);
            string nameOfFigure = name[name.Length - 2];

            if (StartsWith(nameOfFigure, "3"))
            {
                LoopBallCollectionAndCHeckIfFull(sameColorBallsCount3H);
            }
        }
        else if (count == 4 && checkedIfSquareIsFull == false && sameColor != 3 && squareIsFull == false)
        {
            string[] name = this.gameObject.name.Split(new char[] { '_', }, System.StringSplitOptions.RemoveEmptyEntries);
            string nameOfFigure = name[name.Length - 2];

            if (StartsWith(nameOfFigure, "4"))
            {
                
                LoopBallCollectionAndCHeckIfFull(sameColorBallsCount4H);
            }

            //Debug.Log("how many times????? is this palying we");
        }
        //if (count == 8 && checkedIfSquareIsFull == false && sameColor != 3 && squareIsFullWith_4 == false)
        //{
        //    string[] name = this.gameObject.name.Split(new char[] { '_', '(', ')' }, System.StringSplitOptions.RemoveEmptyEntries);
        //    string nameOfFigure = name[1];
        //    if (nameOfFigure == "Polygon")
        //    {
        //        LoopBallCollectionAndCHeckIfFull(sameColorBallsCountPolygon);
        //    }
        //}
        //Debug.Log(this.gameObject + "Count - " + count + " - squareIsFullWith_4 = " + squareIsFull + $"  -  ama v lista sa {balls.Count}" + $"  a ot sushtiq cvqt ? - {sameColor}");
    }

    public bool StartsWith(string value, string currentChar)
    {
        return value.StartsWith(currentChar, true, null);
    }

    private void LoopBallCollectionAndCHeckIfFull(int ballsSameColorCount)
    {
        for (int i = 0, j = 1; j < balls.Count; j++)
        {
            Sprite ballColor = balls[i].GetComponent<SpriteRenderer>().sprite;

            if (balls[j].GetComponent<SpriteRenderer>().sprite == ballColor)
            {
                sameColor++;
            }
        }
        if (sameColor == ballsSameColorCount)
        {
            squareIsFull = true;
        }
        //else
        //{
        //    sameColor = 0;
        //}
        checkedIfSquareIsFull = true;
    }

    private void OnTriggerExit2D(Collider2D otherObject)
    {
        if (otherObject.tag == "Ball")
        {                    
            checkedIfSquareIsFull = false;
            sameColor = 0;           
        }

        //if (otherObject.tag == "Ball" && added == false)
        //{
        //    count--;
        //    exit = true;
        //    Debug.Log("how many times nigga");
        //    StartCoroutine(WaitSinceExit());
        //}
    }
}
