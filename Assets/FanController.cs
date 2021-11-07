using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FanController : Controller
{
    private ParticleSystem[] _fans;
    private SpriteRenderer[] _warnings;
    private int _index;
    private void Awake()
    {
        _fans = GetComponentsInChildren<ParticleSystem>();
        _warnings = new SpriteRenderer[_fans.Length];
        for (int i = 0; i < _fans.Length; i++)
        {
            _warnings[i] = _fans[i].gameObject.GetComponentInChildren<SpriteRenderer>();
            _warnings[i].enabled = false;
        }
    }

    private protected override IEnumerator ShowWarning()
    {
        _index = Random.Range(0, _fans.Length);
        _warnings[_index].enabled = true;
        yield return base.ShowWarning();
        _warnings[_index].enabled = false;
    }

    private protected override IEnumerator Crisis()
    {
        Debug.Log("here");
        _fans[_index].Play();
        yield return new WaitForSeconds(timeout);
        _fans[_index].Stop();
    }
}
