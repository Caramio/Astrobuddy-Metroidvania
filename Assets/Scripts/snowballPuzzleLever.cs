using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowballPuzzleLever : MonoBehaviour
{

    private bool playerInRange;

    public GameObject doorToOpenOne, doorToOpenTwo;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (playerInRange)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {

                this.transform.Rotate(0, 180, 0);

                
                doorToOpenOne.SetActive(!doorToOpenOne.activeInHierarchy);
                if (doorToOpenTwo != null)
                {
                    doorToOpenTwo.SetActive(!doorToOpenTwo.activeInHierarchy);
                }


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
