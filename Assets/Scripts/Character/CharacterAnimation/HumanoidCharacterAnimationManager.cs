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
    public float turnAnimationSpeed;
    [Range(0.0f, 1.0f)]
    public float turnAnimationSecondUpdate;

    public float normalMovementTransitionSpeed;
    public float stopMovingTransitionSpeed;

    public float idleRotationErrorSolverSpeed;

    public float movementTurnSpeed;

    private Vector3 currentAnimationDirectionTarget;
    private Quaternion oldRotation = new Quaternion();

    static int moveState = Animator.StringToHash("Grounded Strafe (Move)");
    static int turnLeftState = Animator.StringToHash("TurnLeft");
    static int turnRightState = Animator.StringToHash("TurnRight");
    static int idleState = Animator.StringToHash("Idle");


    private Coroutine m_TurnAnimationCoroutine;
    /// <summary>
    /// Referencing the current turn animation coroutine
    /// </summary>
    public Coroutine turnAnimationCoroutine
    {
        set
        {
            if (turnAnimationCoroutine != null)
            {
                StopCoroutine(m_TurnAnimationCoroutine);
                animator.speed = 1.0f;
            }
            m_TurnAnimationCoroutine = value;
        }

        get
        {
            return m_TurnAnimationCoroutine;
        }
    }

    private enum AnimatorStateInfoRef { current, next, both };

    private enum TurnAnimationDirection { left, right};
    private float realTurnAnimDuration;
    private float remainingTurnAnimDuration;
    private float turnAnimRotationFactor;
    private float turnAnimAnglesPerSecond;
    private float turnAnimAngularDeltaThisFrame;
    private bool turnAnimationFinished;
    private bool turnAnimationStarted;
    private float rawAnimatorRootRotationThisAnimationPeriod;

    private AnimatorStateInfo currentAnimatorBaseState;




new private void Awake()
    {
        base.Awake();
        currentAnimationDirectionTarget = transform.forward;
    }

    protected override void ApplyMovementVector()
    {
        if (directions[CharacterAnimationDirection.Type.Movement].currentDirection == default(Vector4))
        {
            if (animator.GetBool("isMoving"))
            {
                Debug.Log("Forward: " + this.transform.forward);
               
                directions[CharacterAnimationDirection.Type.Movement].targetDirection = default(Vector4);
                directions[CharacterAnimationDirection.Type.Attention].targetDirection = MovementRef.transform.forward;
                directions[CharacterAnimationDirection.Type.Body].targetDirection = MovementRef.transform.forward;

                //directions[CharacterAnimationDirection.Type.Movement].currentDirection = this.transform.forward;
                //directions[CharacterAnimationDirection.Type.Attention].currentDirection = this.transform.forward;
                //directions[CharacterAnimationDirection.Type.Body].currentDirection = this.transform.forward;

                //directions[CharacterAnimationDirection.Type.Movement].fetchedDirection = this.transform.forward;
                //directions[CharacterAnimationDirection.Type.Attention].fetchedDirection = this.transform.forward;

                directions[CharacterAnimationDirection.Type.Movement].initialCurrentToTargetAngularDelta = 0;
                directions[CharacterAnimationDirection.Type.Attention].initialCurrentToTargetAngularDelta = 0;
                directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta = 0;
            }

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


    protected override void ApplyAttentionVector()
    {
       
    }


    protected override void ApplyBodyVector()
    {
        var currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
        
        if (directions[CharacterAnimationDirection.Type.Movement].currentDirection == default(Vector4) )
        {

            // Debug.Log("Body: " + directions[CharacterAnimationDirection.Type.Body].currentDirection);
            var tempRotationAroundY = GetAngleFromReferenceForward(MovementRef.transform, directions[CharacterAnimationDirection.Type.Body].currentDirection);
            // Update Animator
            var tempAngularDelta = tempRotationAroundY;
            tempAngularDelta *= configuration.turnSensitivity * 0.01f;
            tempAngularDelta = Mathf.Clamp(tempAngularDelta / Time.deltaTime, -1f, 1f);
         
            animator.SetFloat("Turn", tempAngularDelta);




            if (currentAnimationDirectionTarget != (Vector3)directions[CharacterAnimationDirection.Type.Body].targetDirection && turnAnimationCoroutine == null) {

                var cross = Vector3.Cross(currentAnimationDirectionTarget, (Vector3)directions[CharacterAnimationDirection.Type.Body].targetDirection);
                var angle = Vector3.Angle(currentAnimationDirectionTarget, (Vector3)directions[CharacterAnimationDirection.Type.Body].targetDirection);
                //Debug.Log("cross crnt anim target: "+ currentAnimationDirectionTarget +" / crnt target: "+ (Vector3)directions[CharacterAnimationDirection.Type.Body].targetDirection);

                if (angle > 3 && cross.y < 0) 
                {
                    turnAnimationCoroutine = StartCoroutine(TurnAnimation(TurnAnimationDirection.left));
                    currentAnimationDirectionTarget = directions[CharacterAnimationDirection.Type.Body].targetDirection;
                }

                if (angle > 3 && cross.y > 0 )
                {
                    turnAnimationCoroutine = StartCoroutine(TurnAnimation(TurnAnimationDirection.right));
                    currentAnimationDirectionTarget = directions[CharacterAnimationDirection.Type.Body].targetDirection;

                }
            }
            //if (directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta < 0 && curretnAnimationDirectionTarget != (Vector3)directions[CharacterAnimationDirection.Type.Body].targetDirection)
            //{
            //    animator.speed = 1.7f;
            //    animator.SetBool("TurnLeft", true);
            //    animator.SetBool("TurnRight", false);
            //    curretnAnimationDirectionTarget = directions[CharacterAnimationDirection.Type.Body].targetDirection;
            //}

            //if (directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta > 0 && curretnAnimationDirectionTarget != (Vector3)directions[CharacterAnimationDirection.Type.Body].targetDirection)
            //{
            //    animator.speed = 1.7f;
            //    animator.SetBool("TurnLeft", false);
            //    animator.SetBool("TurnRight", true);
            //    curretnAnimationDirectionTarget = directions[CharacterAnimationDirection.Type.Body].targetDirection;

            //}

        
            //if (Mathf.Abs(GetAngleFromReferenceForward(MovementRef.transform, directions[CharacterAnimationDirection.Type.Body].targetDirection)) > 2) { 
            //    var tempSpeedFactor = Mathf.Abs(directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta) / turnAnimationAngularDelta;
            //    var tempAmountFactor = Mathf.Abs(directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta) % turnAnimationAngularDelta;

            
            //}


        }
   

        if( currentBaseState.shortNameHash == moveState && directions[CharacterAnimationDirection.Type.Movement].currentDirection != default(Vector4))
        {
            var tempRotationAroundY = GetAngleFromReferenceForward(MovementRef.transform, directions[CharacterAnimationDirection.Type.Body].targetDirection);
           Debug.Log("Rotate while moving:" + (MovementRef.transform.forward) + " / direction: " + directions[CharacterAnimationDirection.Type.Body].targetDirection);
            MovementRef.transform.Rotate(transform.up, tempRotationAroundY * Time.deltaTime * movementTurnSpeed);
            Debug.Log("1.0");

        }else if(currentBaseState.shortNameHash == moveState)
        {
            var rotationYCorrection = GetAngleFromReferenceForward(MovementRef.transform, directions[CharacterAnimationDirection.Type.Body].targetDirection);
            MovementRef.rotation *= Quaternion.Euler(0, GetSignedAngle(rotationYCorrection) * Time.deltaTime * idleRotationErrorSolverSpeed, 0);
            Debug.Log("1.1");
        }


     

    }

    protected override void CalculateBodyVector()
    {
        base.CalculateBodyVector();

        directions[CharacterAnimationDirection.Type.Body].currentDirection = MovementRef.transform.forward;
    }

   
    IEnumerator TurnAnimation(TurnAnimationDirection turnAnimationDirection)
    {
        // Start 
        Debug.Log("Start");

        // Update animator properties
        animator.speed = turnAnimationSpeed;
        animator.SetBool("TurnLeft", turnAnimationDirection == TurnAnimationDirection.left);
        animator.SetBool("TurnRight", turnAnimationDirection == TurnAnimationDirection.right);

        // Init rotation properties
        realTurnAnimDuration = animator.speed * turnAnimationDuration;

        rawAnimatorRootRotationThisAnimationPeriod = 0.0f;

        AnimatorStateInfo relevantAnimatorStateInfo;

        // Wait for animator to detect setbool true 
        // ToDo: or Timeout
        while (!GetRelevantTurnAnimationAnimatorStateInfo(turnAnimationDirection, AnimatorStateInfoRef.both, out relevantAnimatorStateInfo))
        {
            yield return new WaitForEndOfFrame();
        }


        CalculateTurnAnimationRotationProperties();



        var turnPropertiesAreUpdated = false;
        // Update
        while (TurnAnimationIsPalying(turnAnimationDirection, AnimatorStateInfoRef.both))
        {

            GetRelevantTurnAnimationAnimatorStateInfo(turnAnimationDirection, AnimatorStateInfoRef.both, out relevantAnimatorStateInfo);
            Debug.Log("Start2");
            if (TurnAnimationIsPalying(turnAnimationDirection, AnimatorStateInfoRef.current))
            {
                // Reset turn bools
                animator.SetBool("TurnLeft", false);
                animator.SetBool("TurnRight", false);
            }

            if ((relevantAnimatorStateInfo.normalizedTime -  (int)relevantAnimatorStateInfo.normalizedTime) >= turnAnimationSecondUpdate && !turnPropertiesAreUpdated)
            {
                Debug.Log("Update values at: "+relevantAnimatorStateInfo.normalizedTime);
                UpdateTurnAnimationRotationPropertiesForSecondStep();
                turnPropertiesAreUpdated = true;
            }


            // Update rotation properties each frame
            remainingTurnAnimDuration = realTurnAnimDuration * (1 - relevantAnimatorStateInfo.normalizedTime);


            // Rotate the character ( Stretching the Animation rotation )
            Debug.Log("delta rotation this frame: " + GetSignedAngle(animator.deltaRotation.eulerAngles.y) + " / raw all: " + rawAnimatorRootRotationThisAnimationPeriod);
            rawAnimatorRootRotationThisAnimationPeriod += GetSignedAngle( animator.deltaRotation.eulerAngles.y);

            // Apply Anim rotation with factor
            var rotation = new Vector3( GetSignedAngle(GetSignedAngle(animator.deltaRotation.eulerAngles.x) * Mathf.Abs(turnAnimRotationFactor)),
                                        GetSignedAngle(GetSignedAngle(animator.deltaRotation.eulerAngles.y) * Mathf.Abs(turnAnimRotationFactor)),
                                        GetSignedAngle(GetSignedAngle(animator.deltaRotation.eulerAngles.z) * Mathf.Abs(turnAnimRotationFactor)));

            MovementRef.rotation *= Quaternion.Euler(rotation.x, rotation.y, rotation.z);



            // Set correct current body direction
            directions[CharacterAnimationDirection.Type.Body].currentDirection = MovementRef.transform.forward;



            //animator.speed = 1.0f;

            // ToDo break out
            yield return new WaitForEndOfFrame();
        }


        // End
        currentAnimationDirectionTarget = directions[CharacterAnimationDirection.Type.Body].currentDirection;
       // directions[CharacterAnimationDirection.Type.Body].targetDirection = directions[CharacterAnimationDirection.Type.Body].currentDirection;

        // Stops coroutine automatically
        turnAnimationCoroutine = null;
    }

    private bool GetRelevantTurnAnimationAnimatorStateInfo(TurnAnimationDirection turnAnimationDirection, AnimatorStateInfoRef stateInfoRef, out AnimatorStateInfo animatorStateInfoRef)
    {

        var tempID = turnAnimationDirection == TurnAnimationDirection.left ? turnLeftState : turnRightState;
        AnimatorStateInfo tempStateInfo;
        if ((stateInfoRef == AnimatorStateInfoRef.current || stateInfoRef == AnimatorStateInfoRef.both))
        {
            tempStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (tempStateInfo.shortNameHash == tempID)
            {
                animatorStateInfoRef = tempStateInfo;
                return true;

            }
        }
        else
        {
            if ((stateInfoRef == AnimatorStateInfoRef.next || stateInfoRef == AnimatorStateInfoRef.both))
            {
                tempStateInfo = animator.GetNextAnimatorStateInfo(0);
                if (tempStateInfo.shortNameHash == tempID)
                {
                    animatorStateInfoRef = tempStateInfo;
                    return true;
                }
            }

        }
        animatorStateInfoRef = default(AnimatorStateInfo);
        return false;


    }

    private bool TurnAnimationIsPalying(TurnAnimationDirection turnAnimationDirection, AnimatorStateInfoRef stateInfoRef)
    {
        var tempID = ((turnAnimationDirection == TurnAnimationDirection.left) ? (turnLeftState) : (turnRightState));
        AnimatorStateInfo tempStateInfo;
        if ((stateInfoRef == AnimatorStateInfoRef.current || stateInfoRef == AnimatorStateInfoRef.both))
        {
            tempStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (tempStateInfo.shortNameHash == tempID)
            {
                return true;

            }
        }
        else if ((stateInfoRef == AnimatorStateInfoRef.next || stateInfoRef == AnimatorStateInfoRef.both))
        {
            tempStateInfo = animator.GetNextAnimatorStateInfo(0);
            if (tempStateInfo.shortNameHash == tempID)
            {
                return true;
            }


        }

        return false;
    }

    private void CalculateTurnAnimationRotationProperties()
    {
        remainingTurnAnimDuration = realTurnAnimDuration;

        var amount = Vector3.Angle(directions[CharacterAnimationDirection.Type.Body].currentDirection, directions[CharacterAnimationDirection.Type.Body].targetDirection);

        var cross = Vector3.Cross(directions[CharacterAnimationDirection.Type.Body].currentDirection, directions[CharacterAnimationDirection.Type.Body].targetDirection);
        if (cross.y < 0)
        {
            amount = -amount;
        }
        // ToDo: remaining angular delta to body target berechnen? Oder Attention Target?
        turnAnimRotationFactor = (amount / (turnAnimationAngularDelta - Mathf.Abs(rawAnimatorRootRotationThisAnimationPeriod)));
        //turnAnimAnglesPerSecond = (directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta - turnAnimationAngularDelta) / remainingTurnAnimDuration;
        //turnAnimAngularDeltaThisFrame = turnAnimAnglesPerSecond * Time.deltaTime;
    }

    public void UpdateTurnAnimationRotationPropertiesForSecondStep()
    {

        // ToDo: nur eine propertie update method?


        remainingTurnAnimDuration = realTurnAnimDuration;

        // ToDo: Get biggest amount
        float amount;
        Vector3 cross;

        // Body Target as Target
         amount = Vector3.Angle(directions[CharacterAnimationDirection.Type.Body].currentDirection, directions[CharacterAnimationDirection.Type.Body].targetDirection);
         cross = Vector3.Cross(directions[CharacterAnimationDirection.Type.Body].currentDirection, directions[CharacterAnimationDirection.Type.Body].targetDirection);

        // Attention current as Target
        var tempAmount = Vector3.Angle(directions[CharacterAnimationDirection.Type.Body].currentDirection, directions[CharacterAnimationDirection.Type.Attention].currentDirection);
        var tempCross = Vector3.Cross(directions[CharacterAnimationDirection.Type.Body].currentDirection, directions[CharacterAnimationDirection.Type.Attention].currentDirection);

        if(tempAmount > amount)
        {
            amount = tempAmount;
            cross = tempCross;
            directions[CharacterAnimationDirection.Type.Body].targetDirection = directions[CharacterAnimationDirection.Type.Attention].currentDirection;

        }


        if (cross.y < 0)
        {
            amount = -amount;
        }
        // ToDo: remaining angular delta to body target berechnen? Oder Attention Target?
        Debug.Log("Abzug: " + Mathf.Abs(rawAnimatorRootRotationThisAnimationPeriod));
        turnAnimRotationFactor = (amount/ (turnAnimationAngularDelta - Mathf.Abs(rawAnimatorRootRotationThisAnimationPeriod)));
        //turnAnimAnglesPerSecond = (directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta - turnAnimationAngularDelta) / remainingTurnAnimDuration;
        //turnAnimAngularDeltaThisFrame = turnAnimAnglesPerSecond * Time.deltaTime;

        // ToDo: Apply restrictions!
        // ToDo: Handle Turn over with restrictions?
    }


    void OnAnimatorMove()
    {



        // Update position based on animation movement using navigation surface height
        Vector3 position = animator.deltaPosition;
        // position.y = animator.nextPosition.y;
        MovementRef.position += position;



        var alreadyRotated = 0.0f;
        var rotationFactor = 1.0f;


        //      var currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
        //      var nextStateBase = animator.GetNextAnimatorStateInfo(0);
        //      // Debug.Log("currentBaseState "+ currentBaseState.shortNameHash);

        //      var realDurationInSeconds = animator.speed * turnAnimationDuration;

        //      var remainingDurationInSeconds = realDurationInSeconds;

        //      if (currentBaseState.shortNameHash == turnLeftState || currentBaseState.shortNameHash == turnRightState)
        //      {
        //       //   Debug.Log("Do Stuff Here");
        //          remainingDurationInSeconds *= 1 - currentBaseState.normalizedTime;
        //      }

        ////  Debug.Log("initialDelta: " + directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta);
        //  rotationFactor = (directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta / turnAnimationAngularDelta);
        //  var anglePerSeconds = (directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta - turnAnimationAngularDelta)/ remainingDurationInSeconds;
        //      //Debug.Log("crnt AngularDleta on target switch" + directions[CharacterAnimationDirection.Type.Body].initialCurrentToTargetAngularDelta);
        //      var angularDeltaThisFrame = anglePerSeconds * Time.deltaTime;


        // ToDo: Wann correction? if jetzt idle und kein next?
        if(animator.GetCurrentAnimatorStateInfo(0).shortNameHash == idleState)
        {

            // Correction steps:

            var rotationYCorrection = GetAngleFromReferenceForward(MovementRef.transform, directions[CharacterAnimationDirection.Type.Body].targetDirection);
            // If greater: Wait for Aniamtion
            if (rotationYCorrection < 5.0f)
            {
               // MovementRef.rotation *= Quaternion.Euler(0, GetSignedAngle(rotationYCorrection) * Time.deltaTime * idleRotationErrorSolverSpeed, 0);
                //Debug.Log("3");

            }

            //Debug.Log("NOO: " + GetSignedAngle(rotationYCorrection) * Time.deltaTime * idleRotationErrorSolverSpeed);
        }


        //    if (currentBaseState.shortNameHash == turnLeftState ||
        //currentBaseState.shortNameHash == turnRightState ||
        ////(currentBaseState.shortNameHash == idleState && currentBaseState.normalizedTime < 0.3f) ||
        //currentBaseState.shortNameHash == idleState && (nextStateBase.shortNameHash == turnLeftState || nextStateBase.shortNameHash == turnRightState))
        //    {
        //        //Debug.Log("anim rot1 : " + animator.deltaRotation.eulerAngles + " / factor: "+rotationFactor);
        //        // Apply Anim rotationw ith factor
        //        var rotation = new Vector3(
        //            GetSignedAngle(GetSignedAngle(animator.deltaRotation.eulerAngles.x) * Mathf.Abs(rotationFactor)),
        //            GetSignedAngle(GetSignedAngle(animator.deltaRotation.eulerAngles.y) * Mathf.Abs(rotationFactor)),
        //            GetSignedAngle(GetSignedAngle(animator.deltaRotation.eulerAngles.z) * Mathf.Abs(rotationFactor)));


        //        // Debug.Log("rot : " + rotation + " / factor: " + rotationFactor);
        //        // position.y = animator.nextPosition.y;
        //        MovementRef.rotation *= Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        //        Debug.Log("2");




        //    }
        //    else
        //    {

        //        // Correction steps:

        //        var rotationYCorrection = GetAngleFromReferenceForward(MovementRef.transform, directions[CharacterAnimationDirection.Type.Body].targetDirection);
        //        // If greater: Wait for Aniamtion
        //        if (rotationYCorrection < 5.0f)
        //        {
        //            MovementRef.rotation *= Quaternion.Euler(0, GetSignedAngle(rotationYCorrection) * Time.deltaTime * idleRotationErrorSolverSpeed, 0);
        //            Debug.Log("3");

        //        }

        //        //Debug.Log("NOO: " + GetSignedAngle(rotationYCorrection) * Time.deltaTime * idleRotationErrorSolverSpeed);
        //    }

        //    if (currentBaseState.shortNameHash == turnLeftState || currentBaseState.shortNameHash == turnRightState)
        //    {
        //        directions[CharacterAnimationDirection.Type.Body].currentDirection = MovementRef.transform.forward;
        //    }



    }

    private float GetSignedAngle(float rotation)
    {
        rotation = rotation % 360;
        return (rotation > 180) ? rotation - (360 ) : rotation ;
    }

  

    protected override void ApplyAimVector()
    {
        
            AimTarget.position = transform.position + (Vector3)directions[CharacterAnimationDirection.Type.Aim].currentDirection * 100;

    }

  

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
