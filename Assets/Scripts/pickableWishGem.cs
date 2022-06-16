using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using UnityEngine.SceneManagement;

public class pickableWishGem : MonoBehaviour
{

    //public references
    //RED GEM RELATED---
    //Door to open for the goblin king when the red gem is picked
    public GameObject doorToOpenRight;
    public GameObject doorToOpenLeft;

    public GameObject redGemImagePoint;
    public GameObject redGemInCanvas;


    //GREEN GEM RELATED---
    public GameObject greenGemImagePoint;
    public GameObject greenGemInCanvas;







    public GameObject gemCanvasHolder;

    //Reference to the player
    private GameObject playerObj;

    private bool playerInRange;



    // coroutine related
    private bool startedPlacementRoutine;
    private float placementRoutineTimer = 5f;
    private float placementRoutineCounter;
   
    void Start()
    {
        playerObj = GameObject.Find("Astrobuddy");
       
    }


    void Update()
    {
        //Pick the gem and add it to the gem holder in the inventory
        if (playerInRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                //activate the red gem if picked
                if (this.gameObject.name == "goblinKingsGemRed")
                {
                    Debug.Log("yahoo done");

                    playerObj.GetComponent<astroInventory>().gemHolderGems[0].SetActive(true);

              
                    if(startedPlacementRoutine == false)
                    {
                        StartCoroutine(inventoryPlacementRoutineRedGem());
                    }

                }
                if (this.gameObject.name == "magesGreenGem")
                {


                    playerObj.GetComponent<astroInventory>().gemHolderGems[1].SetActive(true);


                    if (startedPlacementRoutine == false)
                    {
                        StartCoroutine(inventoryPlacementRoutineGreenGem());
                    }

                }

            }
        }

    }

    private IEnumerator inventoryPlacementRoutineRedGem()
    {

        gemCanvasHolder.SetActive(true);

        startedPlacementRoutine = true;

        Vector3 startPos = redGemInCanvas.GetComponent<RectTransform>().position;

        while(placementRoutineCounter <= placementRoutineTimer)
        {

            redGemInCanvas.GetComponent<RectTransform>().position = Vector3.Lerp(startPos, redGemImagePoint.GetComponent<RectTransform>().transform.position , placementRoutineCounter/placementRoutineTimer);


            placementRoutineCounter += Time.deltaTime;

            yield return null;
        }

        placementRoutineCounter = 0;

        startedPlacementRoutine = false;

        doorToOpenLeft.SetActive(false);
        doorToOpenRight.SetActive(false);

        Destroy(this.gameObject);

    }


    //portal to open after the gem is picked
    public GameObject endPortal;
    private IEnumerator inventoryPlacementRoutineGreenGem()
    {

        gemCanvasHolder.SetActive(true);

        startedPlacementRoutine = true;

        Vector3 startPos = greenGemInCanvas.GetComponent<RectTransform>().position;

        playerObj.GetComponent<playerMovement>().enabled = false;   
        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        playerObj.GetComponent<astroAbilities>().enabled = false;

        Debug.Log("set to false");

        while (placementRoutineCounter <= placementRoutineTimer)
        {

            playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            greenGemInCanvas.GetComponent<RectTransform>().position = Vector3.Lerp(startPos, greenGemImagePoint.GetComponent<RectTransform>().transform.position, placementRoutineCounter / placementRoutineTimer);


            placementRoutineCounter += Time.deltaTime;

            yield return null;
        }
        //set to true
        astroBuddyStaticClass.astroPickedGreenGem = true;


        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        playerObj.GetComponent<playerMovement>().enabled = true;
        playerObj.GetComponent<astroAbilities>().enabled = true;


        //after gem is picked, adjust...
        endPortal.SetActive(true);

        placementRoutineCounter = 0;

        startedPlacementRoutine = false;

     
        Destroy(this.gameObject);


     
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
