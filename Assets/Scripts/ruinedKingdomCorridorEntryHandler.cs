using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ruinedKingdomCorridorEntryHandler : MonoBehaviour
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

        if (sceneSwapHolder.enteredWay == "entryToruinedKingdomCorridorFrombuildingsInner")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryToruinedKingdomCorridorFrombuildingsInnerLoc").transform.position;

        }
        
        if (sceneSwapHolder.enteredWay == "entryToruinedKingdomCorridorFromruinedKingdomLibrary")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryToruinedKingdomCorridorFromruinedKingdomLibraryLoc").transform.position;

        }



        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

    }
}
