using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblinEnemyStates : MonoBehaviour
{

    //Private references
    private Animator goblinAnimator;
    private Rigidbody2D goblinBody;
    private GameObject playerObj;
    private SpriteRenderer goblinSpriteRenderer;


    //Private floats
    private float directionIndicator;

    //State variable
    private goblinStates state;


    //Public references
    public float goblinSpeed;
    public GameObject attackPoint;
    public GameObject throwingKnife;

    //reference to audiosrc of the goblin
    private AudioSource goblinAudioSource;

    //Timers
    private float attackDelayTimer = 0.8f;
    private float attackDelayCounter;

    private float standStillTimer = 1f;
    private float standStillCounter;

    private float damageTakeTimer = 0.2f;
    private float damageTakeCounter;

    private float rangedAttackTimer = 0.8f;
    private float rangedAttackCounter;

    //Private booleans
    private bool attackDelayRoutineStarted;
    private bool rangedAttackRoutineStarted;
    private bool standStillRoutineStarted;

    //Health component
    private int goblinHealth = 3;

    public enum goblinStates
    {
        Idle,
        Pathing,
        Attacking,
        AfterAttack,
        RangedAttack,

    }



    void Start()
    {
        goblinAudioSource = this.GetComponent<AudioSource>();


        goblinSpriteRenderer = this.GetComponent<SpriteRenderer>();
        goblinBody = this.GetComponent<Rigidbody2D>();
        goblinAnimator = this.GetComponent<Animator>();
        state = goblinStates.Idle;
    }

      

    void Update()
    {
        // Checking if the player is in the hitbox of the karakter
        Collider2D[] mobTouchBox = Physics2D.OverlapBoxAll(this.transform.position, new Vector2(3f,5f) , 0f);

        foreach (Collider2D mobTouchObjs in mobTouchBox)
        {

            if (mobTouchObjs.tag == "PlayerHitbox")
            {
                Debug.Log("damageed the player");
                //mobTouchObjs.GetComponentInParent<astroStats>().astroTakeDamage();
                mobTouchObjs.GetComponentInParent<astroStats>().astroTakeDamage();
            }

        }

        if (playerObj != null)
        {
            directionIndicator = playerObj.transform.position.x - this.transform.position.x;
        }

        stateChanger();
        
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(this.transform.position, new Vector2(3f, 5f));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(this.transform.position, new Vector2(100f, 5f));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.transform.position + new Vector3(0f,3.5f,0f) , new Vector2(30f, 10f));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.transform.position, 4f);
    }


    private void stateChanger()
    {
        //Idle state
        if(state == goblinStates.Idle)
        {

            goblinBody.velocity = new Vector2(0f,goblinBody.velocity.y);

            goblinAnimator.SetBool("isMoving", false);



            Collider2D[] mobRangeArray = Physics2D.OverlapBoxAll(this.transform.position, new Vector2(100f, 5f), 0f);

            foreach (Collider2D mobRangeObjects in mobRangeArray)
            {

                if (mobRangeObjects.tag == "Player")
                {
                    playerObj = mobRangeObjects.gameObject;
                    state = goblinStates.Pathing;

                }

            }

        }



        //Pathing state
        if(state == goblinStates.Pathing)
        {
            goblinBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            //Rotating while pathing only
            rotateGoblin();

            //directionIndicator = playerObj.transform.position.x - this.transform.position.x;

            goblinBody.velocity = new Vector2(Mathf.Sign(directionIndicator) * goblinSpeed, goblinBody.velocity.y);
            //goblinBody.AddForce(new Vector2(Mathf.Sign(directionIndicator) * goblinSpeed, 0f));
            goblinAnimator.SetBool("isMoving", true);

            if(Mathf.Abs(goblinBody.velocity.x) > goblinSpeed)
            {
                
                goblinBody.velocity = new Vector2(goblinSpeed * Mathf.Sign(directionIndicator), goblinBody.velocity.y);
            }


            // checking if the player came in range of the hitbox
            Collider2D[] mobRangeArray = Physics2D.OverlapBoxAll(this.transform.position + new Vector3(0f, 3.5f, 0f), new Vector2(30f, 10f) , 0f);

            foreach (Collider2D mobRangeObjects in mobRangeArray)
            {

                if (mobRangeObjects.tag == "Player")
                {

                    int randomState = Random.Range(1, 3);
                    //int randomState = 2;

                    if (randomState == 1)
                    {
                        state = goblinStates.Attacking;
                    }
                    if(randomState == 2)
                    {
                        state = goblinStates.RangedAttack;
                    }

                }

                

            }

        }


        //Attacking state
        if(state == goblinStates.Attacking)
        {


            if (attackDelayRoutineStarted == false)
            {
                StartCoroutine(goblinAttackDelay());
            }
            


        }

        if(state == goblinStates.AfterAttack)
        {
            goblinAnimator.SetBool("isMoving", false);
            goblinBody.velocity = new Vector2(0f, goblinBody.velocity.y);

            if (standStillRoutineStarted == false)
            {
                StartCoroutine(goblinAfterAttackStandStill());
            }

        }

        if(state == goblinStates.RangedAttack)
        {
            goblinAnimator.SetBool("isMoving", false);

            //goblinBody.velocity = new Vector2(0f, goblinBody.velocity.y);
            Debug.Log("Ranged attacking");

            if (rangedAttackRoutineStarted == false)
            {
                StartCoroutine(goblinRangedAttackRoutine());
            }

            


        }


        
    }

    

    private void rotateGoblin()
    {
        

        if(Mathf.Sign(directionIndicator) > 0)
        {
            this.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else
        {
            this.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            
        }

    }

    public void animatorDamageEnemy()
    {

        Collider2D[] mobRangeArray = Physics2D.OverlapCircleAll(attackPoint.transform.position, 4f);

        foreach (Collider2D mobRangeObjects in mobRangeArray)
        {

            if (mobRangeObjects.tag == "PlayerHitbox")
            {

                mobRangeObjects.GetComponentInParent<astroStats>().astroTakeDamage();

            }

        }

        

    }

    public void animatorSetPathing()
    {
        goblinAnimator.SetBool("isAttacking", false);
    }



    // Attack delay Enumerator, after the attack is over, reset the player object to make it just pathing again until astrobuddy is seen again

    //attacking sound
    public AudioClip goblinSwordSlash;
    private IEnumerator goblinAttackDelay()
    {
        Debug.Log("Meele attack");

        goblinBody.AddForce(new Vector2(Mathf.Sign(directionIndicator) * 1000f, 30000f));

        attackDelayRoutineStarted = true;
        goblinAnimator.SetBool("isAttacking", true);

        

        while (attackDelayCounter <= attackDelayTimer)
        {
            //rotating during the attack aswell
            rotateGoblin();

            attackDelayCounter += Time.deltaTime;

            yield return null;
        }

        //play audio---
        goblinAudioSource.clip = goblinSwordSlash;
        goblinAudioSource.Play();
        //------

        //state = goblinStates.Idle;

        state = goblinStates.AfterAttack;

        attackDelayCounter = 0f;
     
        attackDelayRoutineStarted = false;

    }

    //Ranged attack and jump back
    //audio related to shuriken
    public AudioClip shurikenThrowClip;
    private IEnumerator goblinRangedAttackRoutine()
    {
       


        rangedAttackRoutineStarted = true;

        
        Debug.Log("Jumped");
        goblinBody.velocity = Vector2.zero;

        goblinBody.AddForce(new Vector2(-Mathf.Sign(directionIndicator) * 50000f, 30000f));

        while(rangedAttackCounter <= rangedAttackTimer)
        {

            rangedAttackCounter += Time.deltaTime;
            yield return null;
        }

        //play audio on throw
        goblinAudioSource.clip = shurikenThrowClip;
        goblinAudioSource.Play();
        //----

        GameObject thrownKnife = Instantiate(throwingKnife, attackPoint.transform.position, throwingKnife.transform.rotation);
        thrownKnife.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sign(directionIndicator) * 5000, 0f));

        rangedAttackCounter = 0;

        rangedAttackRoutineStarted = false;

        state = goblinStates.AfterAttack;

    }

    // Delaying movement after the jump attack was made.
    private IEnumerator goblinAfterAttackStandStill()
    {
        standStillRoutineStarted = true;

        state = goblinStates.AfterAttack;

        while (standStillCounter <= standStillTimer)
        {

            standStillCounter += Time.deltaTime;

            yield return null;
        }

        standStillCounter = 0f;

        state = goblinStates.Idle;

        standStillRoutineStarted = false;

    }


   




}
