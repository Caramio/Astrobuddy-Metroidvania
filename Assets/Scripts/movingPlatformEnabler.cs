using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatformEnabler : MonoBehaviour
{
    
    public GameObject movingWall;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && playerInRange == true)
        {

            movingWall.GetComponent<moveToPoints>().enabled = true;

        }
        
    }

    private bool playerInRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {

            playerInRange = true;

        }
    }

    private void onTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {

            playerInRange = false;

        }
    }
}
