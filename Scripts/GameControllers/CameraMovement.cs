using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Attach to camera
    private PhotonView PV;
    public Transform playerPosition;
    private int mDelta = 10; // Pixels. The width border at the edge in which the movement work
    private float mSpeed = 12.0f; // Scale. Speed of the movement

    private Vector3 mRightDirection = Vector3.right; // Direction the camera should move when on the right edge
    private Vector3 mLeftDirection = Vector3.left; // Direction the camera should move when on the left edge
    private Vector3 mTopDirection = Vector3.forward; // Direction the camera should move when on the top edge
    private Vector3 mBottomDirection = Vector3.back; // Direction the camera should move when on the bottom edge

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            gameObject.GetComponent<Camera>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Camera>().enabled = false;
        }
        
    }

    private void Update()
    {
        if (PV.IsMine)
        {
            if (ScreenRect())
            {
                MovingCamera();
                FocusOnPlayer();
            }
        }
    }

    private void MovingCamera()
    {
        // Check if on the right edge
        if (Input.mousePosition.x >= Screen.width - mDelta )
        {
            // Move the camera
            transform.position += mRightDirection * Time.deltaTime * mSpeed;
            Debug.Log("Moving Camera");
        }
        // Check if on the left edge
        if (Input.mousePosition.x <= mDelta)
        {
            // Move the camera
            transform.position += mLeftDirection * Time.deltaTime * mSpeed;
        }
        // Check if on the top edge
        if (Input.mousePosition.y >= mDelta)
        {
            // Move the camera
            transform.position += mTopDirection * Time.deltaTime * mSpeed;
        }
        // Check if on the bottom edge
        if (Input.mousePosition.y <= Screen.height - mDelta)
        {
            // Move the camera
            transform.position += mBottomDirection * Time.deltaTime * mSpeed;
        }
        
    }

    private void FocusOnPlayer()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position = new Vector3(playerPosition.position.x, 40, playerPosition.position.z-14);
            Debug.Log(transform.position + " ||" + playerPosition.position);
        }
    }

    public void SetPlayerPosition(Transform target)
    {
        playerPosition = target;
    }

    private bool ScreenRect()
    {
        Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
        if (!screenRect.Contains(Input.mousePosition))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
