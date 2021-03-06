using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField] private float firstWindTimeout;
    [SerializeField] private float firstEarthTimeout;
    [SerializeField] private float timeBetweenEarth;
    [SerializeField] private float timeBetweenWindLighting;
    [SerializeField] private GameObject tree;
    private Animator _treeAnimator;
    private Controller _fan;
    private Controller _lighting;
    private Controller _earth;

    private void Awake()
    {
        _fan = GameObject.Find("Fans").GetComponent<Controller>();
        _earth = GameObject.Find("EarthController").GetComponent<Controller>();
        _treeAnimator = tree.GetComponent<Animator>();
        StartCoroutine(StartFirstWind());
        StartCoroutine(StartFirstEarth());
    }

    private IEnumerator StartFirstWind()
    {
        yield return firstWindTimeout;
        StartCoroutine(WindAndLight());
    }
    private IEnumerator StartFirstEarth()
    {
        yield return firstEarthTimeout;
        StartCoroutine(Earth());
    }
    
    private IEnumerator Earth()
    {
        _earth.ActiveCrisis();
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenEarth + _earth.Timeout);
            _earth.ActiveCrisis();
        }
    }
    
    private IEnumerator WindAndLight()
    {
        _fan.ActiveCrisis();
        StartCoroutine(TreeAnim());
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWindLighting + _fan.Timeout);
            _fan.ActiveCrisis();
            StartCoroutine(TreeAnim());
        }
    }

    private IEnumerator TreeAnim()
    {
        _treeAnimator.SetBool("WindActive", true);
        yield return new WaitForSeconds(_fan.Timeout);
        _treeAnimator.SetBool("WindActive", false);
    }
    
    
    
}
