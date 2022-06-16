using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenBridgeEntryHandler : MonoBehaviour
{
    private GameObject playerObj;

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

        if (sceneSwapHolder.enteredWay == "entryTobrokenBridgeFromgoblinWaterWorks")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTobrokenBridgeFromgoblinWaterWorksLoc").transform.position;

        }

        if (sceneSwapHolder.enteredWay == "entryTobrokenBridgeFromlowerCity")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTobrokenBridgeFromlowerCityEntranceLoc").transform.position;

        }

        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

    }
}
