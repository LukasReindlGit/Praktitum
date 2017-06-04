using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for overall input for a character animation manager
/// </summary>
interface ICharacterAnimationDataProvider {

    /// <summary>
    /// Provide a reference to the CharacterAnimationManagerConfiguration for this character.
    /// </summary>
    /// <returns></returns>
    CharacterAnimationDirectionConfiguration GetCurrentCharacterAnimationManagerConfiguration();

    /// <summary>
    /// Provide the calculation to return the current movement vector in 3D space as direction in world space (w==0) or point in world space (w==1).
    /// </summary>
    /// <returns>Current movement vector in 3D space as direction in world space (w==0) or point in world space (w==1)</returns>
    Vector4 MovementVectorUpdate();


    /// <summary>
    /// Provide the calculation to return the current attention vector in 3D space as direction in world space (w==0) or point in world space (w==1).
    /// </summary>
    /// <returns>Current attention vector in 3D space as direction in world space (w==0) or point in world space (w==1)</returns>
    Vector4 AttentionVectorVectorUpdate();

    /// <summary>
    /// Provide the calculation to return the current look vector in 3D space as direction in world space (w==0) or point in world space (w==1).
    /// </summary>
    /// <returns>Current Look vector in 3D space as direction in world space (w==0) or point in world space (w==1)</returns>
    Vector4 LookVectorUpdate();

    /// <summary>
    /// Provide the calculation to return the current aim vector in 3D space as direction in world space (w==0) or point in world space (w==1).
    /// </summary>
    /// <returns>Current aim vector in 3D space as direction in world space (w==0) or point in world space (w==1)</returns>
    Vector4 AimVectorUpdate();

    // ToDo: Add update utility for IK-Transforms for each joint
    // void IKActiveTargetsUpdate();
}
