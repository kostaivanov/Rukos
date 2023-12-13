using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] internal CanvasManager canvasManager;
    [SerializeField] private GameObject hitPoints, extraHitPoint;
    private bool bonusLifeAdded;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = PermanentFunctions.instance.score.ToString();
        bonusLifeAdded = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("ball has returned = " + PermanentFunctions.instance.ballHasReturned);
        //Debug.Log("rounds count = " + PermanentFunctions.instance.roundsCount);
        //Debug.Log("max lives = " + PermanentFunctions.instance.maxlives);
        //Debug.Log("extrapointa active li e = " + extraHitPoint.transform.GetChild(0).gameObject.activeSelf);
        if (string.CompareOrdinal(scoreText.text, PermanentFunctions.instance.score.ToString()) != 0)
        {
            scoreText.text = PermanentFunctions.instance.score.ToString();
        }

        if (PermanentFunctions.instance.ballHasReturned == PermanentFunctions.instance.maxlives)
        {
            canvasManager.SwitchCanvas(CanvasType.GameOverMenu);
            PermanentFunctions.instance.ballHasReturned = 0;
            PermanentFunctions.instance.score = 0;
        }
        else if (IfAllNotActive() == true && PermanentFunctions.instance.ballHasReturned == 3)
        {
            canvasManager.SwitchCanvas(CanvasType.GameOverMenu);
            PermanentFunctions.instance.ballHasReturned = 0;
            PermanentFunctions.instance.score = 0;
        }

        

        if (PermanentFunctions.instance.roundsCount > 0 && bonusLifeAdded == false)
        {
            if (PermanentFunctions.instance.ballHasReturned > 0 && extraHitPoint.transform.GetChild(0).gameObject.activeSelf == false)
            {
                PermanentFunctions.instance.ballHasReturned--;
            }

            bonusLifeAdded = true;

            if (IsAnyChildNotActive() == false)
            {
                extraHitPoint.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                hitPoints.transform.GetChild(PermanentFunctions.instance.ballHasReturned).gameObject.SetActive(true);
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

    public bool IfAllNotActive()
    {
        // Iterates through all direct childs of this object
        foreach (Transform child in hitPoints.transform)
        {
            if (child.gameObject.activeSelf == true) return false;
        }

        return true;
    }
}
