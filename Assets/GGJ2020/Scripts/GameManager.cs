﻿using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<Player> playerList;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public static void StartGame(List<Player> players){
        instance.playerList = players;
    }
}
