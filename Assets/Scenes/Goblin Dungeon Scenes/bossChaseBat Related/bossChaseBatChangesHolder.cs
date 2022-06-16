using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using UnityEngine.SceneManagement;



public class bossChaseBatChangesHolder : MonoBehaviour
{

    

    public GameObject startDoor, endDoor;
    public GameObject batBoss;

    public GameObject startSequenceRelated;

    void Start()
    {

        
        //Check for the file of the scene, if it exists check specific values
        if(File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "bossChaseBatRelated.txt"))
        {
            //Do load operations only if the file isnt empty
            if (new FileInfo(Application.dataPath + SceneManager.GetActiveScene().name + "bossChaseBatRelated.txt").Length != 0)
            {

                string[] batChaseJSON = File.ReadAllLines(Application.dataPath + SceneManager.GetActiveScene().name + "bossChaseBatRelated.txt");

                batBossChaseSwapHandler.batBossChaseInformation batRelatedObj = JsonUtility.FromJson<batBossChaseSwapHandler.batBossChaseInformation>(batChaseJSON[0]);

                //If the end was reached and the bat boss finished the route, adjust certain GameObjects active states
                if (batRelatedObj.finishedBatRoute == true)
                {

                    startDoor.SetActive(false);
                    endDoor.SetActive(false);
                    batBoss.SetActive(false);
                    startSequenceRelated.SetActive(false);

                }

            }


        }




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
