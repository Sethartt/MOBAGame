using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AvatarCombat : MonoBehaviour
{
    private PhotonView PV;
    private AvatarSetup avatarSetup;

    private NavMeshAgent mNavMeshAgent;
    public Transform rayOrigin;
    public Text healthDisplay;
    public Text manaDisplay;
    public Image healthImage;
    public int attackRange = 10;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        avatarSetup = GetComponent<AvatarSetup>();
        healthDisplay = GameSetup.GS.healthDisplay;
        manaDisplay = GameSetup.GS.manaDisplay;
        mNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        if (Input.GetMouseButton(0) )
        {
            PV.RPC("Shooting", RpcTarget.All);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            takeDmg();
        }

        PV.RPC("UpdateHealthBar", RpcTarget.All);
        healthDisplay.text = avatarSetup.playerHealth.ToString();
        manaDisplay.text = avatarSetup.playerMana.ToString();
        
    }

    [PunRPC]
    void Shooting()
    {
        RaycastHit hit;
        //raycast range = infinity
        if (Physics.Raycast(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward), out hit, attackRange))
        {
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * 1000, Color.red);
            Debug.Log("Did hit");
            if (hit.transform.tag == "Avatar")
            {
                hit.transform.gameObject.GetComponent<AvatarSetup>().playerHealth -= avatarSetup.playerDamage;
                mNavMeshAgent.isStopped = true;
            }
        }
        else
        {
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not hit");
        }
    }
    [PunRPC]
    void UpdateHealthBar()
    {
        healthImage.fillAmount = avatarSetup.playerHealth / avatarSetup.maxPlayerHealth;
        
    }
    private void takeDmg()
    {
        gameObject.GetComponent<AvatarSetup>().playerHealth -= avatarSetup.playerDamage;
    }
}
