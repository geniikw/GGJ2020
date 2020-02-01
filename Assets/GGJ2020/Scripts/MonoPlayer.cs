using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MonoPlayer : MonoBehaviour
{
    public static MonoPlayer i; 

    private void Awake() {
        i = this;
    }

    public bool _isMotherShip = true;
    public void Setup(bool isMotherShip, int actorNumber){
        _isMotherShip = true;

    }

}
