using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desertedTownEntryHandler : MonoBehaviour
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

        if (sceneSwapHolder.enteredWay == "entryTodesertedTownFromruinedKingdomEntry")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTodesertedTownFromruinedKingdomEntryLoc").transform.position;

        }

        
        if (sceneSwapHolder.enteredWay == "entryTodesertedTownFrombuildingsInner")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTodesertedTownFrombuildingsInnerLoc").transform.position;

        }
        

        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

    }
}
