using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomNetwork : MonoBehaviour, IConnectionCallbacks
{
    public void OnConnected()
    {
        Debug.Log("Connectted");
    }

    public void OnConnectedToMaster()
    {
        throw new System.NotImplementedException();
    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {
        throw new System.NotImplementedException();
    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
        throw new System.NotImplementedException();
    }

    public void OnDisconnected(DisconnectCause cause)
    {
        throw new System.NotImplementedException();
    }

    public void OnRegionListReceived(RegionHandler regionHandler)
    {
        throw new System.NotImplementedException();
    }

    private void OnEnable() {
        PhotonNetwork.AddCallbackTarget(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
