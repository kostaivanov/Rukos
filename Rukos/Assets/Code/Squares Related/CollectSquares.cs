using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CollectSquares : MonoBehaviour
{
    [SerializeField] private GameObject parentSquares;
    internal List<GameObject> squares;
    private SpriteRenderer squareSpriteRenderer;
    [SerializeField] internal Sprite blueSelectionSquare;
    [SerializeField] internal Sprite whiteSelectionSquare;
    [SerializeField] internal Sprite blueSelectionPolygon;
    [SerializeField] internal Sprite whiteSelectionPolygon;
    private GameObject childWithLight;
    private GenerateNewShapes generateNewShapes;
    //private Light childLight;
    private SpriteRenderer childSpriteRenderer;
    internal bool squaresInitialized = false;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        generateNewShapes = GetComponent<GenerateNewShapes>();
        squares = new List<GameObject>();
        if (squares != null)
        {
            //Transform[] children = parentSquares.GetComponentsInChildren<Transform>(true);
            //Debug.Log(children.Length);
            //foreach (Transform child in children)
            //{
            //    squares.Add(child.gameObject);
            //    child.transform.GetChild(4).gameObject.GetComponent<Light>().enabled = false;
            //    child.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().sprite = whiteSelectionSquare;
            //    child.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.6f);
            //}


            InitializeSquareList();
        }
        //whiteSelectionSquare.color = new Color(0.55f, 0.55f, 0.55f);
        //childWithLight = squares[0].transform.GetChild(4).gameObject;
        //childLight = childWithLight.GetComponent<Light>();
        //childLight.enabled = true;
        //childSpriteRenderer = childWithLight.GetComponent<SpriteRenderer>();
        //childSpriteRenderer.sprite = blueSelectionSquare;

    }

    private void Update()
    {
        if (generateNewShapes.shapesAreDestroyed == true && squaresInitialized == false)
        {
            InitializeSquareList();
            squaresInitialized = true;
        }
    }

    internal void InitializeSquareList()
    {
        for (int i = 0; i < parentSquares.transform.childCount; i++)
        {
            squares.Add(parentSquares.transform.GetChild(i).gameObject);

            squares[i].transform.GetChild(GetLastChild(squares[i])).gameObject.GetComponent<Light>().enabled = false;
            string[] name = squares[i].name.Split(new char[] { '_', }, System.StringSplitOptions.RemoveEmptyEntries);
            string nameOfFigure = name[name.Length - 1];
            //Debug.Log(squares[i].gameObject.name);
            //if (nameOfFigure.Trim() == "Polygon".Trim())
            if (StartsWith(nameOfFigure, "P"))
            {
                squares[i].transform.GetChild(GetLastChild(squares[i])).gameObject.GetComponent<SpriteRenderer>().sprite = whiteSelectionPolygon;
            }
            else
            {
                squares[i].transform.GetChild(GetLastChild(squares[i])).gameObject.GetComponent<SpriteRenderer>().sprite = whiteSelectionSquare;
            }

            squares[i].transform.GetChild(GetLastChild(squares[i])).gameObject.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.6f);

        }
        string[] fullNameFirstShape = squares[0].name.Split(new char[] { '_', }, System.StringSplitOptions.RemoveEmptyEntries);

        string nameOfFirstShape = fullNameFirstShape[fullNameFirstShape.Length - 1];
        FlashLightTheBox(squares[0], nameOfFirstShape.Trim());
    }

    public bool StartsWith(string value, string currentChar)
    {
        return value.StartsWith(currentChar, true, null);
    }

    internal void FlashLightTheBox(GameObject square, string name)
    {
        //squareSpriteRenderer = square.GetComponent<SpriteRenderer>();
        //squareSpriteRenderer.color = new Color(0.55f, 0.55f, 0.55f);

        childWithLight = square.transform.GetChild(GetLastChild(square)).gameObject;
        childWithLight.GetComponent<Light>().enabled = true;
        childSpriteRenderer = childWithLight.GetComponent<SpriteRenderer>();

        if (StartsWith(name, "P"))
        {
            childSpriteRenderer.sprite = blueSelectionPolygon;
        }
        else
        {
            childSpriteRenderer.sprite = blueSelectionSquare;
        }

        if (childSpriteRenderer.sprite == blueSelectionSquare || childSpriteRenderer.sprite == blueSelectionPolygon)
        {
            childSpriteRenderer.color = new Color(1f, 1f, 1f);
        }
        else
        {
            childSpriteRenderer.color = new Color(0.6f, 0.6f, 0.6f);
        }
    }

    internal void ReturnOriginalColor(GameObject square, string name)
    {
        //squareSpriteRenderer = square.GetComponent<SpriteRenderer>();
        //squareSpriteRenderer.color = new Color(1f, 1f, 1f);

        childWithLight = square.transform.GetChild(GetLastChild(square)).gameObject;
        childWithLight.GetComponent<Light>().enabled = false;

        childSpriteRenderer = childWithLight.GetComponent<SpriteRenderer>();

        if (StartsWith(name, "P"))
        {
            childSpriteRenderer.sprite = whiteSelectionPolygon;
        }
        else
        {
            childSpriteRenderer.sprite = whiteSelectionSquare;
        }

        if (childSpriteRenderer.sprite == blueSelectionSquare || childSpriteRenderer.sprite == blueSelectionPolygon)
        {
            childSpriteRenderer.color = new Color(1f, 1f, 1f);
        }
        else
        {
            childSpriteRenderer.color = new Color(0.6f, 0.6f, 0.6f);
        }
    }

    private int GetLastChild(GameObject square)
    {
        int lastChildIndex = square.transform.childCount - 1;
        return lastChildIndex;
    }
}
