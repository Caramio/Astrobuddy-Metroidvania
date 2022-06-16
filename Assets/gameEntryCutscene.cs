using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameEntryCutscene : MonoBehaviour
{
    private GameObject playerObj;


    public Camera sceneCamera;
    public GameObject cameraHolder;

    public Transform cameraMiddlePoint;

    private bool firstTextWasShown, secondTextWasShown, thirdTextWasShown, fourthTextWasShown, fifthTextWasShown,sixthTextWasShown,seventhTextWasShown,eigthTextWasShown;

    void Start()
    {
        var playerObjTry = GameObject.Find("Astrobuddy");

        if (playerObjTry != null)
        {
            playerObj = playerObjTry;


        }

        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        sceneCamera.GetComponent<cameraFollow>().enabled = false;
        sceneCamera.orthographicSize = 200f;
        cameraHolder.transform.position = cameraMiddlePoint.position;

        if(startedTextCooldownRoutine == false && startedShowTextRoutine == false &&firstTextWasShown == false)
        {
            StartCoroutine(textCooldownRoutine("Planet Earth : Year 2563"));
            firstTextWasShown = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (startedTextCooldownRoutine == false &&startedShowTextRoutine == false && secondTextWasShown == false)
        {
            StartCoroutine(textCooldownRoutine("The research on time gates are finally coming to an end"));
            secondTextWasShown = true;
        }

        if (startedTextCooldownRoutine == false && startedShowTextRoutine == false && thirdTextWasShown == false)
        {
            StartCoroutine(textCooldownRoutine("After decades, the researchers at Astrocorp have finally come close to a working product"));
            thirdTextWasShown = true;
        }

        if (startedTextCooldownRoutine == false && startedShowTextRoutine == false && fourthTextWasShown == false)
        {
            StartCoroutine(textCooldownRoutine("After countless tests on lab rats, they managed to send them back in time and record historic events by using an attached camera drone"));
            fourthTextWasShown = true;
        }

        if (startedTextCooldownRoutine == false && startedShowTextRoutine == false && fifthTextWasShown == false)
        {
            StartCoroutine(textCooldownRoutine("But they didn't want to send rats back in time, they wanted to see if a human being can be sent and not lose its mind"));
            fifthTextWasShown = true;
        }

        if (startedTextCooldownRoutine == false && startedShowTextRoutine == false && sixthTextWasShown == false)
        {
            StartCoroutine(textCooldownRoutine("So they announced that they were looking for a human that is ready to risk it all for science"));
            sixthTextWasShown = true;
        }

        if (startedTextCooldownRoutine == false && startedShowTextRoutine == false && seventhTextWasShown == false)
        {
            StartCoroutine(textCooldownRoutine("Thats when you, the unwanting hero of The World arrives..."));
            seventhTextWasShown = true;

           
        }

        //once all text ends
        if (startedTextCooldownRoutine == false && startedShowTextRoutine == false && seventhTextWasShown == true)
        {
            if (startedFadeRoutine == false)
            {
                StartCoroutine(fadeOut());
            }

        }


        


        //once all text is done

    }



    //changing the text
    public TMPro.TextMeshProUGUI relatedText;
    //public GameObject textBackground;

    private bool startedShowTextRoutine;
    private string currentString = "";
    private int textNumberTracker = 0;

    private IEnumerator showText(string givenString, float delay)
    {
       

        //textBackground.SetActive(true);

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
   

        currentString = "";

        textNumberTracker += 1;

        startedShowTextRoutine = false;
    }



    //show text after some time
    private bool startedTextCooldownRoutine;  

    private float textCooldownTimer = 1f;
    private float textCooldownCounter;
    private IEnumerator textCooldownRoutine(string textToShowNext)
    {

        startedTextCooldownRoutine = true;


        while(textCooldownCounter <= textCooldownTimer)
        {

            textCooldownCounter += Time.deltaTime;
            yield return null;
        }

        textCooldownCounter = 0f;
        startedTextCooldownRoutine = false;

        if (startedShowTextRoutine == false)
        {
            StartCoroutine(showText(textToShowNext , 0.005f));
        }

    }


    //fade out after texts
    private bool startedFadeRoutine;

    private float fadeTimer = 5f;
    private float fadeCounter;


    public GameObject darkBackground;
    private IEnumerator fadeOut()
    {

        startedFadeRoutine = true;

        SpriteRenderer darkbckSprite = darkBackground.GetComponent<SpriteRenderer>();

        while (fadeCounter <= fadeTimer)
        {

            darkbckSprite.color = new Color(0f, 0f, 0f, Mathf.Lerp(1f, 0f, fadeCounter / fadeTimer));


            fadeCounter += Time.deltaTime;

            yield return null;

        }


        sceneCamera.GetComponent<cameraFollow>().enabled = true;
        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        playerObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -1f));


    }
}
