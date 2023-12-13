using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;

internal abstract class MenusComponents : ButtonComponents
{
    internal GameObject obj1, obj2, obj3, obj4;

    public virtual GameObject Obj1 { get; set; }
    public virtual GameObject Obj2 { get; set; }
    public virtual GameObject Obj3 { get; set; }
    public virtual GameObject Obj4 { get; set; }

    //internal List<Animator> animators;

    internal virtual void DeactivateObjects()
    {
        if (Obj1 != null)
        {
            foreach (Transform shape in Obj1.transform)
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
        }
       
        Obj2.SetActive(false);
        Obj3.SetActive(false);
        Obj4.SetActive(false);
    }

    internal virtual void ActivateObjects()
    {
        if (Obj1 != null)
        {
            foreach (Transform shape in Obj1.transform)
            {
                foreach (Transform shapeChild in shape.transform)
                {
                    if (shapeChild.gameObject.tag == "ExplosionSprite")
                    {
                        shapeChild.GetComponent<SpriteRenderer>().enabled = true;
                    }
                    if (shapeChild.gameObject.tag == "SquareLight")
                    {
                        shapeChild.gameObject.SetActive(true);
                    }

                   
                }
                // || shape.GetComponent<Animator>().GetBool("PutBrokenSprite") == true
                if (shape.GetComponent<Animator>().GetBool("PutBrokenSprite") == false)
                {
                    shape.GetComponent<SpriteRenderer>().enabled = true;                   
                }
                //Debug.Log(shape.gameObject.name + " - " + shape.GetComponent<Animator>().GetBool("PutBrokenSprite"));
            }
        }         

        Obj2.SetActive(true);
        Obj3.SetActive(true);
        Obj4.SetActive(true);
    }
  
}
