using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickableWings : MonoBehaviour
{
    public GameObject entranceDoor, exitDoor;

    public GameObject tutorialHolder;

    private GameObject playerObj;
    void Start()
    {
        var playerObjTry = GameObject.Find("Astrobuddy");

        if (playerObjTry != null)
        {
            playerObj = playerObjTry;


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        // once this is picked the doors will be opened
        if (entranceDoor != null)
        {
            entranceDoor.SetActive(false);
            exitDoor.SetActive(false);

            tutorialHolder.SetActive(true);
        }



    }

}
