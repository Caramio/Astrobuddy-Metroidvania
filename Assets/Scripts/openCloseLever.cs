using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openCloseLever : MonoBehaviour
{
    public GameObject objectToOpen;

    private bool playerInRange;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(playerInRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.transform.Rotate(0, 180, 0);
                objectToOpen.SetActive(!objectToOpen.activeInHierarchy);
            }
        }

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
