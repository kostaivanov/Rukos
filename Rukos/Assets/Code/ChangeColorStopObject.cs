using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorStopObject : MonoBehaviour
{
    internal SpriteRenderer intersectionSpriteRenderer;
    private Collider2D currentBall;
    private bool hitColliders;
    [SerializeField] private LayerMask ballLayer;
    private const float searchCircleRadius = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        intersectionSpriteRenderer = GetComponent<SpriteRenderer>();
        intersectionSpriteRenderer.color = new Color(1f, 1f, 1f, 0.6509f);
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.tag == "Ball")
        {
            currentBall = otherObject;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position, searchCircleRadius);
    }

    private void OnTriggerStay2D(Collider2D otherObject)
    {
        hitColliders = Physics2D.OverlapCircle(this.transform.position, searchCircleRadius, ballLayer);

        if (currentBall != null && currentBall.transform.hasChanged && hitColliders == true)
        {
            intersectionSpriteRenderer.color = new Color(0f, 0.9528f, 0.1272f, 0.6509f);
        }
    }

    private void OnTriggerExit2D(Collider2D otherObject)
    {
        intersectionSpriteRenderer.color = new Color(1f, 1f, 1f, 0.6509f);
        currentBall = null;
    }
}
