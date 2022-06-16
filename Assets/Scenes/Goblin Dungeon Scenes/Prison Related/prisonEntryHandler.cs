using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prisonEntryHandler : MonoBehaviour
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

        if (sceneSwapHolder.enteredWay == "entryToPrisonUpper")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("upperPrisonEntryLoc").transform.position;

        }

        if (sceneSwapHolder.enteredWay == "entryToPrisonLower")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("lowerPrisonEntryLoc").transform.position;

        }

        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;


    }
}
