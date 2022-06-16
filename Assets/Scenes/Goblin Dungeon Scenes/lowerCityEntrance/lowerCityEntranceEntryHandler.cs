using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lowerCityEntranceEntryHandler : MonoBehaviour
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

        if (sceneSwapHolder.enteredWay == "entryTolowerCityEntranceTopRight")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryFrombrokenBridgeLoc").transform.position;

        }

        if (sceneSwapHolder.enteredWay == "entryToLowerCityEntranceFromGlider")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryFromgliderToLowerCityLoc").transform.position;

        }

        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

    }
}
