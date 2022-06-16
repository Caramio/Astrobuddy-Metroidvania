using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lakePuzzleNoteOpener : MonoBehaviour
{

    private bool playerInRange;


    public GameObject openedNoteCanvas;

    public GameObject openedIndicator;

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
                openedNoteCanvas.SetActive(!openedNoteCanvas.activeInHierarchy);
            }

            openedIndicator.SetActive(true);

        }

        if(playerInRange == false)
        {

            openedNoteCanvas.SetActive(false);
            openedIndicator.SetActive(false);

        }

    }


    private void OnTriggerStay2D(Collider2D collision)
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
