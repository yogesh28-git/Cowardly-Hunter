using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TigerMovement : MonoBehaviour
{
    [SerializeField] private float jumpForceMin;
    [SerializeField] private float jumpForceMax;
    [SerializeField] private GameObject path1;
    [SerializeField] private GameObject path2;
    [SerializeField] private GameObject path3;
    private TilemapCollider2D path1Collider;
    private TilemapCollider2D path2Collider;
    private TilemapCollider2D path3Collider;
    private Tilemap path1TileMap;
    private Tilemap path2TileMap;
    private Tilemap path3TileMap;
    private float jumpForce;
    private bool isGrounded = false;
    private bool turnAround = false;
    private bool turnedAround = false;
    private Vector3 tempPosition;
    [SerializeField] private float moveSpeed;
    private Rigidbody2D tigerRigidBody;
    private Animator tigerAnimator;
    private Path path = Path.path1;
    private Path moveTo = Path.path1;
    private bool pathChanging = false;
    private Color highlight = Color.white;

    //Raycast variables
    private RaycastHit2D hit;
    private void Start()
    {
        path1Collider = path1.GetComponent<TilemapCollider2D>();
        path2Collider = path2.GetComponent<TilemapCollider2D>();
        path3Collider = path3.GetComponent<TilemapCollider2D>();
        path1TileMap = path1.GetComponent<Tilemap>();
        path2TileMap = path2.GetComponent<Tilemap>();
        path3TileMap = path3.GetComponent<Tilemap>();
        tigerRigidBody = GetComponent<Rigidbody2D>();
        tigerAnimator = GetComponent<Animator>();
        highlight.a = 0.5f;
    }

    
    private void FixedUpdate()
    {
        if (!turnAround)
        {
            if (!pathChanging)
            {
                if (isGrounded)
                {
                    pathChanging = true;
                    tigerRigidBody.gravityScale = 0;
                    PathReset();
                    moveTo = (Path)(int)Random.Range(0, 3);
                    //Debug.Log("Random: " + moveTo + ", Path :" + path);
                }
                else
                {
                    moveSpeed = Random.Range(1, 6);
                    tempPosition = transform.position;
                    tempPosition.x += moveSpeed * 0.02f;
                    transform.position = tempPosition;
                }
            }
            else
            {
                PathChanger();
            }
        }
        else
        {
            if (!turnedAround)
            {
                
                TurnAround();
            }
            else
            {
                LookForHunter();
            }
        }   
    }
    private void PathUpdate()
    {
        //Trigger are changed to false and activating that collider
        //Also, setting a highlight effect on the path the tiger is On.
        switch (path)
        {
            case Path.path1:
                
                path1Collider.isTrigger = false;
                path1TileMap.color = highlight;
                break;
            case Path.path2:
                path2Collider.isTrigger = false;
                path2TileMap.color = highlight;
                break;
            case Path.path3:
                path3Collider.isTrigger = false;
                path3TileMap.color = highlight;
                break;
        }
    }
    private void PathReset()
    {
        //collider Reset
        path1Collider.isTrigger = true;
        path2Collider.isTrigger = true;
        path3Collider.isTrigger = true;

        //Color Reset
        path1TileMap.color = Color.grey;
        path2TileMap.color = Color.grey;
        path3TileMap.color = Color.grey;
    }
    private void PathChanger()
    {
        Vector3 pos = transform.position;
        

        if (moveTo == Path.path1)
        {
            pos.y = 0;
        }
        else if (moveTo == Path.path2)
        {
            pos.y = 2;
        }
        else 
        {
            pos.y = 4;
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
        //Debug.Log("new pos: " + transform.position + "target :" + pos);
        if (transform.position.y == pos.y)
        {
            pathChanging = false;
            path = moveTo;
            tigerRigidBody.gravityScale = 2;
            PathUpdate();
            TigerJump();
            Debug.Log("path :" + path);
        }
    }
    private void TigerJump()
    {
        if (!turnAround)
        {
            jumpForce = Random.Range(jumpForceMin, jumpForceMax);
            tigerAnimator.SetBool("isGrounded", false);
            tigerRigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            tigerAnimator.SetBool("isGrounded", true);
            isGrounded = true;
        }
        if(collision.gameObject.layer == 8)
        {
            if(collision.gameObject.GetComponent<ArrowBehaviour>().GetArrowReleasedPath() == path)
                Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    public void ArrowMissed()
    {
        turnAround = true;
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
        if (isGrounded)
        {
            StartCoroutine(turning());
            Debug.Log("got in");
            Vector3 scale = transform.localScale;
            scale.x = -1 * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }   
    }

    IEnumerator turning()
    {
        yield return new WaitForSeconds(1);
        turnedAround = true;
        yield return new WaitForSeconds(2);
        Vector3 scale = transform.localScale;
        scale.x = 1 * Mathf.Abs(scale.x);
        transform.localScale = scale;
        turnedAround = false;
        turnAround = false;
    }
    
}
