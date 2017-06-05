﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidCharacterAnimationManager : CharacterAnimationManager {

    public Transform LookTarget;
    public Transform AimTarget;
    public Transform AttentionTarget;

	   



    //protected override void CalculateMovementVector()
    //{
    //    currentMovementVector = fetchedMovementVector;
    //}


    protected override void ApplyMovementVector()
    {
        if (directions[CharacterAnimationDirection.Type.Movement].currentDirection == default(Vector4))
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }

        var local = transform.InverseTransformDirection(directions[CharacterAnimationDirection.Type.Movement].currentDirection).normalized;
        animator.SetFloat("Forward", local.z);
        animator.SetFloat("Right", local.x);
    }

    //protected override void CalculateAttentionVector()
    //{
    //    if (fetchedAttentionVector != default(Vector4))
    //    {
    //        currentAttentionVector = fetchedAttentionVector;
    //    }
    //    else if(currentMovementVector != default(Vector4))
    //    {
    //        currentAttentionVector = currentMovementVector;
    //    }else
    //    {
    //        // Nothing
    //    }
    //}


    protected override void ApplyAttentionVector()
    {
    }

    //protected override void CalculateBodyVector()
    //{
    //    var tempAngle = GetSignedAngle(currentAttentionVector, currentBodyVector);

    //    Debug.Log("Angle: " + tempAngle);

    //    if (Mathf.Abs(tempAngle) >= _CAMC.bodyMaxAngularDelta)
    //    {
    //        currentBodyVector = currentAttentionVector;
            
    //    }else if (Mathf.Abs(tempAngle) >= _CAMC.bodyAngularDeltaTreshold)
    //    {
    //        bodyAngularDeltaThresholdTimer += Time.deltaTime;

    //        if(bodyAngularDeltaThresholdTimer >= _CAMC.bodyAngularDeltaTresholdLatency)
    //        {
    //            currentBodyVector = currentAttentionVector;
    //            bodyAngularDeltaThresholdTimer = 0.0f;
    //        }
    //    }
    //    else
    //    {
    //        bodyAngularDeltaThresholdTimer = 0.0f;
    //    }

       
    //}

    protected override void ApplyBodyVector()
    {

        // Debug.Log("Body: " + directions[CharacterAnimationDirection.Type.Body].currentDirection);
        var tempRotationAroundY = GetAngleFromForward(directions[CharacterAnimationDirection.Type.Body].currentDirection);
        // transform.Rotate(transform.up, tempRotationAroundY);

      

        // Update Animator
        var tempAngularDelta = tempRotationAroundY;
        tempAngularDelta *= configuration.turnSensitivity * 0.01f;
        tempAngularDelta = Mathf.Clamp(tempAngularDelta / Time.deltaTime, -1f, 1f);
        Debug.Log("TargetValue: " + tempAngularDelta);

        if (Mathf.Abs(tempAngularDelta - animator.GetFloat("Turn")) > Time.deltaTime * configuration.turnSpeed)
        {
            tempAngularDelta = Mathf.Lerp(animator.GetFloat("Turn"), tempAngularDelta, Time.deltaTime * configuration.turnSpeed);

        }else
        {
            tempAngularDelta = tempAngularDelta;

        }


        //var tempAngularDelta = directions[CharacterAnimationDirection.Type.Body].currentAngularDeltaCurrentToTarget;
        //tempAngularDelta *= configuration.turnSensitivity * 0.01f;
        //tempAngularDelta = Mathf.Clamp(tempAngularDelta / Time.deltaTime, -1f, 1f);
        //tempAngularDelta = Mathf.Lerp(animator.GetFloat("Turn"), tempAngularDelta, Time.deltaTime * configuration.turnSpeed);

        //animator.speed = 4.0f;


        animator.SetFloat("Turn", tempAngularDelta);



        //if (Vector3.Angle(directions[CharacterAnimationDirection.Type.Body].currentDirection, transform.forward) > 1)
        //{
        //    // Debug.Log("Body: " + directions[CharacterAnimationDirection.Type.Body].currentDirection);
        //    var tempRotationAroundY = GetAngleFromForward(directions[CharacterAnimationDirection.Type.Body].currentDirection);
        //   // transform.Rotate(transform.up, tempRotationAroundY);

        //     Debug.Log("Rot around: " + tempRotationAroundY);

        //    // Update Animator
        //    var tempAngularDelta = tempRotationAroundY;
        //    tempAngularDelta *= configuration.turnSensitivity * 0.01f;
        //    tempAngularDelta = Mathf.Clamp(tempAngularDelta / Time.deltaTime, -1f, 1f);
        //    tempAngularDelta = Mathf.Lerp(animator.GetFloat("Turn"), tempAngularDelta, Time.deltaTime * configuration.turnSpeed);


        //    //var tempAngularDelta = directions[CharacterAnimationDirection.Type.Body].currentAngularDeltaCurrentToTarget;
        //    //tempAngularDelta *= configuration.turnSensitivity * 0.01f;
        //    //tempAngularDelta = Mathf.Clamp(tempAngularDelta / Time.deltaTime, -1f, 1f);
        //    //tempAngularDelta = Mathf.Lerp(animator.GetFloat("Turn"), tempAngularDelta, Time.deltaTime * configuration.turnSpeed);

        //    //animator.speed = 4.0f;


        //    animator.SetFloat("Turn", tempAngularDelta);



        //    //// Update transform
        //    //var tempVector3 = Vector3.RotateTowards(transform.forward, currentBodyVector, Time.deltaTime * _CAMC.turnSpeed, 1);
        //    //var tempRotationAroundY = GetAngleFromForward(tempVector3);
        //    //transform.Rotate(transform.up, tempRotationAroundY);

        //    //// Update Animator
        //    //var tempAngularDelta = tempRotationAroundY;
        //    //tempAngularDelta *= _CAMC.turnSensitivity * 0.01f;
        //    //tempAngularDelta = Mathf.Clamp(tempAngularDelta / Time.deltaTime, -1f, 1f);
        //    //tempAngularDelta = Mathf.Lerp(animator.GetFloat("Turn"), tempAngularDelta, Time.deltaTime * _CAMC.turnSpeed);
        //    //animator.SetFloat("Turn", tempAngularDelta);

        //    //// ToDo: Update Turn Animation Speed based on angular delta?
        //}
        //else
        //{
        //    animator.speed = 1.0f;
        //    if (Mathf.Abs(animator.GetFloat("Turn")) > 0.05f)
        //    {
        //        animator.SetFloat("Turn", Mathf.Lerp(animator.GetFloat("Turn"), 0.0f, Time.deltaTime * configuration.turnSpeed));

        //    }
        //    else
        //    {
        //        animator.SetFloat("Turn", 0.0f);
        //    }
        //}
    }

    //protected override void CalculateAimVector()
    //{
    //    if(fetchedAimVector != default(Vector4))
    //    {
    //        currentAimVector = fetchedAimVector;
    //    }else
    //    {
    //        currentAimVector = currentAttentionVector;
    //    }
    //}

    protected override void ApplyAimVector()
    {
        
            AimTarget.position = transform.position + (Vector3)directions[CharacterAnimationDirection.Type.Aim].currentDirection * 100;

    }

    //protected override void CalculateLookVector()
    //{
    //    if (fetchedLookVector != default(Vector4))
    //    {
    //        currentLookVector = fetchedLookVector;
    //    }
    //    else
    //    {
    //        currentLookVector = currentAttentionVector;
    //    }
    //}

    protected override void ApplyLookVector()
    {
        if(directions[CharacterAnimationDirection.Type.Look].currentDirection.w == 0)
        {
            LookTarget.position = transform.position + (Vector3)directions[CharacterAnimationDirection.Type.Look].currentDirection * 100;
        }else
        {

            // ToDo
            Debug.LogError("Vector is a point. Implementation required!");
        }
    }

}
