using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class goblinBossEyeStates : MonoBehaviour
{
    //Stats related
    public int eyeHealth;


    //Public references
    public Camera bossCamera;

    public GameObject goblinKingLeftHand,goblinKingRightHand;

    public GameObject goblinGuardObj;

    public GameObject spawnPointLeft, spawnPointRight;

    public Sprite eyeOpenSprite, eyeClosedSprite, eyeHalfOpenSprite;

    public GameObject homingProjectile;

    public GameObject eyeLightHolder;





    //Death related
    public static bool goblinKingDeathState;

    public GameObject explosionAnimationObj;
  
    public GameObject fireHolder;


    public GameObject droppedItem;

    //disabling the whole holder for the boss including entry sequence handler
    public GameObject entrySceneHolder;




    //Timers for coroutines
    private float idleRoutineCounter;
    private float idleRoutineTimer = 15f;

    private float damageRoutineCounter;
    private float damageRoutineTimer = 5f;

    private float projectileRoutineTimer = 5f;
    private float projectileRoutineCounter;

    private float bossDeathRoutineCounter;
    private float bossDeathRoutineTimer = 5f;

    private float explosionRoutineCounter;
    private float explosionRoutineTimer = 0.1f;



    

    //Bools for coroutines
    private bool startedIdleRoutine;
    //------------------------------
    private bool startedSummonRoutine;
    //------------------------------
    private bool startedTakeDamageRoutine;
    //-----------------------------
    private bool startedFireProjectileRoutine;
    //------------------------------
    private bool startedBossDeathRoutine;
    private bool startedExplosionRoutine;


    //---- AUDIO -----
    private AudioSource goblinEyeAudioSource;
    


    private kingEyeStates state;
    public enum kingEyeStates
    {

        Idle,
        FireHomingProjectile,
        SummonGuards,



    }

    void Start()
    {
        goblinEyeAudioSource = this.GetComponent<AudioSource>();

        state = kingEyeStates.Idle;
    }

    
    void Update()
    {

        if(eyeHealth <= 0 && startedBossDeathRoutine == false)
        {          
            Debug.Log("starting to die yo");
            StartCoroutine(bossDeathRoutine());
        }

        Debug.Log(eyeHealth);

        if (eyeHealth > 0)
        {
            stateChanger();
        }
    }

    public void eyeTakeDamage()
    {
       if(startedTakeDamageRoutine == false)
        {          
            StartCoroutine(takeDamageRoutine());      
        }
    }

    private void stateChanger()
    {

        if (this.GetComponent<SpriteRenderer>().sprite == eyeOpenSprite)
        {
            //Wait to do something
            if (state == kingEyeStates.Idle)
            {
                if (startedIdleRoutine == false)
                {
                    StartCoroutine(idleRoutine());
                }

            }
            //Summon soldiers
            if (state == kingEyeStates.SummonGuards)
            {
                if (startedSummonRoutine == false)
                {
                    StartCoroutine(summonGoblins());
                }
            }

            if(state == kingEyeStates.FireHomingProjectile)
            {

                if(startedFireProjectileRoutine == false)
                {

                    StartCoroutine(fireProjectileRoutine());
                }

            }


        }

    }

    //--------------------------------------------------------------------------------------
    //-----------------------------------BOSS DEATH ROUTINE---------------------------------
    //--------------------------------------------------------------------------------------
    
    

    private IEnumerator bossDeathRoutine()
    {
        startedBossDeathRoutine = true;

        fireHolder.SetActive(false);

        goblinKingLeftHand.GetComponent<goblinLeftHandStates>().enabled = false;
        goblinKingRightHand.GetComponent<goblinRightHandStates>().enabled = false;

        goblinKingRightHand.GetComponent<goblinRightHandStates>().StopAllCoroutines();


        goblinKingLeftHand.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        goblinKingLeftHand.GetComponent<goblinLeftHandStates>().StopAllCoroutines();





        while (bossDeathRoutineCounter <= bossDeathRoutineTimer)
        {

            if(startedExplosionRoutine == false)
            {
                StartCoroutine(explosionEffectSpawner());
            }

            bossDeathRoutineCounter += Time.deltaTime;

            yield return null;

        }
        // death is set to true
        goblinKingDeathState = true;

        // reset the camera to the player
        bossCamera.GetComponent<cameraFollow>().enabled = true;
        

        goblinKingLeftHand.SetActive(false);
        goblinKingRightHand.SetActive(false);

        droppedItem.SetActive(true);
        entrySceneHolder.SetActive(false);
    }

    //----------


    private IEnumerator explosionEffectSpawner()
    {
        Debug.Log("spawned expo");

        startedExplosionRoutine = true;

        Vector2 randomPointLeftHand = Random.insideUnitCircle * 8f + new Vector2(goblinKingLeftHand.transform.position.x, goblinKingLeftHand.transform.position.y);
        Vector2 randomPointRightHand = Random.insideUnitCircle * 8f + new Vector2(goblinKingRightHand.transform.position.x, goblinKingRightHand.transform.position.y);
        Vector2 randomPointEye = Random.insideUnitCircle * 8f + new Vector2(this.transform.position.x, this.transform.position.y);

        Instantiate(explosionAnimationObj, randomPointLeftHand, explosionAnimationObj.transform.rotation);
        Instantiate(explosionAnimationObj, randomPointRightHand, explosionAnimationObj.transform.rotation);
        Instantiate(explosionAnimationObj, randomPointEye, explosionAnimationObj.transform.rotation);

        while (explosionRoutineCounter <= explosionRoutineTimer)
        {

            Debug.Log("counting next expo");

            explosionRoutineCounter += Time.deltaTime;

            yield return null;

        }
        explosionRoutineCounter = 0f;

        startedExplosionRoutine = false;

    }



    //--------------------------------------------------------------------------------------
    //---------------------Idle eye routine, waiting for input------------------------------
    //--------------------------------------------------------------------------------------
    private IEnumerator idleRoutine()
    {

        startedIdleRoutine = true;

        while (idleRoutineCounter <= idleRoutineTimer)
        {
            idleRoutineCounter += Time.deltaTime;
            yield return null;
        }

        int randomState = Random.Range(0, 2);

        //int randomState = 1;

        idleRoutineCounter = 0f;

        startedIdleRoutine = false;

        if (randomState == 0)
        {
            state = kingEyeStates.SummonGuards;
        }
        if (randomState == 1)
        {
            state = kingEyeStates.FireHomingProjectile;
        }

    }

    //--------------------------------------------------------------------------------------
    //---------------------Fire homing projectile----------------------
    //--------------------------------------------------------------------------------------

    //
    public AudioClip chargeProjectileSound;
    private IEnumerator fireProjectileRoutine()
    {
        startedFireProjectileRoutine = true;

        //audio
        goblinEyeAudioSource.clip = chargeProjectileSound;
        goblinEyeAudioSource.Play();
        //

        while (projectileRoutineCounter <= projectileRoutineTimer && startedTakeDamageRoutine == false)
        {
            eyeLightHolder.GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.green;
            eyeLightHolder.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = Mathf.Lerp(1f, 5f, projectileRoutineCounter / projectileRoutineTimer);
            

            projectileRoutineCounter += Time.deltaTime;
            yield return null;
        }

        eyeLightHolder.GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.red;
        eyeLightHolder.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 1;


        if (startedTakeDamageRoutine == false)
        {
            Instantiate(homingProjectile, this.transform.position, homingProjectile.transform.rotation);
        }

        projectileRoutineCounter = 0f;

        startedFireProjectileRoutine = false;

        state = kingEyeStates.Idle;
    }

    //--------------------------------------------------------------------------------------
    //---------------------SUMMON GOBLINS FROM THE TOP----------------------
    //--------------------------------------------------------------------------------------
    private IEnumerator summonGoblins()
    {

        startedSummonRoutine = true;

        Instantiate(goblinGuardObj, spawnPointLeft.transform.position, goblinGuardObj.transform.rotation);
        Instantiate(goblinGuardObj, spawnPointRight.transform.position, goblinGuardObj.transform.rotation);

        state = kingEyeStates.Idle;

        

        yield return null;

        startedSummonRoutine = false;


    }

    //--------------------------------------------------------------------------------------
    //---------------------TAKE DAMAGE AND BE INVULNERABLE FOR SOME TIME----------------------
    //--------------------------------------------------------------------------------------

    private IEnumerator takeDamageRoutine()
    {
        eyeHealth -= 1;
        
        startedTakeDamageRoutine = true;

        this.GetComponent<SpriteRenderer>().sprite = eyeClosedSprite;

        while(damageRoutineCounter <= damageRoutineTimer)
        {

            damageRoutineCounter += Time.deltaTime;
            yield return null;
        }

        this.GetComponent<SpriteRenderer>().sprite = eyeOpenSprite;

        damageRoutineCounter = 0f;
        startedTakeDamageRoutine = false;
    }

}
