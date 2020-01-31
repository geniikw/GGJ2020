using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomGuide : MonoBehaviourPunCallbacks
{
    public List<Player> m_playerList = new List<Player>();
    public Text userCount;
    public Text roomNumber;

    public Button playButton;


    public void Start()
    {
        roomNumber.text = PhotonNetwork.CurrentRoom.Name;
        m_playerList.Clear();
        m_playerList.AddRange(PhotonNetwork.CurrentRoom.Players.Values);
        userCount.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        playButton.interactable = PhotonNetwork.CurrentRoom.PlayerCount > 1;

        this.Move(Vector3.zero, 1f);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        m_playerList.Add(newPlayer);
        userCount.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        playButton.interactable = PhotonNetwork.CurrentRoom.PlayerCount > 1;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        m_playerList.Remove(otherPlayer);
        userCount.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        playButton.interactable = PhotonNetwork.CurrentRoom.PlayerCount > 1;
    }

    public void OnPlay(){
        this.Move(Vector3.right * 2000f, 1f);
        GameManager.StartGame();
    }

}
