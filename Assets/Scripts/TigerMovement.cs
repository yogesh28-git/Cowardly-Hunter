using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerMovement : MonoBehaviour
{
    [SerializeField] private float jumpForceMin;
    [SerializeField] private float jumpForceMax;
    private float jumpForce;
    private bool isGrounded = false;
    private bool turnAround = false;
    private Vector3 tempPosition;
    [SerializeField] private float moveSpeed;
    private Rigidbody2D tigerRigidBody;
    private Animator tigerAnimator;
    private void Start()
    {
        tigerRigidBody = GetComponent<Rigidbody2D>();
        tigerAnimator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (isGrounded)
        {
            TigerJump();
        }
        else
        {
            moveSpeed = Random.Range(1, 6);
            tempPosition = transform.position;
            tempPosition.x += moveSpeed * 0.02f;
            transform.position = tempPosition;
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
            Destroy(gameObject);
        }
    }

    public void TurnAround()
    {
        turnAround = true;
        Vector3 scale = transform.localScale;
        scale.x = -1 * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}
