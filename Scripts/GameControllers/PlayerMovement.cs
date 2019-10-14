using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
     

    private Camera myCamera;
    private PhotonView PV;
    private CharacterController myCC;
    private bool mRunning = false; //it's for animations
    private NavMeshAgent mNavMeshAgent;
    

    // Start is called before the first frame update
    void Start()
    {
        //myCamera = GameObject.FindWithTag("MainCamera");
        myCamera = Camera.current;
        PV = GetComponent<PhotonView>();
        myCC = GetComponent<CharacterController>();
        mNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            Movement();
        }
    }

    private void Movement()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;
        
        if (Input.GetMouseButton(0))
        {

            if (Physics.Raycast(ray, out hit, 1000))
            {
                mNavMeshAgent.destination = hit.point;
                
            }
            if (mNavMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                transform.rotation = Quaternion.LookRotation(mNavMeshAgent.velocity.normalized);
            }
        }
        if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
        {
            mRunning = false;
        }
        else
        {
            mRunning = true;
        }
    }


    private void RotateCharacter(float angle)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        Debug.Log(Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg);
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
