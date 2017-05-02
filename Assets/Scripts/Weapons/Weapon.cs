using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject{
    
    /// <summary>
    /// Defines how oponents are targeted.
    /// </summary>
    public TargetingSystem usedTargetingSystem;

    /// <summary>
    /// Called from outside. Tests if weapon can fire.
    /// </summary>
    public abstract void TryShoot();

    /// <summary>
    /// Creates the gameObjects visible to the player.
    /// Call this when switched to this weapon.
    /// </summary>
    public abstract void Spawn();

    public virtual void OnEnable()
    {
        GameHandler.UpdateEvent += Update;
    }

    public virtual void OnDisable()
    {
        GameHandler.UpdateEvent -= Update;
    }

    protected virtual void Update()
    {
        usedTargetingSystem.UpdateTargetGUI();
    }
}
