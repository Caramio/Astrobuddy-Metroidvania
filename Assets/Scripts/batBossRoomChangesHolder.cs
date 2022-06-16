using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using UnityEngine.SceneManagement;

public class batBossRoomChangesHolder : MonoBehaviour
{

    public GameObject entranceDoor, exitDoor;

    public GameObject batBossEntryRelated;
    
    void Start()
    {


        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "batBossRoomRelated.txt"))
        {
            

            // if it isn't an empty file
            if (new FileInfo(Application.dataPath + SceneManager.GetActiveScene().name + "batBossRoomRelated.txt").Length != 0) 
            {

                string[] batBossJSONS = File.ReadAllLines(Application.dataPath + SceneManager.GetActiveScene().name + "batBossRoomRelated.txt");

                batBossRoomSwapHandler.batBossInformation batBossInfObj = JsonUtility.FromJson<batBossRoomSwapHandler.batBossInformation>(batBossJSONS[0]);

                if (batBossInfObj.bossWasKilled)
                {
                    //remind that the boss was killed.
                    batBossStates.bossWasKilled = true;

                    entranceDoor.SetActive(false);
                    exitDoor.SetActive(false);
                    batBossEntryRelated.SetActive(false);
                }


            }

         
            




        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
