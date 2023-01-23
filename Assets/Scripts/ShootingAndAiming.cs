using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAndAiming : MonoBehaviour
{
    
    private bool shootPressed = false;
    private int minTurn = -5;
    private int maxTurn = 70;
    [SerializeField] private float angleIncrement = 10f;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnPoint;
    private PlayerMovement playermovement;
    private GameObject arrowInstance;
    private Vector3 handRotation;

    private void Start()
    {
        playermovement = player.GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            shootPressed = true;
        }
    }
    public bool Aiming()
    {
        handRotation = transform.eulerAngles;
        if(handRotation.z > 270) { handRotation.z -= 360; }
        if(handRotation.z >= maxTurn)
        {
            handRotation.z = maxTurn;
            angleIncrement = -(Mathf.Abs(angleIncrement));
        }
        else if(handRotation.z <= minTurn)
        {
            handRotation.z = minTurn;
            angleIncrement = Mathf.Abs(angleIncrement);
        }
        
        handRotation.z += Time.deltaTime * angleIncrement;
      
        transform.eulerAngles = handRotation;

        if (shootPressed)
        {
            shootPressed = false;
            arrowInstance = Instantiate(arrowPrefab, spawnPoint.position, spawnPoint.rotation);
            
            arrowInstance.GetComponent<ArrowBehaviour>().SetArrowReleasedPath(playermovement.GetPlayerPath());
            transform.eulerAngles = new Vector3(0, 0, -6f);
            return true;
        }
        else
        {
            return false;
        }
    }
}
