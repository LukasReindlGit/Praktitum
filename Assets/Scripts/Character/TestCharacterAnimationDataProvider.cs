using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimationDirectionConfiguration))]
public class TestCharacterAnimationDataProvider : MonoBehaviour, ICharacterAnimationDataProvider
{

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public CharacterAnimationDirectionConfiguration GetCurrentCharacterAnimationManagerConfiguration()
    {
        return this.GetComponent<CharacterAnimationDirectionConfiguration>();
    }


    public Vector4 MovementVectorUpdate() {
        var tempVector = new Vector3(Input.GetAxis("Horizontal2"), 0, Input.GetAxis("Vertical2"));
        //Debug.Log("Movement: " + new Vector4(tempVector.x, tempVector.y, tempVector.z, 0));
        return new Vector4(tempVector.x, tempVector.y, tempVector.z, 0);
    }

    public Vector4 AttentionVectorVectorUpdate()
    {
        var tempVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        //Debug.Log("Attention: " + new Vector4(tempVector.x, tempVector.y, tempVector.z, 0));

        if (Input.GetKey(KeyCode.F1))
        {
            tempVector = new Vector3(0, 0, 1);
        }

        if (Input.GetKey(KeyCode.F2))
        {
            tempVector = new Vector3(0, 0, 1);
            tempVector = Quaternion.AngleAxis(60, Vector3.up) * tempVector;
        }

        if (Input.GetKey(KeyCode.F3))
        {
            tempVector = new Vector3(0, 0, 1);
            tempVector = Quaternion.AngleAxis(90, Vector3.up) * tempVector;
        }

        if (Input.GetKey(KeyCode.F4))
        {
            tempVector = new Vector3(0, 0, 1);
            tempVector = Quaternion.AngleAxis(110, Vector3.up) * tempVector;
        }

        if (Input.GetKey(KeyCode.F5))
        {
            tempVector = new Vector3(0, 0, 1);
            tempVector = Quaternion.AngleAxis(150, Vector3.up) * tempVector;
        }

        if (Input.GetKey(KeyCode.F6))
        {
            tempVector = new Vector3(0, 0, 1);
            tempVector = Quaternion.AngleAxis(179, Vector3.up) * tempVector;
        }

        return new Vector4(tempVector.x, tempVector.y, tempVector.z, 0);
    }

    public Vector4 LookVectorUpdate()
    {

        //return new Vector4(0,0,1,0);
        return default(Vector4);
    }

    public Vector4 AimVectorUpdate()
    {

        return default(Vector4);
    }


}
