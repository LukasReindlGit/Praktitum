using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(CharacterAnimationManager))]
public class CharacterAnimationManagerConfiguration: MonoBehaviour {

    public CharacterAnimationManager manager;

    //[QDivider("Proberties", "Configure the Behaviour of each Vector.")]
    [BoxGroup("Properties")]
    public float turnSensitivity;
    [BoxGroup("Properties")]
    public float turnSpeed;
    [BoxGroup("Properties")]
    public float animSpeedMultiplier;
    [BoxGroup("Properties")]
    public bool smoothFollow = true;
    [BoxGroup("Properties")]
    public float smoothFollowSpeed = 20f;

    /// <summary>
    /// Maximum angular delta between attention vector and body vector
    /// </summary>
    [BoxGroup("Properties")]
    public float bodyMaxAngularDelta;
    /// <summary>
    /// Angular delta treshold for follow after time utility.
    /// </summary>
    [BoxGroup("Properties")]
    public float bodyAngularDeltaTreshold;
    [BoxGroup("Properties")]
    public float bodyAngularDeltaTresholdLatency;


   // [QDivider("Vector Configurations", "Configure the Behaviour of each Vector.")]

    
    [BoxGroup("Direction Configuration")]
    public CharacterAnimationDirectionConfiguration movementDirectionConfiguration;
    [BoxGroup("Direction Configuration")]
    public CharacterAnimationDirectionConfiguration attentionDirectionrConfiguration;
    [BoxGroup("Direction Configuration")]
    public CharacterAnimationDirectionConfiguration lookDirectionConfiguration;
    [BoxGroup("Direction Configuration")]
    public CharacterAnimationDirectionConfiguration aimDirectionConfiguration;
    [BoxGroup("Direction Configuration")]
    public CharacterAnimationDirectionConfiguration bodyDirectionConfiguration;

    private void Awake()
    {
        manager = GetComponent<CharacterAnimationManager>();


        //movementDirectionConfiguration.InitializeAllFilters();

        //attentionDirectionrConfiguration.InitializeAllFilters();

        //lookDirectionConfiguration.InitializeAllFilters();

        //aimDirectionConfiguration.InitializeAllFilters();

        //bodyDirectionConfiguration.InitializeAllFilters();
    }
    private void FixedUpdate()
    {
        //movementDirectionConfiguration.UpdateAllFilter();

        //attentionDirectionrConfiguration.UpdateAllFilter();

        //lookDirectionConfiguration.UpdateAllFilter();

        //aimDirectionConfiguration.UpdateAllFilter();

        //bodyDirectionConfiguration.UpdateAllFilter();

    }


}

