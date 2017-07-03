using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.Component
{

    public class AIAnimation : MonoBehaviour, IActivateable
    {
        [SerializeField]
        Animator anim;

        public void Activate(ActivateableState state = ActivateableState.NONE)
        {
            //TODO starts an animation
            throw new NotImplementedException();
        }
        
    }
}
