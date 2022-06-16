using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using UnityEngine.SceneManagement;
public class buildingsInnerEntryHandler : MonoBehaviour
{
    private GameObject playerObj;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {

    }

    private void entranceHandler()
    {

        if (sceneSwapHolder.enteredWay == "entryTobuildingsInnerFromdesertedTown")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTobuildingsInnerFromdesertedTownLoc").transform.position;

        }

        
        if (sceneSwapHolder.enteredWay == "entryTobuildingsInnerFromruinedKingdomCorridor")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTobuildingsInnerFromruinedKingdomCorridorLoc").transform.position;

        }
        

        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

    }
}
