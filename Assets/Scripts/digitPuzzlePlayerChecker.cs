using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class digitPuzzlePlayerChecker : MonoBehaviour
{

    private bool playerInRange;

    public GameObject puzzleCanvasObj;
    
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
                puzzleCanvasObj.SetActive(!puzzleCanvasObj.activeInHierarchy);
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

            playerInRange = false;

        }
    }

}
