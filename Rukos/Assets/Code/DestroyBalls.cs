using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBalls : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject != null)
        {
            //Debug.Log("something happened ?");
            if (otherObject.tag == "Ball")
            {
                Destroy(otherObject.gameObject, 0.5f);
            }
        }       
    }
}
