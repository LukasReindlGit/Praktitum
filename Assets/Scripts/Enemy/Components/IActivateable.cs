using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.Component
{
    public enum ActivateableState { NONE };

    public interface IActivateable
    {

        void Activate(ActivateableState state = ActivateableState.NONE);


    }
}