using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mageBossRoomEntryHandler : MonoBehaviour
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

        if (sceneSwapHolder.enteredWay == "entryTomageBossRoomFromruinedKingdomLibrary")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTomageBossRoomFromruinedKingdomLibraryLoc").transform.position;

        }

       


        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

    }





    //reset the mage boss variables etc...
    private void OnLevelWasLoaded(int level)
    {
        // set this to false to be able to start attacks again
        allMageBossesStateController.mageDoingAttack = false;
        allMageBossesStateController.startedAdjustMageStates = false;

        //CONTINUE WORKING ON THE GEM ETC...
    }
}
