using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cavernFourEntryHandler : MonoBehaviour
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

        if (sceneSwapHolder.enteredWay == "entryTocavernFourFromcavernThree")
        {
           
            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTocavernFourFromcavernThreeLoc").transform.position;

        }

        
        if (sceneSwapHolder.enteredWay == "entryTocavernFourFromoutsideLast")
        {
            Debug.Log("entered wtf");
            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTocavernFourFromoutsideLastLoc").transform.position;

        }
        
        




        //entryTocavernTwoFromcavernThreeLoc


        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;



    }
}
