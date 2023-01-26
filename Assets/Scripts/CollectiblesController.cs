using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesController : MonoBehaviour
{
    private bool collected = false;
    public SpawnerAndMover moverScript { get; set; }

    void Update()
    {
        if (collected)
        {
            gameObject.transform.position += Vector3.up * Time.deltaTime * 8f;
        }
        else
        {
            moverScript.ObjectMover(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7 && !collected)    //Collision with a tree
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.layer == 6)    //Collision with player
        {
            collected = true;
            Destroy(gameObject, 0.5f);
        }
    }
}
