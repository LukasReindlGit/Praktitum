using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class TargetingSystem {

    public abstract void UpdateTargetGUI(Vector3 direction);

    public abstract Target[] GetTargets(Vector3 direction, float criticalChance, float streuwert, int shotcount, float precision);
    
}
