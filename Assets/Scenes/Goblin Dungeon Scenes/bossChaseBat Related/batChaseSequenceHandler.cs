using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batChaseSequenceHandler : MonoBehaviour
{
    public GameObject cameraHolderObj;
    public Camera currentCamera;

    public GameObject exitPreventingDoor;

    public GameObject batBoss;


    private bool touchedPlayer;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (touchedPlayer == true)
        {
            cameraHolderObj.transform.position = new Vector3(batBoss.transform.position.x, batBoss.transform.position.y, -5f);
        }


    }

    private void startChaseSequence()
    {

        exitPreventingDoor.SetActive(true);

        batBoss.SetActive(true);

    }

    // Once we enter the trigger point, the camera will follow the bat boss, this needs to be set to false 
    // Once the sequence is done
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {
            touchedPlayer = true;
            currentCamera.GetComponent<cameraFollow>().enabled = false;
            /*
            currentCamera.GetComponent<cameraFollow>().isFollowingOther = true;
            currentCamera.GetComponent<cameraFollow>().followedObject = batBoss.transform;
            */
            
            currentCamera.orthographicSize = 35f;

            

            startChaseSequence();

        }
    }
}


