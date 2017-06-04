using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TransitionFunction {

    public enum Type { linear, easeInQuad, easeOutQuad, easeInOutQuad, easeInCubic, easeOutCubic, easeInOutCubic }

    public Type type;

    public float calc(float x, float t, float b, float c, float d)
    {
        switch (type)
        {
            case Type.linear:
                return linear(x, t, b, c, d);

            case Type.easeInQuad:
                Debug.Log("Calc: " + easeInQuad(x, t, b, c, d));
                return easeInQuad(x, t, b, c, d);

            case Type.easeOutQuad:
                return easeOutQuad(x, t, b, c, d);

            case Type.easeInOutQuad:
                return easeInOutQuad(x, t, b, c, d);

            case Type.easeInCubic:
                return easeInCubic(x, t, b, c, d);

            case Type.easeOutCubic:
                return easeOutCubic(x, t, b, c, d);

            case Type.easeInOutCubic:
                return easeInOutCubic(x, t, b, c, d);

            default:
                return linear(x, t, b, c, d);
              
        }
    }
    public float linear(float x, float t, float b, float c, float d)
    {
        t = t / d;
        return c * (t) + b;
    }
    public float easeInQuad(float x, float t, float b, float c, float d)
    {
        t = t / d;

        return c * (t ) * t + b;
    }
	public float easeOutQuad(float x, float t, float b, float c, float d)
    {
        t = t / d;

        return -c * (t ) * (t - 2) + b;
    }
	public float easeInOutQuad(float x, float t, float b, float c, float d)
    {
        t = t / d /2;

        if ((t ) < 1) return c / 2 * t * t + b;
        return -c / 2 * ((--t) * (t - 2) - 1) + b;
    }
	public float easeInCubic(float x, float t, float b, float c, float d)
    {
        t = t / d;

        return c * (t) * t * t + b;
    }
	public float easeOutCubic(float x, float t, float b, float c, float d)
    {
        t = t / d - 1;

        return c * ((t) * t * t + 1) + b;
    }
	public float easeInOutCubic(float x, float t, float b, float c, float d)
    {
        t = t / d;

        if ((t  / 2) < 1) return c / 2 * t * t * t + b;
        return c / 2 * ((t -= 2) * t * t + 2) + b;
    }

}

