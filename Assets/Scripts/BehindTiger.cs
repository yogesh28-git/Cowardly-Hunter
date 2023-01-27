using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindTiger : MonoBehaviour
{
    [SerializeField] private TigerMovement tiger;
    [SerializeField] private PlayerMovement player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            tiger.TurnAround();
        }
        if (collision.gameObject.layer == 6)
        {
            player.PlayerScared(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            player.PlayerScared(false);
        }
    }
}
