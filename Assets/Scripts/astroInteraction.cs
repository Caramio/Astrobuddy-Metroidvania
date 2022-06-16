using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
public class astroInteraction : MonoBehaviour
{

    //Boolean
    private bool canInteract = false;
    private bool canInspect = false;

    //Bool to check if certain powers are picked CHANGE FOR LATER WHEN THE GAME WILL START, CURRENTLY THEY ARE ALL USEABLE
    [HideInInspector]
    public bool pickedAstroWings;

    //Camera to check whether to view info or follow player...
    private Camera screenCamera;

    //GameObject that is being interacted with
    private GameObject interactedObject;


    //Torch to be enabled once we first discover it.
    public GameObject astroTorch;
    public GameObject astroSword;
    

    //Inventory button to be enabled once we obtain an item
    public GameObject torchButton;
    public GameObject swordButton;
    

    //Frog holders spot
    public GameObject frogHolder;


    //misc item canvas
    public List<GameObject> miscellaneousSlotList;
    public Sprite defaultMiscSprite;

    



    //Static variable that will indicate the frog is picked, change to hide in inspector later
    public static bool frogIsAvailable = true;

    void Start()
    {
        screenCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        //clickSpace();

        if (Time.timeScale == 1)
        {
            if (interactedObject != null)
            {
                

                eToOpen();
                eToInspect();
            }
        }


        


    }

    // Pressing E to open a puzzle interface , opening child object which will be the canvas
    private void eToOpen()
    {

        // Opening a puzzle with a canvas in it
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {

            if (interactedObject != null)
            {
                
                if (interactedObject.GetComponent<puzzleObject>() != null)
                {
                    
                    interactedObject.transform.GetChild(0).gameObject.SetActive(true);
                    Debug.Log("enabled " + interactedObject.transform.GetChild(0).gameObject.name);
                }
                


                // checking an information object, like the telescope at the start.
                /*
                if (interactedObject.GetComponent<informationObject>() != null)
                {

                    screenCamera.GetComponent<cameraFollow>().isCheckingInformation = true;
                    screenCamera.GetComponent<cameraFollow>().informationTransform = interactedObject.GetComponent<informationObject>().sceneTransform;


                }
                */


                // Interacting with the torch at the dungeon to be able to take the item.
                // setting the active of the torch button and torch to true
                if (interactedObject.GetComponent<torchPickable>() != null)
                {



                    astroTorch.SetActive(true);
                    torchButton.SetActive(true);

                    interactedObject.SetActive(false);


                    return;
                }

                //Picking up the sword for the first time
                if (interactedObject.GetComponent<swordPickable>() != null)
                {
                    Debug.Log("tried");


                    //set static class variable to true if picked
                    astroBuddyStaticClass.astroHasSword = true;

                    astroSword.SetActive(true);
                    swordButton.SetActive(true);

                    //start tutorial once we pick up the sword
                    interactedObject.GetComponent<swordPickable>().swordTutorialObj.SetActive(true);
                    interactedObject.SetActive(false);

                    return;

                }

                //Picking up the wings for the first time
                if (interactedObject.GetComponent<pickableWings>() != null)
                {

                    astroBuddyStaticClass.astroHasWings = true;



                    pickedAstroWings = true;
                    interactedObject.SetActive(false);

                    return;

                }

                //related to pickable gems...
                if (interactedObject.GetComponent<pickableWishGem>() != null && interactedObject.name == "goblinKingsGemRed")
                {
                    Debug.Log("picked red gem yo");
                    astroBuddyStaticClass.astroPickedRedGem = true;
             

                    return;

                }

                //----

                //Picking a miscellaneous item
                if (interactedObject.GetComponent<miscItemPickable>() != null)
                {
                    Debug.Log("Started");
                    for (int i = 0; i < miscellaneousSlotList.Count; i++)
                    {
                        if (miscellaneousSlotList[i].GetComponent<Image>().sprite == defaultMiscSprite)
                        {
                            Debug.Log("Done");
                            miscellaneousSlotList[i].GetComponent<Image>().sprite = interactedObject.GetComponent<SpriteRenderer>().sprite;
                            Destroy(interactedObject);
                            return;
                        }

                    }
                }
            }

        }


    }

    //Pressing E to inspect
    private void eToInspect()
    {

        if (interactedObject != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && canInspect && showTextOnCommand.inspectTextCooldown)
            {

                interactedObject.GetComponent<showTextOnCommand>().enabled = true;

                Debug.Log("helhelhlelhee");
            }
        }


    }




    //Clicking a wall that can be expanded by sending a raycast to a point
    private void clickSpace()
    {

        if (Input.GetMouseButtonDown(0))
        {
        
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);

                //Assigning the hitObject for later use, this is the object that is clicked on.
                Collider2D hitObject = hit.collider;
                
            }


            //If the hitObject is an information wall
            if(hit.collider.GetComponent<puzzleObject>() != null)
            {

                activatePuzzleObject(hit.collider);

            }

            
           

        }

    }




    //Once a puzzle is clicked, the first transform ( canvas for the puzzle ) will be activated.
    private void activatePuzzleObject(Collider2D hitWall)
    {
        Debug.Log("did thias");
        hitWall.transform.GetChild(0).gameObject.SetActive(true);


    }


    //Once the trigger collider is entered by the player, the interactable object will be assigned
    //Interactable object and inspectable objects are different.
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.GetComponent<interactableWithE>() != null)
        {
           
            interactedObject = collision.gameObject;
            canInteract = true;

        }

        
        if(collision.gameObject.GetComponent<inspectableWithE>() != null)
        {
            
            interactedObject = collision.gameObject;
            canInspect = true;

        }
        
    }
    // also do it on trigger stay
    private void OnTriggerStay2D(Collider2D collision)
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

                // if its an informaiton object, we will reset the camera variable
                if(interactedObject.GetComponent<informationObject>() != null)
                {

                    screenCamera.GetComponent<cameraFollow>().isCheckingInformation = false;
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
