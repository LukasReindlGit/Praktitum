using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusScale : MonoBehaviour {

    [SerializeField]
    float factor = 1;

    [SerializeField]
    float offset = 1;
    public float Offset
    {
        get
        {
            return offset;
        }
        set
        {
            offset = value;
        }
    }

    [SerializeField]
    float speed = 1;

    [SerializeField]
    float timeOffset = 0;

    [SerializeField]
    Projector projector;
	
	// Update is called once per frame
	void Update () {

        float size = (offset + factor * Mathf.Sin((timeOffset+Time.timeSinceLevelLoad )* speed));
        transform.localScale = Vector3.one *size;
        projector.orthographicSize = size;
	}
}
