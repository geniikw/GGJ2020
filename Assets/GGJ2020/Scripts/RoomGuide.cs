using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.Linq;

public class RoomGuide : MonoBehaviourPunCallbacks
{
    public List<Player> m_playerList = new List<Player>();
    public Text userCount;
    public Text roomNumber;

    public Button playButton;

    public List<Text> nickList;


    public void Start()
    {
        UpdateUser();
        this.MoveUI(Vector3.zero, 1f);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdateUser();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateUser();
    }

    public void OnPlay()
    {
        this.MoveUI(Vector3.right * 2000f, 1f);
        GameManager.StartGame(m_playerList);
    }

    public void UpdateUser()
    {
        var n = 0;

        m_playerList.Clear();
        m_playerList.AddRange(PhotonNetwork.CurrentRoom.Players.Values);
        m_playerList = m_playerList.OrderBy(user=>user.ActorNumber).ToList();

        userCount.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        foreach (var user in m_playerList)
            nickList[n++].text = user.NickName + "(" + user.ActorNumber + ")";

        playButton.interactable = PhotonNetwork.CurrentRoom.PlayerCount > 1;
    }

}
