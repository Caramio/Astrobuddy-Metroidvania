using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using System;
using System.IO;

public class ruinedKingdomEntrySceneSwapHandler : MonoBehaviour
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


    public class ruinedKingdomEntryInformation
    {

        public bool dashWasReceived;

    }

    private void writeToJSON()
    {



        // CREATING RELEVANT FILES FOR THE FIRST TIME FOR EACH OF THE NEEDED LISTS
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "ruinedKingdomEntryRelated.txt") == false)
        {
            File.Create(Application.dataPath + SceneManager.GetActiveScene().name + "ruinedKingdomEntryRelated.txt").Dispose();
        }

        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "ruinedKingdomEntryRelated.txt"))
        {

            string[] ruinedKingdomEntryJSONS = File.ReadAllLines(Application.dataPath + SceneManager.GetActiveScene().name + "ruinedKingdomEntryRelated.txt");

            //Check if the file isnt empty first ,f it isnt, we can save.
            //Naming can be confgusing , one is batbossJSON other is batbossJSONS with an S
            if (new FileInfo(Application.dataPath + SceneManager.GetActiveScene().name + "ruinedKingdomEntryRelated.txt").Length != 0)
            {

                ruinedKingdomEntryInformation ruinedKingdomEntryChecker = JsonUtility.FromJson<ruinedKingdomEntryInformation>(ruinedKingdomEntryJSONS[0]);


                if (ruinedKingdomEntryChecker.dashWasReceived == false)
                {

                    ruinedKingdomEntryInformation ruinedKingdomEntryObj = new ruinedKingdomEntryInformation();

                    ruinedKingdomEntryObj.dashWasReceived = snowSpiritLastShowScript.playerReceivedDash;


                    string ruinedKingdomEntryJSON = JsonUtility.ToJson(ruinedKingdomEntryObj);

                    File.WriteAllText(Application.dataPath + SceneManager.GetActiveScene().name + "ruinedKingdomEntryRelated.txt", ruinedKingdomEntryJSON);
                }

            }
            //If its the first time writing to the file
            else
            {
                ruinedKingdomEntryInformation ruinedKingdomEntryObj = new ruinedKingdomEntryInformation();

                ruinedKingdomEntryObj.dashWasReceived = snowSpiritLastShowScript.playerReceivedDash;

                string ruinedKingdomEntryJSON = JsonUtility.ToJson(ruinedKingdomEntryObj);

                File.WriteAllText(Application.dataPath + SceneManager.GetActiveScene().name + "ruinedKingdomEntryRelated.txt", ruinedKingdomEntryJSON);
            }



        }






    }

    private void handleSceneSwap()
    {
        //---------------------Ruined Kingdom to Deserted Town--------------------
        if (this.gameObject.name == "entryTodesertedTownFromruinedKingdomEntry")
        {
            sceneSwapHolder.enteredWay = "entryTodesertedTownFromruinedKingdomEntry";
            sceneToLoad = "desertedTown";
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
