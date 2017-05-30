using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterAnimationVectorConfiguration : System.Object{

    public enum VectorType { Movement, Attention, Look, Aim, Torso };

    public enum VectorBehaviour { Custom, AlwaysFollow, SmoothFollow, OffsetFollow };

    public VectorBehaviour vectorBehaviour;

    public float vectorRotationTransitionSpeed;
    public TransitionFunction.type vectorRotationTransitionFunctionType;

    public VectorType followVector;


    public int amount = 0;
    public int unit = 0;

    public string name = "bla";

}
