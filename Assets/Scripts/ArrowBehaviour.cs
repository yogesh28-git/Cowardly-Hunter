using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    [SerializeField] private float arrowSpeed;
    private Rigidbody2D arrowRigidBody;
    private Path arrowReleasedPath;
    private void Start()
    {
        arrowRigidBody = GetComponent<Rigidbody2D>();
        ArrowVelocity();
        Destroy(gameObject, 5);
    }


    private void FixedUpdate()
    {
        transform.up = arrowRigidBody.velocity;
    }
    private void ArrowVelocity()
    {
        arrowRigidBody.velocity = transform.right * arrowSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            Destroy(gameObject);
        }
        
    }
    public void SetArrowReleasedPath(Path arrowpath)
    {
        arrowReleasedPath = arrowpath;
    }
    public Path GetArrowReleasedPath()
    {
        return arrowReleasedPath;
    }
}
