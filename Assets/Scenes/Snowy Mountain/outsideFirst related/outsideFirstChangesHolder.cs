using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class outsideFirstChangesHolder : MonoBehaviour
{

    public GameObject doorToOpen, bridgeToOpen;


    // Start is called before the first frame update
    void Start()
    {


        //if the file is not empty and it exists, we can load
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "snowPuzzleList.txt"))
        {

            if (new FileInfo(Application.dataPath + SceneManager.GetActiveScene().name + "snowPuzzleList.txt").Length != 0)
            {

                string[] snowPuzzleJSONS = File.ReadAllLines(Application.dataPath + SceneManager.GetActiveScene().name + "snowPuzzleList.txt");

                outsideFirstSceneSwapHandler.snowPuzzle snowPuzzleObj = JsonUtility.FromJson<outsideFirstSceneSwapHandler.snowPuzzle>(snowPuzzleJSONS[0]);


                if (snowPuzzleObj.puzzleCompletionStatus == true)
                {
                    doorToOpen.SetActive(false);
                    bridgeToOpen.SetActive(true);

                }
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
