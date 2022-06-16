using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cavernOfIllusionsEntryHandler : MonoBehaviour
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

        if (sceneSwapHolder.enteredWay == "entryTocavernOfIllusionsFromSnowyLake")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTocavernOfIllusionsFromSnowyLakeLoc").transform.position;

        }

        if (sceneSwapHolder.enteredWay == "entryTocavernOfIllusionsFromcavernTwo")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTocavernOfIllusionsFromcavernTwoLoc").transform.position;

        }

        


        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;



    }
}
