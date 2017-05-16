using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserPointer : MonoBehaviour
{

    [SerializeField]
    float distance=10;

    LineRenderer lineRend;

    // Use this for initialization
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLaser();
    }

    private void UpdateLaser()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward * distance);
        if (Physics.Raycast(ray, out hit, distance))
        {
            lineRend.SetPosition(0, transform.position);
            lineRend.SetPosition(1, hit.point);
        }
        else
        {
            lineRend.SetPosition(0, transform.position);
            lineRend.SetPosition(1, transform.position+transform.forward*distance);
        }
    }
}
