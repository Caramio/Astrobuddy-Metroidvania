using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblinWaterWorksEntryHandler : MonoBehaviour
{

    // Do later

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

        if (sceneSwapHolder.enteredWay == "entryTogoblinWaterWorksFrombrokenBridge")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTogoblinWaterWorksFrombrokenBridgeLoc").transform.position;

        }

        if (sceneSwapHolder.enteredWay == "entryToLowerSewageWithFish")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryToLowerSewageWithFishLoc").transform.position;

        }

        if (sceneSwapHolder.enteredWay == "entryTowaterWorksFromCity")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTowaterWorksFromCityLoc").transform.position;

        }

        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        

    }
}
