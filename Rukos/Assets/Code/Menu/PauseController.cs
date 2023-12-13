using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

internal class PauseController : MonoBehaviour
{
    [SerializeField] internal GameObject fittings, shapes, shadow;
    [SerializeField] private Button resumeButton, homeButton;
    internal List<GameObject> balls;
    [SerializeField] private List<SpriteRenderer> tubes;
    [SerializeField] private SpriteRenderer intersection;
    internal delegate IEnumerator PauseMenuAction(float time, Button button1, Button button2);
    internal static event PauseMenuAction OnAppearPause;

    private void OnEnable()
    {
        OnAppearPause += PermanentFunctions.instance.ScaleOverTime;

        foreach (SpriteRenderer tube in tubes)
        {
            tube.enabled = false;
        }

        fittings.SetActive(false);

        foreach (Transform shape in shapes.transform)
        {
            foreach (Transform shapeChild in shape.transform)
            {
                if (shapeChild.gameObject.tag == "ExplosionSprite")
                {
                    shapeChild.GetComponent<SpriteRenderer>().enabled = false;
                }
                if (shapeChild.gameObject.tag == "SquareLight")
                {
                    shapeChild.gameObject.SetActive(false);
                }
                shape.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        shadow.SetActive(false);

        intersection.enabled = false;
        balls = new List<GameObject>();

        balls.AddRange(GameObject.FindGameObjectsWithTag("Ball"));
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<SpriteRenderer>().enabled = false;
        }

        StartCoroutine(OnAppearPause(0.5f, resumeButton, homeButton));

        Application.targetFrameRate = 30;
        Time.timeScale = 0;
    }
    
    private void OnDisable()
    {
        OnAppearPause -= PermanentFunctions.instance.ScaleOverTime;

        Application.targetFrameRate = 60;
        Time.timeScale = 1;
    }
}
