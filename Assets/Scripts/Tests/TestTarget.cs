using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTarget : MonoBehaviour , IDamageable {

    public void DoDamage(float amount)
    {
        Debug.Log("HIT!");
        StartCoroutine(HitAnimation());
    }
    
    IEnumerator HitAnimation()
    {
        float i = 1f;
        while (i < 2f)
        {
            transform.localScale = Vector3.one * i;
            i += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(2);
        transform.localScale = Vector3.one;
    }
}
