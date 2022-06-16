using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using UnityEngine.SceneManagement;

public class ruinedKingdomEntryChangesHolder : MonoBehaviour
{
    

    public GameObject snowSpiritSequenceRelated;

    void Start()
    {



        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "ruinedKingdomEntryRelated.txt"))
        {
            //Do load operations only if the file isnt empty
            if (new FileInfo(Application.dataPath + SceneManager.GetActiveScene().name + "ruinedKingdomEntryRelated.txt").Length != 0)
            {

                string[] ruinedKingdomEntryJSON = File.ReadAllLines(Application.dataPath + SceneManager.GetActiveScene().name + "ruinedKingdomEntryRelated.txt");

                ruinedKingdomEntrySceneSwapHandler.ruinedKingdomEntryInformation ruinedKingdomEntryObj = JsonUtility.FromJson<ruinedKingdomEntrySceneSwapHandler.ruinedKingdomEntryInformation>(ruinedKingdomEntryJSON[0]);

                if (ruinedKingdomEntryObj.dashWasReceived == true)
                {

                    snowSpiritSequenceRelated.SetActive(false);

     
                }

            }


        }




    }

    // Update is called once per frame
    void Update()
    {

    }
}
