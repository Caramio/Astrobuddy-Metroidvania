using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blueMageStates : MonoBehaviour
{

    //States
    public blueMageStatesENUM stateBlueMage;


    //Checking the pooling class , need access to it.
    public GameObject spreadFirePoolingObj;


    //for audio
    private AudioSource blueMageAudioSource;

    public enum blueMageStatesENUM
    {

        Idle,
        raiseWater,
        spiralWater,



    }

      




    void Start()
    {
        blueMageAudioSource = this.GetComponent<AudioSource>();

        //this will be inside the water;
        startPosAtWater = preSpiralParticles.transform.position;

        stateBlueMage = blueMageStatesENUM.Idle;
    }

    
    void Update()
    {

        blueMageStateChanger();

    }


    private void blueMageStateChanger()
    {

        if(stateBlueMage == blueMageStatesENUM.Idle)
        {

        }

        if(stateBlueMage == blueMageStatesENUM.raiseWater)
        {

            if(startedPreRaiseWaterRoutine == false)
            {
                StartCoroutine(preRaiseWaterRoutine());
            }

        }

        if(stateBlueMage == blueMageStatesENUM.spiralWater)
        {


            if(startedPreSpiralRoutine == false)
            {
                StartCoroutine(preSpiralWaterRoutine());
            }

        }


    }

    //--------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------//
    //-------------------Routine to spiral water attack-------------------------------//
    //--------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------//
    private bool startedPreSpiralRoutine;

    private float preSpiralWaterCounter;
    private float preSpiralWaterTimer = 5f;

    public GameObject preSpiralParticles;

    private Vector3 startPosAtWater;
    private IEnumerator preSpiralWaterRoutine()
    {



        startedPreSpiralRoutine = true;

        

        preSpiralParticles.SetActive(true);

        while(preSpiralWaterCounter <= preSpiralWaterTimer)
        {

            preSpiralParticles.transform.position = Vector3.Lerp(startPosAtWater, new Vector3(preSpiralParticles.transform.position.x, startPosAtWater.y + 30f) , preSpiralWaterCounter/preSpiralWaterTimer);

            preSpiralWaterCounter += Time.deltaTime;
            yield return null;

        }

        if (startedspiralWaterRoutine == false)
        {
            StartCoroutine(spiralWaterProjectile());
        }
            

        startedPreSpiralRoutine = false;

    }
    private bool startedspiralWaterRoutine;

    private float spiralWaterCounter;
    private float spiralWaterTimer = 30f;

    public List<Transform> spiralProjectilePoints;

    
    private IEnumerator spiralWaterProjectile()
    {

        startedspiralWaterRoutine = true;

        Vector3 startPosPartice = preSpiralParticles.transform.position;

        while (spiralWaterCounter <= spiralWaterTimer)
        {

            //defauılt should be *3f
            //preSpiralParticles.transform.position = Vector3.Lerp(startPosPartice, new Vector3(startPosPartice.x, startPosPartice.y + 3f, 0f) , Time.deltaTime * 15f);

            preSpiralParticles.transform.position = new Vector3(startPosPartice.x, startPosPartice.y + Mathf.Sin(spiralWaterCounter *3f) *5f, 0f);

            if(startedSpawnWaterRoutine == false)
            {
                StartCoroutine(spawnWaterProjectile());
            }

            preSpiralParticles.transform.Rotate(Vector3.forward * Time.deltaTime * 100f);

            spiralWaterCounter += Time.deltaTime;

            yield return null;

        }

        startedspiralWaterRoutine = false;

        //reset bools etc-----
      

        preSpiralParticles.SetActive(false);
        preSpiralParticles.transform.position = startPosAtWater;

        startedspiralWaterRoutine = false;
        startedPreSpiralRoutine = false;
        startedSpawnWaterRoutine = false;

        //floats to reset
        spiralWaterCounter = 0f;
        preSpiralWaterCounter = 0f;
        spawnCooldownCounter = 0f;
        //-----
        //resetting all the gameobjects in the pool;
        //reset the counter too
        spiralWaterCounter = 0;
        foreach(GameObject spiralWaterObj in spiralObjectsList)
        {
            spiralWaterObj.SetActive(false);
            
        }

        //indicate that the attack is done
        allMageBossesStateController.mageDoingAttack = false;
        allMageBossesStateController.startedAdjustMageStates = false;


        stateBlueMage = blueMageStatesENUM.Idle;

    }

    private bool startedSpawnWaterRoutine;

    private float spawnCooldownCounter;
    private float spawnCooldownTimer = 0.15f;

    //0.15 should be default
    public AudioClip waterBoltClip;


    List<GameObject> spiralObjectsList;

    private int spiralShotCounter;
    private IEnumerator spawnWaterProjectile()
    {
        //audio
        blueMageAudioSource.clip = waterBoltClip;
        blueMageAudioSource.Play();
        //

        startedSpawnWaterRoutine = true;

        spiralObjectsList = spreadFirePoolingObj.GetComponent<mageBossObjectPool>().waterSpiralPool;

    
        foreach(Transform spiralPoint in spiralProjectilePoints)
        {

            spiralObjectsList[spiralShotCounter].SetActive(true);
            spiralObjectsList[spiralShotCounter].transform.position = spiralPoint.position;
            spiralObjectsList[spiralShotCounter].transform.eulerAngles = spiralPoint.transform.eulerAngles;

            spiralShotCounter += 1;

        }

        while(spawnCooldownCounter <= spawnCooldownTimer)
        {
            spawnCooldownCounter += Time.deltaTime;
            yield return null;
        }

        spawnCooldownCounter = 0f;

        startedSpawnWaterRoutine = false;
    }


    //--------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------//   
    //--------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------//


    //--------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------//
    //-------------------Routine to visually show that the water is going to be raised--------------------------//
    //--------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------//

    private bool startedPreRaiseWaterRoutine;

    private float preraiseCounter;
    private float preraiseTimer = 4f;

    public GameObject waterRaiseParticles;


    //Audioclip for bubbling
    public AudioClip bubblingWater;
    private IEnumerator preRaiseWaterRoutine()
    {
        //audio
        blueMageAudioSource.clip = bubblingWater;
        blueMageAudioSource.Play();
        //

        startedPreRaiseWaterRoutine = true;


        waterRaiseParticles.SetActive(true);

        while(preraiseCounter <= preraiseTimer)
        {

            preraiseCounter += Time.deltaTime;

            yield return null;
        }

        preraiseCounter = 0f;

        waterRaiseParticles.SetActive(false);

        if (startedRaiseWaterRoutine == false)
        {
            StartCoroutine(raiseWaterRoutine());
        }
         
    }
    //Routine to raise water levels
    public GameObject waterBase;

    private bool startedRaiseWaterRoutine;

    private float raiseCounter;
    private float raiseTimer = 4f;

    //spikes that will cover the sticky walls
    public GameObject leftSpike, rightSpike;

    //trap cubbies to activate
    public GameObject leftTrapcubby, rightTrapcubby;
    public GameObject leftTrapCoveringWall, rightTrapCoveringWall;

    //winds to disable
    public List<GameObject> bonfireWindsList;


    //audio
    public AudioClip flowingWaterAudioClip;


    //Spikes related transforms
    private Vector3 spikeStartPos;

    private Transform leftSpikeTransform, rightSpikeTransform;
    private Vector3 startSpikeLeftPos, startSpikeRightPos;
   
    private IEnumerator raiseWaterRoutine() 
    {
        //flowing water audio
        blueMageAudioSource.clip = flowingWaterAudioClip;
        blueMageAudioSource.Play();
        
        //

        startedRaiseWaterRoutine = true;

        Transform baseTransform = waterBase.transform;


        //assigning lowering routine parts here too...
        startWaterBasePosForLower = waterBase.transform.position;
        startBaseScaleForLower = baseTransform.localScale;
        //




        //Starting positions for the water
        Vector3 startBasePos = waterBase.transform.position;
        //Starting scale for the water
        Vector2 startBaseScale = baseTransform.localScale;


        //spikes
        leftSpikeTransform = leftSpike.transform;
        rightSpikeTransform = rightSpike.transform;

        startSpikeLeftPos = leftSpike.transform.position;
        startSpikeRightPos = rightSpike.transform.position;

        while (raiseCounter <= raiseTimer)
        {

            baseTransform.position = Vector3.Lerp(startBasePos, new Vector2(startBasePos.x, startBasePos.y + 13f) , raiseCounter / raiseTimer);
            baseTransform.localScale = new Vector2(startBaseScale.x , Mathf.Lerp(startBaseScale.y, startBaseScale.y + 13f, raiseCounter / raiseTimer));


            //spikes
            leftSpikeTransform.position = Vector3.Lerp(startSpikeLeftPos, new Vector2(startSpikeLeftPos.x + 7f, startSpikeLeftPos.y), raiseCounter / raiseTimer);
            rightSpikeTransform.position = Vector3.Lerp(startSpikeRightPos, new Vector2(startSpikeRightPos.x - 7f, startSpikeLeftPos.y), raiseCounter / raiseTimer);



            raiseCounter += Time.deltaTime;

            yield return null;


        }

        //setting certain gameobject states

        foreach(GameObject windObj in bonfireWindsList)
        {
            windObj.SetActive(false);
        }

        

        leftTrapcubby.SetActive(true);
        rightTrapcubby.SetActive(true);
        leftTrapCoveringWall.SetActive(false);
        rightTrapCoveringWall.SetActive(false);

        //leftSpike.SetActive(true);
        //rightSpike.SetActive(true);

        //raiseCounter = 0f;
        //startedRaiseWaterRoutine = false;
        //startedPreRaiseWaterRoutine = false;


        //stateBlueMage = blueMageStatesENUM.Idle;

        if(startedbubblesRoutine == false)
        {
            StartCoroutine(bubblesRoutine());
        }
    }

    private bool startedbubblesRoutine;

    private float bubblesCounter;
    private float bubblesTimer = 25f;

    //Routine to give a timer to spawning bubbles bubbles after the water is raised
    private IEnumerator bubblesRoutine()
    {
        startedbubblesRoutine = true;


        while(bubblesCounter <= bubblesTimer)
        {
            if(startedSpawnBubbles == false)
            {
                StartCoroutine(spawnBubbles());
            }

            bubblesCounter += Time.deltaTime;
            yield return null;
        }


        //lower water eventually
        if (startedLowerWaterRoutine == false)
        {
            StartCoroutine(lowerWaterRoutine());
        }

    }


    //Routine to spawn bubbles during the bubblesRoutine()

    private bool startedSpawnBubbles;

    public float bubblesDelay;

    public GameObject bubbleObject;
    public List<Transform> bubbleSpawnList;
    private IEnumerator spawnBubbles()
    {

        int randomNum = Random.Range(0, bubbleSpawnList.Count);

        startedSpawnBubbles = true;

        Instantiate(bubbleObject, bubbleSpawnList[randomNum].position, bubbleObject.transform.rotation);

        yield return new WaitForSeconds(bubblesDelay);

        startedSpawnBubbles = false;



        
    
    }


    private bool startedLowerWaterRoutine;

    //start positions
    private Vector2 startWaterBasePosForLower;
    private Vector3 startBaseScaleForLower;


    private float lowerCounter;
    private float lowerTimer = 3f;
    //Lower the water
    private IEnumerator lowerWaterRoutine()
    {
        //audio
        blueMageAudioSource.clip = flowingWaterAudioClip;
        blueMageAudioSource.Play();
        //
        Debug.Log("doin b ryze hack");

        startedLowerWaterRoutine = true;

        Transform baseTransform = waterBase.transform;

        //Starting positions for the water
        Vector3 startBasePos = waterBase.transform.position;
        //Starting scale for the water
        Vector2 startBaseScale = baseTransform.localScale;



        while (lowerCounter <= lowerTimer)
        {

            baseTransform.position = Vector3.Lerp(startBasePos, new Vector2(startWaterBasePosForLower.x, startWaterBasePosForLower.y), lowerCounter / lowerTimer);
            baseTransform.localScale = new Vector2(startBaseScaleForLower.x, Mathf.Lerp(startBaseScale.y, startBaseScaleForLower.y, lowerCounter / lowerTimer));

            //spikes
            leftSpikeTransform.position = Vector3.Lerp(new Vector2(startSpikeLeftPos.x + 7f, startSpikeLeftPos.y) , startSpikeLeftPos , lowerCounter / lowerTimer);
            rightSpikeTransform.position = Vector3.Lerp(new Vector2(startSpikeRightPos.x - 7f, startSpikeLeftPos.y) , startSpikeRightPos, lowerCounter / lowerTimer);



            lowerCounter += Time.deltaTime;

            yield return null;


        }

        //leftSpike.SetActive(false);
        //rightSpike.SetActive(false);

        leftTrapcubby.SetActive(false);
        rightTrapcubby.SetActive(false);

        leftTrapCoveringWall.SetActive(true);
        rightTrapCoveringWall.SetActive(true);


        //Set routine checkers to false after the whole sequence is done
        startedLowerWaterRoutine = false;

        startedbubblesRoutine = false;
        startedRaiseWaterRoutine = false;
        startedPreRaiseWaterRoutine = false;
        startedSpawnBubbles = false;

        //counters to 0
        preraiseCounter = 0f;
        lowerCounter = 0f;
        raiseCounter = 0f;
        bubblesCounter = 0f;



        //reset mage attack var
        allMageBossesStateController.mageDoingAttack = false;
        allMageBossesStateController.startedAdjustMageStates = false;


        stateBlueMage = blueMageStatesENUM.Idle;
       
    }

    //--------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------//   
    //--------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------//
}
