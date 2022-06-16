using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using UnityEngine.SceneManagement;




public class PrisonChangesHolder : MonoBehaviour
{
    //List of doors specific to this scene
    public List<GameObject> doorList;

    //Double door taht opens from the other scene
    public GameObject doubleDoor;



    //key
    public GameObject prisonKey;

   
    void Start()
    {
        //Door data related
        //First check if the given file exists
        
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "doorList.txt"))
        {

            string[] doorJSONS = File.ReadAllLines(Application.dataPath + SceneManager.GetActiveScene().name + "doorList.txt");

            foreach (string eachLine in doorJSONS)
            {

                prisonSceneSwapHandler.doorInformation doorObj = JsonUtility.FromJson<prisonSceneSwapHandler.doorInformation>(eachLine);



                foreach (GameObject doorInScene in doorList)
                {

                    if (doorObj.doorName == doorInScene.name)
                    {
                        //set key to false too
                        prisonKey.SetActive(false);

                        doorInScene.SetActive(doorObj.shouldCloseDoor);

                    }

                }

            }
        }
        


        

        //Specificly opening a door with a name reference by using another scenes folder...[sewageWithFish here]

        if (File.Exists(Application.dataPath + "sewageWithFish" + "doorList.txt"))
        {

            string[] doorJSONS = File.ReadAllLines(Application.dataPath + "sewageWithFish" + "doorList.txt");

            foreach (string eachLine in doorJSONS)
            {



                sewageWithFishSceneSwapHandler.doorInformation doorObj = JsonUtility.FromJson<sewageWithFishSceneSwapHandler.doorInformation>(eachLine);

               
                if(doubleDoor.name == doorObj.doorName)
                {
                    doubleDoor.SetActive(!doorObj.shouldCloseDoor);
                }

            }
        }



    }

    void Update()
    {
        
    }
}
