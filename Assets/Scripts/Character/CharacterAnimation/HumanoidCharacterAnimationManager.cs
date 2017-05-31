using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidCharacterAnimationManager : CharacterAnimationManager {

    public Transform LookTarget;
    public Transform AimTarget;
    public Transform AttentionTarget;


	


    protected override void UpdateAnimationController()
    {
        base.UpdateAnimationController();

        // Resolve Attention Vector
        if (fetchedAttentionVector != default(Vector4))
        {

            //var tempAngle = Mathf.Lerp(animator.GetFloat("Turn"), GetAngularDelta(lastAttentionVector, attentionVector, _CAMC.turnSensitivity), Time.deltaTime * _CAMC.turnSpeed);

            // Update rotation
            //transform.localRotation *= Quaternion.Euler(new Vector3(0, tempAngle, 0));
            //transform.forward = attentionVector;
            currentAttentionVector = fetchedAttentionVector;
        }
        else
        {
            //currentAttentionVector = fetchedAttentionVector;
        }

        //Debug.Log("Attention Vector: " + attentionVector + " / Last AttentionEVctor: " + lastAttentionVector);
        //var tempAngle = Mathf.Lerp(animator.GetFloat("Turn"), GetAngularDelta(lastAttentionVector, attentionVector, _CAMC.turnSensitivity), Time.deltaTime * _CAMC.turnSpeed);

        //transform.localRotation *= Quaternion.Euler(new Vector3(0, tempAngle, 0));
        // Debug.Log("AngleDelta: " + Vector3.Angle(currentBodyVector, currentAttentionVector));


        // Update Animator Turn value
        //animator.SetFloat("Turn", tempAngle);

        CalculateMovementVector();

        ApplyMovementVector();


        CalculateAttentionVector();

        ApplyAttentionVector();



        CalculateBodyVector();

        ApplyBodyVector();



        CalculateAimVector();

        ApplyAimVector();



        CalculateLookVector();

        ApplyLookVector();


        // Resolve Movement (Based on Attention)

        if (fetchedMovementVector == default(Vector4))
        {
            animator.SetBool("isMoving", false);
        }else
        {
            animator.SetBool("isMoving", true);
        }

        // Movement Speed

        // Movement Diretion

        // Attention defines object rotation --> 

    }



    protected override void CalculateMovementVector()
    {
        currentMovementVector = fetchedMovementVector;
    }


    protected override void ApplyMovementVector()
    {
        var local = transform.InverseTransformDirection(currentMovementVector).normalized;
        animator.SetFloat("Forward", local.z);
        animator.SetFloat("Right", local.x);
    }

    protected override void CalculateAttentionVector()
    {
        if (fetchedAttentionVector != default(Vector4))
        {
            currentAttentionVector = fetchedAttentionVector;
        }
        else if(currentMovementVector != default(Vector4))
        {
            currentAttentionVector = currentMovementVector;
        }else
        {
            // Nothing
        }
    }


    protected override void ApplyAttentionVector()
    {
    }

    protected override void CalculateBodyVector()
    {
        var tempAngle = GetSignedAngle(currentAttentionVector, currentBodyVector);

        Debug.Log("Angle: " + tempAngle);

        if (Mathf.Abs(tempAngle) >= _CAMC.bodyMaxAngularDelta)
        {
            currentBodyVector = currentAttentionVector;
            
        }else if (Mathf.Abs(tempAngle) >= _CAMC.bodyAngularDeltaTreshold)
        {
            bodyAngularDeltaThresholdTimer += Time.deltaTime;

            if(bodyAngularDeltaThresholdTimer >= _CAMC.bodyAngularDeltaTresholdLatency)
            {
                currentBodyVector = currentAttentionVector;
                bodyAngularDeltaThresholdTimer = 0.0f;
            }
        }
        else
        {
            bodyAngularDeltaThresholdTimer = 0.0f;
        }

       
    }

    protected override void ApplyBodyVector()
    {
        if (Vector3.Angle(currentBodyVector, transform.forward) > 1)
        {
            // Update transform
            var tempVector3 = Vector3.RotateTowards(transform.forward, currentBodyVector, Time.deltaTime * _CAMC.turnSpeed, 1);
            var tempRotationAroundY = GetAngleFromForward(tempVector3);
            transform.Rotate(transform.up, tempRotationAroundY);

            // Update Animator
            var tempAngularDelta = tempRotationAroundY;
            tempAngularDelta *= _CAMC.turnSensitivity * 0.01f;
            tempAngularDelta = Mathf.Clamp(tempAngularDelta / Time.deltaTime, -1f, 1f);
            tempAngularDelta = Mathf.Lerp(animator.GetFloat("Turn"), tempAngularDelta, Time.deltaTime * _CAMC.turnSpeed);
            animator.SetFloat("Turn", tempAngularDelta);

            // ToDo: Update Turn Animation Speed based on angular delta?
        }else
        {
            if (Mathf.Abs(animator.GetFloat("Turn")) > 0.05f)
            {
                animator.SetFloat("Turn", Mathf.Lerp(animator.GetFloat("Turn"), 0.0f, Time.deltaTime * _CAMC.turnSpeed));

            }else
            {
                animator.SetFloat("Turn", 0.0f);
            }
        }
    }

    protected override void CalculateAimVector()
    {
        if(fetchedAimVector != default(Vector4))
        {
            currentAimVector = fetchedAimVector;
        }else
        {
            currentAimVector = currentAttentionVector;
        }
    }

    protected override void ApplyAimVector()
    {
        if (currentAimVector.w == 0)
        {
            AimTarget.position = transform.position + (Vector3)currentAimVector * 100;
        }
        else
        {

            // ToDo
            Debug.LogError("Vector is a point. Implementation required!");
        }
    }

    protected override void CalculateLookVector()
    {
        if (fetchedLookVector != default(Vector4))
        {
            currentLookVector = fetchedLookVector;
        }
        else
        {
            currentLookVector = currentAttentionVector;
        }
    }

    protected override void ApplyLookVector()
    {
        if(currentLookVector.w == 0)
        {
            LookTarget.position = transform.position + (Vector3)currentLookVector * 100;
        }else
        {

            // ToDo
            Debug.LogError("Vector is a point. Implementation required!");
        }
    }

}
