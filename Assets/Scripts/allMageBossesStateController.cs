using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class allMageBossesStateController : MonoBehaviour
{
    public GameObject redMageRelated, blueMageRelated, purpleMageRelated;
    public GameObject redMageCharacter, blueMageCharacter, purpleMageCharacter;


    public Slider timeBar;


    //public static bool that shows if an attack is being done
    public static bool mageDoingAttack;

    private GameObject playerObj;

    

    void Start()
    {
        var playerObjTry = GameObject.Find("Astrobuddy");

        if (playerObjTry != null)
        {
            playerObj = playerObjTry;


        }


        //set the randomattacklist
        randomAttackList.Add(0);
        randomAttackList.Add(1);
        randomAttackList.Add(2);
        randomAttackList.Add(3);
        randomAttackList.Add(4);

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (startedAdjustMageStates == false)
        {
            StartCoroutine(adjustMageStates());
        }
        */



      
        if (startedmageBossDuration == false)
        {
            StartCoroutine(mageBossDuration());
        }
    }
    //timeBar

    private bool startedmageBossDuration = false;

    private float mageBossDurationCounter;
    //boss duration
    private float mageBossDurationTimer = 240f;

    public GameObject fightEndingRelatedHolder;

   
  
    private IEnumerator mageBossDuration()
    {

        startedmageBossDuration = true;

       

        while (mageBossDurationCounter <= mageBossDurationTimer)
        {
            // cast spells
            if (startedPrepareRoutine == false && mageDoingAttack == false)
            {
                StartCoroutine(prepareMageAndStartAttack());
            }


            timeBar.value = timeBar.maxValue -  mageBossDurationCounter;

            mageBossDurationCounter += Time.deltaTime;

            yield return null;

        }

        //----
        if(startedEndRoutine == false)
        {
            StartCoroutine(endMageBoss());
        }

        //unfreeze player if he was frozen
        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;


        startedmageBossDuration = false;




        //set this to false after the fight is done
        

    }
    //ending of the boss related
    private bool startedEndRoutine;

    //Things to reset at the end of the fight
    public GameObject waterBase;
  
    //beginning positions for the water spots;
    private Vector3 waterBaseStartPos = new Vector3(-1.34f, -17.39f, 0f);
    private Vector3 waterTopStartPos = new Vector3(-0.06836893f, 0.8558463f, 0f);
    //beinning scales for the waters;
    private Vector3 waterBaseStartScale = new Vector3(44.62465f, 8.539553f, 1f);
    private Vector3 waterTopStartScale = new Vector3(0.02240914f, 0.1171022f, 1f);
    //------

    private float endBossCounter;
    private float endBossTimer = 3f;


    //disable discoball
    public GameObject discoBall;
    //lazer holders
    public GameObject wallLazers;
    //spiraling water
    public GameObject spiralWaters;
    //pooling objects ( disable projectiles too )
    public GameObject poolingObj;
    //
    public GameObject entryRelatedHolder;

    //spikes
    public GameObject leftSpike, rightSpike;
    //scene camera
    public Camera sceneCamera;

    //survive timer to disable
    public GameObject surviveCounterHolder;
    private IEnumerator endMageBoss()
    {
        //end coroutines
        blueMageRelated.GetComponent<blueMageStates>().StopAllCoroutines();
        redMageRelated.GetComponent<redMageStates>().StopAllCoroutines();
        purpleMageRelated.GetComponent<purpleMageStates>().StopAllCoroutines();

        blueMageRelated.GetComponent<blueMageStates>().enabled = false;
        redMageRelated.GetComponent<redMageStates>().enabled = false;
        purpleMageRelated.GetComponent<purpleMageStates>().enabled = false;

        //------
        surviveCounterHolder.SetActive(false);

        leftSpike.SetActive(false);
        rightSpike.SetActive(false);

        List<GameObject> eruptionPool = poolingObj.GetComponent<mageBossObjectPool>().eruptionPool;
        List<GameObject> spiralWaterPool = poolingObj.GetComponent<mageBossObjectPool>().waterSpiralPool;
        //List<GameObject> eruptionPool = poolingObj.GetComponent<mageBossObjectPool>().eruptionPool;
        //remove pooled objects(set active to false)
        foreach (GameObject pooledObj in eruptionPool)
        {
            pooledObj.SetActive(false);
        }
        foreach (GameObject pooledObj in spiralWaterPool)
        {
            pooledObj.SetActive(false);
        }




        ///---------------

        startedEndRoutine = true;

        //disable certaingameobjects
        discoBall.SetActive(false);
        wallLazers.SetActive(false);
        spiralWaters.SetActive(false);

        //water starts
        Transform baseTransform = waterBase.transform;

        Vector3 currentBaseStartPos = waterBase.transform.position;

        Vector3 currentBaseScale = baseTransform.localScale;
        //Vector3 currentTopScale = waterBase.transform.localScale;

        

        while (endBossCounter <= endBossTimer)
        {

            //waterBase.transform.position = Vector3.Lerp(currentBaseStartPos, waterBaseStartPos, endBossCounter / endBossTimer);
            //waterBase.transform.localScale = new Vector2(waterBaseStartScale.x, Mathf.Lerp(currentBaseScale.y, waterBaseStartScale.y, endBossCounter / endBossTimer));
            Debug.Log("hello im doing it");

            //baseTransform.position = Vector3.Lerp(currentBaseStartPos, new Vector2(waterBaseStartPos.x, waterBaseStartPos.y), endBossCounter / endBossTimer);
            //baseTransform.localScale = new Vector2(waterBaseStartScale.x, Mathf.Lerp(currentBaseScale.y, waterBaseStartScale.y, endBossCounter / endBossTimer));

            baseTransform.position = Vector3.Lerp(currentBaseStartPos, new Vector2(currentBaseStartPos.x, waterBaseStartPos.y), endBossCounter / endBossTimer);
            baseTransform.localScale = new Vector2(currentBaseScale.x, Mathf.Lerp(currentBaseScale.y, waterBaseStartScale.y, endBossCounter / endBossTimer));


            endBossCounter += Time.deltaTime;

            yield return null;


        }
        //entry related should be gone
        entryRelatedHolder.SetActive(false);

        //start ending related stuff
        fightEndingRelatedHolder.SetActive(true);

        //camera related
        sceneCamera.orthographicSize = 95f;
    }




    //----------------------------



    List<int> randomAttackList = new List<int>();

    public static bool startedAdjustMageStates;
    

    private float prepareTimer = 3f;
    private float prepareCounter;

    private bool startedPrepareRoutine;

    public Transform activeMagePoint;




    //particle systems to indicate whos attacking
    public GameObject purpleMageAttackIndicator, redMageAttackIndicator, blueMageAttackIndicator;
    private IEnumerator prepareMageAndStartAttack()
    {
        startedPrepareRoutine = true;

    

        if (randomAttackList.Count == 0)
        {
            randomAttackList.Add(0);
            randomAttackList.Add(1);
            randomAttackList.Add(2);
            randomAttackList.Add(3);
            randomAttackList.Add(4);
        }

       
        int randomMageAttack = Random.Range(0, randomAttackList.Count);
        //int randomMageAttack = 3;
        Debug.Log("number was " + randomMageAttack);



        //purple-red-blue-blue-red
        while (prepareCounter <= prepareTimer)
        {
            

            prepareCounter += Time.deltaTime;
            yield return null;

        }


        if (mageDoingAttack == false)
        {
           

            //Purple mage wall beams attack
            if (randomAttackList[randomMageAttack] == 0)
            {
                purpleMageRelated.GetComponent<purpleMageStates>().statePurpleMage = purpleMageStates.purpleMageStatesENUM.wallLazer;
                randomAttackList.Remove(0);

            

                mageDoingAttack = true;
            }

            else if (randomAttackList[randomMageAttack] == 1)
            {
                redMageRelated.GetComponent<redMageStates>().stateRedMage = redMageStates.redMageStatesENUM.discoFire;
                randomAttackList.Remove(1);

              


                mageDoingAttack = true;
            }
            else if (randomAttackList[randomMageAttack] == 2)
            {
                blueMageRelated.GetComponent<blueMageStates>().stateBlueMage = blueMageStates.blueMageStatesENUM.spiralWater;
                randomAttackList.Remove(2);

              

                mageDoingAttack = true;

            }
            else if (randomAttackList[randomMageAttack] == 3)
            {
                blueMageRelated.GetComponent<blueMageStates>().stateBlueMage = blueMageStates.blueMageStatesENUM.raiseWater;
                randomAttackList.Remove(3);

                

                mageDoingAttack = true;

            }
            else if (randomAttackList[randomMageAttack] == 4)
            {
                redMageRelated.GetComponent<redMageStates>().stateRedMage = redMageStates.redMageStatesENUM.torchEruption;
                randomAttackList.Remove(4);

                

                mageDoingAttack = true;

            }

        }

        prepareCounter = 0f;

        startedPrepareRoutine = false;
    }



    
}
