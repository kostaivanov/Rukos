using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveBallController : MonoBehaviour
{
    private const float distanceBallPipeCenter = 0.65f;
    private const float searchCircleRadius = 0.325f;

    [SerializeField] internal float moveSpeed = 1f;
    private Rigidbody2D ballRigidBody;
    internal Vector3 direction;
    internal string directionName;
    [SerializeField] private Transform groundCheck;
    private Collider2D startTubeCollider;
    private SpriteRenderer tubeSprite;
    [SerializeField] private LayerMask groundLayer, ballLayer;

    public bool canMove;
    public bool isInside;
    private float speed;
    internal bool pipesIsAvailable;
    //[SerializeField] private Button releaseLeft, releaseRight, releaseUp, releaseDown;
    internal List<Collider2D> foundColliders;
    internal bool collidersAreFound;
    internal bool collidersAdded = false;
    internal GameObject currentVacuumHole;
    public bool youCanRelease_Left = false;
    public bool youCanRelease_Right = false;
    public bool youCanRelease_Up = false;
    public bool youCanRelease_Down = false;
    internal Collider2D currentGroundCollider;
    internal Collider2D searchingCollider;
    public bool readyToRotate = false;
    public float distanceRay = 2.8f;
    public bool thereIsBallInThePipe = false;

    private bool keepInHole = false;
    PushBallFunctions pushBackObject;

    private AudioSource ballAS;
    [SerializeField] private AudioClip clashSound;

    private void Start()
    {
        ballRigidBody = GetComponent<Rigidbody2D>();
        canMove = true;
        foundColliders = new List<Collider2D>();
        collidersAreFound = false;
        pushBackObject = new PushBallFunctions();
        //currentGroundCollider = Physics2D.OverlapCircle(groundCheck.position, 0.95f, groundLayer);
        ballAS = GetComponent<AudioSource>();
    }

    private bool CheckIfThereIsBallInThePipe(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction * distanceRay, distanceRay, ballLayer);

        if (hit.collider != null)
        {
            // Calculate the distance from the surface and the "error" relative
            // to the floating height.
            float distance = Mathf.Abs(hit.point.x - transform.position.x);
            //Debug.Log(hit.collider.transform.parent.name + " " + distance);
            thereIsBallInThePipe = true;

        }
        else
        {
            thereIsBallInThePipe = false;
        }
        return thereIsBallInThePipe;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, searchCircleRadius);

    }
    public void PlayClashSound()
    {
        ballAS.PlayOneShot(clashSound);
    }

    private void FixedUpdate()
    {
        searchingCollider = Physics2D.OverlapCircle(groundCheck.position, searchCircleRadius, groundLayer);

        if (currentGroundCollider != searchingCollider)
        {
            currentGroundCollider = searchingCollider;
        }
        if (searchingCollider == null)
        {
            currentGroundCollider = null;
            youCanRelease_Left = false;
            youCanRelease_Right = false;
            youCanRelease_Up = false;
            youCanRelease_Down = false;
        }

        if (currentGroundCollider != null)
        {
            if (collidersAdded == false && collidersAreFound == false)
            {
                foundColliders.Add(currentGroundCollider);
                collidersAreFound = true;
                collidersAdded = true;
            }
        }



        //Debug.DrawRay(transform.position, Vector2.right.normalized * distanceRay, Color.blue, 5f);

        //if (hit.collider != null)
        //{
        //    // Calculate the distance from the surface and the "error" relative
        //    // to the floating height.
        //    float distance = Mathf.Abs(hit.point.x - transform.position.x);

        //    Debug.Log(hit.collider.transform.parent.name + " " + distance);
        //}
        if (currentGroundCollider != null && foundColliders.Count > 0 && collidersAdded == true)
        {
            if (currentGroundCollider.transform.position.x > groundCheck.position.x && currentGroundCollider.transform.position.x - groundCheck.position.x > distanceBallPipeCenter)
            {
                if (CheckIfThereIsBallInThePipe(Vector2.right) == false)
                {
                    youCanRelease_Right = true;
                }
                else
                {
                    youCanRelease_Right = false;
                }
            }
            else
            {
                youCanRelease_Right = false;
            }

            if (currentGroundCollider.transform.position.x < groundCheck.position.x && groundCheck.position.x - currentGroundCollider.transform.position.x > distanceBallPipeCenter)
            {
                if (CheckIfThereIsBallInThePipe(Vector2.left) == false)
                {
                    youCanRelease_Left = true;
                }
                else
                {
                    youCanRelease_Left = false;
                }
            }
            else
            {
                youCanRelease_Left = false;
            }

            if (currentGroundCollider.transform.position.y > groundCheck.position.y && currentGroundCollider.transform.position.y - groundCheck.position.y > distanceBallPipeCenter)
            {
                if (CheckIfThereIsBallInThePipe(Vector2.up) == false)
                {
                    youCanRelease_Up = true;
                }
                else
                {
                    youCanRelease_Up = false;
                }
            }
            else
            {
                youCanRelease_Up = false;
            }

            if (currentGroundCollider.transform.position.y < groundCheck.position.y && groundCheck.position.y - currentGroundCollider.transform.position.y > distanceBallPipeCenter)
            {
                if (CheckIfThereIsBallInThePipe(Vector2.down) == false)
                {
                    youCanRelease_Down = true;
                }
                else
                {
                    youCanRelease_Down = false;
                }
            }
            else
            {
                youCanRelease_Down = false;
            }
        }

        if (canMove == true && tubeSprite != null)
        {
            //Debug.Log(tubeSprite.gameObject.name + "  " + tubeSprite.bounds.extents.x + "    " + tubeSprite.bounds.extents.y + "  " + directionName);
            readyToRotate = false;
            if (tubeSprite.bounds.extents.x > tubeSprite.bounds.extents.y)
            {
                if (directionName == "Left")
                {
                    MoveBall(-1, 0);
                    //StartCoroutine(WaitBeforeMakeTurn(-1, 0));
                }
                else
                {

                    MoveBall(1, 0);
                    //StartCoroutine(WaitBeforeMakeTurn(1, 0));
                }
            }
            if (tubeSprite.bounds.extents.x < tubeSprite.bounds.extents.y)
            {
                if (directionName == "Down")
                {
                    MoveBall(0, -1);

                    //StartCoroutine(WaitBeforeMakeTurn(0, -1));
                }
                else
                {
                    MoveBall(0, 1);
                    //StartCoroutine(WaitBeforeMakeTurn(0, 1));
                }
            }
        }

        if (keepInHole == true)
        {
            speed = ballRigidBody.velocity.magnitude;

            Physics2D.IgnoreCollision(this.groundCheck.GetComponent<CircleCollider2D>(), tubeSprite.gameObject.GetComponent<EdgeCollider2D>());
            transform.position = currentVacuumHole.transform.position;
            transform.rotation = currentVacuumHole.transform.rotation;
            ballRigidBody.velocity = Vector3.zero;
            readyToRotate = true;
        }
    }

    private void LateUpdate()
    {
        if (currentVacuumHole != null && isInside == true && canMove == false)
        {
            speed = ballRigidBody.velocity.magnitude;

            if (tubeSprite != null)
            {
                Physics2D.IgnoreCollision(this.groundCheck.GetComponent<CircleCollider2D>(), tubeSprite.gameObject.transform.parent.GetComponent<EdgeCollider2D>());
            }

            transform.position = currentVacuumHole.transform.position;
            transform.rotation = currentVacuumHole.transform.rotation;
            ballRigidBody.velocity = Vector3.zero;
            readyToRotate = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.layer == LayerMask.NameToLayer("GroundLayer"))
        {
            if (otherObject.gameObject.transform.childCount > 0)
            {
                tubeSprite = otherObject.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
            }
            else
            {
                tubeSprite = otherObject.gameObject.GetComponent<SpriteRenderer>();
            }
        }

        //if (otherObject.tag != "StopObject")
        //{
        //    tubeSprite = otherObject.gameObject.GetComponent<SpriteRenderer>();
        //}

        if (otherObject.tag == "Magnet")
        {
            //canMove = false;
            currentVacuumHole = otherObject.gameObject;
        }

        if (otherObject.tag == "StopObject")
        {
            //startTubeCollider = otherObject;
            //StartCoroutine(WaitUntilStart(startTubeCollider));
        }

        if (otherObject.tag == "HardColliders" && canMove == true && isInside == false)
        {
            pushBackObject.CheckBallDirectionAndPushOposite(this.gameObject, otherObject.gameObject);
            PlayClashSound();
        }

        if (otherObject.gameObject.layer == LayerMask.NameToLayer("GroundLayer") && tubeSprite.bounds.extents.x > tubeSprite.bounds.extents.y)
        {
            if (this.transform.position.x > otherObject.gameObject.transform.position.x)
            {
                directionName = "Left";
            }
            else if(this.transform.position.x < otherObject.gameObject.transform.position.x)
            {
                directionName = "Right";                   
            }
        }
        else if(tubeSprite != null && tubeSprite.bounds.extents.y > tubeSprite.bounds.extents.x)
        {
            if (otherObject.gameObject.layer == LayerMask.NameToLayer("GroundLayer") && this.transform.position.y > otherObject.gameObject.transform.position.y)
            {
                directionName = "Down";
            }
            else if(otherObject.gameObject.layer == LayerMask.NameToLayer("GroundLayer") && this.transform.position.y < otherObject.gameObject.transform.position.y)
            {
                directionName = "Up";                    
            }
        }   
    }

    //private void Update()
    //{
    //    Debug.Log(this.gameObject.name + " - " + directionName + " - " + direction);
    //}

    internal void MoveBall(int directionValue_X, int directionValue_Y)
    {

        direction = new Vector3(directionValue_X, directionValue_Y, 0);

        //transform.Translate(direction * Time.deltaTime * moveSpeed);
        //ballRigidBody.AddForce(direction * moveSpeed);

        ballRigidBody.velocity = direction * moveSpeed;

    }
}
