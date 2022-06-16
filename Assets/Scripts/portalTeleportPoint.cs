using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;



public class portalTeleportPoint : MonoBehaviour
{
    //Scene the portal will take you to
    string sceneToGo;

    //Astrobuddy object
    private GameObject playerObj;

    //Timers
    private float teleporterEventCounter;
    public float teleporterEventDuration;

    
    void Start()
    {
        sceneToGo = "Prison";
    }

    
    void Update()
    {

        teleporterEvent();
        teleportPlayer();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
         
            playerObj = collision.gameObject;
       
        }

    }

    private void teleportPlayer()
    {
        if (playerObj != null)
        {
            teleporterEventCounter += Time.deltaTime;

            if (teleporterEventDuration <= teleporterEventCounter)
            {
                SceneManager.LoadScene(sceneToGo);
            }
        }
       

    }


    // Some effects that will happen during the teleport event to make it look better
    private void teleporterEvent()
    {
        // If the player has entered the portal, certain actions will be taken
        if(playerObj != null)
        {
            
            playerObj.GetComponent<playerMovement>().canMove = false;
            playerObj.GetComponent<playerAnimations>().enabled = false;

            playerObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(-5f, -5f));
            playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

                
            

        }


    }


    
    
}
