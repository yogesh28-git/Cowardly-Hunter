using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHandler : MonoBehaviour
{
    private float timerCounter = 0;
    private int randomTime = 1;
    private int pathNumber;
    private int treeIndex;
    [SerializeField] private GameObject[] treeList = new GameObject[2];
    [SerializeField] private PathController pathcontroller;
    private void Start()
    {
        
    }
    private void Update()
    {
        if(timerCounter >= randomTime)
        {
            timerCounter = 0;
            randomTime = (int)Random.Range(2, 5);
            pathNumber = (int)Random.Range(0, 3);
            treeIndex = (int)Random.Range(0, 2);
            Vector3 pos = transform.position;
            pos.y = pathcontroller.GetPathPosition((Path)pathNumber).y;
            GameObject tree = Instantiate(treeList[treeIndex], pos, Quaternion.identity);
        }
        timerCounter += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            Destroy(collision.gameObject);
        }
    }
}
