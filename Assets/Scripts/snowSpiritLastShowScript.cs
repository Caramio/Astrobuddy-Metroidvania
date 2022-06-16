using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowSpiritLastShowScript : MonoBehaviour
{
    // this will determine whether or not the player can dash later on
    public static bool playerReceivedDash;

    private GameObject playerObj;

    public ParticleSystem powerParticles;


    //Shown texts
    private bool firstTextWasShown,secondTextWasShown,thirdTextWasShown;

    private int textNumberTracker = 0;

    void Start()
    {


        var playerObjTry = GameObject.Find("Astrobuddy");

        if (playerObjTry != null)
        {
            playerObj = playerObjTry;
            //after the last scene, enable it for the first timer here;
            playerObj.GetComponent<playerMovement>().enabled = true;


           

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && textNumberTracker >= 1)
        {

            if (startedShowTextRoutine == false && secondTextWasShown == false)
            {
                StartCoroutine(showText("You are the one they talked about in the prophecies...", 0.01f, false));
                secondTextWasShown = true;
            }
            if (startedShowTextRoutine == false && thirdTextWasShown == false)
            {
                StartCoroutine(showText("Take the remaining power I have, you must secure the gems...", 0.01f, false));
                thirdTextWasShown = true;

                
            }
            if(textNumberTracker == 3)
            {
                

                relatedText.text = "";
                textBackground.SetActive(false);
                eIndicator.SetActive(false);

                

                if(startedDisolveRoutine == false)
                {
                    StartCoroutine(disolveRoutine());
                }
            }
                  
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {

            collision.GetComponentInParent<playerMovement>().enabled = false;
            collision.GetComponentInParent<Rigidbody2D>().velocity = Vector2.zero;

            this.GetComponent<BoxCollider2D>().enabled = false;
           
            if (startedShowTextRoutine == false && firstTextWasShown == false)
            {
                
                StartCoroutine(showText("I don't have much time... That monster destroyed my physical form...", 0.01f, false));
                firstTextWasShown = true;
            }
           

        }
    }




    //Show text routine
    public TMPro.TextMeshProUGUI relatedText;
    public GameObject eIndicator;
    public GameObject textBackground;

    private bool startedShowTextRoutine;
    private string currentString = "";
    
    private IEnumerator showText(string givenString, float delay, bool isVoidText)
    {
        //if it is a void text, show the
        if (isVoidText == true)
        {
            relatedText.color = new Color(0.52f, 0f, 0.47f);
        }
        else
        {
            relatedText.color = new Color(0f, 0f, 0f);
        }


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

        textNumberTracker += 1;

        startedShowTextRoutine = false;
    }



    //-----------------------------------------------------//
    //-------------------Disolving the spirit using the material------------------------//
    //-----------------------------------------------------//

    private bool startedDisolveRoutine;

    private float disolveCounter;
    private float disolveTimer = 3f;
        

    private IEnumerator disolveRoutine()
    {

        startedDisolveRoutine = true;

        Material shaderMat = this.GetComponent<SpriteRenderer>().material;


        powerParticles.Play();

        while (disolveCounter <= disolveTimer)
        {

            shaderMat.SetFloat("_Fade", Mathf.Lerp(1f, 0f, disolveCounter / disolveTimer));

            yield return null;

            disolveCounter += Time.deltaTime;

        }
        //once the disolve is done, we will unlock dashing
        playerReceivedDash = true;

        //allow the player to dash
        astroBuddyStaticClass.astroHasDash = true;

        powerParticles.Stop();
        playerObj.GetComponent<playerMovement>().enabled = true;
    }
}
