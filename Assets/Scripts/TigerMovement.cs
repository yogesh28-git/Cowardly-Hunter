using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TigerMovement : MonoBehaviour
{
    [SerializeField] private PathController pathcontroller;
    [SerializeField] private SpawnerAndMover moverScript;
    [SerializeField] private Transform player;
    [SerializeField] private TigerAlert tigerAlert;

    private bool turnedAround = false;
    private IEnumerator turnCoroutine;
    private Animator tigerAnimator;
    private float pathChangeTimer = 0;
    private int randomWaitSeconds = 3;
    private Path currentPath = Path.path1;
    private Path moveTo = Path.path1;
    private bool pathChanged = true;
    private Color highlight = Color.white;

    //Raycast variables
    private RaycastHit2D hit;
    [SerializeField] LayerMask layermask;
    private int rayHitLayers;
    private bool rayHit = false;

    private void Start()
    {
        tigerAnimator = GetComponent<Animator>();
        highlight.a = 0.5f;
        rayHitLayers = layermask.value;
        turnCoroutine = turning();

        tigerAnimator.SetBool("walking", true);
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
            if(collision.gameObject.GetComponent<ArrowBehaviour>().GetArrowReleasedPath() == currentPath && !pathChanged && !turnedAround)
                Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    public void TurnAround()
    {
        turnCoroutine = turning();
        StartCoroutine(turnCoroutine);
        Debug.Log("Arrow Missed");
    }

    private void LookForHunter()
    {
        for (int i = 0; i <= 6; i++)
        {
            Vector2 origin = new Vector2(transform.position.x - 2f, -2.5f + i);
            hit = Physics2D.Raycast(origin, Vector2.left, Mathf.Infinity, rayHitLayers);
            Debug.DrawRay(origin, Vector2.left, Color.black, 2f);
            if(hit.collider != null && rayHit == false)
            {
                if ((hit.collider.gameObject.layer == player.gameObject.layer) && (this.currentPath == pathcontroller.GetPath(player)))
                {
                    Debug.Log("Ray Hit");
                    rayHit = true;
                    StopCoroutine(turnCoroutine);
                    turnCoroutine = turning();
                    StartCoroutine(KillThePlayer());
                }
            }
        }
    }

    IEnumerator turning()
    {
        yield return new  WaitForSeconds(1.5f);
        //Debug.Log("got in");
        Vector3 scale = transform.localScale;
        scale.x = -1 * Mathf.Abs(scale.x);
        transform.localScale = scale;
        turnedAround = true;
        moverScript.SetBackSpeed(0);
        moverScript.enabled = false;              // When tiger turns, background objects movement stops
        tigerAnimator.SetBool("walking", false);   // Animation changes to idle


        for (int i=0; i<3; i++)
        {
            bool pathMoved = false;
            while (!pathMoved)
            {
                tigerAnimator.SetBool("walking", true);
                if (currentPath == Path.path1)
                {
                    pathMoved = pathcontroller.PathChanger(transform, Path.path2);
                    currentPath = pathMoved ? Path.path2 : Path.path1;
                }
                else if (currentPath == Path.path2)
                {
                    pathMoved = pathcontroller.PathChanger(transform, Path.path3);
                    currentPath = pathMoved ? Path.path3 : Path.path2;
                }
                else if (currentPath == Path.path3)
                {
                    pathMoved = pathcontroller.PathChanger(transform, Path.path1);
                    currentPath = pathMoved ? Path.path1 : Path.path3;
                }
                yield return new WaitForSeconds(0.02f);
            }
            tigerAnimator.SetBool("walking", false);
            yield return new WaitForSeconds(0.8f);
        }
        

        scale.x = 1 * Mathf.Abs(scale.x);
        transform.localScale = scale;
        turnedAround = false;
        moverScript.enabled = true;
        moverScript.SetBackSpeed(4f);
        tigerAnimator.SetBool("walking", true);
    }
    
    IEnumerator KillThePlayer()
    {
        player.gameObject.GetComponent<PlayerMovement>().PlayerScared(true);
        tigerAnimator.SetBool("walking", true);
        tigerAlert.RemoveAlerting();                                            //deactivate alert circle because player is already being killed.
        player.gameObject.GetComponent<PlayerMovement>().enabled = false;       //deactivate playermovement script
        player.gameObject.GetComponent<ShootingAndAiming>().enabled = false;
        while (transform.position.x > (player.position.x + 3))
        {
            transform.position += Vector3.left * 0.2f;
            pathcontroller.PathChanger(transform, pathcontroller.GetPath(player));  //move to player's path
            yield return new WaitForSeconds(0.02f);
        }
        tigerAnimator.SetTrigger("Attack");
    }
}
