using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroomMobStates : MonoBehaviour
{
    //mushroomStates variable to keep track of which state the mushroom is in
    private mushroomStates state;

    private float directionIndicator;


    //private references
    private Rigidbody2D mushroomBody;
    private GameObject playerObj;
    private SpriteRenderer mushroomSprite;

    //public references
    public Transform attackPoint;
    public Transform headPoint;
    public Animator mushroomAnimator;
    public GameObject projectileObject;

    //Timers
    private float attackTimerCounter;
    private float attackTimer = 1f;

    private float projectileTimerCounter;
    private float projectileTimer = 0.5f;

    private float leftrightProjectileTimerCounter;
    private float leftrightProjectileTimer = 0.5f;

    //private booleans
    private bool AttackRoutineStarted;
    private bool ProjectileRoutineStarted;
    private bool leftrightProjectileRoutineStarted;

    //private int
    private int mushroomHealth = 20;
    


    public enum mushroomStates
    {
        Idle,
        Pathing,
        Combat,
        StandStill,
        playerOnHead,
        waitingQueueState
    }

    void Start()
    {
        mushroomSprite = this.GetComponent<SpriteRenderer>();
        mushroomBody = this.GetComponent<Rigidbody2D>();

        state = mushroomStates.Idle;       
    }

   
    void Update()
    {

       
        stateChanger();

        mushroomDeath();

        

    }

    private void OnDrawGizmos()
    {


        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, new Vector2(40f, 20f));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.transform.position, new Vector2(8f, 5f));

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.transform.position, 2.5f);

        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(headPoint.transform.position, new Vector2(1.5f, 1f));



    }



    private void stateChanger()
    {
        //Idle state
        if(state == mushroomStates.Idle)
        {

            Collider2D[] mobRangeArray = Physics2D.OverlapBoxAll(this.transform.position, new Vector2(40f, 20f), 0f);

            foreach (Collider2D mobRangeObjects in mobRangeArray)
            {

                if (mobRangeObjects.tag == "Player")
                {
                    playerObj = mobRangeObjects.gameObject;
                    state = mushroomStates.Pathing;

                }

            }

        }

        //Pathing state
        if(state == mushroomStates.Pathing)
        {
            // if the constraints didn't get freed, free them here
            mushroomBody.constraints = RigidbodyConstraints2D.FreezeRotation;

            directionIndicator = playerObj.transform.position.x - this.transform.position.x;
            

            //Mushroom should be rotated only when pathing and not on top of the character
            if (Mathf.Abs(directionIndicator) > 2.5f)
            {
                Debug.Log("im pathing yo");

                mushroomBody.velocity = new Vector2(Mathf.Sign(directionIndicator) * 10f, mushroomBody.velocity.y);

                rotateMushroom();

            }
            else
            {
                if (ProjectileRoutineStarted == false)
                {
                    StartCoroutine(shootProjectileDelay());
                }
                //state = mushroomStates.playerOnHead;
            }


            // checking for player in the attack radius
            Collider2D[] mobAttackRange = Physics2D.OverlapBoxAll(this.transform.position, new Vector2(8f, 5f), 0f);

            foreach (Collider2D mobRangeAttackObjects in mobAttackRange)
            {

                if (mobRangeAttackObjects.tag == "Player")
                {
          
                    mushroomAnimator.SetBool("attackingPlayer", true);                   
                    state = mushroomStates.StandStill;

                }

            }

        }

        //If player is on the head state

        if(state == mushroomStates.playerOnHead)
        {
            
            if(ProjectileRoutineStarted == false)
            {

                StartCoroutine(shootProjectileDelay());

            }


        }

        //Stand still state, going into one of the different attack states depending on which random number was chosen
        //
        if(state == mushroomStates.StandStill)
        {

            int randomNumber = Random.Range(0, 2);

            mushroomBody.constraints = RigidbodyConstraints2D.FreezeAll;

            if (randomNumber == 0)
            {
                if (AttackRoutineStarted == false)
                {                    
                    StartCoroutine(attackDelay());
                    state = mushroomStates.waitingQueueState;
                }
            }

            if (randomNumber == 1)
            {

                if(leftrightProjectileRoutineStarted == false)
                {                    
                    StartCoroutine(leftrightProjectileDelay());
                    state = mushroomStates.waitingQueueState;
                }

            }


            
        }

        

    }

    // Used to set the state to pathing once the attack animation is over.
    public void setAnimationIdle()
    {
   
        mushroomAnimator.SetBool("attackingPlayer", false);

    }

    public void detectPlayerHit()
    {

        Collider2D[] mobAttackRange = Physics2D.OverlapCircleAll(attackPoint.transform.position, 2.5f);

        foreach (Collider2D mobRangeAttackObjects in mobAttackRange)
        {

            if (mobRangeAttackObjects.tag == "Player")
            {

                playerObj.GetComponent<astroStats>().astroTakeDamage();


            }

        }


    }

    



    // changing the rotation to look at the player
    private void rotateMushroom()
    {

        if (mushroomBody.velocity.x > 0)
        {
            this.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (mushroomBody.velocity.x < 0)
        {
            this.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }


    }

    // Method to take damage when hit by astroAbilities, changing the color aswell to indicate that it is hit
    public void takeDamage()
    {

        mushroomHealth -= 1;
        mushroomSprite.color = new Color(0.7f, 0f, 0f);

        StartCoroutine(revertOriginal());

    }

    private void mushroomDeath()
    {

        if(mushroomHealth == 0)
        {
            Destroy(this.gameObject);
        }

    }


    private IEnumerator attackDelay()
    {
        AttackRoutineStarted = true;

        
        while (attackTimerCounter <= attackTimer)
        {

            attackTimerCounter += Time.deltaTime;

            yield return null;
        }

        attackTimerCounter = 0f;

        

        Debug.Log("got here");

        //mushroomAnimator.SetBool("attackingPlayer", false);
        mushroomBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        state = mushroomStates.Pathing;


        AttackRoutineStarted = false;



    }

    // change the color of the character over time to indicate that an attack is being charged. Throw a projectile
    // Once the character is fully charged, switch state back to pathing.
    private IEnumerator shootProjectileDelay()
    {
        ProjectileRoutineStarted = true;

        while (projectileTimerCounter <= projectileTimer)
        {

            projectileTimerCounter += Time.deltaTime;

            mushroomSprite.color = new Color(1f, Mathf.Lerp(1f, 0f, projectileTimerCounter *2), 1f);

            yield return null;
        }

        projectileTimerCounter = 0f;

        GameObject firedProjectile = Instantiate(projectileObject, headPoint.transform.position, headPoint.transform.rotation);

        firedProjectile.GetComponent<Rigidbody2D>().velocity = (new Vector2(0f, 20f));

        mushroomSprite.color = new Color(1f,1f,1f);

        state = mushroomStates.Pathing;


        ProjectileRoutineStarted = false;


    }

    // shooting projectiles to the left and right of the character
    // turning the character green to indicate that this is happening
    private IEnumerator leftrightProjectileDelay()
    {
        leftrightProjectileRoutineStarted = true;

        while (leftrightProjectileTimerCounter <= leftrightProjectileTimer)
        {

            leftrightProjectileTimerCounter += Time.deltaTime;

            mushroomSprite.color = new Color(Mathf.Lerp(1f, 0f, leftrightProjectileTimerCounter * 2), 1f , Mathf.Lerp(1f, 0f, leftrightProjectileTimerCounter * 2));

            yield return null;
        }

        leftrightProjectileTimerCounter = 0f;

        GameObject firedProjectile = Instantiate(projectileObject, attackPoint.transform.position, attackPoint.transform.rotation);

        firedProjectile.GetComponent<Rigidbody2D>().velocity = (new Vector2(Mathf.Sign(directionIndicator) * 20f, 0f));

        mushroomSprite.color = new Color(1f, 1f, 1f);

        state = mushroomStates.Pathing;


        leftrightProjectileRoutineStarted = false;


    }


    // reverting the color of the mushroom after getting hit
    IEnumerator revertOriginal()
    {
        yield return new WaitForSeconds(0.3f);
        mushroomSprite.color = new Color(1f, 1f, 1f);

    }
}
