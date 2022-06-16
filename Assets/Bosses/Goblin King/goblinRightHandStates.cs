using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class goblinRightHandStates : MonoBehaviour
{

    //Public references
    public GameObject idleProjectile;
    public GameObject rightHandIdleSpot;
    public GameObject fireGroundHolder;
    public GameObject fallingRock;

    public List<GameObject> rockShowerPoints;

    public GameObject runeLight;
    public GameObject handFire;
    public GameObject greenHandFire;

    
    //Private references
    private GameObject playerObj;


    [HideInInspector]
    public kingRightHandStates state;


    //Bools for coroutines
    private bool startedIdleProjectileRoutine;

    //------------------------
    private bool startedLightGemsRoutine;
    private bool startedBurnGroundRoutine;


    //--------------------
    private bool startedRockShowerRoutine;
    private bool startedRockLightRoutine;

    //Timers for coroutines
    private float idleProjectileTimer = 3f;
    private float idleProjectileCounter;

    //--------------------
    private float lightGemsTimer = 3f;
    private float lightGemsCounter;

    //--------------------
    private float burnGroundTimer = 5.5f;
    private float burnGroundCounter;
    //---------------------
    private float rockShowerTimer = 0.5f;
    private float rockShowerCounter;

    private float rockShowerLighterTimer = 10f;
    private float rockShowerLighterCounter;

    //Number of times the idle projectile is fired to skip to another state
    private int firedIdleProjectileCount;



    //Audio related
    private AudioSource goblinRightHandAudioSource;



    public enum kingRightHandStates
    {

        Idle,
        BurnGround,
        RockShower,
        


    }
    
    void Start()
    {
        goblinRightHandAudioSource = this.GetComponent<AudioSource>();

        playerObj = GameObject.Find("Astrobuddy");
    }

    
    void Update()
    {
        stateChanger(); 
    }



    private void stateChanger()
    {

        if(state == kingRightHandStates.Idle)
        {
            if (startedIdleProjectileRoutine == false)
            {
                StartCoroutine(fireIdleProjectileRoutine());
            }

        }

        if(state == kingRightHandStates.BurnGround)
        {

            if(startedLightGemsRoutine == false)
            {
                StartCoroutine(lightGemsRoutine());
            }

        }

        if(state == kingRightHandStates.RockShower)
        {


            if(startedRockLightRoutine == false)
            {
                StartCoroutine(rockShowerGemLight());
            }

        }


    }

    //--------------------------------------------------------------------------------------
    //---------------------ROCK SHOWER FROM ABOVE ------------------------------------------
    //--------------------------------------------------------------------------------------

    //earthquake audioclip
    public AudioClip earthquakeAudio;
    private IEnumerator rockShowerGemLight()
    {


        //audio related
        goblinRightHandAudioSource.clip = earthquakeAudio;
        goblinRightHandAudioSource.Play();
        //----

        startedRockLightRoutine = true;

        greenHandFire.SetActive(true);

        while (rockShowerLighterCounter <= rockShowerLighterTimer)
        {

            if (startedRockShowerRoutine == false)
            {
                StartCoroutine(rockShowerRoutine());
            }

            rockShowerLighterCounter += Time.deltaTime;
            yield return null;
        }

        //audio related
        goblinRightHandAudioSource.Stop();
        //----

        greenHandFire.SetActive(false);

        rockShowerLighterCounter = 0f;

        startedRockLightRoutine = false;
        

        state = kingRightHandStates.Idle;
    }


   
    private IEnumerator rockShowerRoutine()
    {
       

        startedRockShowerRoutine = true;

        int randomRockPoint = Random.Range(0, rockShowerPoints.Count);

        GameObject fallingRockObj = Instantiate(fallingRock, rockShowerPoints[randomRockPoint].transform.position, fallingRock.transform.rotation);
        

        while (rockShowerCounter <= rockShowerTimer)
        {
            rockShowerCounter += Time.deltaTime;
            yield return null;
        }

        rockShowerCounter = 0f;

        startedRockShowerRoutine = false;


       

    }



    //--------------------------------------------------------------------------------------
    //---------------------BURNING THE GROUND ----------------------------------------------
    //--------------------------------------------------------------------------------------


    private IEnumerator lightGemsRoutine()
    {

        startedLightGemsRoutine = true;

        while(lightGemsCounter <= lightGemsTimer)
        {

            runeLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = Mathf.Lerp(1f, 10f, lightGemsCounter / lightGemsTimer);

            lightGemsCounter += Time.deltaTime;

            yield return null;

        }
        runeLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 0f;
        lightGemsCounter = 0f;


        StartCoroutine(burnGroundRoutine());

    }



    //audio
    public AudioClip burningAudio;
    private IEnumerator burnGroundRoutine()
    {

        startedBurnGroundRoutine = true;

        fireGroundHolder.SetActive(true);
        handFire.SetActive(true);

        //audio related
        goblinRightHandAudioSource.clip = burningAudio;
        goblinRightHandAudioSource.Play();
        //

        while (burnGroundCounter <= burnGroundTimer)
        {

            burnGroundCounter += Time.deltaTime;
            yield return null;

        }


        //audio related
        goblinRightHandAudioSource.Stop();
        //


        burnGroundCounter = 0f;

        fireGroundHolder.SetActive(false);
        handFire.SetActive(false);


        startedBurnGroundRoutine = false;
        startedLightGemsRoutine = false;

        state = kingRightHandStates.Idle;
    }

    //--------------------------------------------------------------------------------------
    //---------------------FIRING PROJECTILES WHILE IDLE------------------------------------
    //--------------------------------------------------------------------------------------

    private IEnumerator fireIdleProjectileRoutine()
    {

        startedIdleProjectileRoutine = true;

        //firing a projectile, settings its rotation to the player once, adding velocity to it
        GameObject FiredProjectile = Instantiate(idleProjectile, this.transform.position, idleProjectile.transform.rotation);

        Vector3 dir = playerObj.transform.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        FiredProjectile.transform.eulerAngles = new Vector3(0, 0, angle - 90f);

        FiredProjectile.GetComponent<Rigidbody2D>().velocity = dir.normalized * 30f;

        firedIdleProjectileCount += 1;



        //Vector for a randomn point
        Vector2 randomPoint = Random.insideUnitCircle * 3f + new Vector2(rightHandIdleSpot.transform.position.x, rightHandIdleSpot.transform.position.y);

        while (idleProjectileCounter <= idleProjectileTimer)
        {

            
    
            this.transform.position = Vector3.MoveTowards(this.transform.position, randomPoint, 5f * Time.deltaTime);

            if (this.transform.position == new Vector3(randomPoint.x, randomPoint.y, 0))
            {

                randomPoint = Random.insideUnitCircle * 3f + new Vector2(rightHandIdleSpot.transform.position.x, rightHandIdleSpot.transform.position.y);

            }



            idleProjectileCounter += Time.deltaTime;

            yield return null;

        }

        if(firedIdleProjectileCount == 5)
        {

            int randomState = Random.Range(0, 2);

            firedIdleProjectileCount = 0;

            if (randomState == 0)
            {
                state = kingRightHandStates.BurnGround;
            }
            if (randomState == 1)
            {

                state = kingRightHandStates.RockShower;
            }

            

        }

        startedIdleProjectileRoutine = false;

        idleProjectileCounter = 0f;


    }


}
