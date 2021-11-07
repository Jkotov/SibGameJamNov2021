using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private Camera _camera;
    private bool _followingMouse;
    private Vector3 _prevMousePos;
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    private Vector3 _defaultPos;
    private SpriteRenderer _renderer;
    private List<Collider2D> _contacts;

    private void Awake()
    {
        _camera = Camera.main;
        tag = "Brick";
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _defaultPos = transform.position;
        _renderer = GetComponent<SpriteRenderer>();
        _contacts = new List<Collider2D>();
    }

    void Update()
    {
        _collider2D.GetContacts(_contacts);
        if (!_followingMouse)
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        foreach (var contact in _contacts)
        {
            if (contact.CompareTag("Tape"))
            {
                _rigidbody2D.bodyType = RigidbodyType2D.Static;
                break;
            }
        }
        
        if (_followingMouse)
        {
            var deltaMouse = _camera.ScreenToWorldPoint(Input.mousePosition) - _prevMousePos;
            transform.position += deltaMouse;
            _prevMousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetKey(KeyCode.R))
            {
                transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            }
        }

        if (!_followingMouse && !_renderer.isVisible)
        {
            transform.position = _defaultPos;
        }
    }
    
    

    private void OnMouseDown()
    {
        _followingMouse = true;
        _prevMousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        _collider2D.isTrigger = true;
        
    }

    private void OnMouseUp()
    {
        _followingMouse = false;
        _collider2D.isTrigger = false;
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

}
