using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureFlowEffect : MonoBehaviour
{

    public Vector2 speed = Vector2.one;

    public bool setEmission = true;
    Renderer rend;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    int counter = 0;

    // Update is called once per frame
    void Update()
    {
        if (counter % 30 == 0)
        {
            Vector2 vec = rend.material.GetTextureOffset("_MainTex");
            vec += speed;
            rend.material.SetTextureOffset("_EmissionMap", vec);
            rend.material.SetTextureOffset("_MainTex", vec);
        }

        counter++;
    }
}
