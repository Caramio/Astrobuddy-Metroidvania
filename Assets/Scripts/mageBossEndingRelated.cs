using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mageBossEndingRelated : MonoBehaviour
{
    private GameObject playerObj;

    public Camera sceneCamera;

    // Start is called before the first frame update
    void Start()
    {


        var playerObjTry = GameObject.Find("Astrobuddy");

        if (playerObjTry != null)
        {
            playerObj = playerObjTry;


        }

    }

    

  

    



    //showtext parameters - text - timerbetweenwords - isred/isblue/ispurple text -
    private void OnEnable()
    {
        if (startedShowTextRoutine == false && firstTextWasShown == false)
        {
            StartCoroutine(showText("Impressive, you managed to survive our combined assault...", 0.001f, false,false,false));
            firstTextWasShown = true;
        }
    }


    private bool firstTextWasShown, secondTextWasShown, thirdTextWasShown, fourthTextWasShown, fifthTextWasShown, sixthTextWasShown, seventhTextWasShown, eigthTextWasShown
                 , ninthTextWasShown, tenthTextWasShown, eleventhTextWasShown, twelthTextWasShown, thirteenthTextWasShown, fourteenthTextWasShown;



   

    //Portal to take the player
    public GameObject endPortal;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //txt two
            if (startedShowTextRoutine == false && secondTextWasShown == false)
            {
                StartCoroutine(showText("The heat of my flames...", 0.001f, true, false, false));
                secondTextWasShown = true;
            }

            //txt three
            if (startedShowTextRoutine == false && thirdTextWasShown == false)
            {
                StartCoroutine(showText("The raging tides of water...", 0.001f, false, true, false));
                thirdTextWasShown = true;
            }
            //txt four
            if (startedShowTextRoutine == false && fourthTextWasShown == false)
            {
                StartCoroutine(showText("The crush of gravity...", 0.001f, false, false, true));
                fourthTextWasShown = true;
            }
            //txt five
            if (startedShowTextRoutine == false && fifthTextWasShown == false)
            {
                StartCoroutine(showText("You have proven to us that you are proficient in combat, unlike our fallen kingdom...", 0.001f, false, false, false));
                fifthTextWasShown = true;
            }
            //txt six
            if (startedShowTextRoutine == false && sixthTextWasShown == false)
            {
                StartCoroutine(showText("Years have past since he betrayed us and planted the seeds for the voids return...", 0.001f, false, false, false));
                sixthTextWasShown = true;
            }
            //txt seven
            if (startedShowTextRoutine == false && seventhTextWasShown == false)
            {
                StartCoroutine(showText("We protected the gem from the betrayer in life and in death, but our powers waver...", 0.001f, false, false, false));
                seventhTextWasShown = true;
            }
            //txt eight
            if (startedShowTextRoutine == false && eigthTextWasShown == false)
            {
                StartCoroutine(showText("We will no longer be able to safeguard the gem, and our spirits are bound to this room...", 0.001f, false, false, false));
                eigthTextWasShown = true;
            }
            //txt nine
            if (startedShowTextRoutine == false && ninthTextWasShown == false)
            {
                StartCoroutine(showText("A familiar power emnates from you...", 0.01f, true, false, false));
                ninthTextWasShown = true;
            }
            //txt ten
            if (startedShowTextRoutine == false && tenthTextWasShown == false)
            {
                StartCoroutine(showText("You have the Gem of Fire too I see... How did you manage to take that from the King...", 0.001f, true, false, false));
                tenthTextWasShown = true;
            }
            //txt eleven
            if (startedShowTextRoutine == false && eleventhTextWasShown == false)
            {
                StartCoroutine(showText("Even more reason to trust you... You must take the Gem of Earth and head to the 'Heart of the World'...", 0.001f, false, false, false));
                eleventhTextWasShown = true;
            }
            //txt twelve
            if (startedShowTextRoutine == false && twelthTextWasShown == false)
            {
                StartCoroutine(showText("Use the power of the gems and empower yourself, that is the only way you can overcome the darkness...", 0.001f, false, false, false));
                twelthTextWasShown = true;
            }

            //if the last text was shown, reveal the gem of earth
            if(twelthTextWasShown == true && startedShowTextRoutine == false)
            {

                
                //Reveal the gem to the player             
                if (startedRevealGemRoutine == false)
                {
                    greenGemPickable.SetActive(true);
                    StartCoroutine(revealGemRoutine());
                }

                //reset screen texts
                relatedText.text = "";
                textBackground.SetActive(false);
                eIndicator.SetActive(false);

            }
           
        }

    }



    //----------------------------------------
    //Routine once the fight ends-------------
    //----------------------------------------
    private bool startedRevealGemRoutine;

    private float gemRevealTimer = 10f;
    private float gemRevealCounter;
    //----------------------------------
    public GameObject greenGemPickable;
    

    //End particles Holder that emit from the mages
    
    public ParticleSystem redParticles, blueParticles, purpleParticles;


    public ParticleSystem revealParticle;
    private IEnumerator revealGemRoutine()
    {
        //assign particle main modules
        redParticles.Play();
        blueParticles.Play();
        purpleParticles.Play();

        revealParticle.Play();
        //---

        startedRevealGemRoutine = true;



        SpriteRenderer gemSprite = greenGemPickable.GetComponent<SpriteRenderer>();

        while (gemRevealCounter <= gemRevealTimer)
        {

            gemSprite.color = new Color(gemSprite.color.r, gemSprite.color.g, gemSprite.color.b, Mathf.Lerp(0f, 1f, gemRevealCounter / gemRevealTimer));

            gemRevealCounter += Time.deltaTime;
            yield return null;



        }

        //--
        redParticles.Stop();
        blueParticles.Stop();
        purpleParticles.Stop();

        revealParticle.Stop();
        //--

        //reveal the red gem
        greenGemPickable.SetActive(true);


        //revert the camera once we are done
        sceneCamera.GetComponent<cameraFollow>().enabled = true;

    }





    //Show text to the player
    //Show text routine
    public TMPro.TextMeshProUGUI relatedText;
    public GameObject eIndicator;
    public GameObject textBackground;

    private bool startedShowTextRoutine;
    private string currentString = "";

    private int textNumberTracker = 0;

    private IEnumerator showText(string givenString, float delay, bool isRedText,bool isBlueText,bool isPurpleText)
    {
        //if it is a void text, show the
        if (isRedText == true)
        {
            relatedText.color = new Color(1f, 0f, 0f);
        }
        else if (isBlueText == true)
        {
            relatedText.color = new Color(0f, 0.2f, 1f);
        }
        else if(isPurpleText == true)
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
}
