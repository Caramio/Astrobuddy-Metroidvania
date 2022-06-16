using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickableGemHolder : MonoBehaviour
{

    //reference to player at the start
    private GameObject playerObj;

    //public references
    public GameObject eIndicator;
    

    //bool to check if player is in range
    private bool playerInRange;

    void Start()
    {
        playerObj = GameObject.Find("Astrobuddy");
    }

    // Update is called once per frame
    void Update()
    {

        if (playerInRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //enable the text to inform the player
                this.GetComponent<showTextOnCommand>().enabled = true;


                playerObj.GetComponent<astroInventory>().gemHolder.SetActive(true);

                this.gameObject.SetActive(false);
            }

        }

    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {
            playerInRange = true;
            eIndicator.SetActive(true);

        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "PlayerHitbox")
        {
            playerInRange = false;
            eIndicator.SetActive(false);

        }

    }
}
