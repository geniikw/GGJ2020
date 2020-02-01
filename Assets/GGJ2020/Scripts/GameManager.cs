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

    public void SetInven(List<int> inven, int idx)
    {
        itemList[idx] = inven;
    }

    void Start()
    {
        i = this;
        
    }

    public static void InitPlayerData(List<Player> players)
    {
        i.playerList = players;
        for (int n = 0; n < players.Count; n++)
        {
            var list = new List<int>(15);
            list.AddRange(Enumerable.Repeat<int>(0, 15));
            i.itemList.Add(list);
        }
        for (int n = 0; n < players.Count; n++)
        {
            i.playerCost.Add(GameData.ins.initialCost);
        }
        
        
    }

    public static void StartItemSetStep()
    {
        Clear();
        i.waitOther.SetActive(true);
        if (!MonoPlayer.i._isMotherShip)
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
