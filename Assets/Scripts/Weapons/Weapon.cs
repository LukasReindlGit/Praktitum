using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject{
    
    /// <summary>
    /// Defines how oponents are targeted.
    /// </summary>
    public TargetingSystem usedTargetingSystem;

    public float criticalChance = 0.1f;
    public float streuwert = 0;
    

    /// <summary>
    /// Called from outside. Tests if weapon can fire.
    /// </summary>
    public abstract void TryShoot(Vector3 direction);

    /// <summary>
    /// Creates the gameObjects visible to the player.
    /// Call this when switched to this weapon.
    /// </summary>
    public abstract void Spawn();

    public virtual void Update()
    {
        usedTargetingSystem.UpdateTargetGUI(Vector3.zero);
    }
}
