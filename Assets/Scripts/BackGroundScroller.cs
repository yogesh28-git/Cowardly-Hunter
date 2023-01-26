using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
    private float backSpeed = 4f;

    void Update()
    {
        gameObject.transform.position += Vector3.left * Time.deltaTime * backSpeed;
    }
}
