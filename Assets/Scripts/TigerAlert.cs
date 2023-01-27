using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerAlert : MonoBehaviour
{
    [SerializeField] private GameObject alertCircle;
    [SerializeField] private TigerMovement tiger;
    private Vector3 actualSize;
    private Vector3 tempScale;
    private float updateValue = -0.1f;
    private IEnumerator alert;

    void Start()
    {
        actualSize = alertCircle.transform.localScale;
        alertCircle.SetActive(false);
        alert = Alert();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            alertCircle.SetActive(true);
            StopCoroutine(alert);
            alert = Alert();
            updateValue = 0.1f;
            StartCoroutine(alert);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            StopCoroutine(alert);
            alert = Alert();
            updateValue = -0.1f;
            StartCoroutine(alert);
        }
    }

    IEnumerator Alert()
    {
        
        tempScale = alertCircle.transform.localScale;
        do
        {
            tempScale = alertCircle.transform.localScale;
            yield return new WaitForSeconds(0.3f);
            float factor = (tempScale.x + updateValue) / tempScale.x;
            tempScale.x += updateValue;
            tempScale.y *= factor;
            alertCircle.transform.localScale = tempScale;
            yield return new WaitForEndOfFrame();
        } while (tempScale.x < 1f && tempScale.x > actualSize.x);

        if (tempScale.x <= actualSize.x)
        {
            alertCircle.SetActive(false);
        }
        else if(tempScale.x >= 1f)
        {
            Debug.Log("Alerted");
            tiger.TurnAround();
            yield return new WaitForSeconds(3f);
        }
        yield return null;
    }

    public void RemoveAlerting()
    {
        StopCoroutine(alert);
        alertCircle.SetActive(false);
        gameObject.SetActive(false);
    }
}
