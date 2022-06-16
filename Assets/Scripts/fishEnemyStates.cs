using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishEnemyStates : MonoBehaviour
{

    //Timers, the going back and forth timers can be set in game.
    private float frontTimerCounter;
    public float frontTimer;

    private float backTimerCounter;
    public float backTimer;


    //State variable
    private fishStates state;


    //Private references
    private Vector2 fishStartPosition;
    private Rigidbody2D fishBody;
    private GameObject frogObj;

    //private booleans
    
    private bool startedPathingRoutine;

    //public references, wentForward can be set to make the fish start left or right
    public float fishSpeed;
    public bool wentForward;



    public enum fishStates
    {
       
        Pathing,
        Attacking
        
    }


    void Start()
    {
        fishBody = this.GetComponent<Rigidbody2D>();
        fishStartPosition = this.transform.position;
        state = fishStates.Pathing;
    }

    
    void Update()
    {

        stateChanger();

    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(this.transform.position,new Vector2(40f,20f));


    }


    private void stateChanger()
    {

        if(state == fishStates.Pathing)
        {

            Collider2D[] mobRangeArray = Physics2D.OverlapBoxAll(this.transform.position, new Vector2(40f, 20f), 0f);

            foreach (Collider2D mobRangeObjects in mobRangeArray)
            {

                if (mobRangeObjects.tag == "Frog")
                {
                    frogObj = mobRangeObjects.gameObject;
                    state = fishStates.Attacking;

                    Debug.Log("Frogman");

                }

            }

            if (!wentForward && !startedPathingRoutine)
            {
                StartCoroutine(fishSwimForward());
            }

            if(wentForward && !startedPathingRoutine)
            {
                StartCoroutine(fishSwimBackwards());
            }
        }

        if(state == fishStates.Attacking)
        {
            fishBody.velocity = Vector2.zero;
            

            transform.position = Vector3.MoveTowards(transform.position, frogObj.transform.position, fishSpeed * Time.deltaTime *5);
        }

       


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Frog")
        {
            Debug.Log("Frog should be dead and level should be reset");
        }

    }

    // Ienumerators to make the fish go fowards and backwards
    // checking if the frog is inside the hunt radius while in the coroutine
    private IEnumerator fishSwimForward()
    {
        startedPathingRoutine = true;


        while (frontTimerCounter <= frontTimer)
        {

            Collider2D[] mobRangeArray = Physics2D.OverlapBoxAll(this.transform.position, new Vector2(40f, 20f), 0f);

            foreach (Collider2D mobRangeObjects in mobRangeArray)
            {

                if (mobRangeObjects.tag == "Frog")
                {
                    state = fishStates.Attacking;
                    yield return null;

                }

            }



            fishBody.velocity = new Vector2(fishSpeed, 0f);
            frontTimerCounter += Time.deltaTime;

            yield return null;
        }
        frontTimerCounter = 0f;

        this.transform.Rotate(0, 180, 0);

        startedPathingRoutine = false;
        wentForward = true;

    }

    private IEnumerator fishSwimBackwards()
    {
        startedPathingRoutine = true;


        while (backTimerCounter <= backTimer)
        {

            Collider2D[] mobRangeArray = Physics2D.OverlapBoxAll(this.transform.position, new Vector2(40f, 20f), 0f);

            foreach (Collider2D mobRangeObjects in mobRangeArray)
            {

                if (mobRangeObjects.tag == "Frog")
                {
                    state = fishStates.Attacking;
                    yield return null;

                }

            }


            fishBody.velocity = new Vector2(-fishSpeed, 0f);
            backTimerCounter += Time.deltaTime;

            yield return null;
        }

        backTimerCounter = 0f;

        this.transform.Rotate(0, 180, 0);

        startedPathingRoutine = false;
        wentForward = false;

    }

}
