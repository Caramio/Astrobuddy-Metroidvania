using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using System;
using System.IO;

public class ruinedKingdomCorridorSceneSwapHandler : MonoBehaviour
{
    //EVERY SCENESWAP HANDLER SHOULD BE THE SAME, JUST CUSTOMIZED NAMES FOR DIFFERENT SCENES
    //EACH LEVEL SHOULD ALSO HAVE ENTRY CHECKERS AND PERMANENT OBJECT CHECKERS.



    //Timers
    private float fadeTimer = 0.5f;
    private float fadeCounter;

    //Fadescreen
    public GameObject fadeScreen;

    //Boolean for fade routine
    private bool startedFadeRoutine;

    //Public reference to stop astrobuddy from moving
    private GameObject playerObj;

    //private string , scene to load
    private string sceneToLoad;

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

    private void writeToJSON()
    {



        //Nothing to save in this scene




    }

    private void handleSceneSwap()
    {
        //---
        if (this.gameObject.name == "entryTobuildingsInnerFromruinedKingdomCorridor")
        {
            sceneSwapHolder.enteredWay = "entryTobuildingsInnerFromruinedKingdomCorridor";
            sceneToLoad = "buildingsInner";
            if (startedFadeRoutine == false)
            {
                StartCoroutine(fadeScreenRoutine());
            }

        }

        if (this.gameObject.name == "entryToruinedKingdomLibraryFromruinedKingdomCorridor")
        {
            sceneSwapHolder.enteredWay = "entryToruinedKingdomLibraryFromruinedKingdomCorridor";
            sceneToLoad = "ruinedKingdomLibrary";
            if (startedFadeRoutine == false)
            {
                StartCoroutine(fadeScreenRoutine());
            }

        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "PlayerHitbox")
        {
            Debug.Log("teleported");
            handleSceneSwap();
            //collision.transform.position = Vector3.zero;

        }
    }

    IEnumerator loadAsyncScene()
    {
        Debug.Log("Load was done");

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }


    }

    private IEnumerator fadeScreenRoutine()
    {
        writeToJSON();

        startedFadeRoutine = true;

        //Freeze the player once they enter a new screen zone
        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        while (fadeCounter <= fadeTimer)
        {

            fadeScreen.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, Mathf.Lerp(0, 1, fadeCounter / fadeTimer));

            fadeCounter += Time.deltaTime;

            yield return null;

        }

        fadeCounter = 0f;

        startedFadeRoutine = false;

        StartCoroutine(loadAsyncScene());
    }
}
