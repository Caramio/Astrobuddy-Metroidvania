using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using System;
using System.IO;

public class prisonSceneSwapHandler : MonoBehaviour
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


    //Private bool to see if the file was created
    private bool createdFiles;






    //PERMANENT CHANGES RELATED
    public List<GameObject> doorList;


    //FULL JSON STRINGS
    private string fullDoorJSON;

    public class doorInformation
    {

        public string doorName;

        public bool shouldCloseDoor;



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

    private void writeToJSON()
    {


     

        
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "doorList.txt") == false)
        {
            //File.Create(Application.dataPath + SceneManager.GetActiveScene().name + "doorList.txt");
            File.Create((Application.dataPath + SceneManager.GetActiveScene().name + "doorList.txt")).Dispose();
        }
        


        
        {
            //Saving elevator related work
            foreach (GameObject doorObj in doorList)
            {

                Debug.Log("Adding them");

                doorInformation doorInf = new doorInformation();

                doorInf.doorName = doorObj.gameObject.name;

                doorInf.shouldCloseDoor = doorObj.activeInHierarchy;


                string doorJSON = JsonUtility.ToJson(doorInf);

                fullDoorJSON += doorJSON + "\n";



            }
           

            File.WriteAllText(Application.dataPath + SceneManager.GetActiveScene().name + "doorList.txt", fullDoorJSON);

        }
        



    }


    private void handleSceneSwap()
    {
        //---------------------SEWAGE TO PRISON--------------------
        if (this.gameObject.name == "entryToUpperSewageWithFish")
        {
            sceneSwapHolder.enteredWay = "entryToUpperSewageWithFish";
            sceneToLoad = "sewageWithFish";
            if (startedFadeRoutine == false)
            {
                StartCoroutine(fadeScreenRoutine());
            }
        }
        if (this.gameObject.name == "entryToLowerSewageWithFish")
        {
            sceneSwapHolder.enteredWay = "entryToLowerSewageWithFish";
            sceneToLoad = "sewageWithFish";
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

        //write t json on fade routine
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
