using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class TestTargetingSystem : TargetingSystem
{

    public override Target[] GetTargets(Vector3 position, Vector3 direction, Parameters parameters)
    {
        return null;
    }

    public override void OnDestroy()
    {
        
    }

    public override void UpdateTargetSystem(Vector3 position, Vector3 direction)
    {
        //throw new NotImplementedException();
    }
    
}
