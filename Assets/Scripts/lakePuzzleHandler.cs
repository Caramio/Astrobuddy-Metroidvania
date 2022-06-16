using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lakePuzzleHandler : MonoBehaviour
{

    //reference to the player
    private GameObject playerObj;



    public List<GameObject> lakePuzzlePlatformsList;

    public int solutionOrder;

    public static string lakeSteppedPlatformName;

    public static int platformJumpCounter = -1;




    void Start()
    {

        var playerObjTry = GameObject.Find("Astrobuddy");

        if (playerObjTry != null)
        {
            playerObj = playerObjTry;


        }



    }

   

    void Update()
    {

        platformCheck();

      

    }



    private void platformCheck()
    {

        if (platformJumpCounter > -1)
        {

            if (lakePuzzlePlatformsList[platformJumpCounter].name != lakeSteppedPlatformName && lakeSteppedPlatformName != null)
            {

                playerObj.GetComponent<playerMovement>().canMove = false;
                playerObj.GetComponent<playerMovement>().canJump = false;
                playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;



                GameObject.Find(lakeSteppedPlatformName).SetActive(false);



                platformJumpCounter = 0;
                lakeSteppedPlatformName = null;
            }


        }

    }
}
