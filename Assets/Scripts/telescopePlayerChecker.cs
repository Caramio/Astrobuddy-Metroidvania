using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class telescopePlayerChecker : MonoBehaviour
{
    private bool playerInRange;

    

    public GameObject cameraHolder;
    public Camera sceneCamera;

    public GameObject cameraFocusPoint;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                sceneCamera.GetComponent<cameraFollow>().enabled = !sceneCamera.GetComponent<cameraFollow>().isActiveAndEnabled;
                sceneCamera.orthographicSize = 6.5f;
                cameraHolder.transform.position = cameraFocusPoint.transform.position;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {

            playerInRange = true;

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {

            playerInRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {
            sceneCamera.GetComponent<cameraFollow>().enabled = true;
            playerInRange = false;

        }
    }
}
