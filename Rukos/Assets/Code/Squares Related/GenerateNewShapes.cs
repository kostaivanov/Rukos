using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNewShapes : MonoBehaviour
{
    private CollectSquares collectSquares;
    [SerializeField] private GameObject[] standardGeometricShapes, nonStandardGeometricShapes;
    private Transform[] shapesPositions = new Transform[6];
    //private IncreaseDifficulty increaseDifficulty;
    private SwitchSquares switchSquares;
    public GameObject[] ballsInGame;
    //private GameObject clone_Shape;
    // Start is called before the first frame update
    internal bool shapesAreDestroyed = false;
    [SerializeField] private RetryHandler retryHandler;

    [SerializeField] private AudioSource squaresAS;
    [SerializeField] private AudioClip generateNewSound;
    [SerializeField] private GameObject hitPoints;

    public float chanceSpawnRare = 0.1f;
    private bool livesIncreased;

    void Start()
    {       
        switchSquares = GetComponent<SwitchSquares>();
        //increaseDifficulty = GetComponent<IncreaseDifficulty>();
        collectSquares = GetComponent<CollectSquares>();
        livesIncreased = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (AreAllShapesBroken() == true && shapesAreDestroyed == false)
        {
            GenerateNewShapesFunction(this.gameObject);
            if (PermanentFunctions.instance.roundsCount > 0 && PermanentFunctions.instance.maxlives == 3 && IsAnyChildNotActive() == false && livesIncreased == false)
            {
                PermanentFunctions.instance.maxlives = 4;
                livesIncreased = true;
            }
        }
    }

    public bool IsAnyChildNotActive()
    {
        // Iterates through all direct childs of this object
        foreach (Transform child in hitPoints.transform)
        {
            if (child.gameObject.activeSelf == false) return true;
        }

        return false;
    }

    public void PlayGenerateSound()
    {
        squaresAS.PlayOneShot(generateNewSound);
    }

    internal void GenerateNewShapesFunction(GameObject gameObject)
    {
        if (collectSquares.squares != null)
        {
            for (int j = 0; j < collectSquares.squares.Count; j++)
            {
                shapesPositions[j] = collectSquares.squares[j].transform;
            }
        }

        int i = DeleteShapesAndCreateNew();

        ballsInGame = GameObject.FindGameObjectsWithTag("Ball");
        if (ballsInGame.Length != 0)
        {
            foreach (GameObject ball in ballsInGame)
            {
                Destroy(ball);
            }
        }

        i = 0;
        collectSquares.squares.Clear();
        switchSquares.indexSquare = 0;
        if (gameObject.name != "Retry")
        {
            PermanentFunctions.instance.roundsCount++;
            //.Log("toq kur sq raboti li");
        }
        shapesAreDestroyed = true;
        StartCoroutine(TimeToResetSquares());
    }

    private int DeleteShapesAndCreateNew()
    {
        int i = 0;
        foreach (GameObject shape in collectSquares.squares)
        {
            foreach (GameObject ball in shape.GetComponent<CollectBalls>().balls)
            {
                Destroy(ball);
            }

            Destroy(shape);
            GameObject clone_Shape;
            if (retryHandler.retryClicked == true)
            {
                if (i % 2 == 0)
                {
                    //Debug.Log("even number");
                    clone_Shape = Instantiate(standardGeometricShapes[1], shapesPositions[i].position, Quaternion.identity);
                    clone_Shape.transform.parent = this.gameObject.transform;
                }
                else
                {
                    //Debug.Log("odd number");
                    clone_Shape = Instantiate(standardGeometricShapes[0], shapesPositions[i].position, Quaternion.identity);
                    clone_Shape.transform.parent = this.gameObject.transform;
                }
            }
            else
            {
                if (Random.Range(0f, 0.8f) > chanceSpawnRare)
                {
                    clone_Shape = Instantiate(standardGeometricShapes[Random.Range(0, standardGeometricShapes.Length)], shapesPositions[i].position, Quaternion.identity);
                    clone_Shape.transform.parent = this.gameObject.transform;
                }
                else
                {
                    clone_Shape = Instantiate(nonStandardGeometricShapes[Random.Range(0, nonStandardGeometricShapes.Length)], shapesPositions[i].position, Quaternion.identity);
                    clone_Shape.transform.parent = this.gameObject.transform;
                }               
            }

            i++;
        }

        retryHandler.retryClicked = false;

        return i;
    }

    private IEnumerator TimeToResetSquares()
    {
        yield return new WaitForSecondsRealtime(2f);
        shapesAreDestroyed = false;
        //increaseDifficulty.speedWasIncreased = false;
        collectSquares.squaresInitialized = false;
        PlayGenerateSound();
    }

    private bool AreAllShapesBroken()
    {
        foreach (GameObject shape in collectSquares.squares)
        {
            if (shape != null && shape.GetComponent<PlayExplosion>().shapeIsInBrokenState == false)
            {
                return false;
            }
        }
        return true;
    }
}
