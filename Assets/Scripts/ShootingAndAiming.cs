using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAndAiming : MonoBehaviour
{
    
    private bool shootPressed = false;
    private int minTurn = -5;
    private int maxTurn = 70;
    private float angleIncrement = 30f;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject hand;
    [SerializeField] private Transform spawnPoint;
    private PlayerMovement playermovement;
    private GameObject arrowInstance;
    private Vector3 handRotation;

    private void Start()
    {
        playermovement = GetComponent<PlayerMovement>();
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
        handRotation = hand.transform.eulerAngles;
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

        hand.transform.eulerAngles = handRotation;

        if (shootPressed)
        {
            shootPressed = false;
            arrowInstance = Instantiate(arrowPrefab, spawnPoint.position, spawnPoint.rotation);
            
            arrowInstance.GetComponent<ArrowBehaviour>().SetArrowReleasedPath(playermovement.GetPlayerPath());
            hand.transform.eulerAngles = new Vector3(0, 0, -6f);
            return true;
        }
        else
        {
            return false;
        }
    }
}
