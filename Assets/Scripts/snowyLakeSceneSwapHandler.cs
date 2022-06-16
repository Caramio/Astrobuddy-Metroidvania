using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using System;

using System.IO;

public class snowyLakeSceneSwapHandler : MonoBehaviour
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

    



    //Classes that hold the relevant information for given objects
   

    [Serializable]
    public class wheelPuzzleInformation
    {

        public bool wheelPuzzleSolvedStatus;

    }



    // Writing relevant information to JSON files
    private void writeToJSON()
    {

        // CREATING RELEVANT FILES FOR THE FIRST TIME FOR EACH OF THE NEEDED LISTS
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "wheelPuzzleList.txt") == false)
        {
            File.Create(Application.dataPath + SceneManager.GetActiveScene().name + "wheelPuzzleList.txt").Dispose();

        }


        //Saving values for the snow puzzle
        

        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "wheelPuzzleList.txt"))
        {

            string[] wheelPuzzleCheckerJSON = File.ReadAllLines(Application.dataPath + SceneManager.GetActiveScene().name + "wheelPuzzleList.txt");

            //Check if the file isnt empty first ,f it isnt, we can save.
            if (new FileInfo(Application.dataPath + SceneManager.GetActiveScene().name + "wheelPuzzleList.txt").Length != 0)
            {

                wheelPuzzleInformation wheelPuzzleCheckerObj = JsonUtility.FromJson<wheelPuzzleInformation>(wheelPuzzleCheckerJSON[0]);

                if (wheelPuzzleCheckerObj.wheelPuzzleSolvedStatus == false)
                {

                    wheelPuzzleInformation wheelPuzzleInf = new wheelPuzzleInformation();

                    wheelPuzzleInf.wheelPuzzleSolvedStatus = lakePuzzleCompletionChecker.wheelPuzzleSolved;

                    string wheelPuzzleJSON = JsonUtility.ToJson(wheelPuzzleInf);

                    File.WriteAllText(Application.dataPath + SceneManager.GetActiveScene().name + "wheelPuzzleList.txt", wheelPuzzleJSON);
                }
                

            }

            //If its the first time writing to the file
            else
            {
                wheelPuzzleInformation wheelPuzzleInf = new wheelPuzzleInformation();

                wheelPuzzleInf.wheelPuzzleSolvedStatus = lakePuzzleCompletionChecker.wheelPuzzleSolved;

                string wheelPuzzleJSON = JsonUtility.ToJson(wheelPuzzleInf);

                File.WriteAllText(Application.dataPath + SceneManager.GetActiveScene().name + "wheelPuzzleList.txt", wheelPuzzleJSON);
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
        if (Input.GetKeyDown(KeyCode.K))
        {
            File.Delete(Application.dataPath + SceneManager.GetActiveScene().name + "elevatorList.txt");
        }
    }


    private void handleSceneSwap()
    {
        //---------------------SNOWY LAKE TO OUTSIDEFIRST--------------------
        if (this.gameObject.name == "entryToOutsideFirstFromSnowyLake")
        {
            sceneSwapHolder.enteredWay = "entryToOutsideFirstFromSnowyLake";
            sceneToLoad = "outsideFirst";
            if (startedFadeRoutine == false)
            {
                StartCoroutine(fadeScreenRoutine());
            }
        }

        // ---------------------SNOWY LAKE TO CAVEROFILLUSIONS--------------------
        if (this.gameObject.name == "entryTocavernOfIllusionsFromSnowyLake")
        {
            sceneSwapHolder.enteredWay = "entryTocavernOfIllusionsFromSnowyLake";
            sceneToLoad = "cavernOfIllusions";
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
