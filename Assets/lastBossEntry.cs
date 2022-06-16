using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lastBossEntry : MonoBehaviour
{

    public GameObject cutsceneBoss;

    public Camera sceneCamera;

    private bool firstTextWasShown, secondTextWasShown, thirdTextWasShown, fourthTextWasShown, fifthTextWasShown, sixthTextWasShown, seventhTextWasShown , lastTextWasShown;

    private GameObject playerObj;
    void Start()
    {
        var playerObjTry = GameObject.Find("Astrobuddy");

        if (playerObjTry != null)
        {
            playerObj = playerObjTry;


        }
    }

    public Transform teleportPosition;

    public GameObject preFightObj;

    // Update is called once per frame

   
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && firstTextWasShown == true && lastTextWasShown == false)
        {


            if(startedShowTextRoutine == false && secondTextWasShown == false)
            {

                StartCoroutine(showText("HOW KIND OF YOU TO BRING THOSE GEMS WITH YOU...", 0.01f));
                secondTextWasShown = true;

            }

            if (startedShowTextRoutine == false && thirdTextWasShown == false)
            {

                StartCoroutine(showText("ILL BE NEEDING THE TWO THAT YOU HAVE...", 0.01f));
                thirdTextWasShown = true;

            }

            if (startedShowTextRoutine == false && fourthTextWasShown == false)
            {

                StartCoroutine(showText("YOU HAVE PLAYED YOUR PART IN THE GRAND PLAN...", 0.01f));
                fourthTextWasShown = true;

            }

            if (startedShowTextRoutine == false && fifthTextWasShown == false)
            {

                StartCoroutine(showText("UNFORTUNATELY FOR YOU, YOU ARE NO LONGER A PART OF IT...", 0.01f));
                fifthTextWasShown = true;

            }

            if (startedShowTextRoutine == false && sixthTextWasShown == false)
            {

                StartCoroutine(showText("NOW PREPARE TO FACE THE TRUE POWER OF THE VOID...", 0.01f));
                sixthTextWasShown = true;

            }

            if(sixthTextWasShown == true && startedShowTextRoutine == false)
            {

                playerObj.transform.position = teleportPosition.position;


                preFightObj.SetActive(true);


                //close text etc
                textBackground.SetActive(false);
                eIndicator.SetActive(false);
                relatedText.text = "";

                lastTextWasShown = true;

            }




        }
      




    }


    //-----
    private float entryTimer = 3f;
    private float entryCounter;

    public Transform entryEndWalkPoint;

    private bool startedEntryCutsceneRoutine;

    public GameObject entrypurplePortal;
  
    private IEnumerator entryCutscene()
    {

        //set animation bool
        

        cutsceneBoss.SetActive(true);
        entrypurplePortal.SetActive(true);

        cutsceneBoss.GetComponent<Animator>().SetBool("isWalking", true);

        startedEntryCutsceneRoutine = true;

        Vector3 startPoint = cutsceneBoss.transform.position;

        while (entryCounter <= entryTimer)
        {


            cutsceneBoss.transform.position = Vector3.Lerp(startPoint, new Vector3(entryEndWalkPoint.transform.position.x,startPoint.y), entryCounter / entryTimer);

            entryCounter += Time.deltaTime;

            yield return null;

        }

        //set animation bool
        cutsceneBoss.GetComponent<Animator>().SetBool("isWalking", false);


        entrypurplePortal.SetActive(false);

        //Show the first text
        if(startedShowTextRoutine == false && firstTextWasShown == false)
        {

            StartCoroutine(showText("WELCOME TO THE HEART OF THE WORLD..." , 0.01f));
            firstTextWasShown = true;

        }


    }

    // show text on screen

    //Show text to the player
    //Show text routine
    public TMPro.TextMeshProUGUI relatedText;
    public GameObject eIndicator;
    public GameObject textBackground;

    private bool startedShowTextRoutine;
    private string currentString = "";

    private IEnumerator showText(string givenString, float delay)
    {
       


        eIndicator.SetActive(false);

        textBackground.SetActive(true);

        startedShowTextRoutine = true;

        while (currentString.Length < givenString.Length)
        {

            for (int i = 0; i <= givenString.Length; i++)
            {

                currentString = givenString.Substring(0, i);
                relatedText.text = currentString;

                yield return new WaitForSeconds(delay);

            }

        }

        eIndicator.SetActive(true);

        currentString = "";

        startedShowTextRoutine = false;
    }



    //trigger entry
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "PlayerHitbox")
        {
            //Player related
            collision.GetComponentInParent<playerMovement>().playerAudioSource.stopFootstepGroundSound();
            collision.GetComponentInParent<playerMovement>().enabled = false;
            collision.GetComponentInParent<astroAbilities>().enabled = false;

            //Camera Related
            sceneCamera.GetComponent<cameraFollow>().enabled = false;
            sceneCamera.orthographicSize = 55f;
            
            //start cutscene
            if(startedEntryCutsceneRoutine == false)
            {
                StartCoroutine(entryCutscene());
            }






        }
    }





    
}
