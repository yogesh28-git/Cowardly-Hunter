using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed = 3f;
    [SerializeField] private float transitionSpeed = 1f;
    [SerializeField] private PathController pathcontroller;
    [SerializeField] private GameObject player;
    [SerializeField] private UI_Controller uiScript;
    private Vector3 playerPosition;
    private Vector3 playerScale;
    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    private SpriteRenderer playerRenderer;
    private bool transitioningPath = false;
    private bool playerscared = false;


    //Shooting Related Variables

    private bool isShootingDone = false;
    [Header("Shooting Variables")]
    private ShootingAndAiming shootScript;
    [SerializeField] private GameObject bowOnShoulder;
    [SerializeField] private GameObject hand;
 
    private Path moveTo = Path.path1;
    private void Start()
    {
        playerScale = transform.localScale;
        playerRigidBody = GetComponent<Rigidbody2D>();
        shootScript = GetComponent<ShootingAndAiming>();
        playerAnimator = player.GetComponent<Animator>();
        playerRenderer = player.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!transitioningPath)
        {
            Movement();
        }
        else
        {
            TransitionMotion();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            isShootingDone = false;
            bowOnShoulder.SetActive(false);
            hand.SetActive(true);

            playerAnimator.SetBool("aiming", true);
        }
        if (Input.GetKey(KeyCode.I) && !isShootingDone)
        {
            isShootingDone = shootScript.Aiming();
            if (isShootingDone)
            {
                hand.SetActive(false);
                bowOnShoulder.SetActive(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            hand.SetActive(false);
            bowOnShoulder.SetActive(true);

            playerAnimator.SetBool("aiming", false);
        }


        //player Sorting Layer
        playerRenderer.sortingLayerID = SortingLayer.layers[(int)moveTo].id;
        
    }
    private void FixedUpdate()
    {
        
    }
    private void Movement()
    {
        //left and right movement
        playerPosition = transform.position;
        if (Input.GetKey(KeyCode.A))
        {
            playerPosition.x += -1 * Time.deltaTime * runSpeed;
            playerScale.x = -(Mathf.Abs(playerScale.x));
        }
        else if (Input.GetKey(KeyCode.D) && !playerscared)
        {
            playerPosition.x += Time.deltaTime * runSpeed;
            playerScale.x = Mathf.Abs(playerScale.x);
        }
        transform.position = playerPosition;
        transform.localScale = playerScale;

        //Input Detection for up and down path transition
        if (Input.GetKeyDown(KeyCode.W) && moveTo != Path.path3)
        {
            transitioningPath = true;
            if (moveTo == Path.path1) { moveTo = Path.path2; }
            else if (moveTo == Path.path2) { moveTo = Path.path3; }
        }
        if (Input.GetKeyDown(KeyCode.S) && moveTo != Path.path1)
        {
            transitioningPath = true;
            if (moveTo == Path.path3) { moveTo = Path.path2; }
            else if (moveTo == Path.path2) { moveTo = Path.path1; }
        }
        
    }
    private void TransitionMotion()
    {
        Vector3 pos = transform.position;
        pos.y = pathcontroller.GetPathPosition(moveTo).y;
        playerRigidBody.MovePosition(pos);
        if (transform.position.y == pos.y)
        {
            transform.position = pos;
            transitioningPath = false;
        }
       
    }
    public Path GetPlayerPath()
    {
        return moveTo;
    }

    public void PlayerScared(bool isScared)
    {
        playerscared = isScared;
        if (isScared)
        {
            playerAnimator.SetBool("scared", true);
        }
        else
        {
            playerAnimator.SetBool("scared", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            uiScript.GameOver();
        }
    }

}
