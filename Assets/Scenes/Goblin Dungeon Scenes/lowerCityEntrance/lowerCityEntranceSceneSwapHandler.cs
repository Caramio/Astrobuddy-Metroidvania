using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

using UnityEngine.SceneManagement;

public class lowerCityEntranceSceneSwapHandler : MonoBehaviour
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



    //SAVE RELATED
    public List<GameObject> doorList;


    private string fullDoorJSON;

    //Classes that hold the relevant information for given objects
    [Serializable]
    public class doorInformation
    {
        public string doorName;

        public bool shouldCloseDoor;

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

    }
    //Save to json when needed
    private void writeToJson()
    {
        // CREATING RELEVANT FILES FOR THE FIRST TIME FOR EACH OF THE NEEDED LISTS
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "doorList.txt") == false)
        {
            File.Create(Application.dataPath + SceneManager.GetActiveScene().name + "doorList.txt").Dispose();
                      
        }

        //Saving door related work
        foreach (GameObject doorObject in doorList)
        {

            Debug.Log("Adding them");

            doorInformation doorInf = new doorInformation();

            doorInf.doorName = doorObject.gameObject.name;

            //If the door was closed while existing the scene, keep it closed
            if (doorObject.activeInHierarchy == false)
            {
                doorInf.shouldCloseDoor = true;
            }

           

            string doorJSON = JsonUtility.ToJson(doorInf);

            fullDoorJSON += doorJSON + "\n";


        }
        // WRITING FULL DOOR LIST TO THE FILE
        File.WriteAllText(Application.dataPath + SceneManager.GetActiveScene().name + "doorList.txt", fullDoorJSON);
    }


    private void handleSceneSwap()
    {
        //---------------------LOWER CITY TO BROKEN BRIDGE--------------------
        if (this.gameObject.name == "entryTobrokenBridgeFromlowerCity")
        {
            sceneSwapHolder.enteredWay = "entryTobrokenBridgeFromlowerCity";
            sceneToLoad = "brokenBridge";
            if (startedFadeRoutine == false)
            {
                StartCoroutine(fadeScreenRoutine());
            }

        }

        //---------------------LOWER CITY TO GLIDER--------------------
        if (this.gameObject.name == "entryTogliderLocationFromlowerCityEntrance")
        {
            sceneSwapHolder.enteredWay = "entryTogliderLocationFromlowerCityEntrance";
            sceneToLoad = "gliderToLowerCity";

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
        //WRITING TO JSONS SHOULD BE DONE DURING THE FADE ROUTINE
        writeToJson();

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
