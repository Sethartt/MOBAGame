using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;
    public GameObject battleButton;
    public GameObject cancelButton;

    // Start is called before the first frame update
    private void Awake()
    {
        lobby = this;
    }
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to Master server");
        PhotonNetwork.AutomaticallySyncScene = true;
        battleButton.SetActive(true);
    }
    public void OnBattleButtonClicked()
    {
        Debug.Log("Battle Button was click");
        battleButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to connect to random room but failed. There must be no open room right now");
        
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Trying to create new room");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)MultiPlayerSettings.multiPlayerSettings.maxPlayers };
        PhotonNetwork.CreateRoom("Room" + randomRoomName);
    }



    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room, this room must to already exist");
        CreateRoom();
    }

    public void OnCancelButtonClicked()
    {
        Debug.Log("Cancel was clicked");
        battleButton.SetActive(true);
        cancelButton.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }
    // Update is called once per frame
    void Update()
    {
        
    }   
}
