using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PermanentFunctions : MonoBehaviour
{
    //internal int InitialLivesCount => 5;
    public int GettingDamagedCount { get; set; } = 0;
    internal static PermanentFunctions instance;
    internal  float increaseTheSpeed = 0.15f;
    internal int score = 0;

    internal int roundsCount = 0;
    internal int ballHasReturned = 0;
    internal int maxlives = 3;

    //protected override void Awake()
    //{
    //    base.Awake();
    //}


    //this starts before Start() function
    private void Awake()
    {
        //Debug.Log("on enable permanent awake");

        //checking if there is isntance of this object, if not, create one
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);

        }
        //if there was already instance of the object, destroy it, to aovid multiple gameobject of this type
        else
        {
            Destroy(this.gameObject);
        }
    }

    internal IEnumerator ScaleOverTime(float time, Button button1, Button button2)
    {
        Vector2 destinationScale = new Vector3(1f, 1f, 1f);
        Vector2 originalScale = new Vector3(1f, 0, 1f);
        float currentTime = 0.0f;

        do
        {
            button1.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            button2.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.unscaledDeltaTime;
            yield return null;
        }
        while (currentTime <= time);
        button1.transform.localScale = destinationScale;
        button2.transform.localScale = destinationScale;

    }

}
