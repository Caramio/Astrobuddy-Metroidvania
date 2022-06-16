using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class frogInteractions : MonoBehaviour
{


    private GameObject interactedObject;


    private bool canInteract;
    private bool canInspect;


    //INVENTORY RELATED PART
    public List<GameObject> miscellaneousSlotList;
    public Sprite defaultMiscSprite;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        eToOpen();
    }

    private void eToOpen()
    {

        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {

            //Picking a miscellaneous item
            if (interactedObject.GetComponent<miscItemPickable>() != null)
            {
                Debug.Log("Started");
                for (int i = 0; i < miscellaneousSlotList.Count; i++)
                {
                    if (miscellaneousSlotList[i].transform.GetChild(0).GetComponent<Image>().sprite == defaultMiscSprite)
                    {
                        Debug.Log("Done");
                        miscellaneousSlotList[i].transform.GetChild(0).GetComponent<Image>().sprite = interactedObject.GetComponent<SpriteRenderer>().sprite;

                        interactedObject.SetActive(false);
                        return;
                    }

                }

                
            }

        }


    }



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<interactableWithE>() != null)
        {

            interactedObject = collision.gameObject;
            canInteract = true;

        }


        if (collision.gameObject.GetComponent<inspectableWithE>() != null)
        {

            interactedObject = collision.gameObject;
            canInspect = true;

        }

    }
    // Once the trigger collider is left, the screen will be closed and interactedObject will be reset
    private void OnTriggerExit2D(Collider2D collision)
    {

        //Interactable
        if (collision.gameObject.GetComponent<interactableWithE>() != null)
        {

            if (interactedObject != null)
            {
                // if its a puzzle object, we will close the canvas once we go out
                if (interactedObject.GetComponent<puzzleObject>() != null)
                {
                    interactedObject.transform.GetChild(0).gameObject.SetActive(false);
                }

            
                interactedObject = null;
                canInteract = false;
            }

        }


        //Inspectable
        if (collision.gameObject.GetComponent<inspectableWithE>() != null)
        {

            if (interactedObject != null)
            {
                interactedObject = null;
                canInspect = false;
            }

        }

    }
}
