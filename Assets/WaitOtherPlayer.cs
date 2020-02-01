using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class WaitOtherPlayer : MonoBehaviour,IOnEventCallback
{
    public static WaitOtherPlayer i;

    public List<Image> m_waitList;

    public void OnEvent(EventData photonEvent)
    {
        
    }

    private void Awake() {
        i = this;
    }

    private void OnEnable() {
        
    }



}
