using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Path
{
    path1,
    path2,
    path3
}
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed = 3f;
    [SerializeField] private float transitionSpeed = 1f; 
    private Vector3 playerPosition;
    private Vector3 playerScale;

    //Shooting Related Variables
    
    private bool isShootingDone = false;
    [Header("Shooting Variables")]
    [SerializeField] private ShootingAndAiming shootScript;
    [SerializeField] private GameObject bowOnShoulder;
    [SerializeField] private GameObject hand;
    [SerializeField] private Rigidbody2D playerRigidBody;
    private bool transitioningPath = false;

    
    private Path moveTo = Path.path1;
    private void Start()
    {
        playerScale = transform.localScale;
        playerRigidBody = GetComponent<Rigidbody2D>();
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
        else if (Input.GetKey(KeyCode.D))
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
        if (moveTo == Path.path1)
        {
            pos.y = 0;
        }
        else if(moveTo == Path.path2)
        {
            pos.y = 2;
        }
        else
        {
            pos.y = 4;
        }
        //transform.position = Vector3.MoveTowards(transform.position, pos, 0.02f);
        playerRigidBody.MovePosition(pos);
        Debug.Log(transform.position + "pos"+ pos);
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


}
