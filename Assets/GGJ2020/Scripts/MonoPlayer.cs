using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
/// <summary>
/// 내정보만 저장
/// </summary>
public class MonoPlayer : MonoBehaviour
{
    public static MonoPlayer i;

    public int actorNumber;
    public int _myIdx;
    public bool _isMotherShip = true;

    private void Awake() {
        i = this;
    }

    public void Setup(bool isMotherShip, int an, int myIdx){
        _isMotherShip = isMotherShip;
        this.actorNumber = an;
        this._myIdx = myIdx;
    }

}
