using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerAlert : MonoBehaviour
{
    [SerializeField] private GameObject alertCircle;
    private Vector3 actualSize;
    private Vector3 tempScale;
    private bool entered = false;
    void Start()
    {
        actualSize = alertCircle.transform.localScale;
        alertCircle.SetActive(false);
    }
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            entered = true;
            alertCircle.SetActive(true);
            StartCoroutine(Alert());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            entered = false;
            StopCoroutine(Alert());
            alertCircle.transform.localScale = actualSize;
            alertCircle.SetActive(false);
        }
    }

    IEnumerator Alert()
    {
        int i = 0;
        do
        {
            yield return new WaitForSeconds(0.3f);
            tempScale = alertCircle.transform.localScale;
            float factor = (tempScale.x + 0.1f) / tempScale.x;
            tempScale.x += 0.1f;
            tempScale.y *= factor;
            tempScale.z += factor;
            alertCircle.transform.localScale = tempScale;
            i++;
        } while (i<10);
        if(i == 10)
        {
            //kill player
        }
    }
}
