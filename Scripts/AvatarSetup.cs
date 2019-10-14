using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AvatarSetup : MonoBehaviour
{
    private PhotonView PV;

    public int characterValue;
    public GameObject myCharacter;

    public float maxPlayerHealth;
    public float maxPlayerMana;

    public float playerHealth;
    public float playerMana;

    public int playerDamage;

    private GameObject myCam;
    public Camera myCamera;
    public AudioListener myAL;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        
        if (PV.IsMine)
        {
            PV.RPC("RPC_AddCharacter", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedCharacter);
            myCam.GetComponent<CameraMovement>().SetPlayerPosition(gameObject.transform);
        }
        else
        {
            Destroy(myAL); 
        }
    }

    [PunRPC]
    void RPC_AddCharacter(int whichCharacter)
    {
        characterValue = whichCharacter;
        myCharacter = Instantiate(PlayerInfo.PI.allCharacters[whichCharacter], transform.position, transform.rotation, transform);
        myCam = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "MainCamera"), new Vector3(0,40,0), Quaternion.Euler(70,0,0));
    }

}
