using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementTeleport : Movementtype {

    [SerializeField]
    protected float distance = 8; //Distance from player

    [SerializeField]
    protected float teleportCD = 3; //teleportcooldown

    protected Vector3 newPos;

    private void Update()
    {
        if (active)
        {
            teleport();
            Debug.Log("TELEPORTED");
            Activate();
        }
    }

    IEnumerator WaitCD()
    {
        yield return new WaitForSeconds(teleportCD);
    }

    protected void teleport()
    {
        // get random point in radius "distance" from player
        newPos = RandomNavSphere(target.position, distance, NavMesh.AllAreas);
        //start tp animation in destination and origin
        //TODO
        // Wait the defined amount of time
        StartCoroutine(WaitCD());
        //teleport there after cooldown "teleportCD"
        gameObject.transform.position = newPos;
        //enable attack
        //TODO
        //when player runs out of atack range -> conditon
        //teleport again
    }


}
