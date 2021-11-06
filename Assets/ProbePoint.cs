using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbePoint : MonoBehaviour
{
    private ContactFilter2D contactFilter2D;
    public bool isOnBrick;
    public List<ProbePoint> probePoints;
    private Collider2D _collider2D;
    private SpriteRenderer _renderer;
    private List<Collider2D> _contacts;
    private bool _isOnBlueprint;
    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _contacts = new List<Collider2D>();
    }

    void Start()
    {
        StartCoroutine(RemoveIfIsNotOnBlueprint());
    }

    // Update is called once per frame
    void Update()
    {
        _collider2D.OverlapCollider(contactFilter2D.NoFilter(), _contacts);
        
        isOnBrick = false;
        foreach (var contact in _contacts)
        {
            if (contact.CompareTag("Brick"))
            {
                isOnBrick = true;
                break;
            }
        }

        if (isOnBrick)
            _renderer.color = Color.red;
        else
            _renderer.color = Color.green;
        
    }
    
    

    IEnumerator RemoveIfIsNotOnBlueprint()
    {
        yield return new WaitForFixedUpdate();
        _collider2D.OverlapCollider(contactFilter2D.NoFilter(), _contacts);
        
        foreach (var contact in _contacts)
        {
            if (contact.CompareTag("Window"))
            {
                probePoints.Remove(this);
                Destroy(gameObject);
                yield break;
            }
        }
        foreach (var contact in _contacts)
        {
            if (contact.CompareTag("HouseBlueprint"))
            {
                yield break;
            }
        }
        probePoints.Remove(this);
        Destroy(gameObject);

    }
}
