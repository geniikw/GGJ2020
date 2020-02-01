using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class WaitOtherPlayer : MonoBehaviour, IOnEventCallback
{
    public static WaitOtherPlayer i;

    public List<Image> m_waitList;

    public Button startButton;

    private int complete = 0;

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == 6)
        {
            var packet = (object[])photonEvent.CustomData;
            var idx = (int)packet[0];
            var itemList = (List<int>)packet[1];
            m_waitList[idx].color = Color.green;
            GameManager.i.SetInven(itemList, idx);
        }
    }

    private void Awake()
    {
        i = this;
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }


    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }



}
