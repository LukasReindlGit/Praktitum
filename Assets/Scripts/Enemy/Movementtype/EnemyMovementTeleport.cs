using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementTeleport : Movementtype {

    [SerializeField]
    protected float distance; //Distance from player

    [SerializeField]
    protected float teleportCD; //teleportcooldown

	//summary:
    // get random point in radius "distance" from player
    //teleport there after cooldown "teleportCD"
    //enable attack
    //when player runs out of atack range -> conditon
    //teleport again
}
