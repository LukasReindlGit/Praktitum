using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour {

    private CharacterAnimationManagerConfiguration m_CAMC;
    public CharacterAnimationManagerConfiguration _CAMC
    {
        private get
        {
            return this.m_CAMC;
        }

        set
        {
            m_CAMC = value;
            Initialize();
        }
    }


    private ICharacterAnimationDataProvider crntCADP;





    /// <summary>
    /// Movement vector in 3D space. Always required. Zero vector is valid. Defines overall rotation of the character game object. The character is currently moving in the direction described by [x,y,z] (direction (if w==0) or a point (if w==0) in world space). 
    /// </summary>
    private Vector4 MovementVector;
    /// <summary>
    /// Attention vector in 3D space. Always required. Zero vector is valid. The character's overall attention/ point of interest is in the direction described by [x,y,z] (direction (if w==0) or a point (if w==0) in world space).
    /// </summary>
    private Vector4 AttentionVector;
    /// <summary>
    /// Look vector in 3D space. Not always required. Zero vector is invalid --> Attention vector as default. The character's head or appropriate part of his body is facing towarts the direction described by [x,y,z] (direction (if w==0) or a point (if w==0) in world space).
    /// </summary>
    private Vector4 LookVector;
    /// <summary>
    /// Aim vector in 3D space. Not always required. Zero vector is invalid --> Attention vector as default. The character is currently aiming in the direction described by [x,y,z] (direction (if w==0) or a point (if w==0) in world space).
    /// </summary>
    private Vector4 AimVector;
    /// <summary>
    /// Movement vector in 3D space.  Not always required. Zero vector is invalid --> TorsoBehaviour defines default. The character's torso is currently facing in the direction described by [x,y,z] (direction (if w==0) or a point (if w==0) in world space).
    /// </summary>
    private Vector4 TorsoVector;





    private void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// Initialize the CharacterAnimationManager. 
    /// </summary>
    private void Initialize()
    {

    }


    private void FetchCurrentData()
    {
        MovementVector = crntCADP.MovementVectorUpdate();

        AttentionVector = crntCADP.AttentionVectorVectorUpdate();

        LookVector = crntCADP.LookVectorUpdate();

        AimVector = crntCADP.AimVectorUpdate();
    }

    private void UpdateAnimationController()
    {

    }

    private bool VectorIsValid(Vector4 v4)
    {
        if(v4 != null)
        {
            if(v4 != default(Vector4))
            {
                return false;
            }
        }

        return true;
    }


}
