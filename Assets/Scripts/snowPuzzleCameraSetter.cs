using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowPuzzleCameraSetter : MonoBehaviour
{

    public Camera sceneCamera;
    public GameObject cameraHolder;

    public Transform cameraMiddleSpot;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "PlayerHitbox")
        {
            sceneCamera.orthographicSize = 60;

            cameraHolder.transform.position = cameraMiddleSpot.position;
            sceneCamera.transform.position = cameraHolder.transform.position;

            sceneCamera.GetComponent<cameraFollow>().enabled = false;


        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {
            sceneCamera.GetComponent<cameraFollow>().enabled = true;
        }

    }



}
