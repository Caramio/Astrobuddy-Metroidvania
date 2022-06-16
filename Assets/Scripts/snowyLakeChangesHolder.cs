using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using UnityEngine.SceneManagement;




public class snowyLakeChangesHolder : MonoBehaviour
{

    public GameObject wallToOpen;

    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "wheelPuzzleList.txt") == true)
        {

            string[] wheelJSONS = File.ReadAllLines(Application.dataPath + SceneManager.GetActiveScene().name + "wheelPuzzleList.txt");

            snowyLakeSceneSwapHandler.wheelPuzzleInformation wheelObj = JsonUtility.FromJson<snowyLakeSceneSwapHandler.wheelPuzzleInformation>(wheelJSONS[0]);

            //if the puzzle is solved, set water to inactive
            wallToOpen.SetActive(!wheelObj.wheelPuzzleSolvedStatus);



        }
    }

    // Update is called once per frame

    void Update()
    {
        
    }
}
