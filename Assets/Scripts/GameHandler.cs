﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DefaultDelegate();

public class GameHandler : MonoBehaviour
{

    public static event DefaultDelegate UpdateEvent;

    public static GameHandler instance;

    public static List <Player> players = new List<Player>();

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Calls Update in Classes not inheriting from monobehaviour
        if(UpdateEvent!=null)
        {
            UpdateEvent.Invoke();
        }

    }
}
