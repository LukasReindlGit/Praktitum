using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{

    /// <summary>
    /// The one actually controlled
    /// (Puppet master)
    /// </summary>
    public Transform pawn;

    /// <summary>
    /// Puppet
    /// </summary>
    public Transform character;

    public float pawnSpeed = 10;
    public float characterSpeed = 10;
    public float rotationSpeed = 10;

    public float minDist = 0.25f;

    private void LateUpdate()
    {
        MovePawn();
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        // Rotate towards pawn
        //character.LookAt(pawn);
        //character.rotation = Quaternion.Euler(0, character.rotation.eulerAngles.y, 0);
        character.rotation = Quaternion.Slerp(character.rotation, Quaternion.LookRotation(pawn.position - character.position), Time.deltaTime* rotationSpeed);




        float dist = Vector3.Distance(pawn.position, character.position);

        if (dist < minDist)
            return;

        // Move towards pawn:
        Vector3 moveVec = pawn.position - character.position;
        moveVec *= Time.deltaTime;
        moveVec *= characterSpeed;
        character.Translate(moveVec, Space.World);
    }

    private void MovePawn()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        pawn.transform.Translate(new Vector3(x, 0, y) * Time.deltaTime * pawnSpeed, Space.World);
    }
}
