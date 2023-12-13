using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayExplosion : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    private GameObject clone_Explosion;
    //[SerializeField] private GameObject explosionParts;

    [SerializeField] private Sprite normalSquare;
    [SerializeField] private Sprite destroyedSquare;

    [SerializeField] private Material greenParts;
    [SerializeField] private Material blueParts;
    private Animator squareAnimator;

    private SpriteRenderer squareSpriteRenderer;
    private bool animationDone = false;
    internal bool shapeIsInBrokenState = false;

    private void Start()
    {
        squareSpriteRenderer = GetComponent<SpriteRenderer>();
        squareSpriteRenderer.sprite = normalSquare;
        squareAnimator = GetComponent<Animator>();
        //explosionParts.SetActive(false);
    }

    // Start is called before the first frame update

    public void PlayTheExplosion()
    {
        //Debug.Log("is this playing ?");
        if (this.gameObject.tag == "SquareClockWise")
        {
            explosionEffect.transform.GetChild(2).gameObject.GetComponent<ParticleSystemRenderer>().material = greenParts;
        }
        else if(this.gameObject.tag == "SquareCounterClockWise")
        {
            explosionEffect.transform.GetChild(2).gameObject.GetComponent<ParticleSystemRenderer>().material = blueParts;
        }

        ShakeCamera.Shake(0.5f, 0.1f);

        clone_Explosion = Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
        Destroy(clone_Explosion, 0.8f);
        squareAnimator.SetBool("PutBrokenSprite", true);
        squareSpriteRenderer.enabled = false;

        string[] shapeName = this.gameObject.name.Split(new char[] { '_' }, System.StringSplitOptions.RemoveEmptyEntries);
        string nameOfFigure = shapeName[shapeName.Length - 1];

        if(StartsWith(nameOfFigure, "P"))
        {
            PermanentFunctions.instance.score += 175;
        }
        else
        {
            PermanentFunctions.instance.score += 125;
        }
        shapeIsInBrokenState = true;
    }
    //private void Update()
    //{
    //    Debug.Log(this.gameObject.name + " - Shape is in Broken State = " + shapeIsInBrokenState);
    //}

    public void SetTheBrokenStateToTrue()
    {
        shapeIsInBrokenState = true;
    }

    public bool StartsWith(string value, string currentChar)
    {
        return value.StartsWith(currentChar, true, null);
    }

    //private IEnumerator DisableAndResetExplosionParts()
    //{
    //    yield return new WaitForSecondsRealtime(1.5f);
    //    explosionParts.SetActive(false);

    //}
}
