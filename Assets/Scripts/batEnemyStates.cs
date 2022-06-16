using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batEnemyStates : MonoBehaviour
{

    private batStates state;

    //Public references
    //Set reachedEnd at inspector to decide rotation
    public bool reachedEnd;
    public bool bombLeftToRight;  

    public Transform startPoint;
    public Transform endPoint;

    public Transform eyeAttackPoint;
    public GameObject projectileObject;
    public float batSpeed;




    //private booleans
    
    private Rigidbody2D batBody;
    private bool attackRoutineStarted;


    //Timers
    public float attackTimer;
    private float attackTimerCounter;


    //Audio
    private AudioSource batAudioSource;
    public enum batStates
    {
        
        Pathing,
        Attacking,
        AfterAttack,

    }

    private void OnDrawGizmos()
    {
        //audiorange
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, circleDistanceForAudio);

        //Gizmos.color = Color.green;
        //Gizmos.DrawWireCube(eyeAttackPoint.transform.position, new Vector2(15f, 20f));



    }

    private GameObject playerObj;

    void Start()
    {
        var playerObjTry = GameObject.Find("Astrobuddy");

        if (playerObjTry != null)
        {
            playerObj = playerObjTry;


        }

        batAudioSource = this.GetComponent<AudioSource>();
        batBody = this.GetComponent<Rigidbody2D>();
        state = batStates.Pathing;

    }

    // Update is called once per frame

    public float circleDistanceForAudio;

    //audio related
    private bool shouldPlayAudio;
    void Update()
    {

        if (Vector3.Distance(this.transform.position, playerObj.transform.position) < circleDistanceForAudio)
        {
            shouldPlayAudio = true;
        }
        else
        {
            shouldPlayAudio = false;
        }


        stateChanger();


    }


    private void stateChanger()
    {

        if(state == batStates.Pathing)
        {

            moveBat();

        }

        

    }

    private void moveBat()
    {

        if (this.transform.localPosition.x >= endPoint.localPosition.x)
        {
            
            reachedEnd = true;
            this.transform.eulerAngles = new Vector2(0f, 180f);


        }


        if (this.transform.localPosition.x <= startPoint.localPosition.x)
        {
            this.transform.eulerAngles = new Vector2(0f, 0f);
            reachedEnd = false;

        }



        if (!reachedEnd)
        {

            if (attackRoutineStarted == false && bombLeftToRight)
            {
                StartCoroutine(batEyeAttack());
            }

            batBody.velocity = new Vector2(batSpeed, 0f);

            

        }

        if (reachedEnd)
        {

            if (attackRoutineStarted == false && bombLeftToRight == false)
            {
                StartCoroutine(batEyeAttack());
            }

            batBody.velocity = new Vector2(-batSpeed, 0f);

           

        }
    }

    public AudioClip projectileAudio;
    private IEnumerator batEyeAttack()
    {
        //
        if (shouldPlayAudio == true)
        {
            batAudioSource.clip = projectileAudio;
            batAudioSource.Play();
        }
        //

        attackRoutineStarted = true;

        GameObject firedProjectile = Instantiate(projectileObject, this.transform.position, projectileObject.transform.rotation);

        //firedProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -20f);

        while (attackTimerCounter <= attackTimer)
        {

            attackTimerCounter += Time.deltaTime;

            yield return null;
        }

        attackTimerCounter = 0f;

        attackRoutineStarted = false;

    }
}
