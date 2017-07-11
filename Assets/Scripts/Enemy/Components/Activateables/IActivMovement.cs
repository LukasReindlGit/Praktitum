using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.Component
{

    public class IActivMovement : MonoBehaviour, IActivateable
    {
        Movementtype move;
       
        //searches the Movementtype
        void Start()
        {
            move = GetComponent(typeof(Movementtype)) as Movementtype;   
        }

        //wenn activated activates the movementtype
        public void Activate(ActivateableState state = ActivateableState.NONE)
        {
            move.Activate();  
        }
    }
}
