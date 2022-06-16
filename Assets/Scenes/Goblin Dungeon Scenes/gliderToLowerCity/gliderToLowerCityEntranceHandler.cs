using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gliderToLowerCityEntranceHandler : MonoBehaviour
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
    // Assign a position depending on the scene that we entered here from.
    private void entranceHandler()
    {

        if (sceneSwapHolder.enteredWay == "entryTogliderLocationFromlowerCityEntrance")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryFromLowerCityEntranceLoc").transform.position;

        }

        if (sceneSwapHolder.enteredWay == "entryTogliderToLowerCityFromsewageWithFish")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryFromsewageWithFishLoc").transform.position;

        }

        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

    }
}
