using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using System;

using System.IO;

public class goblinKingSceneSwapHandler : MonoBehaviour
{
    //Timers
    private float fadeTimer = 0.5f;
    private float fadeCounter;

    //Fadescreen
    public GameObject fadeScreen;

    //Boolean for fade routine
    private bool startedFadeRoutine;


    //reference to the player
    private GameObject playerObj;

    //private string , scene to lad
    private string sceneToLoad;



    //Permanently changeable values in the goblinWaterWorks scene
    //reference to the boss to be able to tell if we should save the game or not
    public GameObject goblinBossEye;

    public GameObject entrySceneHolder;

    public GameObject leftDoor;
    public GameObject rightDoor;





    //Classes that hold the relevant information for given objects
    [Serializable]
    public class entrySceneInformation
    {

        public bool entranceSceneActiveState;

        public string leftDoorName;
        public string rightDoorName;

        
    }



    // Writing relevant information to JSON files
    private void writeToJSON()
    {

        // We will only save if the eye health is below 0 , which means the boss is dead, saving before this is not neccessary
        if (goblinBossEye.GetComponent<goblinBossEyeStates>().eyeHealth <= 0)
        {
            // if the file isnt empty
            if (new FileInfo(Application.dataPath + SceneManager.GetActiveScene().name + "bossEntryRelated.txt").Length != 0)
            {
                // CREATING RELEVANT FILES FOR THE FIRST TIME FOR EACH OF THE NEEDED LISTS
                if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "bossEntryRelated.txt") == false)
                {
                    File.Create(Application.dataPath + SceneManager.GetActiveScene().name + "bossEntryRelated.txt").Dispose();

                }

                entrySceneInformation entryInfo = new entrySceneInformation();

                entryInfo.entranceSceneActiveState = entrySceneHolder.activeInHierarchy;

                string entranceJSON = JsonUtility.ToJson(entryInfo);

                File.WriteAllText(Application.dataPath + SceneManager.GetActiveScene().name + "bossEntryRelated.txt", entranceJSON);

            }
        }






    }




    // Start is called before the first frame update
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


    private void handleSceneSwap()
    {
        //---------------------GOBLIN KING TO CITY--------------------
        if (this.gameObject.name == "entryToGoblinCityFromGoblinKing")
        {
            sceneSwapHolder.enteredWay = "entryToGoblinCityFromGoblinKing";
            sceneToLoad = "goblinCity";
            if (startedFadeRoutine == false)
            {
                StartCoroutine(fadeScreenRoutine());
            }
        }

        
        //go outside
        if (this.gameObject.name == "entryToOutsideFromGoblinKing")
        {
            sceneSwapHolder.enteredWay = "entryToOutsideFromGoblinKing";
            sceneToLoad = "outsideFirst";
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
        //Write to the json file when we are swapping out from the scene
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
