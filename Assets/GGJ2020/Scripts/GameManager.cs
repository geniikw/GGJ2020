﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public static void StartGame(){
        
    }


}
