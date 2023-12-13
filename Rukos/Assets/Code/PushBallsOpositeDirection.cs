using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBallsOpositeDirection : MonoBehaviour
{
    [SerializeField] private ChangeColorStopObject interSectionObjectChangeColor;
    [SerializeField] private GameObject hitPoints, extraHitPoint;
    private bool extraPointDeactivated;

    private Color currentColor;
    private bool colorIsChanged = false;


    private void Start()
    {
        extraPointDeactivated = false;
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.tag == "Ball" && otherObject.transform.position.y > this.gameObject.transform.position.y && colorIsChanged == false)
        {
            Handheld.Vibrate();

            otherObject.gameObject.GetComponent<MoveBallController>().directionName = "Up";
            currentColor = interSectionObjectChangeColor.intersectionSpriteRenderer.color;
            StartCoroutine(TurnRed());

            if (PermanentFunctions.instance.maxlives > 3 && extraHitPoint.transform.GetChild(0).gameObject.activeSelf == true)
            {
                extraHitPoint.transform.GetChild(0).gameObject.SetActive(false);
                extraPointDeactivated = true;
            }
            else
            {
                hitPoints.transform.GetChild(PermanentFunctions.instance.ballHasReturned).gameObject.SetActive(false);
            }

            if (extraPointDeactivated == false)
            {
                PermanentFunctions.instance.ballHasReturned++;

            }
            extraPointDeactivated = false;
            colorIsChanged = true;
        }
    }

    private IEnumerator TurnRed()
    {
        interSectionObjectChangeColor.intersectionSpriteRenderer.color = new Color(1f, 0f, 0f, 0.6509f);
        yield return new WaitForSecondsRealtime(1.5f);
        interSectionObjectChangeColor.intersectionSpriteRenderer.color = currentColor;
        colorIsChanged = false;
    }
}
