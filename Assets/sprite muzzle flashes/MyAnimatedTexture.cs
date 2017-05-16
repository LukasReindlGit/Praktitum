using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAnimatedTexture : MonoBehaviour
{

    public float fps = 30.0f;
    public Texture2D[] frames;

    public bool destroyAfterFirstLoop = false;

    private int frameIndex;
    private MeshRenderer rendererMy;

    void Start()
    {
        frameIndex = 0;
        rendererMy = GetComponent<MeshRenderer>();
        this.StartCoroutine(FrameLoop());
    }

    IEnumerator FrameLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / fps);
            NextFrame();
           
        }
    }

    void NextFrame()
    {
        rendererMy.sharedMaterial.SetTexture("_MainTex", frames[frameIndex]);
        frameIndex = (frameIndex + 1);
        if (frameIndex >= frames.Length)
        {
            if (destroyAfterFirstLoop)
            {
                Destroy(gameObject);
            }
            else
            {
                frameIndex = 0;
            }
        }
    }
}
