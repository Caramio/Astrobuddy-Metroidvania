using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryButtons : MonoBehaviour
{
    //Objects that are used as items
    public GameObject astroSword;
    public GameObject astroTorch;
    


    //Bool to only equip one item at a time
    private bool holdingSomething;


    //public reference to all the relevant UI canvas
    public List<GameObject> canvasList;

    //Inventory number checker
    public static int inventoryPageNum = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Go to the next canvas in the order
    public void nextCanvas()
    {
        //First canvas is always the inventory
        /*
        if (canvasList[inventoryPageNum].activeInHierarchy)
        {
            canvasList[inventoryPageNum].SetActive(false);
        }
        */

        if (Time.timeScale == 1)
        {

            canvasList[inventoryPageNum].SetActive(false);

            inventoryPageNum += 1;

            if (inventoryPageNum == canvasList.Count)
            {
                inventoryPageNum = 0;
            }

            canvasList[inventoryPageNum].SetActive(true);
        }

        


       

        

        

    }

    // Checking if other items are active in hierarchy to be able to equip or de-equip

    public void slotOneButton()
    {

        if (Time.timeScale == 1)
        {

            if (astroTorch.activeInHierarchy == false)
            {
                if (!astroSword.activeInHierarchy)
                {

                    astroSword.SetActive(true);
                }
                else
                {

                    astroSword.SetActive(false);
                }
            }

        }
       // Debug.Log("it works2joı12fjo12");

    }

    public void torchButton()
    {
        if (Time.timeScale == 1)
        {

            Debug.Log("torhced");
            if (astroSword.activeInHierarchy == false)
            {

                if (!astroTorch.activeInHierarchy)
                {

                    astroTorch.SetActive(true);
                }
                else
                {

                    astroTorch.SetActive(false);
                }
            }

        }


    }

    
}
