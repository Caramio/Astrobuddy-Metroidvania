using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;



public class sewageWithFishChangesHolder : MonoBehaviour
{

    //Doors List
    public List<GameObject> doorList;
    //water holder
    public GameObject waterHolder;
    //items that can be picked only once list
    public List<GameObject> pickableOnceList;

    //water Light after waterworks
    public GameObject waterLight;

    private void Start()
    {

        //LOADING DOOR DATA
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "doorList.txt"))
        {
            string[] doorJSONS = File.ReadAllLines(Application.dataPath + SceneManager.GetActiveScene().name + "doorList.txt");

            foreach (string eachLine in doorJSONS)
            {
                //MAKE SURE THIS LINE IS REFERENCING THE CURRENT SCENES LIST
                sewageWithFishSceneSwapHandler.doorInformation doorObj = JsonUtility.FromJson<sewageWithFishSceneSwapHandler.doorInformation>(eachLine);

                foreach (GameObject doorInScene in doorList)
                {

                    if (doorObj.doorName == doorInScene.name)
                    {
                        //opposite of should close door
                        doorInScene.SetActive(!doorObj.shouldCloseDoor);
                    }

                }
            }

       

        }

        //Loading pickableObj data if the textfile exists LOADING PICKABLE DATA

        if(File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "pickableList.txt"))
        {

            string[] pickableJSONS = File.ReadAllLines(Application.dataPath + SceneManager.GetActiveScene().name + "pickableList.txt");

            foreach (string eachLine in pickableJSONS)
            {
                //MAKE SURE THIS LINE IS REFERENCING THE CURRENT SCENES LIST
                sewageWithFishSceneSwapHandler.pickableInformation pickableObj = JsonUtility.FromJson<sewageWithFishSceneSwapHandler.pickableInformation>(eachLine);

                foreach (GameObject pickableObjInScene in pickableOnceList)
                {

                    if (pickableObj.pickableName == pickableObjInScene.name)
                    {
                        //opposite of should close door
                        pickableObjInScene.SetActive(!pickableObj.shouldBeInactive);
                    }

                }

            }

    
        }
        




        //OTHER SCENES RELATED
        //Checking water data from the goblinwaterworks scene to close in the sewage scene :)

        if (File.Exists(Application.dataPath + "goblinWaterWorks" + "waterPuzzleList.txt") == true)
        {

            string[] waterJSONS = File.ReadAllLines(Application.dataPath + "goblinWaterWorks" + "waterPuzzleList.txt");

            Debug.Log("Debugging" + waterJSONS);

            goblinWaterWorksSceneSwapHandler.waterPuzzleInformation waterObj = JsonUtility.FromJson<goblinWaterWorksSceneSwapHandler.waterPuzzleInformation>(waterJSONS[0]);

            //if the puzzle is solved, set water to inactive
            waterHolder.SetActive(waterObj.puzzleSolved);

            Debug.Log("TRIED TO TURN THE THING OFF");

            waterLight.SetActive(!waterObj.puzzleSolved);

            


        }

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
