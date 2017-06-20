using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class RangeDetector : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [SerializeField]
    MonoBehaviour activateableTarget;

    [SerializeField]
    bool onlyOnce = false;
    bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (onlyOnce && activated)
        {
            return;
        }

        if (other.gameObject == target)
        {
            activateableTarget.GetComponent<IActivateable>().Activate();
            activated = true;
        }
    }
}
