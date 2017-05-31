using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManagerConfiguration: MonoBehaviour {


    [QDivider("Proberties", "Configure the Behaviour of each Vector.")]

    public float turnSensitivity;
    public float turnSpeed;
    public float animSpeedMultiplier;
    public bool smoothFollow = true;
    public float smoothFollowSpeed = 20f;

    /// <summary>
    /// Maximum angular delta between attention vector and body vector
    /// </summary>
    public float bodyMaxAngularDelta;
    /// <summary>
    /// Angular delta treshold for follow after time utility.
    /// </summary>
    public float bodyAngularDeltaTreshold;
    public float bodyAngularDeltaTresholdLatency;


    [QDivider("Vector Configurations", "Configure the Behaviour of each Vector.")]

    
   
    public CharacterAnimationVectorConfiguration movementVectorConfiguration;

    public CharacterAnimationVectorConfiguration attentionVectorConfiguration;

    public CharacterAnimationVectorConfiguration LookVectorConfiguration;

    public CharacterAnimationVectorConfiguration AimVectorConfiguration;

    public CharacterAnimationVectorConfiguration TorsoVectorConfiguration;





}

