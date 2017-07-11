using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementTeleport : Movementtype {

    [SerializeField]
    protected float distance; //Distance from player

    [SerializeField]
    protected float teleportCD; //teleportcooldown

    protected Vector3 newPos;

    private void Update()
    {
        if (active)
        {
            // get random point in radius "distance" from player
            newPos = RandomNavSphere(target.position, distance, NavMesh.AllAreas);
            //start tp animation in destination and origin
            //TODO
            //teleport there after cooldown "teleportCD"

            //enable attack
            //when player runs out of atack range -> conditon
            //teleport again
        }
    }

    
}
