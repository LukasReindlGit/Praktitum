using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Include this to a class to make it hitable by weapons
/// </summary>
public interface IDamageable {

    void DoDamage(float amount);
        
}
