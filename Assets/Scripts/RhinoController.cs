using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoController : MonoBehaviour
{
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * 15f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            Destroy(collision.gameObject);
        }
    }
}
