﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class CharacterAnimationDirectionBehaviour {


   
    public enum Transition { instant, byFunction };

   

   
    


    public enum Condition { unconditionally, angularDeltaGreaterThan, angularDeltaLessThan }

    // [QTitle("WHICH")]

    [BoxGroup("Behavioural Trehshold")]
    public Condition condition;

    [BoxGroup("Behavioural Trehshold")]
    [Indent(2)]
    [ShowIf("ShowConditionValue")]
    public float conditionValue;


    [BoxGroup("Behavioural Trehshold")]
    [Indent(0)]
    [OnValueChanged("UpdateFilterValues")]
    public CharacterAnimationDirectionFilter.Filter filter;

    [BoxGroup("Behavioural Trehshold")]
    [Indent(2)]
    [ShowIf("ShowAverageTime")]
    [OnValueChanged("UpdateFilterValues")]
    public float averageTime;

    [BoxGroup("Behavioural Trehshold")]
    [Indent(0)]
    public Transition transition;

    [HideInInspector]
    public TransitionFunction transitionFunction = new TransitionFunction();

    [BoxGroup("Behavioural Trehshold")]
    [Indent(2)]
    [ShowIf("ShowTransitionFunction")]
    [OnValueChanged("UpdateTransitionValues")]
    public TransitionFunction.Type transitionType;

    [BoxGroup("Behavioural Trehshold")]
    [Indent(2)]
    [ShowIf("ShowTransitionFunction")]
    public float transitionSpeed;

    public CharacterAnimationDirectionBehaviour()
    {
        this.condition = CharacterAnimationDirectionBehaviour.Condition.unconditionally;
        this.filter = CharacterAnimationDirectionFilter.Filter.none;
        this.transition = CharacterAnimationDirectionBehaviour.Transition.instant;
    }

    private bool ShowConditionValue()
    {
        if (condition == Condition.unconditionally)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private bool ShowTransitionFunction()
    {
        if (transition == Transition.byFunction)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool ShowAverageTime()
    {
        if (filter == CharacterAnimationDirectionFilter.Filter.average)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void UpdateFilterValues()
    {

    }
    private void UpdateTransitionValues()
    {
        transitionFunction.type = transitionType;
        
    }


}
