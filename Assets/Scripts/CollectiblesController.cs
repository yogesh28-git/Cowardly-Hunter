using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesController : MonoBehaviour
{
    private bool collected = false;
    public SpawnerAndMover moverScript { get; set; }
    public UI_Controller scoreScript { get; set; }
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
        if((collision.gameObject.GetComponent<BackGroundScroller>() != null) && !collected)    //Collision with a tree (To avoid spawning a coin on top of tree, where it cannot be picked up from)
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.GetComponent<PlayerMovement>() != null)    //Collision with player
        {
            AudioManager.Instance.PlayEffects(Sounds.pickUp);
            scoreScript.ScoreIncrease(10);
            collected = true;
            Destroy(gameObject, 0.5f);
        }
    }
}
