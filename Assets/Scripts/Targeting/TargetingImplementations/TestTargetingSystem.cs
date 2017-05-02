using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTargetingSystem : TargetingSystem
{
    public override Target[] GetTargets(Vector3 direction, float criticalChance, float streuwert, int shotcount, float precision)
    {
        throw new NotImplementedException();
    }

    public override void UpdateTargetGUI()
    {
        throw new NotImplementedException();
    }
    
}
