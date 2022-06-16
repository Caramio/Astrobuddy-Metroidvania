using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using System;
using System.IO;

public class sewageWithFishSceneSwapHandler : MonoBehaviour
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




    //----SAVE RELATED-----
    public List<GameObject> doorList;

    public List<GameObject> pickableOnceList;



    //Full JSON STRING RELATED
    private string fullDoorJSON;
    private string fullPickableJSON;

    //Classes that hold the relevant information for given objects
    [Serializable]
    public class doorInformation
    {
        public string doorName;

        public bool shouldCloseDoor;

    }

    [Serializable]
    public class pickableInformation
    {
        public string pickableName;

        public bool shouldBeInactive;

    }



    void Start()
    {

        var playerObjTry = GameObject.Find("Astrobuddy");

        if(playerObjTry != null)
        {
            playerObj = playerObjTry;

            
        }

     
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //WRİTİNG TO JSONS
    private void writeToJson()
    {
       

        
        // CREATING RELEVANT FILES FOR THE FIRST TIME FOR EACH OF THE NEEDED LISTS
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "doorList.txt") == false)
        {
            File.Create(Application.dataPath + SceneManager.GetActiveScene().name + "doorList.txt").Dispose();

        }

        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "pickableList.txt") == false)
        {
            File.Create(Application.dataPath + SceneManager.GetActiveScene().name + "pickableList.txt").Dispose();

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
        // SAVİNG ALL THE DOOR RELATED DATA 
     
        File.WriteAllText(Application.dataPath + SceneManager.GetActiveScene().name + "doorList.txt", fullDoorJSON);


        //Saving pickable related work

        foreach (GameObject pickableObj in pickableOnceList)
        {

            pickableInformation pickableInf = new pickableInformation();

            pickableInf.pickableName = pickableObj.name;
            
            if(pickableObj.activeInHierarchy == false)
            {
                pickableInf.shouldBeInactive = true;
            }

            string pickableJSON = JsonUtility.ToJson(pickableInf);

            fullPickableJSON += pickableJSON + "\n";


        }
        //SAVING ALL THE PICKABLE RELATED DATA

        File.WriteAllText(Application.dataPath + SceneManager.GetActiveScene().name + "pickableList.txt", fullPickableJSON);

    }

    private void handleSceneSwap()
    {
        //---------------------SEWAGE TO PRISON--------------------
        if (this.gameObject.name == "entryToPrisonLower")
        {
            sceneSwapHolder.enteredWay = "entryToPrisonLower";
            sceneToLoad = "Prison";
            if (startedFadeRoutine == false)
            {
                StartCoroutine(fadeScreenRoutine());
            }
            
        }
        if (this.gameObject.name == "entryToPrisonUpper")
        {
            sceneSwapHolder.enteredWay = "entryToPrisonUpper";
            sceneToLoad = "Prison";

            if (startedFadeRoutine == false)
            {
                StartCoroutine(fadeScreenRoutine());
            }
            
        }

        if (this.gameObject.name == "entryTobossChaseBat")
        {
            sceneSwapHolder.enteredWay = "entryTobossChaseBat";
            sceneToLoad = "bossChaseBat";

            if (startedFadeRoutine == false)
            {
                StartCoroutine(fadeScreenRoutine());
            }

        }

        //entry to glider to lower city...
        if (this.gameObject.name == "entryTogliderToLowerCityFromsewageWithFish")
        {
            sceneSwapHolder.enteredWay = "entryTogliderToLowerCityFromsewageWithFish";
            sceneToLoad = "gliderToLowerCity";

            if (startedFadeRoutine == false)
            {
                StartCoroutine(fadeScreenRoutine());
            }

        }

        //entry to secret room
        if (this.gameObject.name == "entryTosecretRoom")
        {
            sceneSwapHolder.enteredWay = "entryTosecretRoom";
            sceneToLoad = "secretRoom";

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
