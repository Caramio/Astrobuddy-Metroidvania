using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batBossStates : MonoBehaviour
{
    private batBossMainState state;

    

    //Public references
    public Transform startPoint, endPoint ,middlePoint;
    public Transform bottomLeft, bottomRight;
    public List<Transform> shadowRushStartPoints;
    public Transform eyePoint;

    public static bool bossWasKilled;

    // disabling entry camera adjust after the fight
    public GameObject batBossCamera;

    //public gameobject for the scenes main camera...

    public GameObject sceneCamera;


    

    //opened doors after the fight
    public GameObject OpenedDoorOne, OpenedDoorTwo;

    public GameObject droppedItem;

    //Attack related public gameobject references
    public GameObject lazerBeam;
    public GameObject shadowRushClone;
    public GameObject projectileObj;

    //boss attack related numeric variables
    public int numOfProjectiles;
    public float routeSpeed;
    public int batBossHealth = 1;


    //Private booleans
    private bool startedCooldownRoutine;
    private bool wasGoingRight;

    private bool startedWingFlapLeftRoutine;


    //Lazer related booleans
    private bool startedMiddleLazerRoutine;
    private bool startedLeftLazerRoutine;
    private bool startedRightLazerRoutine;

    //Fade and shadowrush related booleans
    private bool startedFadeRoutine;
    private bool startedShadowDashRoutine;

    //Projectile related bools
    private bool startedAoeProjectileRoutine;







    //Timers
    private float coolDownCounter;
    private float cooldownTimer;

    private float wingFlapLeftTimer = 10f;
    private float wingFlapLeftCounter;

    private float wingFlapRightTimer;
    private float wingFlapRightCounter;

    //Lazer related timers
    private float middleLazerTimer = 4f;
    private float middleLazerCounter;

    private float leftLazerTimer = 2.5f;
    private float leftLazerCounter;

    private float rightLazerTimer = 2.5f;
    private float rightLazerCounter;



    //Shadowrush and fade relayed timers
    private float fadeTimer = 6f;
    private float fadeCounter;

    //Projectile related timers
    private float projectileAoeTimer = 1.5f;
    private float projectileAoeCounter;


    //Health and damage related timers
    private float damageTakeTimer = 0.2f;
    private float damageTakeCounter;








    private bool reachedEnd;

    public enum batBossMainState
    {

        Cooldown,

        MiddleLazer,
        LeftLazer,
        RightLazer,

        Fading,

        AoeProjectile,

        WingFlapAttackLeft,
        WingFlapAttackRight,


    }

    void Start()
    {
        state = batBossMainState.Cooldown;
    }

    
    void Update()
    {
        bossDeath();


        stateChanger();
    }


    private void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector2(eyePoint.transform.position.x + 60, eyePoint.transform.position.y), new Vector2(100f, 80f));

    }

    private void stateChanger()
    {



        //Pathing between start and end point, will jump to a new state after a certain time , will also fire projectiles while moving back and forth 
        if(state == batBossMainState.Cooldown)
        {

            if (startedCooldownRoutine == false)
            {
                cooldownTimer = Random.Range(5f, 8f);
                StartCoroutine(cooldownRoutine(cooldownTimer));
            }

            // this will keep the aoeprojectiles from spawning into other routines
            if(startedAoeProjectileRoutine == false && projectileAoeTimer + coolDownCounter < cooldownTimer)
            {
                StartCoroutine(AoeProjectileRoutine());
            }



            if (transform.position.x == endPoint.transform.position.x && transform.position.y == endPoint.transform.position.y)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                reachedEnd = true;
            }

            if (transform.position.x == startPoint.transform.position.x && transform.position.y == startPoint.transform.position.y)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                reachedEnd = false;
            }

            if (reachedEnd == false)
            {
                wasGoingRight = true;
                transform.position = Vector3.MoveTowards(transform.position, endPoint.position, Time.deltaTime * routeSpeed);

            }
            if (reachedEnd == true)
            {
                wasGoingRight = false;
                transform.position = Vector3.MoveTowards(transform.position, startPoint.position, Time.deltaTime * routeSpeed);

            }


        }

        //------------------------LAZER STATES----------------------------------------
        if(state == batBossMainState.MiddleLazer)
        {

            
            transform.position = Vector3.MoveTowards(transform.position, middlePoint.position, Time.deltaTime * routeSpeed);

            //If we reach the middle point , we will start the lazer routine
            if (transform.position.x == middlePoint.transform.position.x && transform.position.y == middlePoint.transform.position.y)
            {

                
                if (startedMiddleLazerRoutine == false)
                {

                    StartCoroutine(middleLazerRoutine());
                }

            }
                
        }

        if(state == batBossMainState.LeftLazer)
        {
            transform.position = Vector3.MoveTowards(transform.position, bottomLeft.position, Time.deltaTime * routeSpeed);

            if (transform.position.x == bottomLeft.transform.position.x && transform.position.y == bottomLeft.transform.position.y)
            {

                if (startedLeftLazerRoutine == false)
                {

                    StartCoroutine(leftLazerRoutine());
                }


            }

        }

        if(state == batBossMainState.RightLazer)
        {
            transform.position = Vector3.MoveTowards(transform.position, bottomRight.position, Time.deltaTime * routeSpeed);

            if (transform.position.x == bottomRight.transform.position.x && transform.position.y == bottomRight.transform.position.y)
            {

                if (startedRightLazerRoutine == false)
                {

                    StartCoroutine(rightLazerRoutine());
                }

            }

        }

        //Fade and shadowrush states
        if(state == batBossMainState.Fading)
        {

            if(startedFadeRoutine == false)
            {
                StartCoroutine(fadeRoutine());
            }

        }

        //Projectile states
        if(state == batBossMainState.AoeProjectile)
        {
         
            AoeProjectile();
            
        }


    }

    //--------------------------------------- LAZER ROUTINES ---------------------------------
    //audio source for the lasers
    public AudioSource laserAudioSource;
    private IEnumerator middleLazerRoutine()
    {

        //
        laserAudioSource.Play();
        //

        startedMiddleLazerRoutine = true;

        lazerBeam.SetActive(true);

        while(middleLazerCounter <= middleLazerTimer)
        {
                          
            eyePoint.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(-15f, -165f, middleLazerCounter / middleLazerTimer));
            
            middleLazerCounter += Time.deltaTime;

            yield return null;
        }

        laserAudioSource.Stop();


        middleLazerCounter = 0f;

        
        lazerBeam.SetActive(false);
        eyePoint.transform.localEulerAngles = Vector3.zero;

        startedMiddleLazerRoutine = false;

        state = batBossMainState.Cooldown;


    }
    //Left bottom to top left lazer
    private IEnumerator leftLazerRoutine()
    {

        //
        laserAudioSource.Play();
        //

        startedLeftLazerRoutine = true;

        lazerBeam.SetActive(true);

        while (leftLazerCounter <= leftLazerTimer)
        {

            transform.position = Vector3.Lerp(bottomLeft.transform.position, new Vector2(startPoint.transform.position.x,
               startPoint.transform.position.y), leftLazerCounter / leftLazerTimer);


            leftLazerCounter += Time.deltaTime;

            yield return null;
        }

        laserAudioSource.Stop();

        state = batBossMainState.Cooldown;

        startedLeftLazerRoutine = false;

        leftLazerCounter = 0f;

        lazerBeam.SetActive(false);

    }

    //Right bottom to top right lazer
    private IEnumerator rightLazerRoutine()
    {

        startedRightLazerRoutine = true;

        //
        laserAudioSource.Play();
        //

        lazerBeam.SetActive(true);

        while (rightLazerCounter <= rightLazerTimer)
        {

            transform.position = Vector3.Lerp(bottomRight.transform.position, new Vector2(endPoint.transform.position.x,
               endPoint.transform.position.y), rightLazerCounter / rightLazerTimer);


            rightLazerCounter += Time.deltaTime;

            yield return null;
        }

        laserAudioSource.Stop();

        state = batBossMainState.Cooldown;

        startedRightLazerRoutine = false;

        rightLazerCounter = 0f;

        lazerBeam.SetActive(false);

    }






    //--------------------------------------- END END END END---------------------------------

    //--------------------------------------- FADE ROUTINE  ---------------------------------


    private IEnumerator fadeRoutine()
    {

        startedFadeRoutine = true;

        if (startedShadowDashRoutine == false)
        {
            
            StartCoroutine(spawnCloneRoutine(2f, shadowRushStartPoints[0]));
            StartCoroutine(spawnCloneRoutine(3.5f, shadowRushStartPoints[1]));
            StartCoroutine(spawnCloneRoutine(5f, shadowRushStartPoints[2]));
        }


        while (fadeCounter <= fadeTimer)
        {

            this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.Lerp(1f, 0f, fadeCounter / fadeTimer * 6f));

            fadeCounter += Time.deltaTime;
            yield return null;

        }

        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

        state = batBossMainState.Cooldown;

        fadeCounter = 0f;

        startedFadeRoutine = false;


    }

    private IEnumerator spawnCloneRoutine(float spawnTime, Transform spawnPosition)
    {
        // Internal counter for the rush attacks so they are not determined by a variable pre-assigned.
        float shadowRushInternalCounter = 0f;

        startedShadowDashRoutine = true;

        while (shadowRushInternalCounter <= spawnTime)
        {
            shadowRushInternalCounter += Time.deltaTime;
            yield return null;

        }

        Debug.Log("instantiated" + spawnTime);


        Instantiate(shadowRushClone, spawnPosition.position, shadowRushClone.transform.rotation);


        startedShadowDashRoutine = false;




    }

    //--------------------------------------- PROJECTILE ROUTINES ---------------------------------
    //instantiating projectiles with the given amount around the boss with the given radius
    private void AoeProjectile()
    {

        for (int i = 0; i < numOfProjectiles; i++)
        {
            float theta = i * 2 * Mathf.PI / numOfProjectiles;
            float x = Mathf.Sin(theta) * 10;
            float y = Mathf.Cos(theta) * 10;


            Instantiate(projectileObj, new Vector3(x, y, 0) + this.transform.position , Quaternion.Euler(0f,0f,(-theta * Mathf.Rad2Deg)));
            
            if(i == numOfProjectiles -1)
            {
                state = batBossMainState.Cooldown;
            }
        }

    }

    private IEnumerator AoeProjectileRoutine()
    {

        startedAoeProjectileRoutine = true;

        while(projectileAoeCounter <= projectileAoeTimer)
        {
            projectileAoeCounter += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < numOfProjectiles; i++)
        {
            float theta = i * 2 * Mathf.PI / numOfProjectiles;
            float x = Mathf.Sin(theta) * 10;
            float y = Mathf.Cos(theta) * 10;


            Instantiate(projectileObj, new Vector3(x, y, 0) + this.transform.position, Quaternion.Euler(0f, 0f, (-theta * Mathf.Rad2Deg)));

            
        }

        projectileAoeCounter = 0f;

        startedAoeProjectileRoutine = false;


    }

    //--------------------------------------- DEATH ROUTINE ---------------------------------------


    
    //--------------------------------------- TAKE DAMAGE ROUTINE ---------------------------------

    public void takeDamage()
    {

        batBossHealth -= 1;

        this.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0f, 0f);

        StartCoroutine(damageColorChange());

    }

    private void bossDeath()
    {
        if(batBossHealth == 0)
        {
            // make a coroutine out of these later;
            //cameraFollow.fightingBoss = false;
            //batbosscamera is the related entry checker for the room, not the camera itself...
            batBossCamera.SetActive(false);
            sceneCamera.GetComponent<cameraFollow>().enabled = true;
         
            droppedItem.SetActive(true);

            bossWasKilled = true;

            //change save/load objects value aswell
            dataSavingObjScript.batBossWasDead = true;

            Destroy(this.gameObject);
            
        }
    }

    private IEnumerator damageColorChange()
    {


        while (damageTakeCounter <= damageTakeTimer)
        {

            damageTakeCounter += Time.deltaTime;

            yield return null;
        }



        damageTakeCounter = 0f;

        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);

    }


    //--------------------------------------- END END END END---------------------------------

    //--------------------------------------- WING FLAP ROUTINES ---------------------------------

    private IEnumerator wingFlapFromLeftRoutine()
    {
        startedWingFlapLeftRoutine = true;

        while (wingFlapLeftCounter <= wingFlapLeftTimer)
        {

            wingFlapLeftCounter += Time.deltaTime;

            Collider2D[] insideFlap = Physics2D.OverlapBoxAll(new Vector2(eyePoint.transform.position.x + 60, eyePoint.transform.position.y), new Vector2(100f, 30f), 0f);

            foreach (Collider2D insideFlapObjects in insideFlap)
            {

                if (insideFlapObjects.tag == "PlayerHitbox")
                {

                    insideFlapObjects.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(250f, 0f));

                }



            }

            yield return null;

        }

        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);

        wingFlapLeftCounter = 0f;
        
        startedWingFlapLeftRoutine = false;

        state = batBossMainState.Cooldown;

    }



    

    private IEnumerator wingFlapFromRightRoutine()
    {


        yield return null;
    }

    //---------------------------------------END END END END-------------------------------------------


    private IEnumerator cooldownRoutine(float cooldownTimer)
    {
        startedCooldownRoutine = true;

        //float cooldownTimer = Random.Range(5f, 8f);
        //float cooldownTimer = 2f;

        while(coolDownCounter <= cooldownTimer)
        {

            coolDownCounter += Time.deltaTime;
            yield return null;

        }

        startedCooldownRoutine = false;

        coolDownCounter = 0f;

        int nextStateSelect = Random.Range(1, 6);
        //int nextStateSelect = 5;

        if(nextStateSelect == 1)
        {
            state = batBossMainState.MiddleLazer;
        }
        if(nextStateSelect == 2)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            state = batBossMainState.LeftLazer;
        }
        if(nextStateSelect == 3)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            state = batBossMainState.RightLazer;
        }
        if(nextStateSelect == 4)
        {

            state = batBossMainState.Fading;

        }
        if(nextStateSelect == 5)
        {
            state = batBossMainState.Cooldown;
        }

        
    }
}
