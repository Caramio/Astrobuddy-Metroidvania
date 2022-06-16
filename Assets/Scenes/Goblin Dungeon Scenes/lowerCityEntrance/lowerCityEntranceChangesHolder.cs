using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using UnityEngine.SceneManagement;

public class lowerCityEntranceChangesHolder : MonoBehaviour
{
    //Doors List
    public List<GameObject> doorList;



    private void Start()
    {

        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "doorList.txt"))
        {

            //Loading elevator data    
            string[] doorJSONS = File.ReadAllLines(Application.dataPath + SceneManager.GetActiveScene().name + "doorList.txt");

            foreach (string eachLine in doorJSONS)
            {

                lowerCityEntranceSceneSwapHandler.doorInformation doorObj = JsonUtility.FromJson<lowerCityEntranceSceneSwapHandler.doorInformation>(eachLine);

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


    }
}
