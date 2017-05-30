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
        Debug.Log("Default: " + default(Vector4));
	}

    public CharacterAnimationManagerConfiguration GetCurrentCharacterAnimationManagerConfiguration()
    {
        return this.GetComponent<CharacterAnimationManagerConfiguration>();
    }


    public Vector4 MovementVectorUpdate() {

        return default(Vector4);
    }

    public Vector4 AttentionVectorVectorUpdate()
    {

        return default(Vector4);
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
