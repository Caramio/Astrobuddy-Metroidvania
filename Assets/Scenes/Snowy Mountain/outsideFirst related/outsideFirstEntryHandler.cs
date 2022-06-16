using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outsideFirstEntryHandler : MonoBehaviour
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

        if (sceneSwapHolder.enteredWay == "entryToOutsideFromGoblinKing")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryToOutsideFromGoblinKingLoc").transform.position;

        }

  
        if (sceneSwapHolder.enteredWay == "entryToOutsideFirstFromSnowyLake")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryToOutsideFirstFromSnowyLakeLoc").transform.position;

        }




        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;



    }
}
