using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class CharacterAnimationDirection
{
     public enum Type { Movement, Attention, Look, Aim, Body };
    public enum State { normal, fallback }


    private CharacterAnimationManager CAM;
    private Type type;
    private State state;

    private Vector4 m_currentDirection = default(Vector4);
    /// <summary>
    /// Current direction for the Animation Controller.
    /// </summary>
    public Vector4 currentDirection
{
    get
    {
        return m_currentDirection;
    }
    set
    {
        m_currentDirection = GetDiretionFromCenter(value);
    }
}

    private Vector4 m_targetDirection = default(Vector4);
    /// <summary>
    /// Target direction of the current direction. 
    /// </summary>
    public Vector4 targetDirection
        {
            get
            {
                return m_targetDirection;
            }
            set
            {
                m_targetDirection = GetDiretionFromCenter(value);
            }
        }

    private Vector4 m_fetchedDirection = default(Vector4);
    /// <summary>
    /// Fetched input direction.
    /// </summary>
    public Vector4 fetchedDirection
        {
            get
            {
                return m_fetchedDirection;
            }
            set
            {
                m_fetchedDirection = GetDiretionFromCenter(value);
            }
        }

    private Vector4 m_CandidateDirection = default(Vector4);
    /// <summary>
    /// Direction candidate for new target direction. (is fetched input from current frame or fallback direction)
    /// </summary>
    public Vector4 candidateDirection
    {
        get
        {
            return m_CandidateDirection;
        }
        set
        {
            m_CandidateDirection = GetDiretionFromCenter(value);
        }
    }


    private CharacterAnimationDirectionBehaviour currentBehaviour;

    public CharacterAnimationDirectionConfiguration configuration;


    public CharacterAnimationDirection(CharacterAnimationManager CAM, Type type)
    {
        this.CAM = CAM;
        this.type = type;
        switch (type)
        {
            case Type.Movement:
                this.configuration = CAM.configuration.movementDirectionConfiguration;
                break;
            case Type.Attention:
                this.configuration = CAM.configuration.attentionDirectionrConfiguration;

                break;
            case Type.Look:
                this.configuration = CAM.configuration.lookDirectionConfiguration;

                break;
            case Type.Aim:
                this.configuration = CAM.configuration.aimDirectionConfiguration;

                break;
            case Type.Body:
                this.configuration = CAM.configuration.bodyDirectionConfiguration;

                break;

        }
    }


    public void CalculateCandidate()
    {
        // Update the current vector
        if (m_fetchedDirection != default(Vector4))
        {
            // Normal
            candidateDirection = fetchedDirection;
            state = State.normal;
        }
        else if (configuration.useFallbackDirection)
        {
            if (CAM.GetAnimationDirectionCurrentDirection(configuration.fallbackVector) != default(Vector4))
            {

                // Fallback
                candidateDirection = CAM.GetAnimationDirectionCurrentDirection(configuration.fallbackVector);
                state = State.fallback;

            }
            else
            {
                // Nothing
                state = State.normal;

            }
        }else
        {
            candidateDirection = fetchedDirection;
            state = State.normal;
        }
       
    }

    public void CalculateTarget()
    {
        List<CharacterAnimationDirectionBehaviour> tempBehaviour;
        if (state == State.normal)
        {
            tempBehaviour = configuration.normalVectorBehaviour;
        }
        else
        {
            tempBehaviour = configuration.fallbackVectorBehaviour;
        }
        if (tempBehaviour == null)
        {
            if (type == Type.Body) Debug.Log("Use default config settings");
            tempBehaviour = new List<CharacterAnimationDirectionBehaviour>();
            tempBehaviour.Add(new CharacterAnimationDirectionBehaviour());

        }
        // fetch target --> call get on filter mit State
        for (int i = 0; i < tempBehaviour.Count; i++)
        {
            // Get filtered target
            var filteredTarget = default(Vector3);
            if (tempBehaviour[i].filter != CharacterAnimationDirectionFilter.Filter.none)
            {
                filteredTarget = CAM.filters[tempBehaviour[i]].GetFilteredDirection();

            }
            else
            {
                filteredTarget = candidateDirection;
            }


            // Check filtered target with condition
            switch (tempBehaviour[i].condition)
            {
                case CharacterAnimationDirectionBehaviour.Condition.angularDeltaGreaterThan:

                    if (Mathf.Abs(GetSignedAngle(targetDirection, filteredTarget)) > tempBehaviour[i].conditionValue)
                    {
                        // TransitionStep(tempBehaviour[i]);
                        targetDirection = candidateDirection;
                        currentBehaviour = tempBehaviour[i];
                    }

                    break;

                case CharacterAnimationDirectionBehaviour.Condition.angularDeltaLessThan:

                    if (Mathf.Abs(GetSignedAngle(targetDirection, filteredTarget)) < tempBehaviour[i].conditionValue)
                    {
                        // TransitionStep(tempBehaviour[i]);
                        targetDirection = candidateDirection;
                        currentBehaviour = tempBehaviour[i];
                    }

                    break;

                default:
                case CharacterAnimationDirectionBehaviour.Condition.unconditionally:

                    // TransitionStep(tempBehaviour[i]);
                    targetDirection = candidateDirection;
                    currentBehaviour = tempBehaviour[i];
                    break;
            }

        }


    }


    public void UpdateCurrentVector()
    {

        TransitionStep(currentBehaviour);

        // Transition
        // --> Wende erste Behaviour an, welches passt

    }

    private void TransitionStep(CharacterAnimationDirectionBehaviour tempBehaviour)
    {
        if(tempBehaviour.transition == CharacterAnimationDirectionBehaviour.Transition.byFunction)
        {
            if (type == Type.Body)
            {
                Debug.Log("t: " + (Time.deltaTime * tempBehaviour.transitionSpeed) + " / b: " + currentDirection.x + " / c: " + (targetDirection.x - currentDirection.x) + " / d: " + ((targetDirection.x - currentDirection.x) / (Time.deltaTime * tempBehaviour.transitionSpeed)));
            }
            tempBehaviour.transitionFunction.type = tempBehaviour.transitionType;

            //tempBehaviour.transitionSpeed = tempBehaviour.transitionSpeed;
            //currentDirection = new Vector4(
            //    Mathf.Lerp(currentDirection.x, targetDirection.x, tempBehaviour.transitionSpeed * Time.deltaTime),
            //    Mathf.Lerp(currentDirection.y, targetDirection.y, tempBehaviour.transitionSpeed * Time.deltaTime), 
            //    Mathf.Lerp(currentDirection.z, targetDirection.z, tempBehaviour.transitionSpeed * Time.deltaTime),
            //    0);

            currentDirection = new Vector4(
                tempBehaviour.transitionFunction.calc(0, tempBehaviour.transitionSpeed * Time.deltaTime, currentDirection.x, targetDirection.x - currentDirection.x, 1),
                tempBehaviour.transitionFunction.calc(0, tempBehaviour.transitionSpeed * Time.deltaTime, currentDirection.y, targetDirection.y - currentDirection.y, 1),
                tempBehaviour.transitionFunction.calc(0, tempBehaviour.transitionSpeed * Time.deltaTime, currentDirection.z, targetDirection.z - currentDirection.z, 1),
                0);

            //currentDirection = new Vector4(
            //    tempBehaviour.transitionFunction.calc(0, Mathf.Abs(targetDirection.x - currentDirection.x),0, targetDirection.x - currentDirection.x,180),
            //    tempBehaviour.transitionFunction.calc(0, Mathf.Abs(targetDirection.y - currentDirection.y), 0, targetDirection.y - currentDirection.y, 180),
            //    tempBehaviour.transitionFunction.calc(0, Mathf.Abs(targetDirection.z - currentDirection.z), 0, targetDirection.z - currentDirection.z, 180),
            //    0);
            if (type == Type.Body)
            {
                Debug.Log("Current: " + currentDirection);
            }

        }
        else
        {
            currentDirection = targetDirection;
        }

    }

    private void ApplyCurrentVector()
    {

    }

    /// <summary>
    /// Returns a normalized direction vector from the transform center towards vector. Calculates the direction if vector is a point.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    private Vector4 GetDiretionFromCenter(Vector4 vector)
        {
            // is default
            if (vector == default(Vector4))
            {
                return default(Vector4);
            }

            // is direction already
            if (vector.w == 0)
            {
                return vector.normalized;
            }

            // is point
            vector /= vector.w;
            return (CAM.transform.position - (Vector3)vector).normalized;

        }

    protected float GetSignedAngle(Vector3 forwardA, Vector3 forwardB)
    {
        // get a numeric angle for each vector, on the X-Z plane (relative to world forward)
        var angleA = Mathf.Atan2(forwardA.x, forwardA.z) * Mathf.Rad2Deg;
        var angleB = Mathf.Atan2(forwardB.x, forwardB.z) * Mathf.Rad2Deg;

        // get the signed difference in these angles

        return Mathf.DeltaAngle(angleA, angleB);
    }

}

