using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using System;

using System.IO;

public class batBossChaseSwapHandler : MonoBehaviour
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

    //private string , scene to lad
    private string sceneToLoad;





    public class batBossChaseInformation
    {

        public bool finishedBatRoute;

    }



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
        Debug.Log(batChaseSequenceEnder.endedChaseSequence);
    }

    private void writeToJSON()
    {
        

        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "bossChaseBatRelated.txt") == false)
        {
            File.Create(Application.dataPath + SceneManager.GetActiveScene().name + "bossChaseBatRelated.txt").Dispose();
        }

        //Only save if the text value in the file is false, if its true we dont need to do any more saves
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "bossChaseBatRelated.txt"))
        {
            string[] batChaseJSONS = File.ReadAllLines(Application.dataPath + SceneManager.GetActiveScene().name + "bossChaseBatRelated.txt");

            if (new FileInfo(Application.dataPath + SceneManager.GetActiveScene().name + "bossChaseBatRelated.txt").Length != 0)
            {

                batBossChaseInformation batChaseInf = JsonUtility.FromJson<batBossChaseInformation>(batChaseJSONS[0]);


                if (batChaseInf.finishedBatRoute == true)
                {
                    Debug.Log("saved the info YOOOOOOOOOOOOOOOOO");

                    batBossChaseInformation batInf = new batBossChaseInformation();

                    batInf.finishedBatRoute = batChaseSequenceEnder.endedChaseSequence;

                    string batJSON = JsonUtility.ToJson(batInf);

                    File.WriteAllText(Application.dataPath + SceneManager.GetActiveScene().name + "bossChaseBatRelated.txt", batJSON);
                }

            }

            //If its the first time writing to the file
            else
            {
                Debug.Log("saved the info YOOOOOOOOOOOOOOOOO ELSEEEEEEEEEEEEEEEEEEEEEEEE");

                batBossChaseInformation batBossChaseInf = new batBossChaseInformation();

                batBossChaseInf.finishedBatRoute = batChaseSequenceEnder.endedChaseSequence;

                string batBossJSON = JsonUtility.ToJson(batBossChaseInf);

                File.WriteAllText(Application.dataPath + SceneManager.GetActiveScene().name + "bossChaseBatRelated.txt", batBossJSON);
            }

        }
        

    }

    private void handleSceneSwap()
    {
        //---------------------Chase room to sewage--------------------
        if (this.gameObject.name == "entryTosewageWithFishFromBatChase")
        {
            sceneSwapHolder.enteredWay = "entryTosewageWithFishFromBatChase";
            sceneToLoad = "sewageWithFish";
            if (startedFadeRoutine == false)
            {
                StartCoroutine(fadeScreenRoutine());
            }

        }
        

        if (this.gameObject.name == "entryTobatBossRoomFrombossChaseBat")
        {
            sceneSwapHolder.enteredWay = "entryTobatBossRoomFrombossChaseBat";
            sceneToLoad = "batBossRoom";
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
