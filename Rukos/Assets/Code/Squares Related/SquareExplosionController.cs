using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SquareExplosionController : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect, stopObject;
    private LetFirstBall leftFirstBall;

    private GameObject clone_Explosion;
    private CollectSquares collectSquares;
    private List<GameObject> vacuumList;
    private SwitchSquares switchSquares;
    private List<VacuumController> squareWithBalls;
    private CollectBalls collectBalls;
    private int index = 0;
    private Animator squareAnimator;
    private bool squaresAssigned;


    [SerializeField] private AudioSource squaresAS;
    [SerializeField] private AudioClip explosionSound;
    private bool explosionSoundPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        squaresAssigned = false;

        collectSquares = GetComponent<CollectSquares>();
        switchSquares = GetComponent<SwitchSquares>();
        vacuumList = new List<GameObject>();
        squareWithBalls = new List<VacuumController>();
        leftFirstBall = stopObject.GetComponent<LetFirstBall>();

        if (vacuumList != null && collectSquares.squares != null)
        {
            foreach (Transform item in collectSquares.squares[switchSquares.indexSquare].transform)
            {
                vacuumList.Add(item.gameObject);
            }

        }

        foreach (var item in vacuumList)
        {
            squareWithBalls.Add(item.GetComponent<VacuumController>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (squaresAssigned == false && collectSquares.squares != null)
        {
            collectBalls = collectSquares.squares[switchSquares.indexSquare].GetComponent<CollectBalls>();
            squaresAssigned = true;
        }

        //Debug.Log(collectSquares.squares[switchSquares.indexSquare] + " - Count = " + collectSquares.squares[switchSquares.indexSquare].GetComponent<CollectBalls>().count);
        if (collectSquares.squares.Where(x => x != null).Any(x => x.GetComponent<CollectBalls>() != null) && collectSquares.squares.Any(x => x.GetComponent<CollectBalls>().squareIsFull == true))
        {
            PlayExplosionSound();
            float timeToDestroyBalls = 1.3f;
            GameObject collectBalls_OBj;
            collectBalls_OBj = collectSquares.squares.FirstOrDefault(x => x.GetComponent<CollectBalls>().squareIsFull == true);
            if (collectBalls_OBj != null)
            {
                squareAnimator = collectBalls_OBj.GetComponent<Animator>();
                //Debug.Log(collectBalls_OBj.name);
              

                if (collectBalls_OBj.GetComponent<PlayExplosion>().shapeIsInBrokenState == true || squareAnimator.GetCurrentAnimatorStateInfo(0).IsName("BrokenSquare") || squareAnimator.GetCurrentAnimatorStateInfo(0).IsName("GreenBrokenPolygon") || squareAnimator.GetCurrentAnimatorStateInfo(0).IsName("BlueBrokenPolygon"))
                {
                    ShakeCamera.Shake(0.5f, 0.1f);

                    clone_Explosion = Instantiate(explosionEffect, collectBalls_OBj.transform.position, Quaternion.identity);
                    Destroy(clone_Explosion, 0.8f);
                    timeToDestroyBalls = 0.3f;
                }
                else
                {
                    squareAnimator.SetTrigger("CrushTheSquare");
                    //if (leftFirstBall.ballThatIsMoving != null && leftFirstBall.ballThatIsMoving.GetComponent<MoveBallController>().isInside == true && leftFirstBall.ballIsUnleashed == false)
                    //{
                    //    StartCoroutine(leftFirstBall.WaitUntilLetTheBallGo());
                    //    leftFirstBall.ballIsUnleashed = true;
                    //
                    //}
                }

                foreach (var ball in collectBalls_OBj.GetComponent<CollectBalls>().balls)
                {
                    Destroy(ball, timeToDestroyBalls);
                }

                collectBalls_OBj.GetComponent<CollectBalls>().balls.Clear();
                collectBalls_OBj.GetComponent<CollectBalls>().squareIsFull = false;
                collectBalls_OBj.GetComponent<CollectBalls>().count = 0;
                collectBalls_OBj.GetComponent<CollectBalls>().sameColor = 0;
                collectBalls_OBj.GetComponent<CollectBalls>().checkedIfSquareIsFull = false;
                collectBalls_OBj = null;

            }
           
        }
    }

    public void PlayExplosionSound()
    {
        squaresAS.PlayOneShot(explosionSound);
    }
}
