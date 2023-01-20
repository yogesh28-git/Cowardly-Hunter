using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed = 3f;
    private Vector3 playerPosition;
    [SerializeField]private Vector3 playerScale;
    private void Start()
    {
        playerScale = transform.localScale;
    }

    private void Update()
    {
        Movement();
    }
    private void Movement()
    {
        Debug.Log("transform.localScale.x Before changing : " + transform.localScale.x);
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
        Debug.Log("playerSCale.x :"+playerScale.x);
        transform.position = playerPosition;
        transform.localScale = playerScale;
        Debug.Log("transform.localScale.x After changing : "+transform.localScale.x);
    }
}
