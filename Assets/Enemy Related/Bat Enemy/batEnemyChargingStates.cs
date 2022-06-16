using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batEnemyChargingStates : MonoBehaviour
{

    private batEnemyChargingStatesHolder state;

    //Player character
    private GameObject playerObj;

    //Transform
    private Vector3 playerPosBeforeAttack;
    private Vector3 dir;


    //Timers
    private float bounceBackTimer = 1.5f;
    private float bounceBackCounter;

    private float chargeAttackTimer = 2f;
    private float chargeAttackCounter;

    

    //Bool for coroutines
    private bool startedChargeRoutine;
    private bool startedBounceBackRoutine;

    



    //Bools
    private bool collidedWithSomething;

  


    public enum batEnemyChargingStatesHolder
    {
        Idle,
        Pathing,
        Attacking,
        BouncingBack,
        
    }

    void Start()
    {

        var playerObjTry = GameObject.Find("Astrobuddy");

        if (playerObjTry != null)
        {
            playerObj = playerObjTry;


        }

        state = batEnemyChargingStatesHolder.Idle;
        
    }

    
    void Update()
    {

        stateChanger();

    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 40f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, 25f);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(this.transform.position, 5f);



    }


    private void stateChanger()
    {
        //If the state is idle, stand still till the player comes in range
        if(state == batEnemyChargingStatesHolder.Idle)
        {

            //setting velkocity to 0 once we enter this state
            this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;


            Collider2D[] mobRangeArray = Physics2D.OverlapCircleAll(this.transform.position, 40f);

            foreach (Collider2D mobRangeObjects in mobRangeArray)
            {

                if (mobRangeObjects.tag == "PlayerHitbox")
                {

                    state = batEnemyChargingStatesHolder.Pathing;

                }

            }


        }

        //Pathing towards player state
        if(state == batEnemyChargingStatesHolder.Pathing)
        {

            Debug.Log("pathing");

            if (Mathf.Abs(Vector3.Distance(this.transform.position, playerObj.transform.position)) > 25f)
            {
                
                this.transform.position = Vector3.MoveTowards(this.transform.position, playerObj.transform.position, 10f * Time.deltaTime);


            }

            if (Mathf.Abs(Vector3.Distance(this.transform.position, playerObj.transform.position)) <= 25f)
            {

                state = batEnemyChargingStatesHolder.Attacking;

            }


        }


        //Charging towards player state
        if(state == batEnemyChargingStatesHolder.Attacking)
        {

            if(startedChargeRoutine == false)
            {

                StartCoroutine(chargeAttackRoutine());
            }


        }

        if( state == batEnemyChargingStatesHolder.BouncingBack)
        {

            if(startedBounceBackRoutine == false)
            {

                StartCoroutine(bounceBackRoutine());
            }

        }


    }



    //Charge attack routine
    private IEnumerator chargeAttackRoutine()
    {

        
        startedChargeRoutine = true;

        //Vector3 playerPosBeforeAttack = playerObj.transform.position + new Vector3(0f, 2f);

        playerPosBeforeAttack = playerObj.transform.position + new Vector3(0f, 2f);
        dir = (transform.position - playerPosBeforeAttack).normalized;

        while (collidedWithSomething == false)
        {

            //this.transform.position = Vector3.MoveTowards(this.transform.position, playerPosBeforeAttack, 30f * Time.deltaTime);
            chargeAttackCounter += Time.deltaTime;
            Debug.Log(chargeAttackTimer);

            if(chargeAttackCounter >= chargeAttackTimer)
            {

                collidedWithSomething = true;

            }
           

            Collider2D[] mobBumpArray = Physics2D.OverlapCircleAll(this.transform.position, 3f);

            foreach (Collider2D mobBumpable in mobBumpArray)
            {

                if (mobBumpable.tag == "Ground" || mobBumpable.tag == "PlayerHitbox" || mobBumpable.tag == "Wall")
                {

                    Debug.Log("Hallo");
                    collidedWithSomething = true;

                    if(mobBumpable.tag == "PlayerHitbox")
                    {

                        playerObj.GetComponent<astroStats>().astroTakeDamage();

                    }

                }

            }

            

            this.GetComponent<Rigidbody2D>().AddForce(-dir * 3500f * Time.deltaTime);


            yield return null;
       
        }

        
        Debug.Log("exit");

        chargeAttackCounter = 0f;

        state = batEnemyChargingStatesHolder.BouncingBack;

        startedChargeRoutine = false;
    
        collidedWithSomething = false;
    }

    private IEnumerator bounceBackRoutine()
    {

        startedBounceBackRoutine = true;

        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        

        while (bounceBackCounter <= bounceBackTimer)
        {

            Debug.Log("bouncing back");

            

            this.GetComponent<Rigidbody2D>().AddForce(dir * 1000f * Time.deltaTime);

            bounceBackCounter += Time.deltaTime;

            yield return null;
           
        }

        bounceBackCounter = 0f;

        startedBounceBackRoutine = false;

        state = batEnemyChargingStatesHolder.Idle;




    }
}
