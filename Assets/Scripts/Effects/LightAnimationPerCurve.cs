using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnimationPerCurve : MonoBehaviour {

    [SerializeField]
    AnimationCurve curve;
    [SerializeField]
    float duration = 1;

    [SerializeField]
    float factor = 1;
    [SerializeField]
    float offset = 0;

    float startTime;
    Light light;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        light = GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        float t = (Time.time - startTime)/duration;
        light.intensity = curve.Evaluate(t)*factor+offset;
	}
}
