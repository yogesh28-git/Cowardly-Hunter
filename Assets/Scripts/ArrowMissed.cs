using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMissed : MonoBehaviour
{
    [SerializeField] private TigerMovement tiger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            tiger.ArrowMissed();
        }
    }
}
