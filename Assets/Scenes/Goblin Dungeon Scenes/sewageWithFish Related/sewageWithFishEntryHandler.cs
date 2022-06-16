using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sewageWithFishEntryHandler : MonoBehaviour
{
    private GameObject playerObj;

    public GameObject cameraHolder;
    void Start()
    {

        // try to find the playerobj if not null assign it
        var playerObjTry = GameObject.Find("Astrobuddy");

        if (playerObjTry != null)
        {
            playerObj = playerObjTry;


        }

        entranceHandler();

    }


    void Update()
    {

    }

    private void entranceHandler()
    {

        if (sceneSwapHolder.enteredWay == "entryToUpperSewageWithFish")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryToUpperSewageWithFishLoc").transform.position;

        }

        if (sceneSwapHolder.enteredWay == "entryToLowerSewageWithFish")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryToLowerSewageWithFishLoc").transform.position;

        }

        if (sceneSwapHolder.enteredWay == "entryTosewageWithFishFromgliderToLowerCity")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTosewageWithFishFromgliderToLowerCityLoc").transform.position;

        }

        if (sceneSwapHolder.enteredWay == "entryToSewageWithFishFromWaterWorks")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryToSewageWithFishFromWaterWorksLoc").transform.position;

        }

        if (sceneSwapHolder.enteredWay == "entryTosewageWithFishFromBatChase")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTosewageWithFishFromBatChaseLoc").transform.position;

        }


        //snap the camera to the player at the start of the levels
        cameraHolder.transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, -5f);
        //make velocity 0 for the first entry
        playerObj.GetComponent<Rigidbody2D>().velocity = Vector3.zero;


        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        //Let player jump once on entry to sewage
        playerObj.GetComponent<playerMovement>().canJump = true;
        
    }
}
