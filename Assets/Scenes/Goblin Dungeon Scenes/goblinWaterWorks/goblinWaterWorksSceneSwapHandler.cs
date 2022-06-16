using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

using System.IO;

public class goblinWaterWorksSceneSwapHandler : MonoBehaviour
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

    public List<GameObject> elevatorList;

    //Water puzzle and water to close
    public GameObject waterHolder;
    //public GameObject waterDigitPuzzle;



    //STRINGS TO HOLD THE FULL JSONS FORMATTING
    private string fullElevatorJson;

   



    //Private bool to check if we created the savefile once atleast
    private bool createdSave = false;


    //Classes that hold the relevant information for given objects
    [Serializable]
    public class elevatorInformation
    {

        public string elevatorName;

        public float elevatorXpos;
        public float elevatorYpos;     

    }

    [Serializable]
    public class waterPuzzleInformation
    {

        public bool puzzleSolved;    

    }

    

    // Writing relevant information to JSON files
    private void writeToJSON()
    {


        // CREATING RELEVANT FILES FOR THE FIRST TIME FOR EACH OF THE NEEDED LISTS
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "elevatorList.txt") == false)
        {
            File.Create(Application.dataPath + SceneManager.GetActiveScene().name + "elevatorList.txt").Dispose();

        }


        //Saving for the first time for elevator related work

        foreach (GameObject elevatorObj in elevatorList)
        {

            Debug.Log("Adding them");

            elevatorInformation elevatorInf = new elevatorInformation();

            elevatorInf.elevatorName = elevatorObj.gameObject.name;

            elevatorInf.elevatorXpos = elevatorObj.transform.position.x;
            elevatorInf.elevatorYpos = elevatorObj.transform.position.y;



            string elevatorJSON = JsonUtility.ToJson(elevatorInf);

            fullElevatorJson += elevatorJSON + "\n";

            

        }
        //writing all text to the json
        File.WriteAllText(Application.dataPath + SceneManager.GetActiveScene().name + "elevatorList.txt", fullElevatorJson);
     



        //Water related work
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "waterPuzzleList.txt") == false)
        {
            File.Create(Application.dataPath + SceneManager.GetActiveScene().name + "waterPuzzleList.txt").Dispose();
        }
        waterPuzzleInformation waterPuzzleInf = new waterPuzzleInformation();
        waterPuzzleInf.puzzleSolved = puzzleButtons.waterPuzzleSolved;

        string waterPuzzleJSON = JsonUtility.ToJson(waterPuzzleInf);

        File.WriteAllText(Application.dataPath + SceneManager.GetActiveScene().name + "waterPuzzleList.txt", waterPuzzleJSON + "\n");

        //



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
        //---------------------WATERWORKS TO BRIDGE--------------------
        if (this.gameObject.name == "entryTobrokenBridgeFromgoblinWaterWorks")
        {
            sceneSwapHolder.enteredWay = "entryTobrokenBridgeFromgoblinWaterWorks";
            sceneToLoad = "brokenBridge";
            if (startedFadeRoutine == false)
            {
                StartCoroutine(fadeScreenRoutine());
            }
        }

        //---------- WATERWORKS TO SEWAGE WITH FISH --------------

        if (this.gameObject.name == "entryToSewageWithFishFromWaterWorks")
        {
            sceneSwapHolder.enteredWay = "entryToSewageWithFishFromWaterWorks";
            sceneToLoad = "sewageWithFish";
            if (startedFadeRoutine == false)
            {
                StartCoroutine(fadeScreenRoutine());
            }
        }
        
        //--------- WATERWORKS TO CITY------------

        if (this.gameObject.name == "entryTogoblinCityFromwaterWorks")
        {
            sceneSwapHolder.enteredWay = "entryTogoblinCityFromwaterWorks";
            sceneToLoad = "goblinCity";
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
