using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class goblinLeftHandStates : MonoBehaviour
{
    //private reference to astrobuddy
    private GameObject playerObj;
    private Rigidbody2D leftHandRigidBody;

    //Public references to routes
    public GameObject leftHandIdleSpot;
    public GameObject smashLightHolder;
    public GameObject groundPoint;

    //Smashing from left to right routes
    public List<GameObject> leftToRightSmashRoute;

    //Swipe point list
    public List<GameObject> swipePointsRoute;
    public List<GameObject> swipeFromRightToLeftRoute;


    //Shockwave left and right points
    public GameObject shockwaveObject;
    public GameObject leftShockwave, rightShockwave;

    [HideInInspector]
    public kingLeftHandStates state;




    //Timers
    private float idleCounter;
    private float idleTimer = 3f;

    private float chargeFistCounter;
    private float chargeFistTimer = 0.5f;

   

    //Coroutine bools
    private bool startedIdleRoutine;
    private bool startedMoveToIdleSpotRoutine;
    //---------------------------------------

    private bool startedSmashRoomRoutine;
    private bool startedSmashDownRoutine;
    //Boolean to check whether or not the hand reached the ground
    private bool handReachedGround;

    //--------------------------------------
    private bool startedSwipeRoutine;


    //--------------------------------------
    private bool startedMoveToPlayerRoutine;

   
    
   





    //Smashing route counter int
    private int smashRouteCounterInt;

    //Swipe route counter int and randomizer for left to right
    private int swipeLeftToRightRouteCounterInt = 0;
    private int swipeRightToLeftRouteCounterInt = 0;

    

    //Box normalizer
    private Vector3 boxNormalizer = new Vector3(1f, 0f, 0f);



    //AUDIO RELATED
    private AudioSource leftHandAudioSource; 






    public enum kingLeftHandStates
    {
        
        Idle,
        SmashRoom,
        Swiping,
        SmashPlayer,
        testState,

        

    }

    private void OnDrawGizmos()
    {
        // moving the box start position to the right
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(this.transform.position + new Vector3(1f,0f,0f) , new Vector2(9f, 10f));

    }


    void Start()
    {
        //assign audio
        leftHandAudioSource = this.GetComponent<AudioSource>();

        leftHandRigidBody = this.GetComponent<Rigidbody2D>();

        playerObj = GameObject.Find("Astrobuddy");

        state = kingLeftHandStates.Idle;

        //state = kingLeftHandStates.testState;
    }

    
    void Update()
    {
        // checking to see if the player was hit 
        Collider2D[] mobTouchBox = Physics2D.OverlapBoxAll(this.transform.position + boxNormalizer, new Vector2(8f, 10f), 0f);

        foreach (Collider2D mobTouchObjs in mobTouchBox)
        {

            if (mobTouchObjs.tag == "PlayerHitbox")
            {
                Debug.Log("hit the playa");
                mobTouchObjs.GetComponentInParent<astroStats>().astroTakeDamage();
            }

            if(mobTouchObjs.tag == "Enemy")
            {
                Destroy(mobTouchObjs.gameObject);
            }

        }


        stateChanger();
        
    }


    private void stateChanger()
    {
       

        //Idle
        if(state == kingLeftHandStates.Idle)
        {

            if(startedMoveToIdleSpotRoutine == false)
            {
                StartCoroutine(moveTowardIdleLocationRoutine());
            }


        }
        //Smashing the room
        if(state == kingLeftHandStates.SmashRoom)
        {

            if(startedSmashRoomRoutine == false)
            {
                StartCoroutine(smashRoomRoutine());
            }


        }

        //Swiping the room

        if(state == kingLeftHandStates.Swiping)
        {

            if(startedSwipeRoutine == false)
            {
               
         

                StartCoroutine(swipeRoom());
            }

        }

        //Smashing the players location and creating a shockwave
        
        if(state == kingLeftHandStates.SmashPlayer)
        {

            if(startedMoveToPlayerRoutine == false)
            {
                StartCoroutine(moveToPlayerLocation());
            }

        }


    }
    //--------------------------------------------------------------------------------------
    //---------------------SMASH PLAYER LOCATION AND SHOCKWAVE RELATED----------------------
    //--------------------------------------------------------------------------------------


    private IEnumerator moveToPlayerLocation()
    {

        startedMoveToPlayerRoutine = true;

        // CONTINUE ON THIS TOMORROW-------------------------
        
        while (Mathf.Abs(playerObj.transform.position.x - this.transform.position.x) > 0.5f)
        {



            float xDir = (playerObj.transform.position.x - this.transform.position.x);

            leftHandRigidBody.velocity =  new Vector3(25f * Mathf.Sign(xDir), 0f, 0f);
            yield return null;

        }

        /*        
        while (this.transform.position.x != playerObj.transform.position.x)
        {

            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(playerObj.transform.position.x, this.transform.position.y, 0), 30f * Time.deltaTime);

            yield return null;

        }
        */

        leftHandRigidBody.velocity = Vector3.zero;

        StartCoroutine(chargeFistRoutine());

    }

    private IEnumerator chargeFistRoutine()
    {

        smashLightHolder.SetActive(true);

        while (chargeFistCounter <= chargeFistTimer)
        {
           

            smashLightHolder.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = Mathf.Lerp(0, 10, chargeFistCounter / chargeFistTimer);

            chargeFistCounter += Time.deltaTime;

            yield return null;

        }

        chargeFistCounter = 0f;


        smashLightHolder.SetActive(false);

        StartCoroutine(smashGroundRoutine());
    }



    //Smash audio
    public AudioClip smashSound;

    private IEnumerator smashGroundRoutine()
    {
        

       
        //startedSmashGroundRoutine = true;
        /*
        while (this.transform.position.y > groundPoint.transform.position.y)
        {

            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, groundPoint.transform.position.y, 0f) , 100f * Time.deltaTime);

            yield return null;

        }
        */

        


        while (Mathf.Abs(this.transform.position.y - groundPoint.transform.position.y) > 0.5f)
        {
            
           

            leftHandRigidBody.velocity = new Vector3(0f , -50f , 0f);
            yield return null;

        }

        //---Play smash audio
        leftHandAudioSource.clip = smashSound;
        leftHandAudioSource.Play();
        //---


        leftHandRigidBody.velocity = Vector3.zero;

        //instantiate shockwave
        Instantiate(shockwaveObject, leftShockwave.transform.position, shockwaveObject.transform.rotation);
        Instantiate(shockwaveObject, rightShockwave.transform.position, Quaternion.Euler(0f,180f,0f));
        
        //Setting initial states for bools
        //startedSmashGroundRoutine = false;
        startedMoveToPlayerRoutine = false;

        state = kingLeftHandStates.Idle;
    }





    //-------------------------------------------------------------
    //---------------------HAND SWIPE RELATED----------------------
    //-------------------------------------------------------------

    private IEnumerator swipeRoom()
    {
        //Determine left to right at random
        int swipeLeftToRight = Random.Range(0, 2);

        

        startedSwipeRoutine = true;

        //if we follow left to right route according to the random number
        if (swipeLeftToRight == 0)
        {

            while (swipeLeftToRightRouteCounterInt < swipePointsRoute.Count)
            {

               

                this.transform.position = Vector3.MoveTowards(this.transform.position, swipePointsRoute[swipeLeftToRightRouteCounterInt].transform.position, 50f * Time.deltaTime);

                if (this.transform.position == swipePointsRoute[swipeLeftToRightRouteCounterInt].transform.position)
                {
                    
                    swipeLeftToRightRouteCounterInt += 1;
                }

                yield return null;
            }

        }

        


        // if we follow the other route according to the random number
        if (swipeLeftToRight == 1)
        {

            while (swipeRightToLeftRouteCounterInt < swipeFromRightToLeftRoute.Count)
            {


                this.transform.position = Vector3.MoveTowards(this.transform.position, swipeFromRightToLeftRoute[swipeRightToLeftRouteCounterInt].transform.position, 50f * Time.deltaTime);

                if (this.transform.position == swipeFromRightToLeftRoute[swipeRightToLeftRouteCounterInt].transform.position)
                {
                    swipeRightToLeftRouteCounterInt += 1;
                }

                yield return null;
            }

        }


        // ending routine and setting variables 
        startedSwipeRoutine = false;

        //return numbers to initial state
        swipeRightToLeftRouteCounterInt = 0;
        swipeLeftToRightRouteCounterInt = 0;



        //back to the idle state
        state = kingLeftHandStates.Idle;

        


    }

   
    

    //------------------------------------------------------------
    //---------------------SMASH ROOM RELATED----------------------
    //------------------------------------------------------------

    // Hand smash from left to the right of the room , using two coroutines to keep track of the hand smashing (this and smashDownRoutine)
    private IEnumerator smashRoomRoutine()
    {

        startedSmashRoomRoutine = true;

        while(this.transform.position != leftToRightSmashRoute[smashRouteCounterInt].transform.position)
        {

            this.transform.position = Vector3.MoveTowards(this.transform.position, leftToRightSmashRoute[smashRouteCounterInt].transform.position, 100f * Time.deltaTime);


            yield return null;
        }


        StartCoroutine(smashDownRoutine());
        Debug.Log("reached it yooo");

        

    }

    // ssmashing down
    private IEnumerator smashDownRoutine()
    {
        
        

        startedSmashDownRoutine = true;

        while(handReachedGround == false)
        {
            // move the box to the right by adding a vector3 to it
            Collider2D[] mobTouchBox = Physics2D.OverlapBoxAll(this.transform.position + boxNormalizer , new Vector2(8f, 10f), 0f);

            foreach (Collider2D mobTouchObjs in mobTouchBox)
            {

                if (mobTouchObjs.name == "bossGround")
                {
                    Debug.Log("reached ground bossground");
                    handReachedGround = true;
                }

                

            }

            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -200f);

            yield return null;
        }

        //---Play smash audio
        leftHandAudioSource.clip = smashSound;
        leftHandAudioSource.Play();
        //---


        //Bools changed
        startedSmashRoomRoutine = false;

        handReachedGround = false;
        startedSmashDownRoutine = false;

        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        //Increasing the routelist counter by one
        smashRouteCounterInt += 1;

        //If we have reached the limit of the counter, we will return to the idle state
        if(smashRouteCounterInt == leftToRightSmashRoute.Count)
        {
            state = kingLeftHandStates.Idle;
            smashRouteCounterInt = 0;
        }
        
    }


    //------------------------------------------------------------
    //---------------------IDLE HAND RELATED----------------------
    //------------------------------------------------------------

    private IEnumerator moveTowardIdleLocationRoutine()
    {

        startedMoveToIdleSpotRoutine = true;

        /*
        while (this.transform.position != leftHandIdleSpot.transform.position)
        {

            this.transform.position = Vector3.MoveTowards(this.transform.position, leftHandIdleSpot.transform.position, 50f * Time.deltaTime);
            yield return null;
        }
        */

        while (Vector3.Distance(leftHandIdleSpot.transform.position , this.transform.position) > 0.5f)
        {

            Vector3 dir = (leftHandIdleSpot.transform.position - this.transform.position).normalized;




            leftHandRigidBody.velocity = dir * 40f;
            yield return null;

        }

        leftHandRigidBody.velocity = Vector3.zero;

        //Once we reach the idle hand spot, we start the idle hand routine

        if (startedIdleRoutine == false)
        {
            StartCoroutine(idleHandRoutine());
        }

        
    }

    // Simulate movement while the hand is idle
    private IEnumerator idleHandRoutine()
    {

        startedIdleRoutine = true;

        Vector2 randomPoint = Random.insideUnitCircle * 3f + new Vector2(leftHandIdleSpot.transform.position.x , leftHandIdleSpot.transform.position.y);

        while (idleCounter <= idleTimer)
        {
            
            this.transform.position = Vector3.MoveTowards(this.transform.position, randomPoint, 5f * Time.deltaTime);

            if (this.transform.position == new Vector3(randomPoint.x ,randomPoint.y,0))
            {
                
                randomPoint = Random.insideUnitCircle * 3f + new Vector2(leftHandIdleSpot.transform.position.x, leftHandIdleSpot.transform.position.y);
            }

            
                

            idleCounter += Time.deltaTime;

            yield return null;
        }

        idleCounter = 0f;

        //Set this routine start to false, along with the moving to idle spot routine above to false
        startedIdleRoutine = false;
        startedMoveToIdleSpotRoutine = false;


        int randomState = Random.Range(0, 3);

        //int randomState = 2;

        // Changing the state to a random state after the idle ends
        if (randomState == 0)
        {
            state = kingLeftHandStates.SmashRoom;
        }
        if (randomState == 1)
        {
            state = kingLeftHandStates.Swiping;
        }
        if (randomState == 2)
        {
            state = kingLeftHandStates.SmashPlayer;
        }

        

    }
}
