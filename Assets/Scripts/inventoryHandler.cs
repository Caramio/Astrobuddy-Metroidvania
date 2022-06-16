using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryHandler : MonoBehaviour
{

    //Public references
    public Canvas inventoryCanvas;

    //Public list for the canvases
    public List<GameObject> canvasList;

    //Booleans
    private bool isInventoryOpen = false;

    void Start()
    {
        // start the inventory canvas as inactive
        inventoryCanvas.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        // if the game is not paused
        if (Time.timeScale == 1)
        {
            openInventoryScreen();
        }

    }


    void openInventoryScreen()
    {

        /*
        if (Input.GetKeyDown(KeyCode.Tab) && !isInventoryOpen)
        {

            inventoryCanvas.gameObject.SetActive(true);

            isInventoryOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isInventoryOpen)
        {

            inventoryCanvas.gameObject.SetActive(false);

            isInventoryOpen = false;

        }
        */

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (canvasList[inventoryButtons.inventoryPageNum].activeInHierarchy == true)
            {
                canvasList[inventoryButtons.inventoryPageNum].gameObject.SetActive(false);
            }

            else if (canvasList[inventoryButtons.inventoryPageNum].activeInHierarchy == false)
            {
                canvasList[inventoryButtons.inventoryPageNum].gameObject.SetActive(true);
            }

            isInventoryOpen = true;

        }


    }
}
