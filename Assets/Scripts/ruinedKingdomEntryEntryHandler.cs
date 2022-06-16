using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ruinedKingdomEntryEntryHandler : MonoBehaviour
{
    // Start is called before the first frame update

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

        //outsideLastTeleportation


    }

    private void entranceHandler()
    {

        if (sceneSwapHolder.enteredWay == "outsideLastTeleportation")
        {

            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("outsideLastTeleportationLoc").transform.position;
            Debug.Log("did this 1111");

            //reset color here
            playerObj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }

      



        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
