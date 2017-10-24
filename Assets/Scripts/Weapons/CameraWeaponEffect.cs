using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWeaponEffect : MonoBehaviour
{
    Camera cam;
    float startFov;
    public float speed = 1;
    public AnimationCurve curve;

    bool isRunning = false;

    private void Start()
    {
        cam = GetComponent<Camera>();
        startFov = cam.fieldOfView;

    }
    public void CallEffect()
    {
        if (!isRunning || true)
        {
            StopAllCoroutines();
            StartCoroutine(Effect());
        }
    }

    private IEnumerator Effect()
    {
        isRunning = true;
        float t = 0;

        while (t <= 1)
        {
            cam.fieldOfView = startFov * curve.Evaluate(t * speed);
            t += 0.02f;
            yield return new WaitForSeconds(0.02f);
        }

        cam.fieldOfView = startFov;
        isRunning = false;
    }
}
