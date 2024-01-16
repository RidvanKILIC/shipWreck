using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonController : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField]float power=0f;
    [SerializeField] float smoothTime = 0f;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine(powerDown());
            StartCoroutine(powerUo());
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(powerUo());
            StartCoroutine(powerDown());
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        StopCoroutine(powerDown());
        StartCoroutine(powerUo());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(powerUo());
        StartCoroutine(powerDown());
    }
    public float getValue()
    {
        return power;
    }
    private IEnumerator powerUo()
    {
        float duration = 1f; // 3 seconds you can change this 
                             //to whatever you want
        while (power <= 1f)
        {
            power += Time.deltaTime / duration;
            yield return null;
        }
    }
    private IEnumerator powerDown()
    {
        float duration = 1f; // 3 seconds you can change this 
                             //to whatever you want
        while (power > 0f)
        {
            power -= Time.deltaTime / duration;
            yield return null;
        }
    }
}
