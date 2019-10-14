using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;

    public Text healthDisplay;
    public Text manaDisplay;

    public int nextPlayerTeam;
    public Transform[] spawnPointsTeamOne;
    public Transform[] spawnPointsTeamTwo;


    private void OnEnable()
    {
        
        if(GameSetup.GS == null)
        {
            GameSetup.GS = this;
        }
    }

    public void DisconnectPlayer()
    {
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        SceneManager.LoadScene(MultiPlayerSettings.multiPlayerSettings.menuScene);
    }

    public void TeamUpdate()
    {
        if(nextPlayerTeam == 1)
        {
            nextPlayerTeam = 2;
        }
        else
        {
            nextPlayerTeam = 1; 
        }
    }
}
