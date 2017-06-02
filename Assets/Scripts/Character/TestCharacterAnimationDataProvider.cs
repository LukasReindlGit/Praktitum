using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimationManagerConfiguration))]
public class TestCharacterAnimationDataProvider : MonoBehaviour, ICharacterAnimationDataProvider
{

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public CharacterAnimationManagerConfiguration GetCurrentCharacterAnimationManagerConfiguration()
    {
        return this.GetComponent<CharacterAnimationManagerConfiguration>();
    }


    public Vector4 MovementVectorUpdate() {
        var tempVector = new Vector3(Input.GetAxis("Horizontal2"), 0, Input.GetAxis("Vertical2"));
        Debug.Log("Movement: " + new Vector4(tempVector.x, tempVector.y, tempVector.z, 0));
        return new Vector4(tempVector.x, tempVector.y, tempVector.z, 0);
    }

    public Vector4 AttentionVectorVectorUpdate()
    {
        var tempVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        Debug.Log("Attention: " + new Vector4(tempVector.x, tempVector.y, tempVector.z, 0));
        return new Vector4(tempVector.x, tempVector.y, tempVector.z, 0);
    }

    public Vector4 LookVectorUpdate()
    {

        return default(Vector4);
    }

    public Vector4 AimVectorUpdate()
    {

        return default(Vector4);
    }


}
