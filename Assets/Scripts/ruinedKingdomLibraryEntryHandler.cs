using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ruinedKingdomLibraryEntryHandler : MonoBehaviour
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

        if (sceneSwapHolder.enteredWay == "entryToruinedKingdomLibraryFrommageBossRoom")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryToruinedKingdomLibraryFrommageBossRoomLoc").transform.position;

        }
        
        if (sceneSwapHolder.enteredWay == "entryToruinedKingdomLibraryFromruinedKingdomCorridor")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryToruinedKingdomLibraryFromruinedKingdomCorridorLoc").transform.position;

        }



        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

    }
}
