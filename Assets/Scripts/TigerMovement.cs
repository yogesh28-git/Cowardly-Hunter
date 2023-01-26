using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TigerMovement : MonoBehaviour
{
    [SerializeField] private PathController pathcontroller;
    private bool turnAround = false;
    private bool turnedAround = false;
    private bool cameraFollowEnabled = false;
    [SerializeField] private float moveSpeed;
    private Rigidbody2D tigerRigidBody;
    private Animator tigerAnimator;
    private float pathChangeTimer = 0;
    private int randomWaitSeconds = 3;
    private Path currentPath = Path.path1;
    private Path moveTo = Path.path1;
    private bool pathChanged = true;
    private Color highlight = Color.white;
    private Vector3 velocity;
    //Raycast variables
    private RaycastHit2D hit;
    private void Start()
    {
        tigerRigidBody = GetComponent<Rigidbody2D>();
        tigerAnimator = GetComponent<Animator>();
        highlight.a = 0.5f;
    }

    
    private void FixedUpdate()
    {
        if (!turnedAround)
        {
            if(pathChangeTimer >= randomWaitSeconds)
            {
                //Resetting loop control variables
                pathChangeTimer = 0;
                randomWaitSeconds = (int)Random.Range(1, 4);

                //setting next path and moving
                do
                {
                    moveTo = (Path)(int)Random.Range(0, 3);
                } while (currentPath == moveTo);
                pathChanged = false;
                
            }
            else
            {
                pathChangeTimer += 0.02f;
                if (!pathChanged)
                {
                    pathChanged = pathcontroller.PathChanger(transform, moveTo);
                }
                else
                {
                    currentPath = moveTo;
                }
            }
        }
        else
        {
            LookForHunter();
        } 

        /*if (cameraFollowEnabled && !turnedAround)
        {
            CameraFollow();
        }
        */
    }
   /* private void TigerMove()
    {
        if (!turnAround)
        {
            tigerRigidBody.velocity = velocity;
        } 
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            if(collision.gameObject.GetComponent<ArrowBehaviour>().GetArrowReleasedPath() == currentPath && !pathChanged)
                Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        //camera follow starts when tiger reaches this collider.
        if (collision.gameObject.layer == 10)
        {
            cameraFollowEnabled = true;
        }
    }*/

    /*private void CameraFollow()
    {
        Vector3 camPos = Camera.main.transform.position;
        camPos.x += moveSpeed * Time.fixedDeltaTime;
        Camera.main.transform.position = camPos;
    }*/

    public void ArrowMissed()
    {
        TurnAround();
        Debug.Log("Arrow Missed");
    }

    private void LookForHunter()
    {
        for (int i = 0; i <= 6; i++)
        {
            Vector2 origin = new Vector2(transform.position.x - 2f, -2.5f + i);
            hit = Physics2D.Raycast(origin, Vector2.left);
            Debug.DrawRay(origin, Vector2.left, Color.black, 2f);
            if(hit.collider != null)
            {
                if (hit.collider.gameObject.layer == 6)
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
    public void TurnAround()
    {
        StartCoroutine(turning());
        


    }

    IEnumerator turning()
    {
        yield return new  WaitForSeconds(1.5f);
        //Debug.Log("got in");
        Vector3 scale = transform.localScale;
        scale.x = -1 * Mathf.Abs(scale.x);
        transform.localScale = scale;
        turnedAround = true;

        // Turns back to front after waiting 2 secs
        yield return new WaitForSeconds(2.5f);
        scale.x = 1 * Mathf.Abs(scale.x);
        transform.localScale = scale;
        turnedAround = false;
    }
    
}
