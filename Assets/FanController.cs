using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FanController : Controller
{
    private ParticleSystem[] _fans;

    private void Awake()
    {
        _fans = GetComponentsInChildren<ParticleSystem>();
    }


    private protected override IEnumerator Crisis()
    {
        Debug.Log("here");
        var index = Random.Range(0, _fans.Length);
        _fans[index].Play();
        yield return new WaitForSeconds(timeout);
        _fans[index].Stop();
    }
}
