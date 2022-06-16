using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class goblinKingChangesHolder : MonoBehaviour
{
    // entry scene holder related
    public GameObject entrySceneHolder;

    public GameObject rightDoorToOpen;


    //Red gem to open if the player couldnt pick it up
    public GameObject pickableRedGem;



    private void Start()
    {
        //Loading entry data
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "bossEntryRelated.txt"))
        {



            //Loading entry data
            string[] entryJSONS = File.ReadAllLines(Application.dataPath + SceneManager.GetActiveScene().name + "bossEntryRelated.txt");

            goblinKingSceneSwapHandler.entrySceneInformation entryObj = JsonUtility.FromJson<goblinKingSceneSwapHandler.entrySceneInformation>(entryJSONS[0]);

            entrySceneHolder.SetActive(entryObj.entranceSceneActiveState);

            rightDoorToOpen.SetActive(entryObj.entranceSceneActiveState);


            //If the gem should be set to active or not ? the player might not have picked it and quit the game afterwards.
            if (File.Exists(Application.dataPath + "astroBuddyStats.txt"))
            {

                string[] astroJSON = File.ReadAllLines(Application.dataPath + "astroBuddyStats.txt");

                dataSavingObjScript.astroBuddyStatsInformation astroStatObj = JsonUtility.FromJson<dataSavingObjScript.astroBuddyStatsInformation>(astroJSON[0]);

                if(astroStatObj.astroPickedRedGem == false && entryObj.entranceSceneActiveState == false)
                {
                    Debug.Log("set the gem to true");
                    pickableRedGem.SetActive(true);
                }




            }


        }


       
    }

}
