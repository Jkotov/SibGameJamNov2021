using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteSound : MonoBehaviour
{
    private AudioListener _listener;
    // Start is called before the first frame update
    void Start()
    {
        _listener = GetComponent<AudioListener>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            _listener.enabled = !_listener.enabled;

    }
}
