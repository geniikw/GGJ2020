using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkHub : MonoBehaviourPunCallbacks
{
    public string gameversion = "1";
    public InputField nick;
    public InputField roomNumber;
    public Text status;

    private bool isJoin = false;

    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
        
        if (isJoin)
        {
            status.text = "방으로 들어갑니다";
        status.color = Color.white;

            PhotonNetwork.JoinRoom(roomNumber.text);
        }
        else
        {
            status.text = "방을 만듭니다.";
        status.color = Color.white;

            PhotonNetwork.CreateRoom(Random.Range(1000, 10000).ToString(), new RoomOptions()
            {
                MaxPlayers = 4
            });
        }


    }

    public override void OnJoinedLobby()
    {
        status.text = "로비에 들어왔습니다.";
        status.color = Color.white;
        PhotonNetwork.JoinRoom(roomNumber.text);
    }

    public override void OnJoinedRoom()
    {
        status.text = PhotonNetwork.CurrentRoom.Name + "번 방으로 들어갑니다.";
        status.color = Color.white;

        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
    }

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        //PhotonNetwork.autoJoinLobby  = true;
        nick.text = "jammer" + Random.Range(1000, 10000);
    }

    public void MakeRoom()
    {
        PhotonNetwork.NickName = nick.text;
        isJoin = false;
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {

            PhotonNetwork.GameVersion = gameversion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void JoinRoom()
    {
        if (string.IsNullOrEmpty(roomNumber.text))
        {
            status.text = "방번호를 입력해야 합니다.";
            status.color = Color.red;
            return;
        }


        isJoin = true;
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.NickName = nick.text;
            PhotonNetwork.JoinRoom(roomNumber.text);
        }
        else
        {
            PhotonNetwork.GameVersion = gameversion;
            PhotonNetwork.ConnectUsingSettings();
        }


    }

    public void EnterScene()
    {

    }

}
