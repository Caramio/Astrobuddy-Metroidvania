using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowyLakeEntryHandler : MonoBehaviour
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



    private void entranceHandler()
    {

        if (sceneSwapHolder.enteredWay == "entryToSnowyLakeFromoutsideFirst")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTosnowyLakeFromoutsideFirstLoc").transform.position;

        }

        if (sceneSwapHolder.enteredWay == "entryToSnowyLakeFromcavernOfIllusions")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryToSnowyLakeFromcavernOfIllusionsLoc").transform.position;

        }




        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;



    }
}
