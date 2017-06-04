using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class CharacterAnimationDirectionConfiguration {

    private CharacterAnimationManagerConfiguration referenceManagerConfiguration;


    [BoxGroup("NORMAL")]
    public CharacterAnimationDirection.Type normalVector;

    [BoxGroup("NORMAL")]
    public List<CharacterAnimationDirectionBehaviour> normalVectorBehaviour;


    [BoxGroup("FALLBACK")]
    public bool useFallbackDirection;

    [EnableIf("useFallbackDirection")]
    [BoxGroup("FALLBACK")]
    public CharacterAnimationDirection.Type fallbackVector;

    [EnableIf("useFallbackDirection")]
    [BoxGroup("FALLBACK")]
    public List<CharacterAnimationDirectionBehaviour> fallbackVectorBehaviour;

    public CharacterAnimationDirectionConfiguration(CharacterAnimationManagerConfiguration referenceManagerConfiguration)
    {
        this.referenceManagerConfiguration = referenceManagerConfiguration;
    }

    
}


