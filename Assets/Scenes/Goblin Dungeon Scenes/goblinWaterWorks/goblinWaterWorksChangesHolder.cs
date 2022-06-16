using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using UnityEngine.SceneManagement;



public class goblinWaterWorksChangesHolder : MonoBehaviour
{
    //Elevators list
    public List<GameObject> elevatorList;


    //Objects related to the waterPuzzle
    public GameObject waterDigitPuzzle;
    public GameObject waterHolder;



    



    private void Start()
    {




        //Loading elevator related
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "elevatorList.txt"))
        {
            //Loading elevator data    
            string[] elevatorJSONS = File.ReadAllLines(Application.dataPath + SceneManager.GetActiveScene().name + "elevatorList.txt");

            foreach (string eachLine in elevatorJSONS)
            {

                goblinWaterWorksSceneSwapHandler.elevatorInformation elevatorObj = JsonUtility.FromJson<goblinWaterWorksSceneSwapHandler.elevatorInformation>(eachLine);

                foreach (GameObject elevatorInScene in elevatorList)
                {

                    if (elevatorObj.elevatorName == elevatorInScene.name)
                    {
                        elevatorInScene.transform.position = new Vector3(elevatorObj.elevatorXpos, elevatorObj.elevatorYpos);
                    }

                }

            }


          

        }


        //Loading water related
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "waterPuzzleList.txt") == true)
        {

            string[] waterJSONS = File.ReadAllLines(Application.dataPath + SceneManager.GetActiveScene().name + "waterPuzzleList.txt");

            goblinWaterWorksSceneSwapHandler.waterPuzzleInformation waterObj = JsonUtility.FromJson<goblinWaterWorksSceneSwapHandler.waterPuzzleInformation>(waterJSONS[0]);

            //if the puzzle is solved, set water to inactive
            puzzleButtons.waterPuzzleSolved = waterObj.puzzleSolved;
            waterHolder.SetActive(!waterObj.puzzleSolved);

            //if the puzzle is solved, set the puzzles interactableWithE to false
            waterDigitPuzzle.SetActive(false);


        }

       


    }

    public void Update()
    {
        


    }






}
