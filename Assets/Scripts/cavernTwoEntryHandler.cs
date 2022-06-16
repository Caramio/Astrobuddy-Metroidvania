using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cavernTwoEntryHandler : MonoBehaviour
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

        if (sceneSwapHolder.enteredWay == "entryTocavernTwoFromcavernOfIllusions")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTocavernTwoFromcavernOfIllusionsLoc").transform.position;

        }

        if (sceneSwapHolder.enteredWay == "entryTocavernTwoFromcavernThree")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTocavernTwoFromcavernThreeLoc").transform.position;

        }

        //entryTocavernTwoFromcavernThreeLoc


        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;



    }
}
