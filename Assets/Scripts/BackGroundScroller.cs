using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
    public SpawnerAndMover moverScript { get; set; }

    void Update()
    {
        moverScript.ObjectMover(gameObject);
    }
}
