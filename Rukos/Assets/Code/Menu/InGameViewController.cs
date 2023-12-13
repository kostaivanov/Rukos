using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class InGameViewController : MenusComponents
{
    [SerializeField] internal GameObject shapes, fittings, shadow, stopobject, hitPointsObj, extraPointObj;
    internal List<GameObject> balls;
    [SerializeField] private List<SpriteRenderer> tubes;
    [SerializeField] private SpriteRenderer intersection;
    [SerializeField] private PlayHandler playHandler;

    public override GameObject Obj1 { get { return shapes; } set { shapes = value; } }
    public override GameObject Obj2 { get { return fittings; } set { fittings = value; } }
    public override GameObject Obj3 { get { return shadow; } set { shadow = value; } }
    public override GameObject Obj4 { get { return stopobject; } set { stopobject = value; } }

    private void OnEnable()
    {
        //Debug.Log("on enable ingameviewcontroller");
        foreach (SpriteRenderer tube in tubes)
        {
            tube.enabled = true;
        }      

        base.ActivateObjects();
     
        intersection.enabled = true;
        balls = new List<GameObject>();

        balls.AddRange(GameObject.FindGameObjectsWithTag("Ball"));
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<SpriteRenderer>().enabled = true;
        }

        Application.targetFrameRate = 60;
        Time.timeScale = 1;
        if (PermanentFunctions.instance.ballHasReturned == 0)
        {
            foreach (Transform hitPoint in hitPointsObj.transform)
            {
                hitPoint.gameObject.SetActive(true);
            }
        }

        if (PermanentFunctions.instance.roundsCount > 0)
        {
            extraPointObj.gameObject.SetActive(true);
        }
        else
        {
            extraPointObj.transform.GetChild(0).gameObject.SetActive(false);

        }
    }
}
