using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class outsideLastCutsceneHandler : MonoBehaviour
{
    //public reference to the snowspirit
    public GameObject snowSpirit;

    //Reference to the scenes camera
    public Camera sceneCamera;
    public GameObject cameraHolder;
    public Transform sceneCameraPoint;



    //Text showing related
    
    private string givenText;
    public TMPro.TextMeshProUGUI relatedText;
    public GameObject textBackground;
    public GameObject eIndicator;

    private string currentString = "";


    //Bools to check if certain texts were shown (SpiritText)
    private bool firstTextWasShown;
    private bool secondTextWasShown;
    private bool thirdTextWasShown;
    private bool fourthTextWasShown;
    private bool fifthTextWasShown;
    private bool sixthTextWasShown;
    private bool seventhTextWasShown;
    private bool eigthTextWasShown;
    private bool ninthTextWasShown;
    private bool tenthTextWasShown;
    private bool eleventhTextWasShown,twelthTextWasShown,thirteenthTextWasShown,fourteenthTextWasShown,sixteenthTextWasShown;


    //Coroutine bools
    private bool startedShowTextRoutine;


    //reveal gem related
    public GameObject revealedGem;
    public GameObject gemRevealParticles;

    private bool startedRevealGemRoutine;
    private float revealGemTimer = 3f;
    private float revealGemCounter;


    //Public float for text Times
    public float textTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {
            //allow player to not move
            collision.GetComponentInParent<Rigidbody2D>().velocity = Vector2.zero;
            collision.GetComponentInParent<playerMovement>().enabled = false;

            sceneCamera.GetComponent<cameraFollow>().enabled = false;
            cameraHolder.transform.position = sceneCameraPoint.position;

            sceneCamera.orthographicSize = 55f;


            //Show text when player arrives

            StartCoroutine(showText("You have made it all this way...", 0.01f, false));
            
        }
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(startedVoidBackgroundRoutine);


        // After each click, set the text to ""
        if (Input.GetKeyDown(KeyCode.E) && Time.timeScale != 0)
        {

           

           

            //Text coroutines
            if (startedShowTextRoutine == false && firstTextWasShown == false)
            {
                relatedText.text = "";

                StartCoroutine(showText("But you seem... Different...", textTimer, false));
                firstTextWasShown = true;
            }
            if (startedShowTextRoutine == false && secondTextWasShown == false)
            {
                relatedText.text = "";

                StartCoroutine(showText("I can feel that you are not from around here...", textTimer, false));
                secondTextWasShown = true;
            }
            if (startedShowTextRoutine == false && thirdTextWasShown == false)
            {
                relatedText.text = "";

                StartCoroutine(showText("Although you look like the old inhabitants of the Ruined Kingdom...", textTimer, false));
                thirdTextWasShown = true;
            }
            if (startedShowTextRoutine == false && fourthTextWasShown == false)
            {
                relatedText.text = "";

                StartCoroutine(showText("Something seperates you from them...", textTimer, false));
                fourthTextWasShown = true;
            }
            if (startedShowTextRoutine == false && fifthTextWasShown == false)
            {
                relatedText.text = "";

                StartCoroutine(showText("Either way, you have proven yourself... Mind...Body...Soul...", textTimer, false));
                fifthTextWasShown = true;
            }
            if (startedShowTextRoutine == false && sixthTextWasShown == false)
            {
                relatedText.text = "";

                StartCoroutine(showText("The Gem of Winter is being sought by ones who plan to misuse it...", textTimer, false));
                sixthTextWasShown = true;
            }
            if (startedShowTextRoutine == false && seventhTextWasShown == false)
            {
                relatedText.text = "";

                StartCoroutine(showText("It must not get into the wrong hands... ", textTimer, false));
                seventhTextWasShown = true;
            }
            if (startedShowTextRoutine == false && eigthTextWasShown == false)
            {
                relatedText.text = "";

                StartCoroutine(showText("Along with the three other gems, it can cause devastation... ", textTimer, false));
                eigthTextWasShown = true;
            }
            if (startedShowTextRoutine == false && ninthTextWasShown == false)
            {
                relatedText.text = "";

                StartCoroutine(showText("You must safeguard it, it is no longer safe under my protection... ", textTimer, false));
                ninthTextWasShown = true;

                //Gem coroutine
                if (startedRevealGemRoutine == false)
                {
                    StartCoroutine(revealGem());
                }

            }
            if (startedShowTextRoutine == false && tenthTextWasShown == false)
            {
                relatedText.text = "";

                StartCoroutine(showText("Take it and make your way to the Ruined Kingdom, you will find what you need there... ", textTimer, false));
                tenthTextWasShown = true;

            }

            if (startedShowTextRoutine == false && eleventhTextWasShown == false)
            {
                
                //Start the void portal opening routine
                if(startedVoidBackgroundRoutine == false)
                {
                    StartCoroutine(voidBackgroundRoutine());
                }


                relatedText.text = "";

                StartCoroutine(showText("SO RECKLESS... SO FOOLISH... ", textTimer, true));
                eleventhTextWasShown = true; 
                
            }

            if (startedShowTextRoutine == false && twelthTextWasShown == false && finishedWalkingFromPortal == true && enabledBackground == true)
            {
                Debug.Log("we came here with " + startedVoidBackgroundRoutine);
                relatedText.text = "";

                StartCoroutine(showText("No... It is too late... TAKE THE GEM AND RUN, I WILL HOLD HIM OFF...", textTimer, false));
               


                if (startedShieldDestroyRoutine == false)
                {
                    StartCoroutine(voidLordDestroyShieldRoutine());
                }

                twelthTextWasShown = true;

            }

            if (startedShowTextRoutine == false && thirteenthTextWasShown == false && shieldWasDestroyed == true)
            {


                relatedText.text = "";

                StartCoroutine(showText("YOU ARE WEAK... THE MASTER WILL HAVE ALL THE GEMS... ALL WILL END...", textTimer, true));
                thirteenthTextWasShown = true;

            }
            
            if (startedShowTextRoutine == false && fourteenthTextWasShown == false && thirteenthTextWasShown == true)
            {


                relatedText.text = "";

                StartCoroutine(showText("It all makes sense now... You are our only hope...", textTimer, false));
                fourteenthTextWasShown = true;

                if(startedSpiritSavesAstroRoutine == false)
                {
                    StartCoroutine(spiritSavesAstrobuddyRoutine());
                }
                

            }

            if (startedShowTextRoutine == false && sixteenthTextWasShown == false && completedSpiritSavesRoutine == true)
            {


                relatedText.text = "";

                StartCoroutine(showText("I WILL BE TAKING THE GEM, AND YOU WILL BE DYING...", textTimer, true));
                sixteenthTextWasShown = true;

                if(startedVoidLordKillsSpiritRoutine == false)
                {
                    StartCoroutine(voidLordKillsSpiritRoutine());
                }

            }

            if(voidLordAnimatorScript.finishedKillSpirit == true)
            {

                relatedText.text = "";
                textBackground.SetActive(false);
                eIndicator.SetActive(false);


                if (startedTeleportPlayerRoutine == false)
                {
                    StartCoroutine(teleportPlayerRoutine());
                }

            }
            
           
            









        }

    }

    //Reveal the winter gem along with its particles
    private IEnumerator revealGem()
    {

        startedRevealGemRoutine = true;
        
        
        revealedGem.SetActive(true);

        SpriteRenderer gemSR = revealedGem.GetComponent<SpriteRenderer>();
        Color gemStartColor = gemSR.color;

        while (revealGemCounter <= revealGemTimer)
        {

            gemSR.color = new Color(gemStartColor.r, gemStartColor.g, gemStartColor.b, Mathf.Lerp(0f, 1f, revealGemCounter / revealGemTimer));

            revealGemCounter += Time.deltaTime;
            yield return null;

        }

        gemRevealParticles.SetActive(true);


    }


    //Showing text to the player
    private IEnumerator showText(string givenString , float delay , bool isVoidText)
    {
        //if it is a void text, show the
        if(isVoidText == true)
        {
            relatedText.color = new Color(0.52f, 0f , 0.47f);
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

        
        startedShowTextRoutine = false;
    }
    //------------------------------------------------------//
    //-----------------Void Background Spawn--------------------//
    //------------------------------------------------------//
    private bool startedVoidBackgroundRoutine;

    private float voidBackgroundCounter;
    private float voidBackgroundTimer = 2f;
    
    public GameObject voidBackground;
    public GameObject voidPortal;

    private bool enabledBackground;
    //Coroutine to setup the void background alpha
    private IEnumerator voidBackgroundRoutine()
    {
        

        startedVoidBackgroundRoutine = true;

        SpriteRenderer backgroundSR = voidBackground.GetComponent<SpriteRenderer>();

        while(voidBackgroundCounter <= voidBackgroundTimer)
        {

            backgroundSR.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, voidBackgroundCounter / voidBackgroundTimer));

            voidBackgroundCounter += Time.deltaTime;



            yield return null;
            

        }

        startedVoidBackgroundRoutine = false;

        //set portal to true once the background is done
        voidPortal.SetActive(true);

        enabledBackground = true;

        if (startedvoidLordWalkFromPortalRoutine == false)
        {
            StartCoroutine(voidLordWalkFromPortalRoutine());
        }

    }
    //------------------------------------------------------//
    //-------------------Void Lord Walk From Portal-------------//
    //------------------------------------------------------//
    private bool startedvoidLordWalkFromPortalRoutine;

    private float voidLordWalkTimer = 5f;
    private float voidLordWalkCounter;

    public GameObject voidLord;
    public GameObject pointToWalkTo;
   

    private bool finishedWalkingFromPortal;

 
    private IEnumerator voidLordWalkFromPortalRoutine()
    {

        snowSpirit.transform.Rotate(0, 180, 0);
       

        voidLord.SetActive(true);
        voidLord.GetComponent<Animator>().SetBool("startedWalking", true);

        startedvoidLordWalkFromPortalRoutine = true;

        Vector3 startPos = voidLord.transform.position;

        while (voidLordWalkCounter <= voidLordWalkTimer)
        {

            voidLord.transform.position = Vector3.Lerp(startPos,new Vector3(pointToWalkTo.transform.position.x , startPos.y,startPos.z), voidLordWalkCounter / voidLordWalkTimer);

            voidLordWalkCounter += Time.deltaTime;

            yield return null;
        }

        startedvoidLordWalkFromPortalRoutine = false;

        

        voidLord.GetComponent<Animator>().SetBool("startedWalking", false);

        finishedWalkingFromPortal = true;



    }

    //------------------------------------------------------//
    //-------------------Void Lord BLOWUP SHIELD ATTACK-------------//
    //------------------------------------------------------//
    private bool startedShieldDestroyRoutine;

    private float destroyShieldTimer = 5f;
    private float destroyShieldCounter;

    public GameObject beamObject;

    public GameObject shieldParticleSystem;
    public GameObject shieldShatterParticleSystem;
    public GameObject shieldSpriteMask;

    private bool shieldWasDestroyed;
    private IEnumerator voidLordDestroyShieldRoutine()
    {



        startedShieldDestroyRoutine = true;
        shieldParticleSystem.SetActive(true);

        voidLord.GetComponent<Animator>().SetBool("startedCasting", true);
        

        while (destroyShieldCounter <= destroyShieldTimer)
        {
            if(beamObject.activeInHierarchy == false && destroyShieldCounter >= 1)
            {
                beamObject.SetActive(true);
            }

            destroyShieldCounter += Time.deltaTime;

            yield return null;
        }

        shieldParticleSystem.SetActive(false);
        shieldShatterParticleSystem.SetActive(true);
        shieldSpriteMask.SetActive(false);

        beamObject.SetActive(false);


        voidLord.GetComponent<Animator>().SetBool("startedCasting", false);

        shieldWasDestroyed = true;

        startedShieldDestroyRoutine = false;


    }



    //------------------------------------------------------------------//
    //-------------------Snow Spirit Saves Astrobuddy-----------------//
    //------------------------------------------------------------------//

    private bool startedSpiritSavesAstroRoutine;
    public GameObject savingPortalParticleSystem;

    private bool completedSpiritSavesRoutine;

    private float spiritSavingTimer = 3f;
    private float spiritSavingCounter;


    private IEnumerator spiritSavesAstrobuddyRoutine()
    {

        startedSpiritSavesAstroRoutine = true;
        savingPortalParticleSystem.SetActive(true);

        GameObject playerObj = GameObject.Find("Astrobuddy");
        SpriteRenderer playerSpriteRenderer = playerObj.GetComponent<SpriteRenderer>();

        savingPortalParticleSystem.transform.position = new Vector2(playerObj.transform.position.x, playerObj.transform.position.y + 1f);

        while (spiritSavingCounter <= spiritSavingTimer)
        {

            

            playerSpriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Lerp(1f, 0f, spiritSavingCounter / spiritSavingTimer));

            spiritSavingCounter += Time.deltaTime;

            yield return null;

        }

        completedSpiritSavesRoutine = true;
    }

    //------------------------------------------------------------------//
    //-------------------Void Lord Kills Snow Spirit--------------------//
    //------------------------------------------------------------------//
    private bool startedVoidLordKillsSpiritRoutine;


    private IEnumerator voidLordKillsSpiritRoutine()
    {

        startedVoidLordKillsSpiritRoutine = true;


        voidLord.GetComponent<Animator>().SetBool("startedMeeleAttack", true);

        yield return null;



    }


    //------------------------------------------------------------------//
    //-------------------Player gets teleported-------------------------//
    //------------------------------------------------------------------//
    private bool startedTeleportPlayerRoutine;

    private float teleportTimer = 10f;
    private float teleportCounter;

    public GameObject teleportCameraPoint;
    public GameObject astroClone;


    private IEnumerator teleportPlayerRoutine()
    {
        startedTeleportPlayerRoutine = true;

        cameraHolder.transform.position = teleportCameraPoint.transform.position;
        sceneCamera.orthographicSize = 20f;

        SpriteRenderer astroCloneRen = astroClone.GetComponent<SpriteRenderer>();

        Vector2 startPos = astroClone.transform.position;

        while (teleportCounter <= teleportTimer)
        {

            astroCloneRen.color = new Color(1f, 1f, 1f, Mathf.Lerp(1f, 0f, teleportCounter / teleportTimer));
            
            astroClone.transform.Rotate(Vector3.back, 100f * Time.deltaTime);

            teleportCounter += Time.deltaTime;

            yield return null;

        }
        

        //set entered place
        sceneSwapHolder.enteredWay = "outsideLastTeleportation";

        //send to ruined kingdom entry
        SceneManager.LoadScene("ruinedKingdomEntry");

       
    }

    




}
