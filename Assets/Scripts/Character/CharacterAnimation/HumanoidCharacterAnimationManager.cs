using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidCharacterAnimationManager : CharacterAnimationManager {

    public Transform MovementRef;

    public Transform LookTarget;
    public Transform AimTarget;
    public Transform AttentionTarget;

    public float turnAnimationDuration;
    public float turnAnimationAngularDelta;

    public float normalMovementTransitionSpeed;
    public float stopMovingTransitionSpeed;

    private Vector3 curretnAnimationDirectionTarget;
    private Quaternion oldRotation = new Quaternion();

    static int turnLeftState = Animator.StringToHash("TurnLeft");
    static int turnRightState = Animator.StringToHash("TurnRight");
    static int idleState = Animator.StringToHash("Idle");


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

        var tempSpeed = normalMovementTransitionSpeed;
        if(new Vector2(local.x, local.z).magnitude < 0.5f)
        {
            tempSpeed = stopMovingTransitionSpeed;
        }
        var forward = Mathf.Lerp(animator.GetFloat("Forward"), local.z, Time.deltaTime * tempSpeed);
        animator.SetFloat("Forward", forward);

        var right = Mathf.Lerp(animator.GetFloat("Right"), local.x, Time.deltaTime * tempSpeed);
        animator.SetFloat("Right", right);
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
        animator.speed = 1.0f;
        if (directions[CharacterAnimationDirection.Type.Movement].currentDirection == default(Vector4))
        {
            // Debug.Log("Body: " + directions[CharacterAnimationDirection.Type.Body].currentDirection);
            var tempRotationAroundY = GetAngleFromForward(directions[CharacterAnimationDirection.Type.Body].currentDirection);

           

            // Update Animator
            var tempAngularDelta = tempRotationAroundY;
            tempAngularDelta *= configuration.turnSensitivity * 0.01f;
            tempAngularDelta = Mathf.Clamp(tempAngularDelta / Time.deltaTime, -1f, 1f);
            //Debug.Log("TargetValue: " + tempAngularDelta);

            //if (Mathf.Abs(tempAngularDelta - animator.GetFloat("Turn")) > Time.deltaTime * configuration.turnSpeed)
            //{

            //tempAngularDelta = Mathf.Lerp(animator.GetFloat("Turn"), tempAngularDelta, Time.deltaTime * configuration.turnSpeed);


            //if (Mathf.Abs(tempAngularDelta - animator.GetFloat("Turn")) > Time.deltaTime * configuration.turnSpeed)
            //{
            //    tempAngularDelta = Mathf.Lerp(animator.GetFloat("Turn"), tempAngularDelta, Time.deltaTime * configuration.turnSpeed);

            //}
            //else
            //{
            //    tempAngularDelta = tempAngularDelta;

            //}




            //var tempAngularDelta = directions[CharacterAnimationDirection.Type.Body].currentAngularDeltaCurrentToTarget;
            //tempAngularDelta *= configuration.turnSensitivity * 0.01f;
            //tempAngularDelta = Mathf.Clamp(tempAngularDelta / Time.deltaTime, -1f, 1f);
            //tempAngularDelta = Mathf.Lerp(animator.GetFloat("Turn"), tempAngularDelta, Time.deltaTime * configuration.turnSpeed);

            //animator.speed = 4.0f;


            animator.SetFloat("Turn", tempAngularDelta);

            animator.SetBool("TurnRight", false);
            animator.SetBool("TurnLeft", false);


            if (directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta < 0 && curretnAnimationDirectionTarget != (Vector3)directions[CharacterAnimationDirection.Type.Body].targetDirection)
            {
                animator.SetBool("TurnLeft", true);
                animator.SetBool("TurnRight", false);
                curretnAnimationDirectionTarget = directions[CharacterAnimationDirection.Type.Body].targetDirection;
            }

            if (directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta > 0 && curretnAnimationDirectionTarget != (Vector3)directions[CharacterAnimationDirection.Type.Body].targetDirection)
            {
                animator.SetBool("TurnLeft", false);
                animator.SetBool("TurnRight", true);
                curretnAnimationDirectionTarget = directions[CharacterAnimationDirection.Type.Body].targetDirection;

            }

            //if (Vector3.Angle(directions[CharacterAnimationDirection.Type.Body].currentDirection, transform.forward) > 1)
            //{
            if (Mathf.Abs(GetAngleFromForward(directions[CharacterAnimationDirection.Type.Body].targetDirection)) > 2) { 
                var tempSpeedFactor = Mathf.Abs(directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta) / turnAnimationAngularDelta;
                var tempAmountFactor = Mathf.Abs(directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta) % turnAnimationAngularDelta;

                //Debug.Log("AngularDelta: " + directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta + " / SpeedFactor: " + tempSpeedFactor + " / Amount: " + (tempAmountFactor));

                //Debug.Log("Ausgleich: " + (Mathf.Sign(directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta) * tempAmountFactor));
                //animator.speed = tempSpeedFactor;
               // transform.Rotate(transform.up, (Mathf.Sign(directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta) * tempAmountFactor * (Time.deltaTime / (turnAnimationDuration * tempSpeedFactor))));

            }


        }
        else
        {

            // is Moving
            var tempRotationAroundY = GetAngleFromForward(directions[CharacterAnimationDirection.Type.Body].currentDirection);
            transform.Rotate(transform.up, tempRotationAroundY);


        }


            //if (Vector3.Angle(directions[CharacterAnimationDirection.Type.Body].currentDirection, transform.forward) > 1)
            //{
            //    // Debug.Log("Body: " + directions[CharacterAnimationDirection.Type.Body].currentDirection);
            //    var tempRotationAroundY = GetAngleFromForward(directions[CharacterAnimationDirection.Type.Body].currentDirection);
            //    transform.Rotate(transform.up, tempRotationAroundY);

            //    Debug.Log("Rot around: " + tempRotationAroundY);

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

    void OnAnimatorMove()
    {



        // Update position based on animation movement using navigation surface height
        Vector3 position = animator.deltaPosition;
        // position.y = animator.nextPosition.y;
        MovementRef.position += position;

      




            var currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
            var nextStateBase = animator.GetNextAnimatorStateInfo(0);
            // Debug.Log("currentBaseState "+ currentBaseState.shortNameHash);

            var realDurationInSeconds = animator.speed * turnAnimationDuration;

            var remainingDurationInSeconds = realDurationInSeconds;

            if (currentBaseState.shortNameHash == turnLeftState || currentBaseState.shortNameHash == turnRightState)
            {
                Debug.Log("Do Stuff Here");
                remainingDurationInSeconds *= currentBaseState.normalizedTime;
            }


            var anglePerSeconds = directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta / remainingDurationInSeconds;
            //Debug.Log("crnt AngularDleta on target switch" + directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta);
            var angularDeltaThisFrame = anglePerSeconds * Time.deltaTime;


        // IS NOT MOVING
        if (directions[CharacterAnimationDirection.Type.Movement].currentDirection == default(Vector4))
        {

            //Debug.Log("realDurationInSeconds " + realDurationInSeconds + " / angelPerSeconds: " + anglePerSeconds + " / angularDeltaThisFrame: " + angularDeltaThisFrame);
            if (currentBaseState.shortNameHash == turnLeftState ||
                currentBaseState.shortNameHash == turnRightState ||
                //(currentBaseState.shortNameHash == idleState && currentBaseState.normalizedTime < 0.3f) ||
                currentBaseState.shortNameHash == idleState && (nextStateBase.shortNameHash == turnLeftState || nextStateBase.shortNameHash == turnRightState))
            {
                if (Mathf.Abs(GetSignedAngularDelta(Quaternion.FromToRotation(MovementRef.forward, directions[CharacterAnimationDirection.Type.Body].targetDirection).eulerAngles.y)) >= Mathf.Abs(angularDeltaThisFrame))
                {
                    // Debug.Log("YES");
                    MovementRef.rotation *= Quaternion.AngleAxis(angularDeltaThisFrame, Vector3.up);
                }
                else
                {
                    // Debug.Log("NO");
                    MovementRef.rotation *= Quaternion.FromToRotation(MovementRef.forward, directions[CharacterAnimationDirection.Type.Body].targetDirection);

                }

            }
            else
            {
                // Debug.Log("NO");
               // MovementRef.rotation *= Quaternion.FromToRotation(MovementRef.forward, directions[CharacterAnimationDirection.Type.Body].targetDirection);

            }
        }





        //Quaternion deltaRotation = animator.deltaRotation;
        //Quaternion rotation = animator.rootRotation;
        //Debug.Log("deltaRotation: " + deltaRotation.eulerAngles.y);

        //var rotationY = rotation.eulerAngles.y > 180 ? (rotation.eulerAngles.y - 360) : rotation.eulerAngles.y;

        //var oldRotaionY = oldRotation.eulerAngles.y > 180 ? (oldRotation.eulerAngles.y - 360) : oldRotation.eulerAngles.y;

        //var tempAnimRotDelta = oldRotaionY - rotationY;
        //Debug.Log("Anim Rot Delta:" + tempAnimRotDelta);
        //oldRotation = rotation;

        //Vector3 local = MovementRef.transform.InverseTransformDirection(directions[CharacterAnimationDirection.Type.Body].targetDirection);

        //var test = Mathf.Atan2(local.x, local.z) * Mathf.Rad2Deg;
        ////test = Mathf.LerpAngle(MovementRef.transform.rotation.eulerAngles.y, test, Time.deltaTime * 5);
        ////transform.rotation = rotation;
        //var angularDelta = 0.0f;
        //if (Mathf.Abs(Vector3.Angle(MovementRef.transform.forward, directions[CharacterAnimationDirection.Type.Body].targetDirection) ) > 10) // animator.GetCurrentAnimatorStateInfo().na
        //{
        //    var tempSpeedFactor = Mathf.Abs(directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta) / turnAnimationAngularDelta;
        //    var tempAmountFactor = Mathf.Abs(directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta) - turnAnimationAngularDelta;

        //    //tempSpeedFactor = 1.5f;
        //    //tempAmountFactor = 45;
        //    //var targetRotation = transform.rotation * tempAnimRotDelta;
        //    //targetRotation.eulerAngles += new Vector3(0, GetAngleFromForward(directions[CharacterAnimationDirection.Type.Body].targetDirection), 0);
        //    angularDelta = (Mathf.Sign(directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta) * tempAmountFactor * (Time.deltaTime / (turnAnimationDuration )));
        //    //rotation = Quaternion.RotateTowards(rotation, targetRotation, angularDelta);
        //    //  rotation.eulerAngles += new Vector3(0, ,0);
        //}else
        //{

        //}


        ////var angularDeltaY = tempAnimRotDelta.eulerAngles.y > 180 ? (tempAnimRotDelta.eulerAngles.y - 360) : tempAnimRotDelta.eulerAngles.y;
        ////Debug.Log(angularDeltaY);
        ////amount = amount * 2;
        ////var tempEuler = transform.rotation.eulerAngles;
        ////tempEuler += amount;
        //// transform.rotation *= tempAnimRotDelta ;
        ////Debug.Log("DeltaRootRotation: " + deltaRotation.eulerAngles.y + " / angularDelta: " + angularDelta);
        //MovementRef.rotation *= Quaternion.AngleAxis((deltaRotation.eulerAngles.y > 180 ? (deltaRotation.eulerAngles.y - 360) : deltaRotation.eulerAngles.y) + angularDelta, Vector3.up);
        ////transform.rotation *= angularDeltaY < 0 ? Quaternion.Inverse(Quaternion.Euler(Vector3.up * angularDeltaY)) : Quaternion.Euler(Vector3.up * angularDeltaY);

    }

    private float GetSignedAngularDelta(float rotation)
    {
        return (rotation > 180) ? rotation - 360 : rotation ;
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
