using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed = 3f;
    [SerializeField] private float jumpForce = 12f;
    private Vector3 playerPosition;
    private Vector3 playerScale;
    [SerializeField] private ShootingAndAiming shootScript;
    private Rigidbody2D playerRigidBody;
    private bool jumpPressed = false;
    private bool isGrounded = false;

    //Shooting Related Variables
    
    private bool isShootingDone = false;
    [Header("Shooting Variables")]
    [SerializeField] private GameObject bowOnShoulder;
    [SerializeField] private GameObject hand;

    private void Start()
    {
        playerScale = transform.localScale;
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerRigidBody.gravityScale = 2f;
    }

    private void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.I))
        {
            isShootingDone = false;
            bowOnShoulder.SetActive(false);
            hand.SetActive(true);
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
        }
        
    }
    private void FixedUpdate()
    {
        Jump();
    }
    private void Movement()
    {
        playerPosition = transform.position;
        if (Input.GetKey(KeyCode.A))
        {
            playerPosition.x += -1 * Time.deltaTime * runSpeed;
            playerScale.x = -(Mathf.Abs(playerScale.x));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerPosition.x += Time.deltaTime * runSpeed;
            playerScale.x = Mathf.Abs(playerScale.x);
        }
        
        transform.position = playerPosition;
        transform.localScale = playerScale;

        if (Input.GetKeyDown(KeyCode.W))
        {
            jumpPressed = true;
        }
    }

    private void Jump()
    {
        if (jumpPressed && isGrounded)
        {
            playerRigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpPressed = false;
            isGrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            isGrounded = true;
        }
    }
}
