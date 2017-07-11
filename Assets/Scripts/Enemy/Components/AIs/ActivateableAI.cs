using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.Component
{
    public class ActivateableAI : MonoBehaviour, IActivateable
    {
        public void Activate(ActivateableState state = ActivateableState.NONE)
        {
            throw new NotImplementedException();
        }
    }
}