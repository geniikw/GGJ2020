using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine;
using ExitGames.Client.Photon;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static string myNick;

    public List<Player> playerList;

    public GameObject selector;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public static void StartSetPhoneStep(List<Player> players)
    {
        instance.playerList = players;
        var myname = players.First(p => p.NickName == PhotonNetwork.NickName);
        var idx = players.IndexOf(myname);
        FindObjectOfType<MonoPlayer>().Setup(idx == 0,myname.ActorNumber); 


        SendEvent(1, null);
    }



    public static void SendEvent(byte code, object param)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        SendOptions sendOptions = new SendOptions { Reliability = true };


        PhotonNetwork.RaiseEvent(1, null, raiseEventOptions, sendOptions);
    }
}
