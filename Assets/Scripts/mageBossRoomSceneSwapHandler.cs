using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using System;
using System.IO;

public class mageBossRoomSceneSwapHandler : MonoBehaviour
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

    [Serializable]
    public class mageBossInformation
    {

        public bool greenGemWasPicked;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void writeToJSON()
    {



        // CREATING RELEVANT FILES FOR THE FIRST TIME FOR EACH OF THE NEEDED LISTS
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "mageBossRelated.txt") == false)
        {
            File.Create(Application.dataPath + SceneManager.GetActiveScene().name + "mageBossRelated.txt").Dispose();
        }

        // if the boss was not killed, we can save states, if the boss is killed, we shouldnt

        // check the file if it exists and the boss was killed, make sure to not save wrong values...
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "mageBossRelated.txt"))
        {

            string[] mageBossJSONS = File.ReadAllLines(Application.dataPath + SceneManager.GetActiveScene().name + "mageBossRelated.txt");

            //Check if the file isnt empty first ,f it isnt, we can save.
            //Naming can be confgusing , one is batbossJSON other is batbossJSONS with an S
            if (new FileInfo(Application.dataPath + SceneManager.GetActiveScene().name + "mageBossRelated.txt").Length != 0)
            {

                mageBossInformation mageBossInfObj = JsonUtility.FromJson<mageBossInformation>(mageBossJSONS[0]);


                if (mageBossInfObj.greenGemWasPicked == false)
                {
                    mageBossInformation mageBossInf = new mageBossInformation();

                    mageBossInf.greenGemWasPicked = astroBuddyStaticClass.astroPickedGreenGem;

                    string mageBossJSON = JsonUtility.ToJson(mageBossInf);

                    File.WriteAllText(Application.dataPath + SceneManager.GetActiveScene().name + "mageBossRelated.txt", mageBossJSON);
                }

            }
            //If its the first time writing to the file
            else
            {
                mageBossInformation mageBossInf = new mageBossInformation();

                mageBossInf.greenGemWasPicked = astroBuddyStaticClass.astroPickedGreenGem;

                string mageBossJSON = JsonUtility.ToJson(mageBossInf);

                File.WriteAllText(Application.dataPath + SceneManager.GetActiveScene().name + "mageBossRelated.txt", mageBossJSON);
            }



        }


    }

    private void handleSceneSwap()
    {
        //---------------------SEWAGE TO PRISON--------------------
        if (this.gameObject.name == "entryToruinedKingdomLibraryFrommageBossRoom")
        {
            sceneSwapHolder.enteredWay = "entryToruinedKingdomLibraryFrommageBossRoom";
            sceneToLoad = "ruinedKingdomLibrary";
            if (startedFadeRoutine == false)
            {
                StartCoroutine(fadeScreenRoutine());
            }
            

        }

        if (this.gameObject.name == "bluePortalToHeart")
        {
            sceneSwapHolder.enteredWay = "bluePortalToHeart";
            sceneToLoad = "worldsHeart";
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
