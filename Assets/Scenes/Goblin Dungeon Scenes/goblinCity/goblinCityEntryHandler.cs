using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblinCityEntryHandler : MonoBehaviour
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


   
    private void entranceHandler()
    {
        //from waterworks to goblin city
        if (sceneSwapHolder.enteredWay == "entryTogoblinCityFromwaterWorks")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTogoblinCityFromwaterWorksLoc").transform.position;

        }

        if (sceneSwapHolder.enteredWay == "entryToGoblinCityFromGoblinKing")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryToGoblinCityFromGoblinKingLoc").transform.position;

        }

        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

    }
}
