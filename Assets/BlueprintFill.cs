using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintFill : MonoBehaviour
{
    [Tooltip("Probe points number is 4^n from this")][SerializeField] private int probePointsPower;
    [Tooltip("0 < x < 1")][SerializeField] private float needToFillPart;
    [SerializeField] private GameObject probePointPrefab;
    public List<ProbePoint> probePoints;
    private PolygonCollider2D _collider2D;


    private void Awake()
    {
        _collider2D = GetComponent<PolygonCollider2D>();
        GenerateProbePoints();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateProbePoints()
    {
        probePoints = new List<ProbePoint>();
        var colliderPoints = _collider2D.points;
        for (int i = 0; i < colliderPoints.Length; i++)
        {
            colliderPoints[i] = transform.TransformPoint(colliderPoints[i]);
        }
        float minX, maxX, minY, maxY;
        minX = maxX = colliderPoints[0].x;
        minY = maxY = colliderPoints[0].y;
        foreach (var point in colliderPoints)
        {
            if (point.x > maxX)
                maxX = point.x;
            if (point.x < minX)
                minX = point.x;
            if (point.y > maxY)
                maxY = point.y;
            if (point.y < minY)
                minY = point.y;
        }

        var deltaX = maxX - minX;
        var deltaY = maxY - minY;

        if (deltaX > deltaY)
            maxY += deltaX - deltaY;
        else
            maxX += deltaY - deltaX;
        
        deltaX = deltaY = Mathf.Max(deltaX, deltaY) / Mathf.Pow(2, probePointsPower);
        Vector2 probePoint = new Vector2(minX + deltaX / 2, minY + deltaY / 2);
        Debug.Log(minY + "\t" +  maxY + "\t" + probePoint);
        for (int i = 0; i < Mathf.Pow(2, probePointsPower); i++)
        {
            for (int j = 0; j < Mathf.Pow(2, probePointsPower); j++)
            {
                var tmpGO = Instantiate(probePointPrefab,
                    probePoint + Vector2.right * i * deltaX + Vector2.up * j * deltaY, Quaternion.identity, transform);
                var tmpPP = tmpGO.GetComponent<ProbePoint>();
                tmpPP.probePoints = probePoints;
                probePoints.Add(tmpPP);
                
            }
        }

    }
}
