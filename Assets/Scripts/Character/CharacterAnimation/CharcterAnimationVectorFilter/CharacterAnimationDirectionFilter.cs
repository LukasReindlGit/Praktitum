using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class CharacterAnimationDirectionFilter {

    public enum Filter { none, average };

    public Filter filter;


    public float averageTime;

    [HideInInspector]
    public CharacterAnimationManager referenceManager;
    CharacterAnimationDirection.Type type;

    private List<Vector4> values = new List<Vector4>();

   public CharacterAnimationDirectionFilter(Filter filter, CharacterAnimationDirection.Type type, CharacterAnimationManager referenceManager , float averageTime = 1.0f)
    {
        this.referenceManager = referenceManager;
        this.filter = filter;
        this.type = type;
        this.averageTime = averageTime;
    }

    public void UpdateValuesInFixedUpdate( )
    {
        values.Add(referenceManager.GetAnimationDirectionForFilters(type));

        if (values.Count > averageTime / Time.fixedUnscaledDeltaTime)
        {
            values.RemoveAt(0);
        }
    }

    Vector4 getAverage()
    {
        var tempVector4 = default(Vector4);
        for(int i = 0; i < values.Count; i++)
        {
            tempVector4 += values[i];
        }
        return tempVector4 / values.Count;
    }

    public Vector3 GetFilteredDirection()
    {
        switch (filter)
        {
            case Filter.none:
                return referenceManager.GetAnimationDirectionForFilters(type);

            case Filter.average:

                return getAverage();

            default:
                return referenceManager.GetAnimationDirectionForFilters(type);
        }
    }


   

}
