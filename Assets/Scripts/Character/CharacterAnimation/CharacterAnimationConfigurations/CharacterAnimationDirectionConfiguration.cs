using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class CharacterAnimationDirectionConfiguration {

    private CharacterAnimationManagerConfiguration referenceManagerConfiguration;


    [BoxGroup("NORMAL BEHAVIOUR")]
    public CharacterAnimationDirection.Type normalVector;

    [BoxGroup("NORMAL BEHAVIOUR")]
    public List<CharacterAnimationDirectionBehaviour> normalVectorBehaviour;


    [BoxGroup("FALLBACK BEHAVIOUR")]
    public bool useFallbackDirection;

    [EnableIf("useFallbackDirection")]
    [BoxGroup("FALLBACK BEHAVIOUR")]
    public CharacterAnimationDirection.Type fallbackVector;


public enum fallbackDiretionRefState { current, target};

   
   [EnableIf("useFallbackDirection")]
    [BoxGroup("FALLBACK BEHAVIOUR")]
    public fallbackDiretionRefState directionState ;


       

    [EnableIf("useFallbackDirection")]
    [BoxGroup("FALLBACK BEHAVIOUR")]
    public List<CharacterAnimationDirectionBehaviour> fallbackVectorBehaviour;



    [BoxGroup("RESTRICTIVE BEHAVIOUR")]
    public bool useRestrictiveDirection;

    [EnableIf("useRestrictiveDirection")]
    [BoxGroup("RESTRICTIVE BEHAVIOUR")]
    public CharacterAnimationDirection.Type restrictiveDirection;

    [EnableIf("useRestrictiveDirection")]
    [BoxGroup("RESTRICTIVE BEHAVIOUR")]
    public float angularThreshold;

    [BoxGroup("RESTRICTIVE BEHAVIOUR")]
    public bool useRestrictiveSpeed;

    [EnableIf("useRestrictiveSpeed")]
    [BoxGroup("RESTRICTIVE BEHAVIOUR")]
    public float maxAngularSpeed;


    public CharacterAnimationDirectionConfiguration(CharacterAnimationManagerConfiguration referenceManagerConfiguration)
    {
        this.referenceManagerConfiguration = referenceManagerConfiguration;
    }

    
}


