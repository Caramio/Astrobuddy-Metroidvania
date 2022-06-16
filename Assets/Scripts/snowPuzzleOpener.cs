using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowPuzzleOpener : MonoBehaviour
{

    public GameObject openedBridge;

    public static bool puzzleWasSolved;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if(winterWeightPuzzleMassChecker.largeRockInPlace && winterWeightPuzzleMassChecker.mediumRockInPlace && winterWeightPuzzleMassChecker.smallRockInPlace)
        {
            puzzleWasSolved = true;

            openedBridge.SetActive(true);
            this.gameObject.SetActive(false);

            //puzzleWasSolved = true;
        }

    }
}
