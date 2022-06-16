using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redMageStates : MonoBehaviour
{
    [HideInInspector]
    public redMageStatesENUM stateRedMage;

    private GameObject playerObj;

    //Checking the pooling class , need access to it.
    public GameObject spreadFirePoolingObj;


    //Audio player related
    AudioSource redMageAudioSource;


    public enum redMageStatesENUM
    {

        Idle,
        torchFireball,
        torchEruption,
        discoFire,



    }

    void Start()
    {
        //inst auto
        redMageAudioSource = this.GetComponent<AudioSource>();

        //instantiate the lists at the start of the scene
        projectileList = spreadFirePoolingObj.GetComponent<mageBossObjectPool>().spreadFirePool;


        var playerObjTry = GameObject.Find("Astrobuddy");

        if (playerObjTry != null)
        {
            playerObj = playerObjTry;

        }


        stateRedMage = redMageStatesENUM.Idle;
        
    }

    // Update is called once per frame
    void Update()
    {

        stateChanger();
    }

    private void stateChanger()
    {

        if(stateRedMage == redMageStatesENUM.Idle)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                stateRedMage = redMageStatesENUM.torchEruption;
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                stateRedMage = redMageStatesENUM.discoFire;
            }

        }

        if(stateRedMage == redMageStatesENUM.torchFireball)
        {
            if(startedpretorchFireballRoutine == false)
            {
                StartCoroutine(pretorchFireballRoutine());
            }
        }

        

        if (stateRedMage == redMageStatesENUM.torchEruption)
        {
            if (startedpreEruptionRoutine == false)
            {
                StartCoroutine(preEruptionRoutine());
            }
        }

        if (stateRedMage == redMageStatesENUM.discoFire)
        {
            if (startedSummonBallRoutine == false)
            {
                StartCoroutine(summonDiscoball());
            }
        }

        




    }


    //--------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------//
    //---------showing that the fireball is being summoned----------------------------//
    //-----------------------------CURRENTLY NOT IN USE-------------------------------//
    //--------------------------------------------------------------------------------//
    private bool startedpretorchFireballRoutine;

    public GameObject preTorchParticles;

    private int randomBonfireNum;

    private float preFireballCounter;
    private float preFireballTimer = 2f;

    private Vector3 fireballsummonPoint;
    private Vector3 playerStartPos;


    public GameObject redMage;


    
    private IEnumerator pretorchFireballRoutine()
    {

        startedpretorchFireballRoutine = true;

       

        randomBonfireNum = Random.Range(0, 3);

        
        playerStartPos = playerObj.transform.position;


        preTorchParticles.transform.position = redMage.transform.position;
        fireBall.transform.position = redMage.transform.position;

        preTorchParticles.SetActive(true);

        while (preFireballCounter <= preFireballTimer)
        {

            yield return null;

            preFireballCounter += Time.deltaTime;
        }

        
        if(startedtorchFireballRoutine == false)
        {
            StartCoroutine(torchFireballRoutine());
        }
    }

    
    public List<GameObject> bonfireList;
    public GameObject fireBall;

    // use this to deactivate all the pooled objects once this ends
    public static bool startedtorchFireballRoutine;

    //fireballTimer is the lifecycle for the fireball routeine
    private float fireballCounter;
    private float fireballTimer = 25f;

    public float fireballSpeed;

    
    private IEnumerator torchFireballRoutine()
    {
        startedtorchFireballRoutine = true;

        fireBall.SetActive(true);

        fireBall.GetComponent<Rigidbody2D>().velocity = (playerObj.transform.position - fireBall.transform.position).normalized * fireballSpeed;
        

        


        while (fireballCounter <= fireballTimer)
        {

            

            fireBall.transform.Rotate(Vector3.back * 50f * Time.deltaTime);

       
            fireballCounter += Time.deltaTime;

            yield return null;

        }

        //reset stuff
       

        startedpretorchFireballRoutine = false;
        startedtorchFireballRoutine = false;
        fireballCounter = 0f;

        //reset the fireball
        fireBall.SetActive(false);
        preTorchParticles.SetActive(false);


        //Set related pooling variables to their initial states
        redMageFireballScript.startedaoeProjectileRoutine = false;
        


        //Change state to idle
        stateRedMage = redMageStatesENUM.Idle;


       
    }


    //--------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------//   
    //--------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------//

    //--------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------//
    //----------------------------DISCO FIREBALL RELATED------------------------------//
    //-----------------------------CURRENTLY NOT IN USE-------------------------------//
    //--------------------------------------------------------------------------------//

    private bool startedSummonBallRoutine;

    public GameObject discoFireball;

    public Transform roomMiddlePoint;


    private float summonBallTimer = 3f;
    private float summonBallCounter;

    public List<GameObject> bonfireWindsList;

    Vector3 ballStartPos;

    //variables to increase collider range of the discolines
    bool increasedDiscoLineRange;
    float discoLineRangeIncreaseTimer = 1.5f;
    float discoLineRangeIncreaseCounter;

    //Audio to indicate fireball chargeup
    public AudioClip fireBallChargeAudio;
    private IEnumerator summonDiscoball()
    {

        //play audio for the fireball
        redMageAudioSource.clip = fireBallChargeAudio;
        redMageAudioSource.Play();


        startedSummonBallRoutine = true;

        //set the winds to active once this beings
        foreach(GameObject windObj in bonfireWindsList)
        {
            windObj.SetActive(true);
        }

        discoFireball.transform.position = redMage.transform.position;
        discoFireball.SetActive(true);

        Vector3 ballStartPos = discoFireball.transform.position;
        Transform discoFireballTransform = discoFireball.transform;

        while (summonBallCounter <= summonBallTimer)
        {

            discoFireballTransform.position = Vector3.Lerp(ballStartPos, roomMiddlePoint.position, summonBallCounter / summonBallTimer);

            summonBallCounter += Time.deltaTime;

            yield return null;
        }

        
        if(startedBallAttack == false)
        {
            StartCoroutine(discoBallAttack());
        }

    }

    private bool startedBallAttack;

    private float ballAttackTimer = 30f;
    private float ballAttackCounter;

    public GameObject discoFireballLines;
    public List<BoxCollider2D> discoFireballLinesColliderList;
    //routine to turn the fireball and fire projectiles

    //Audio to indicate firedisco is burning
    public AudioClip fireDiscoAudio;
    private IEnumerator discoBallAttack()
    {
        //audio
        redMageAudioSource.clip = fireDiscoAudio;
        redMageAudioSource.Play();
        //

        startedBallAttack = true;

        Transform discoFireballTransform = discoFireball.transform;

        discoFireballLines.SetActive(true);

        


        //randomly spin the ball in different directions
        float discoTurnTimer = 2f;
        float discoTurnCounter = 0f;

        float stopAtTurnTime = 1f;

        float turnDir = 0;
        

        bool canTurn = false;


       

        while (ballAttackCounter <= ballAttackTimer)
        {




            foreach (BoxCollider2D discoCollider in discoFireballLinesColliderList)
            {
                discoCollider.size = new Vector2(Mathf.Lerp(1f, 113f, ballAttackCounter * 30f / ballAttackTimer), discoCollider.size.y);

                discoCollider.offset = new Vector2(Mathf.Lerp(1f, 56f, ballAttackCounter * 30f / ballAttackTimer), discoCollider.offset.y);
            }





            //---


            if (discoTurnCounter <= discoTurnTimer)
            {
                discoTurnCounter += Time.deltaTime;
            }
            else
            {
                canTurn = false;

                if (stopAtTurnTime >= 0)
                {
                    stopAtTurnTime -= Time.deltaTime;
                }
                else
                {
                    
                    stopAtTurnTime = 1f;
                    discoTurnCounter = 0f;
                    discoTurnTimer = Random.Range(3f, 6f);

                    turnDir = Random.Range(0, 2);

                    

                    canTurn = true;
                }

            }



            if (canTurn == true)
            {
                if (turnDir == 0)
                {
                    discoFireballTransform.Rotate(Vector3.back * Time.deltaTime * 25f);
                }
                if(turnDir == 1)
                {
                    
                    discoFireballTransform.Rotate(Vector3.forward * Time.deltaTime * 25);
                }
            }




            ballAttackCounter += Time.deltaTime;


            yield return null;
        }


        //end adjustments
        discoLineRangeIncreaseCounter = 0f;

        discoFireball.SetActive(false);
        discoFireballLines.SetActive(false);

        startedSummonBallRoutine = false;
        startedBallAttack = false;

        ballAttackCounter = 0f;
        summonBallCounter = 0f;

        discoFireball.transform.position = ballStartPos;

        foreach (BoxCollider2D discoCollider in discoFireballLinesColliderList)
        {
            discoCollider.size = new Vector2(1f, discoCollider.size.y);
            discoCollider.offset = new Vector2(1, discoCollider.offset.y);
        }


        foreach (GameObject windObj in bonfireWindsList)
        {
            windObj.SetActive(false);
        }

        //stopped an attack
        allMageBossesStateController.mageDoingAttack = false;
        allMageBossesStateController.startedAdjustMageStates = false;

        stateRedMage = redMageStatesENUM.Idle;




    }

    //inactive-----------------------

    public List<Transform> discoFirePoints;

    private bool startedProjectileDiscoRoutine;

    List<GameObject> projectileList;

    private IEnumerator fireProjectileFromDisco()
    {
        

        startedProjectileDiscoRoutine = true;

        foreach (Transform firePoint in discoFirePoints)
        {
            projectileList[mageBossObjectPool.pooledTimes].SetActive(true);
            projectileList[mageBossObjectPool.pooledTimes].transform.position = firePoint.position;
            projectileList[mageBossObjectPool.pooledTimes].transform.rotation = firePoint.rotation;

            mageBossObjectPool.pooledTimes += 1;


           
        }
            
              

        yield return new WaitForSeconds(2f);
        
        startedProjectileDiscoRoutine = false;


    }




    //--------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------//   
    //--------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------//



    //--------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------//
    //------------------------------TORCH ERUPTION RELATED----------------------------//
    //--------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------//
    private float preEruptionTimer = 3f;
    private float preEruptionCounter;

    private bool startedpreEruptionRoutine;

    public List<GameObject> preEruptionParticleList;
    private IEnumerator preEruptionRoutine()
    {

        startedpreEruptionRoutine = true;

        foreach (GameObject eruptionParticles in preEruptionParticleList)
        {
            eruptionParticles.SetActive(true);
        }

        while(preEruptionCounter <= preEruptionTimer)
        {
            preEruptionCounter += Time.deltaTime;

            yield return null;
        }


     

        if(startedEruptionRoutine == false)
        {
            StartCoroutine(eruptionRoutine());
        }

    }


    private float eruptionRoutineCounter;
    private float erupitionRoutineTimer = 10f;

    private bool startedEruptionRoutine;

    List<GameObject> eruptionObjectsList;
    private IEnumerator eruptionRoutine()
    {
        //define the list here so we can clear once this coroutine ends
        eruptionObjectsList = spreadFirePoolingObj.GetComponent<mageBossObjectPool>().eruptionPool;

        startedEruptionRoutine = true;

        while(eruptionRoutineCounter <= erupitionRoutineTimer)
        {

            eruptionRoutineCounter += Time.deltaTime;

            if(startederuptSummonRoutine == false)
            {
                StartCoroutine(eruptFireballs());
            }

            yield return null;

        }

        //resetting stuff
        foreach(GameObject bonfireParticles in preEruptionParticleList)
        {
            bonfireParticles.SetActive(false);
        }

        foreach(GameObject eruptionObj in eruptionObjectsList)
        {
            eruptionObj.SetActive(false);
        }

        
        mageBossObjectPool.pooledEruptionTimes = 0;
        


        eruptionRoutineCounter = 0;
        preEruptionCounter = 0f;
        eruptSummonCounter = 0f;

        startederuptSummonRoutine = false;
        startedpreEruptionRoutine = false;
        startedEruptionRoutine = false;

        //reset mage attack var
        allMageBossesStateController.mageDoingAttack = false;
        allMageBossesStateController.startedAdjustMageStates = false;

        stateRedMage = redMageStatesENUM.Idle;
    }

    

    

    private bool startederuptSummonRoutine;

    private float eruptSummonTimer = 0.2f;
    private float eruptSummonCounter;

    //public static float numberOfEruptionProjectiles;
    

    public List<GameObject> bonfiresList;

    public AudioClip eruptionAudioClip;
    private IEnumerator eruptFireballs()
    {

        //play audio each time this is done
        redMageAudioSource.clip = eruptionAudioClip;
        redMageAudioSource.Play();

        startederuptSummonRoutine = true;

        

        foreach(GameObject bonfireObj in bonfiresList)
        {

            //random force to add to the gameobject
            int randomSign = Random.Range(0, 2) * 2 - 1;


            Vector2 randomForce = new Vector2(Random.Range(50f,400f) * randomSign, Random.Range(500f,800f));

            //make the bonfire in the middle spread more
            if(bonfireObj.gameObject.name == "bonfireMiddle")
            {
                randomForce = new Vector2(Random.Range(0f, 1000f) * randomSign, Random.Range(500f, 800f));
            }

            Debug.Log("added force");

            eruptionObjectsList[mageBossObjectPool.pooledEruptionTimes].SetActive(true);
            eruptionObjectsList[mageBossObjectPool.pooledEruptionTimes].transform.position = bonfireObj.transform.position;
            eruptionObjectsList[mageBossObjectPool.pooledEruptionTimes].GetComponent<Rigidbody2D>().AddForce(randomForce);



            mageBossObjectPool.pooledEruptionTimes += 1;
        }

        startederuptSummonRoutine = true;

        while (eruptSummonCounter <= eruptSummonTimer)
        {

            eruptSummonCounter += Time.deltaTime;
            yield return null;
        }


        eruptSummonCounter = 0f;
        startederuptSummonRoutine = false;

    }

}
