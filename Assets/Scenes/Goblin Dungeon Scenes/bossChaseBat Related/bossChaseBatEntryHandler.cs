using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossChaseBatEntryHandler : MonoBehaviour
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
        
        if (sceneSwapHolder.enteredWay == "entryTobatBossChaseFromBossRoom")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTobatBossChaseFromBossRoom").transform.position;

        }

        if (sceneSwapHolder.enteredWay == "entryTobossChaseBat")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("entryTobossChaseBatFromsewageWithFish").transform.position;

        }

        
        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

    }
}
