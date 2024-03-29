﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{
    public PhotonView PV;
    public GameObject myAvatar;
    public int myTeam;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            PV.RPC("RPC_GetTeam", RpcTarget.MasterClient);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (myAvatar == null && myTeam != 0)
        {
            if (myTeam == 1)
            {
                int spawnPicker = Random.Range(0, GameSetup.GS.spawnPointsTeamOne.Length);
                if (PV.IsMine)
                {
                    myAvatar =
                    PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"),
                        GameSetup.GS.spawnPointsTeamOne[spawnPicker].position,
                        GameSetup.GS.spawnPointsTeamOne[spawnPicker].rotation, 0);
                }
            }
            else
            {
                int spawnPicker = Random.Range(0, GameSetup.GS.spawnPointsTeamTwo.Length);
                if (PV.IsMine)
                {
                    myAvatar =
                    PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"),
                        GameSetup.GS.spawnPointsTeamTwo[spawnPicker].position,
                        GameSetup.GS.spawnPointsTeamTwo[spawnPicker].rotation, 0);
                }
            }
        }
    }

    [PunRPC]
    void RPC_GetTeam()
    {
        myTeam = GameSetup.GS.nextPlayerTeam;
        GameSetup.GS.TeamUpdate();
        PV.RPC("RPC_SendTeam", RpcTarget.OthersBuffered, myTeam);
    }

    [PunRPC]
    void RPC_SendTeam(int whichTeam)
    {
        myTeam = whichTeam;
    }
}
