using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using System;
using System.IO;

public class goblinCitySceneSwapHandler : MonoBehaviour
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



    //
    public GameObject goblinElevator;

    public GameObject lightPuzzleObject;

    public GameObject gemHolderObject;
    


    //STRINGS TO HOLD THE FULL JSONS FORMATTING
    private string fullElevatorJson;








    //Classes that hold the relevant information for given objects
    [Serializable]
    public class elevatorInformation
    {

        public string elevatorName;

        public int elevatorFloor;

        public float elevatorXpos;
        public float elevatorYpos;

        public string doorOneName;
        public string doorTwoName;

    }

    public class lightPuzzleInformation
    {

     
        public bool doorIsOpen;

    }

    public class pickableItemInformation
    {


        public string pickableName;

        public bool pickableState;



    }




    // Writing relevant information to JSON files
    private void writeToJSON()
    {
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "lightPuzzleList.txt") == false)
        {
            File.Create(Application.dataPath + SceneManager.GetActiveScene().name + "lightPuzzleList.txt").Dispose();

        }


        // CREATING RELEVANT FILES FOR THE FIRST TIME FOR EACH OF THE NEEDED LISTS
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "elevatorList.txt") == false)
        {
            File.Create(Application.dataPath + SceneManager.GetActiveScene().name + "elevatorList.txt").Dispose();

        }


        //GOBLIN ELEVATOR RELATED
        elevatorInformation elevatorInf = new elevatorInformation();

        elevatorInf.elevatorName = goblinElevator.name;
        elevatorInf.elevatorFloor = goblinElevatorScript.elevatorFloor;
        elevatorInf.elevatorXpos = goblinElevator.transform.position.x;
        elevatorInf.elevatorYpos = goblinElevator.transform.position.y;

        string elevatorJSON = JsonUtility.ToJson(elevatorInf);

        File.WriteAllText(Application.dataPath + SceneManager.GetActiveScene().name + "elevatorList.txt", elevatorJSON);






        //LIGHT PUZZLE RELATED
        lightPuzzleInformation lightInf = new lightPuzzleInformation();

        // it is set to true if the door was opened
        lightInf.doorIsOpen = lightPuzzleCompletionChecker.goblinDoorOpened;

        string lightJSON = JsonUtility.ToJson(lightInf);

        File.WriteAllText(Application.dataPath + SceneManager.GetActiveScene().name + "lightPuzzleList.txt", lightJSON);





        //PICKABLE ITEMS RELATED
        pickableItemInformation pickableInf = new pickableItemInformation();

        pickableInf.pickableName = gemHolderObject.name;
        pickableInf.pickableState = gemHolderObject.activeInHierarchy;

        string pickableJSON = JsonUtility.ToJson(pickableInf);

        File.WriteAllText(Application.dataPath + SceneManager.GetActiveScene().name + "pickableList.txt", pickableJSON);


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
        //---------------------CITY TO WATERWORKS--------------------
        if (this.gameObject.name == "entryTowaterWorksFromCity")
        {
            sceneSwapHolder.enteredWay = "entryTowaterWorksFromCity";
            sceneToLoad = "goblinWaterWorks";
            if (startedFadeRoutine == false)
            {
                StartCoroutine(fadeScreenRoutine());
            }
        }

        //---------------------CITY TO GOBLIN KING--------------------
        if (this.gameObject.name == "entryTogoblinKingBoss")
        {

            //If the dungeon door is open
            if (lightPuzzleCompletionChecker.goblinDoorOpened)
            {
                sceneSwapHolder.enteredWay = "entryTogoblinKingBoss";
                sceneToLoad = "goblinKingBoss";
                if (startedFadeRoutine == false)
                {
                    StartCoroutine(fadeScreenRoutine());
                }
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
