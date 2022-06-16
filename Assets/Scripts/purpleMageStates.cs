using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purpleMageStates : MonoBehaviour
{
    [HideInInspector]
    public purpleMageStatesENUM statePurpleMage;

    private GameObject playerObj;



   

    //AUDIOSOURCE FOR THIS CHARACTER
    AudioSource purpleMageAudioSource;


    public enum purpleMageStatesENUM
    {
        
        Idle,
        wallLazer,
        
    }

    void Start()
    {
        //audiosource setting
        purpleMageAudioSource = this.GetComponent<AudioSource>();

        
        //statePurpleMage = purpleMageStatesENUM.Idle;


        var playerObjTry = GameObject.Find("Astrobuddy");

        if (playerObjTry != null)
        {
            playerObj = playerObjTry;


        }
    }

    
    void Update()
    {
       
        Debug.Log(statePurpleMage);
        stateChanger();
    }

    private void stateChanger()
    {
        if (statePurpleMage == purpleMageStatesENUM.Idle)
        {

           
        }


        if (statePurpleMage == purpleMageStatesENUM.wallLazer)
        {
            
            if(startedWallBeamsAttacks == false)
            {
                StartCoroutine(wallBeamsAttacks());
            }
        }

    }



    //--------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------//
    //--------------VOID BEAMS FROM WALLS ROUTINE-------------------------------------//
    //--------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------//

    

    private bool startedWallBeamsAttacks;

    private float wallBeamAttackTimer = 35f;
    private float wallBeamAttackCounter;
    private IEnumerator wallBeamsAttacks()
    {
        

     


        startedWallBeamsAttacks = true;

        while(wallBeamAttackCounter <= wallBeamAttackTimer)
        {
            if (startedPreWallBeamRoutine == false && lazerFiredAmount < 4)
            {
                StartCoroutine(preWallBeamRoutine());
            }

            wallBeamAttackCounter += Time.deltaTime;

            yield return null;

        }
        //not doing an attack
        allMageBossesStateController.mageDoingAttack = false;
        allMageBossesStateController.startedAdjustMageStates = false;



        //change the state here once this ends
        //reset floats    
        wallBeamAttackCounter = 0f;
        lazerFiredAmount = 0;
        //reset bools       
        startedWallBeamsAttacks = false;


     


        //allow player to move again if we couldnt end the lazers
        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        //set to idle state to wait for a change;
        statePurpleMage = purpleMageStatesENUM.Idle;




    }





    private bool startedPreWallBeamRoutine;

    public List<GameObject> preWallBeamParticles;
    public List<GameObject> preGroundBeamParticles;

    private float preWallBeamCounter;
    //this will be adjust according to the sfx
    private float preWallBeamTimer = 6.7f;


    private int randomSpawnWallNum;
    private int randomSpawnGround;

    //Amount of times the lazers have been done in the attack routine so we can limit it
    private int lazerFiredAmount = 0;


    //particlesystem of pre beam
    ParticleSystem.MainModule preParticleGroundMain , preParticleWallMain;
    

    //Laser windup soundclip
    public AudioClip laserWindUpSound;
    private IEnumerator preWallBeamRoutine()
    {
        lazerFiredAmount += 1;

        //play audio to simulate wind up
        purpleMageAudioSource.clip = laserWindUpSound;
        purpleMageAudioSource.Play();


        startedPreWallBeamRoutine = true;

        randomSpawnWallNum = Random.Range(0, preWallBeamParticles.Count);
        randomSpawnGround = Random.Range(0, preGroundBeamParticles.Count);
        
        for(int i = 0; i < preWallBeamParticles.Count; i++)
        {
            //leave a place open for the wall
            if(i == randomSpawnWallNum)
            {
                // adjusting the saving player part for the lazer
                preParticleWallMain = preWallBeamParticles[i].GetComponent<ParticleSystem>().main;
                preParticleWallMain.startColor = Color.black;

                preWallBeamParticles[i].SetActive(true);

                continue;
            }

            preWallBeamParticles[i].SetActive(true);
        }
        

        for (int i = 0; i < preGroundBeamParticles.Count; i++)
        {
            //leave a place open for the wall
            if (i == randomSpawnGround)
            {
                // adjusting the saving player part for the lazer
                preParticleGroundMain = preGroundBeamParticles[i].GetComponent<ParticleSystem>().main;
                preParticleGroundMain.startColor = Color.black;

                preGroundBeamParticles[i].SetActive(true);

                continue;
            }

            preGroundBeamParticles[i].SetActive(true);
        }


        while (preWallBeamCounter <= preWallBeamTimer)
        {

            preWallBeamCounter += Time.deltaTime;

            yield return null;

        }

        //reset the prewall beams to normal
        preParticleGroundMain.startColor = Color.white;
        preParticleWallMain.startColor = Color.white;
        

        if(startedLazersRoutine == false)
        {
            StartCoroutine(startLazersRoutine());
        }


    }


    public List<GameObject> groundLazers;
    public List<GameObject> wallLazers;

    private bool startedLazersRoutine;




    private float lazerActiveTimer = 1f;
    private float lazerActiveCounter;


    //pre assign the particle reference
    ParticleSystem.MainModule lazerParticleMainWall, lazerParticleMainGround;
    private IEnumerator startLazersRoutine()
    {

        startedLazersRoutine = true;

        //set the active of the pre particles to false
        foreach (GameObject preWallParticle in preWallBeamParticles)
        {
            preWallParticle.SetActive(false);
        }
        foreach (GameObject preGroundParticle in preGroundBeamParticles)
        {
            preGroundParticle.SetActive(false);
        }
        


        //for ground
        for(int i = 0; i < groundLazers.Count; i++)
        {
            if(i == randomSpawnGround)
            {
                // adjusting the saving player part for the lazer
                lazerParticleMainGround = groundLazers[i].GetComponent<ParticleSystem>().main;
                lazerParticleMainGround.startColor = Color.green;

                //make it not do damage to the player
                groundLazers[i].GetComponent<lazerZeroGravity>().enabled = true;
                groundLazers[i].GetComponent<fireLineParticleCollision>().enabled = false;

                groundLazers[i].SetActive(true);

                
                continue;
            }
            groundLazers[i].SetActive(true);
        }

        //for wall
        for (int i = 0; i < wallLazers.Count; i++)
        {
            if (i == randomSpawnWallNum)
            {
                lazerParticleMainWall = wallLazers[i].GetComponent<ParticleSystem>().main;
                lazerParticleMainWall.startColor = Color.green;



                wallLazers[i].GetComponent<lazerZeroGravity>().enabled = true;
                wallLazers[i].GetComponent<fireLineParticleCollision>().enabled = false;

                wallLazers[i].SetActive(true);

                continue;
            }
            wallLazers[i].SetActive(true);
        }



        while(lazerActiveCounter <= lazerActiveTimer)
        {

            lazerActiveCounter += Time.deltaTime;
            yield return null;
        }

        //reset the lazers that were changed
        lazerParticleMainGround.startColor = Color.white;
        lazerParticleMainWall.startColor = Color.white;

        wallLazers[randomSpawnWallNum].GetComponent<lazerZeroGravity>().enabled = false;
        wallLazers[randomSpawnWallNum].GetComponent<fireLineParticleCollision>().enabled = true;

        groundLazers[randomSpawnGround].GetComponent<lazerZeroGravity>().enabled = false;
        groundLazers[randomSpawnGround].GetComponent<fireLineParticleCollision>().enabled = true;


        //reset indic
      


        //disable the lazers
        foreach (GameObject wallLazer in wallLazers)
        {
            wallLazer.SetActive(false);
        }
        foreach (GameObject groundLazer in groundLazers)
        {
            groundLazer.SetActive(false);
        }

        //allow player to move again
        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        //allow player to dash again
        playerObj.GetComponent<playerMovement>().canDash = true;



        //reset floats
        preWallBeamCounter = 0f;
        lazerActiveCounter = 0f;
        //reset bools
        startedLazersRoutine = false;
        startedPreWallBeamRoutine = false;


    }

   
}
