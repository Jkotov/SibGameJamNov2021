using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField] private float firstWindTimeout;
    [SerializeField] private float timeBetweenWindLighting;
    private Controller _fan;
    private Controller _lighting;
    private Controller _earth;

    private void Awake()
    {
        _fan = GameObject.Find("Fans").GetComponent<Controller>();
        StartCoroutine(StartFirstWind());
    }

    private IEnumerator StartFirstWind()
    {
        yield return firstWindTimeout;
        StartCoroutine(WindAndLight());
    }
    private IEnumerator WindAndLight()
    {
        _fan.ActiveCrisis();
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWindLighting + _fan.Timeout);
            _fan.ActiveCrisis();
        }
    }
    
    
    
}
