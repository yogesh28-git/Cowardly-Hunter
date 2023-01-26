using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TigerMovement : MonoBehaviour
{
    [SerializeField] private PathController pathcontroller;
    [SerializeField] private SpawnerAndMover moverScript;
    
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
    [SerializeField] LayerMask layermask;
    private int rayHitLayers;
    private void Start()
    {
        tigerRigidBody = GetComponent<Rigidbody2D>();
        tigerAnimator = GetComponent<Animator>();
        highlight.a = 0.5f;
        rayHitLayers = layermask.value;
    }


    private void FixedUpdate()
    {
        if (!turnedAround)
        {
            if (pathChangeTimer >= randomWaitSeconds)
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            if(collision.gameObject.GetComponent<ArrowBehaviour>().GetArrowReleasedPath() == currentPath && !pathChanged)
                Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

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
            hit = Physics2D.Raycast(origin, Vector2.left, Mathf.Infinity, rayHitLayers);
            Debug.DrawRay(origin, Vector2.left, Color.black, 2f);
            if(hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.gameObject.layer == 6)
                {
                    Debug.Log("Ray Hit");
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
        moverScript.SetBackSpeed(0f);               //When tiger turns, background objects movement stops

        // Turns back to front after waiting 2 secs
        yield return new WaitForSeconds(2.5f);
        scale.x = 1 * Mathf.Abs(scale.x);
        transform.localScale = scale;
        turnedAround = false;
        moverScript.SetBackSpeed(4f);
    }
    
}
