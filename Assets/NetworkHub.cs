using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkHub : MonoBehaviour
{
    public string gameversion = "1";

    
    void Awake(){

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        Connect();
    }


    void Connect(){
        if(PhotonNetwork.IsConnected){
            PhotonNetwork.JoinRandomRoom();
                        Debug.Log("b");
        }
        else{
            Debug.Log("a");
            PhotonNetwork.GameVersion = gameversion;
            PhotonNetwork.ConnectUsingSettings();
        }   
    }

}
