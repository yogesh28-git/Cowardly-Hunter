using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindTiger : MonoBehaviour
{
    [SerializeField] private TigerMovement tiger;
    [SerializeField] private PlayerMovement player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ArrowBehaviour>() != null)
        {
            tiger.TurnAround();
        }
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            player.PlayerScared(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            player.PlayerScared(false);
        }
    }
}
