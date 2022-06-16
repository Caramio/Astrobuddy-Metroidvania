using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using System;

using System.IO;


public class goblinCityChangesHolder : MonoBehaviour
{
    //Elevators list
    public GameObject goblinElevator;

    public GameObject topLeftDoor, topRightDoor;

    public GameObject goblinKingDoor;
    public Sprite openDoorSprite;

    public GameObject gemHolderObject;

    


    private void Start()
    {
        //Loading elevator related
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "elevatorList.txt"))
        {
            //Loading elevator data    
            string elevatorJSONS = File.ReadAllLines(Application.dataPath + SceneManager.GetActiveScene().name + "elevatorList.txt")[0];

            goblinCitySceneSwapHandler.elevatorInformation elevatorObj = JsonUtility.FromJson<goblinCitySceneSwapHandler.elevatorInformation>(elevatorJSONS);

            goblinElevator.transform.position = new Vector3(elevatorObj.elevatorXpos, elevatorObj.elevatorYpos, 0f);
            goblinElevatorScript.elevatorFloor = elevatorObj.elevatorFloor;

            if(elevatorObj.elevatorFloor == 5)
            {
                topLeftDoor.SetActive(false);
                topRightDoor.SetActive(false);
            }

        }
        

        //Loading light data
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "lightPuzzleList.txt"))
        {
            //Loading elevator data    
            string lightPuzzleJSON = File.ReadAllLines(Application.dataPath + SceneManager.GetActiveScene().name + "lightPuzzleList.txt")[0];

            goblinCitySceneSwapHandler.lightPuzzleInformation lightObj = JsonUtility.FromJson<goblinCitySceneSwapHandler.lightPuzzleInformation>(lightPuzzleJSON);

           
            lightPuzzleCompletionChecker.goblinDoorOpened = lightObj.doorIsOpen;

            if(lightPuzzleCompletionChecker.goblinDoorOpened == true)
            {

                goblinKingDoor.GetComponent<SpriteRenderer>().sprite = openDoorSprite;

            }

           

        }

        //loading pickable data
        if(File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "pickableList.txt"))
        {


            string pickableJSON = File.ReadAllLines(Application.dataPath + SceneManager.GetActiveScene().name + "pickableList.txt")[0];

            goblinCitySceneSwapHandler.pickableItemInformation pickableObj = JsonUtility.FromJson<goblinCitySceneSwapHandler.pickableItemInformation>(pickableJSON);

            gemHolderObject.SetActive(pickableObj.pickableState);




        }

    }

}
