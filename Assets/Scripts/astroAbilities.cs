using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.SceneManagement;

public class astroAbilities : MonoBehaviour
{
    //Public references
    public Transform attackPoint;
    public GameObject astroSword;
    public GameObject astroTorch;
    public GameObject astroWings;

    //Timers
    private float attackCooldownTimer;
    private float attackCooldownDuration = 0.5f;

    private float torchCooldownTimer;
    private float torchCooldownDuration = 0.5f;

    //Private references
    private Rigidbody2D playerBody;
        

    //booleans
    [HideInInspector]
    public bool canAttack = true;

    private bool canSwingTorch = true;

    public bool usingWings;


    //Bools for coroutines
    private bool startedGlideRoutine;


    //public static bool
    public static bool isInSnowyLevel;

    //Audio source...
    public playerAudioSource playerAudioSource;


    void Start()
    {
      

        playerBody = this.GetComponent<Rigidbody2D>();

    }

    private void OnLevelWasLoaded(int level)
    {

        string sceneName = SceneManager.GetActiveScene().name;

        // if we are in a snowy level
        if (sceneName == "outsideFirst" || sceneName == "outsideLast" || sceneName == "snowyLake" || sceneName == "cavernOfIllusions"
            || sceneName == "cavernTwo" || sceneName == "cavernThree" || sceneName == "cavernFour")
        {

            isInSnowyLevel = true;
        }
        else
        {
            isInSnowyLevel = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {


        //For gliding with the wings
        if (isInSnowyLevel == false)
        {
            useWings();
        }


        // if we have the sword equipped and movingAstro is true
        if (astroSword.activeInHierarchy && playerMovement.movingAstro)
        {

            if (canAttack)
            {
                swingSword();

            }


            if (!canAttack)
            {
                changeSwordRotation();
            }


            if (astroSword.transform.eulerAngles.z < 15f)
            {
                astroSword.transform.eulerAngles = new Vector3(astroSword.transform.eulerAngles.x, astroSword.transform.eulerAngles.y,
                  70f);
            }


        }

        // if we have the torch equipped and movingAstro is true
        if(astroTorch.activeInHierarchy && playerMovement.movingAstro)
        {

            if (canSwingTorch)
            {
                swingTorch();
            }

            if (!canSwingTorch)
            {
                changeTorchRotation();
            }

            


        }

        
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, 2.5f);

    }
    //----------------SWORD RELATED---------------
    // Swinging the sword 


    //audio related
    public AudioClip swingSwordAudio;
    private void swingSword()
    {
        //audio related
        playerAudioSource.playSwordSound();




        if (Input.GetKeyDown(controlsStaticClass.attackControl))
        {

            StartCoroutine(attackAbilityDelay());
            canAttack = false;


            
            Collider2D[] hitEnemiesArray = Physics2D.OverlapCircleAll(attackPoint.position, 2.5f);

            foreach (Collider2D collider in hitEnemiesArray)
            {

                // If the hit target is a mushroomMan
                if(collider.GetComponent<mushroomMobStates>() != null)
                {

                    collider.GetComponent<mushroomMobStates>().takeDamage();
                }

                if(collider.GetComponent<goblinBossEyeStates>() != null)
                {
                    collider.GetComponent<goblinBossEyeStates>().eyeTakeDamage();
                }

                

                if(collider.GetComponent<batBossHitbox>() != null)
                {
                    collider.GetComponentInParent<batBossStates>().takeDamage();
                }

               if(collider.GetComponent<enemyHealth>() != null)
                {
                    collider.GetComponent<enemyHealth>().takeDamage();
                }





                // if the hitTarget is a sword-hittable secret layer

                if(collider.GetComponent<hiddenLayerEnabler>() != null)
                {

                    collider.GetComponent<hiddenLayerEnabler>().accessLayer();

                }

                
                
            }

            
        }


    }

    private void changeSwordRotation()
    {

        astroSword.transform.eulerAngles = new Vector3(astroSword.transform.eulerAngles.x, astroSword.transform.eulerAngles.y,
               Mathf.Lerp(70, 5, attackCooldownTimer * 10f));

    }



    private void useWings()
    {
        if (this.GetComponent<astroInteraction>().pickedAstroWings == true || astroBuddyStaticClass.astroHasWings == true)
        {
            if (Input.GetKey(controlsStaticClass.specialOneControl))
            {
                //play wings sound
                playerAudioSource.playWingsSound();

                usingWings = true;

                if (startedGlideRoutine == false)
                {
                    StartCoroutine(glideWithWings());
                }

                /*
                astroWings.SetActive(true);
                //playerBody.velocity = new Vector2(playerBody.velocity.x, 0f);
                playerBody.gravityScale = 0f;
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 0.05f);
                */

            }

            if (Input.GetKeyUp(controlsStaticClass.specialOneControl))
            {

                playerAudioSource.stopWingsSound();

                usingWings = false;

            }
        }
        
    }

    IEnumerator attackAbilityDelay()
    {

        

        while (attackCooldownTimer <= attackCooldownDuration)
        {
            attackCooldownTimer += Time.deltaTime;

            yield return null;
        }

        canAttack = true;

        
        attackCooldownTimer = 0f;

    }


    
    //Change later
    private float usingCounter = 0f;
    private float timeToLower = 2f;

    public static bool recentlyAppliedJumpForce;

    //Wings routine
    private IEnumerator glideWithWings()
    {
       

        startedGlideRoutine = true;



        astroWings.SetActive(true);

        //playerBody.velocity = new Vector2(playerBody.velocity.x, 0f);


        float startYvel = playerBody.velocity.y;


        while (usingWings == true && usingCounter <= timeToLower)
        {
            

            if (recentlyAppliedJumpForce == false)
            {
                playerBody.velocity = new Vector2(playerBody.velocity.x, -1f);
            }
            else
            {
                if(startedJumpforcecooldownRoutine == false) 
                { 
                StartCoroutine(addJumpForceCooldown());
                }


            }
            

            //playerBody.velocity = new Vector2(playerBody.velocity.x, Mathf.Lerp(startYvel, -1f , usingCounter/timeToLower));

            yield return null;
        }
        // reset once the button is unpressed
        

        startYvel = 0;

        usingCounter = 0f;

        startedGlideRoutine = false;

        astroWings.SetActive(false);

        playerBody.gravityScale = 10f;
    }

    private bool startedJumpforcecooldownRoutine;

    //-----remove recentlyappliedjumpforce after a cooldown
    private IEnumerator addJumpForceCooldown()
    {
        startedJumpforcecooldownRoutine = true;

        while(playerBody.velocity.y >= -1f)
        {
            
            yield return null;
        }
        recentlyAppliedJumpForce = false;

        startedJumpforcecooldownRoutine = false;
    }


    //-----------------------------------//
    //-----------TORCH RELATED-------------
    //-----------------------------------//

    private void changeTorchRotation()
    {

        astroTorch.transform.eulerAngles = new Vector3(astroSword.transform.eulerAngles.x, astroSword.transform.eulerAngles.y,
               Mathf.Lerp(-30, -80, torchCooldownTimer * 2f));

    }



    IEnumerator torchAbilityDelay()
    {



        while (torchCooldownTimer <= torchCooldownDuration)
        {
            torchCooldownTimer += Time.deltaTime;

            yield return null;
        }

        astroTorch.transform.eulerAngles = new Vector3(astroTorch.transform.eulerAngles.x, astroTorch.transform.eulerAngles.y,
                  -30f);

        canSwingTorch = true;


        torchCooldownTimer = 0f;

    }
    // Swinging torch
    private void swingTorch()
    {



        if (Input.GetMouseButtonDown(0))
        {

            StartCoroutine(torchAbilityDelay());
            canSwingTorch = false;

            Debug.Log("swinging");

            Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, 2.5f);

            foreach (Collider2D collider in hitObjects)
            {

                if(collider.GetComponent<burnableObject>() != null)
                {
                    // If the colliders name contains flammable , this means that it should be destroyed when the torch touches it.
                    if (collider.gameObject.name.Contains("flammable"))
                    {
                        collider.GetComponent<burnDestruct>().enabled = true;
                    }

                    
                }

                if(collider.GetComponent<lightableObject>() != null)
                {

                    if (collider.gameObject.name.Contains("unlit"))
                    {
                        collider.GetComponent<UnityEngine.Rendering.Universal.Light2D>().enabled = true;
                        collider.GetComponent<lightChanger>().enabled = true;

                        collider.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
                    }

                }

                
            }


        }


    }


}
