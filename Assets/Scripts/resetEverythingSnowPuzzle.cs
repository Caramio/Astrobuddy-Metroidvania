using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetEverythingSnowPuzzle : MonoBehaviour
{
    public List<GameObject> wallList;

    public List<GameObject> snowballList;

    public List<GameObject> pressurePlatformList;

    private bool playerInRange;


    Vector3 startPositionOne, startPositionTwo, startPositionThree;

    Vector3 startPlatePosOne, startPlatePosTwo, startPlatePosThree;
    void Start()
    {
        // start positions for snowballs
        startPositionOne = snowballList[0].transform.position;
        startPositionTwo = snowballList[1].transform.position;
        startPositionThree = snowballList[2].transform.position;


        //start positions for pressure plates
        startPlatePosOne = pressurePlatformList[0].transform.position;
        startPlatePosTwo = pressurePlatformList[1].transform.position;
        startPlatePosThree = pressurePlatformList[2].transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        if (playerInRange)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {

                this.transform.Rotate(0, 180, 0);

                foreach(GameObject wallObj in wallList)
                {

                    wallObj.SetActive(true);

                }

                //reset snowball spots
                snowballList[0].transform.position = startPositionOne;
                snowballList[1].transform.position = startPositionTwo;
                snowballList[2].transform.position = startPositionThree;


                //reset mass check values
                winterWeightPuzzleMassChecker.largeRockInPlace = false;
                winterWeightPuzzleMassChecker.mediumRockInPlace = false;
                winterWeightPuzzleMassChecker.smallRockInPlace = false;

                //put the pressure plates in the original spots

                pressurePlatformList[0].transform.position = startPlatePosOne;
                pressurePlatformList[1].transform.position = startPlatePosTwo;
                pressurePlatformList[2].transform.position = startPlatePosThree;

                //snow platform counter reset
                snowballDroppingLever.snowPlatformToOpenCounter = 0;




            }
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {

            playerInRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "PlayerHitbox")
        {

            playerInRange = false;

        }

    }
}
