using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batEnemyWithProjectile : MonoBehaviour
{

    private batEnemyWithProjectileStates state;

    //Player object
    private GameObject playerObj;

    //Public references
    public GameObject projectileObj;

    //Timers
    private float moveOutTimer = 2f;
    private float moveOutCounter;

    private float projectileTimer = 2f;
    private float projectileCounter;

    private float damageTakeCounter;
    private float damageTakeTimer = 0.2f;

    //Booleans for Coroutines
    private bool startedMoveOutRoutine;
    private bool startedProjectileRoutine;

    //Stats
    private int batHealth = 2;


    
    public enum batEnemyWithProjectileStates
    {
        Idle,
        Pathing,
        Attacking,
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, 3f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position , 30f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, 20f);




    }

    void Start()
    {

        

        var playerObjTry = GameObject.Find("Astrobuddy");

        if (playerObjTry != null)
        {
            playerObj = playerObjTry;


        }

        state = batEnemyWithProjectileStates.Idle;

    }

    
    void Update()
    {
        // Checking if the player is in the hitbox of the karakter
        Collider2D[] mobTouchBox = Physics2D.OverlapCircleAll(this.transform.position, 3f);

        foreach (Collider2D mobTouchObjs in mobTouchBox)
        {

            if (mobTouchObjs.tag == "PlayerHitbox")
            {
                Debug.Log("damageed the player");          
                mobTouchObjs.GetComponentInParent<astroStats>().astroTakeDamage();
            }

        }


        stateChanger();

        //Debug.Log(Mathf.Abs(Vector3.Distance(this.transform.position, playerObj.transform.position)));
    }

    private void stateChanger()
    {

        if(state == batEnemyWithProjectileStates.Idle)
        {

            Collider2D[] mobRangeArray = Physics2D.OverlapCircleAll(this.transform.position, 30f);

            foreach (Collider2D mobRangeObjects in mobRangeArray)
            {

                if (mobRangeObjects.tag == "PlayerHitbox")
                {

                    state = batEnemyWithProjectileStates.Pathing;

                }

            }


        }


        if(state == batEnemyWithProjectileStates.Pathing)
        {


            if (startedMoveOutRoutine == false)
            {
                //Shoot projectiles
                if (startedProjectileRoutine == false)
                {
                    StartCoroutine(projectileAttack());
                }

                // Move the bat towards the player
                if (Mathf.Abs(Vector3.Distance(this.transform.position, playerObj.transform.position)) > 20f)
                {
                    Debug.Log("moving mr white");
                    this.transform.position = Vector3.MoveTowards(this.transform.position, playerObj.transform.position, 10f * Time.deltaTime);


                }
            }



            if (Mathf.Abs(Vector3.Distance(this.transform.position, playerObj.transform.position)) <= 20f)
            {
                if (startedMoveOutRoutine == false)
                {
                    StartCoroutine(moveOutOfRange());
                    
                }
            }
            
            

        }


    }


    

    // Run from the player once in a certain radius with a lower speed
    private IEnumerator moveOutOfRange()
    {

        Debug.Log("ayyyy");

        startedMoveOutRoutine = true;

        while(moveOutCounter <= moveOutTimer)
        {
            // Fire projectiles while moving out
            if (startedProjectileRoutine == false)
            {
                StartCoroutine(projectileAttack());
            }


            Debug.Log("moving proper");

            Vector3 dir = (transform.position - playerObj.transform.position).normalized;
            transform.Translate(dir * 3 *  Time.deltaTime);

            moveOutCounter += Time.deltaTime;
           
            yield return null;

        }

        moveOutCounter = 0f;

        startedMoveOutRoutine = false;

    }

    private IEnumerator projectileAttack()
    {

        GameObject thrownProjectile =  Instantiate(projectileObj, this.transform.position, projectileObj.transform.rotation);

        // Get the direction to rotate the projectile at the start
        Vector3 dir = playerObj.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
       

        thrownProjectile.GetComponent<Rigidbody2D>().velocity = (playerObj.transform.position - transform.position).normalized * 30f;
        thrownProjectile.transform.eulerAngles = new Vector3(0, 0, angle - 90f);



        startedProjectileRoutine = true;

        while (projectileCounter <= projectileTimer)
        {

            projectileCounter += Time.deltaTime;

            yield return null;

        }

        projectileCounter = 0f;

        startedProjectileRoutine = false;

    }

   
}
