using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public Vector2 speed = Vector3.one;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        transform.Rotate(new Vector3(Input.GetAxis("Vertical") * speed.x, 0, 0) * Time.deltaTime, Space.Self);
        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * speed.y, 0) * Time.deltaTime, Space.World);

        
    }
}
