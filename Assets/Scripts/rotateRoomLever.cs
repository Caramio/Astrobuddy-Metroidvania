using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateRoomLever : MonoBehaviour
{
    public GameObject roomHolder;

    public GameObject middlePoint;


    private bool startedRoomRotateRoutine;

    private float rotateRoomCounter;
    private float rotateRoomTimer = 2f;


    private bool playerInRange;

    private GameObject playerObj;

    void Start()
    {
        playerObj = GameObject.Find("Astrobuddy");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(playerInRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {


                if(startedRoomRotateRoutine == false)
                {
                    this.transform.Rotate(0, 180, 0);

                    StartCoroutine(rotateRoom());
                }

            }
        }

    }


    private IEnumerator rotateRoom()
    {

        playerObj.GetComponent<playerMovement>().enabled = false;
        playerObj.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        startedRoomRotateRoutine = true;

        while (rotateRoomCounter <= rotateRoomTimer)
        {

            roomHolder.transform.RotateAround(middlePoint.transform.position, Vector3.forward, 45f * Time.deltaTime);

            rotateRoomCounter += Time.deltaTime;
            yield return null;
        }

        playerObj.GetComponent<playerMovement>().enabled = true;

        rotateRoomCounter = 0f;

        startedRoomRotateRoutine = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {
            playerInRange = false;
        }
    }
}
