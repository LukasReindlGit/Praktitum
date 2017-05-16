using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEventOnKey : MonoBehaviour
{
    [SerializeField]
    KeyCode key;

    public UnityEngine.Events.UnityEvent events;
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            if (events != null) events.Invoke();
        }
    }
}
