using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CharacterAnimationManager : MonoBehaviour {

    private CharacterAnimationManagerConfiguration m_CAMC;
    protected CharacterAnimationManagerConfiguration _CAMC
    {
        get
        {
            return this.m_CAMC;
        }

        set
        {
            m_CAMC = value;
            Initialize();
        }
    }


    [SerializeField]
    private ICharacterAnimationDataProvider crntCADP;
    protected Animator animator;
    protected Vector3 lastForward;




    /// <summary>
    /// Movement vector in 3D space. Always required. Zero vector is valid. Defines overall rotation of the character game object. The character is currently moving in the direction described by [x,y,z] (direction (if w==0) or a point (if w==0) in world space). 
    /// </summary>
    protected Vector4 fetchedMovementVector = default(Vector4);
    protected Vector4 currentMovementVector = default(Vector4);
    /// <summary>
    /// Attention vector in 3D space. Always required. Zero vector is valid. The character's overall attention/ point of interest is in the direction described by [x,y,z] (direction (if w==0) or a point (if w==0) in world space).
    /// </summary>
    protected Vector4 fetchedAttentionVector = default(Vector4);
    protected Vector4 currentAttentionVector = default(Vector4);

    /// <summary>
    /// Look vector in 3D space. Not always required. Zero vector is invalid --> Attention vector as default. The character's head or appropriate part of his body is facing towarts the direction described by [x,y,z] (direction (if w==0) or a point (if w==0) in world space).
    /// </summary>
    protected Vector4 fetchedLookVector = default(Vector4);
    protected Vector4 currentLookVector = default(Vector4);

    /// <summary>
    /// Aim vector in 3D space. Not always required. Zero vector is invalid --> Attention vector as default. The character is currently aiming in the direction described by [x,y,z] (direction (if w==0) or a point (if w==0) in world space).
    /// </summary>
    protected Vector4 fetchedAimVector = default(Vector4);
    protected Vector4 currentAimVector = default(Vector4);

    /// <summary>
    /// Movement vector in 3D space.  Not always required. Zero vector is invalid --> TorsoBehaviour defines default. The character's torso is currently facing in the direction described by [x,y,z] (direction (if w==0) or a point (if w==0) in world space).
    /// </summary>
    protected Vector4 fetchedBodyVector = default(Vector4);
    private Vector4 m_CurrentBodyVector = default(Vector4);
    protected Vector4 currentBodyVector {

        get {

            return m_CurrentBodyVector;
        }

        set {
            bodyAngularDeltaThresholdTimer = 0.0f;
            m_CurrentBodyVector = value;

        }
    } 
    protected float bodyAngularDeltaThresholdTimer = 0;
    

    private Vector3 lastPosition;
    private Vector3 localPosition;
    private Quaternion localRotation;
    private Quaternion lastRotation;

    /// <summary>
    /// Initialize the CharacterAnimationManager. 
    /// </summary>
    protected virtual void Initialize()
    {

    }

    public void Awake()
    {

        animator = GetComponent<Animator>();
        _CAMC = GetComponent<CharacterAnimationManagerConfiguration>();

        var tempComponents = GetComponents(typeof(Component));
        for(int i = 0; i < tempComponents.Length; i++)
        {
            if (tempComponents[i] is ICharacterAnimationDataProvider)
            {
                crntCADP = tempComponents[i] as ICharacterAnimationDataProvider;
                break;

            }
        }
    }

    // Gets angle around y axis from a world space direction
    public float GetAngleFromForward(Vector3 worldDirection)
    {
        Vector3 local = transform.InverseTransformDirection(worldDirection);
        return Mathf.Atan2(local.x, local.z) * Mathf.Rad2Deg;
    }

    /// Gets angle around y axis from a world space direction
    protected float GetSignedAngle(Vector3 forwardA, Vector3 forwardB)
    {
        // get a numeric angle for each vector, on the X-Z plane (relative to world forward)
        var angleA = Mathf.Atan2(forwardA.x, forwardA.z) * Mathf.Rad2Deg;
        var angleB = Mathf.Atan2(forwardB.x, forwardB.z) * Mathf.Rad2Deg;

        // get the signed difference in these angles
        
        return Mathf.DeltaAngle(angleA, angleB);
    }

    //protected float GetSignedAngularDelta(Vector3 forwardA, Vector3 forwardB, float turnSensitivity)
    //{
    //    float angle = GetSignedAngle(forwardA, forwardB);
    //    angle *= turnSensitivity * 0.01f;
    //    angle = Mathf.Clamp(angle / Time.deltaTime, -1f, 1f);

    //    return angle;
    //}



    private void FetchCurrentData()
    {

        fetchedMovementVector = RemasterDirection(crntCADP.MovementVectorUpdate());

        fetchedAttentionVector = RemasterDirection(crntCADP.AttentionVectorVectorUpdate());

        fetchedLookVector = RemasterDirection(crntCADP.LookVectorUpdate());

        fetchedAimVector = RemasterDirection(crntCADP.AimVectorUpdate());

        // ToDo: This is only for debug purposes
        //fetchedBodyVector = fetchedAttentionVector;
    }

    /// <summary>
    /// Returns a normalized direction vector from the transform center towards vector.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    private Vector4 RemasterDirection(Vector4 vector)
    {
        // is default
        if (vector == default(Vector4))
        {
            return default(Vector4);
        }

        // is direction already
        if(vector.w == 0)
        {
            return vector.normalized;
        }

        // is point
        vector /= vector.w;
        return (transform.position - (Vector3)vector).normalized;

    }

    protected virtual void UpdateAnimationController()
    {
        
    }

    protected virtual void Update()
    {
        // Return if game is paused --> updating the animator is unnecessary
        if (Time.deltaTime == 0f)
        {
            return;
        }

        FetchCurrentData();

        UpdateAnimationController();
    }



    protected abstract void CalculateMovementVector();

    protected abstract void ApplyMovementVector();

    protected abstract void CalculateAttentionVector();

    protected abstract void ApplyAttentionVector();

    protected abstract void CalculateBodyVector();

    protected abstract void ApplyBodyVector();

    protected abstract void CalculateAimVector();

    protected abstract void ApplyAimVector();

    protected abstract void CalculateLookVector();

    protected abstract void ApplyLookVector();


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
