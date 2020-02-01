using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine;
using ExitGames.Client.Photon;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    public static string myNick;

    public List<Player> playerList;

    public GameObject selector;

    public GameObject setItem;

    public GameObject waitOther;


    public List<List<int>> itemList = new List<List<int>>();
    public List<int> playerCost = new List<int>();

    public List<Vector2Int> positionList = new List<Vector2Int>(4);
    public List<bool> verticalList = new List<bool>(4);

    public int GetMyCost()
    {
        return playerCost[MonoPlayer.i._myIdx];
    }

    public List<int> GetInven()
    {
        return itemList[MonoPlayer.i._myIdx];
    }

    void Start()
    {
        i = this;
        ClearInfo();
    }

    public void ClearInfo()
    {
        for (int n = 0; n < itemList.Count; n++){
            itemList.Add(new List<int>(15));
            itemList[n].AddRange(Enumerable.Repeat<int>(0,15));
        }
        for (int n = 0; n < playerCost.Count; n++)
        {
            playerCost.Add(GameData.ins.initialCost);
        }
    }

    public static void StartSetPhoneStep(List<Player> players)
    {
        i.playerList = players;
        SendEvent(PK.SelectStart, null);
    }

    public static void StartItemSetStep()
    {
        Clear();
        if (MonoPlayer.i._isMotherShip)
            i.waitOther.SetActive(true);
        else
            i.setItem.SetActive(true);
    }

    public static void Clear()
    {
        i.waitOther.SetActive(false);
        i.setItem.SetActive(false);
        i.selector.SetActive(false);
    }

    public static void SendEvent(PK code, object param = null)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        SendOptions sendOptions = new SendOptions { Reliability = true };

        PhotonNetwork.RaiseEvent((byte)code, param, raiseEventOptions, sendOptions);
    }


}
