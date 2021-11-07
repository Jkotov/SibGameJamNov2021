using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeController : MonoBehaviour
{
    [SerializeField] private float maxXScale;
    private bool _activeSpread;
    private bool _disabled;
    private Collider2D _collider2D;
    private SpriteRenderer _renderer;
    private Vector3 _defaultScale;
    private Camera _camera;
    
    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _defaultScale = transform.localScale;
        _defaultScale.x = 0;
        _camera = Camera.main;
        Disable();
    }

    private void Disable()
    {
        transform.localScale = _defaultScale;
        _disabled = true;
        _activeSpread = false;
        _renderer.enabled = false;
        _collider2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            var pos = _camera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = -2;
            if (!_activeSpread && _disabled)
            {
                Debug.Log("tape enabled");;
                _collider2D.enabled = false;
                transform.position = pos;
                _activeSpread = true;
                _renderer.enabled = true;
            }

            if (_activeSpread)
            {
                float scale = (pos - transform.position).magnitude;
                scale = scale > maxXScale ? maxXScale : scale;
                transform.localScale = _defaultScale + Vector3.right * scale;
                transform.rotation = Quaternion.FromToRotation(Vector3.right, pos - transform.position);
            }
            
            if (!_activeSpread && !_disabled)
                Disable();
        }
        else if (_activeSpread)
        {
            _activeSpread = false;
            _collider2D.enabled = true;
        }
    }
}
