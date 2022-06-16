using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class goblinPrisonGuardStates : MonoBehaviour
{
    // pre assign vectors with Y equal to goblins y
    Vector3 leftPointToGo, rightPointToGo;
    // Start is called before the first frame update
    private GameObject playerObj;

    private bool facingRight = true;


    private Animator thisAnimator;

    
    void Start()
    {

        thisAnimator = this.GetComponent<Animator>();

        if (moveRightAtStart == false)
        {
            this.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            facingRight = false;
        }

        var playerObjTry = GameObject.Find("Astrobuddy");

        if (playerObjTry != null)
        {
            playerObj = playerObjTry;


        }

        //------

        leftPointToGo = new Vector3(leftPoint.position.x, this.transform.position.y, leftPoint.position.z);
        rightPointToGo = new Vector3(rightPoint.position.x, this.transform.position.y, rightPoint.position.z);

    }

    public float detectionRange;
    public float offsetX;

    private void OnDrawGizmos()
    {
        
   
    }

    // Update is called once per frame
    public float detectionDistance;
    void Update()
    {

        if(isWaitingOnSpot == false)
        {
            thisAnimator.SetBool("isWalking", true);
        }
        else
        {
            thisAnimator.SetBool("isWalking", false);
        }

        if (startedCatchRoutine == false)
        {
            patrolRoute();
        }

        if (facingRight)
        {
            
            Debug.DrawLine(this.transform.position, this.transform.position + Vector3.right * detectionDistance);

        }
        if (facingRight == false)
        {
       
            Debug.DrawLine(this.transform.position, this.transform.position + Vector3.left * detectionDistance);

        }

        detectPlayer();
    }
    //offset
    private RaycastHit2D firstHit;

    
    private void detectPlayer()
    {

        if (facingRight)
        {
            //firstHit = Physics2D.BoxCast(this.transform.position + new Vector3(offsetX, 0f, 0f), new Vector2(detectionRange, 5f), 0f, Vector3.zero);
            
            firstHit = Physics2D.Linecast(this.transform.position, this.transform.position + Vector3.right * detectionDistance);
            

        }
        if (facingRight == false)
        {

            //firstHit = Physics2D.BoxCast(this.transform.position - new Vector3(offsetX, 0f, 0f), new Vector2(-detectionRange, 5f), 0f, Vector3.zero);
            
            firstHit = Physics2D.Linecast(this.transform.position, this.transform.position + Vector3.left * detectionDistance);
            
        }



        Debug.Log(firstHit.collider);



        if (firstHit.collider != null)
        {
            if (firstHit.collider.tag == "PlayerHitbox" || firstHit.collider.tag == "Player")
            {

                

                if (startedCatchRoutine == false)
                {
                    StartCoroutine(catchPlayer());
                }

            }
        }

    }

    //run and capture player
    private bool startedCatchRoutine;

    private float catchTimer = 1.5f;
    private float catchCounter;

    public GameObject exclamationMarkIndicator;
    private IEnumerator catchPlayer()
    {
        startedCatchRoutine = true;
        exclamationMarkIndicator.SetActive(true);

        //set player velocity and movement to 0 
        //playerObj.GetComponent<playerMovement>().enabled = false;
        //playerObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //

        Vector3 startPos = this.transform.position;
        while(catchCounter <= catchTimer)
        {

            //this.transform.position = Vector3.Lerp(startPos, new Vector3(playerObj.transform.position.x,startPos.y,0f) , catchCounter / catchTimer); 

            catchCounter += Time.deltaTime;
            yield return null;

        }
        //---
        if(startedFadeCatchRoutine == false)
        {
            StartCoroutine(fadeScreenAfterCatch());
        }
        //---

        

    }

    //----
    private bool startedFadeCatchRoutine;

    private float fadeTimer = 1f;
    private float fadeCounter;

    public GameObject fadeBackground;
    private IEnumerator fadeScreenAfterCatch()
    {
        startedFadeCatchRoutine = true;

      
        while (fadeCounter <= fadeTimer)
        {

            fadeBackground.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, fadeCounter / fadeTimer));

            fadeCounter += Time.deltaTime;
            yield return null;

        }

        

        startedCatchRoutine = false;
        catchCounter = 0f;
        exclamationMarkIndicator.SetActive(false);


        fadeCounter = 0f;
        fadeBackground.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        playerObj.GetComponent<astroStats>().astroHealth = 0;


        


        startedFadeCatchRoutine = false;

    }

   


    public Transform leftPoint, rightPoint;
    public bool moveRightAtStart;
    public float moveSpeed;

    private bool reachedSide;
    private bool isWaitingOnSpot;


    public float waitAtSideTime;
    private void patrolRoute()
    {


        if(moveRightAtStart == true)
        {
            
            if(this.transform.position.x == rightPointToGo.x)
            {

                if (startedWaitRoutine == false && reachedSide == false)
                {
                    StartCoroutine(waitOnSpot(waitAtSideTime));
                    reachedSide = true;
                }
                
            }
            if (this.transform.position.x == leftPointToGo.x)
            {

                if (startedWaitRoutine == false && reachedSide == true)
                {
                    StartCoroutine(waitOnSpot(waitAtSideTime));
                    reachedSide = false;
                }
                
            }

            // if not on cd
            if (isWaitingOnSpot == false)
            {

                if (reachedSide == false)
                {
                    Debug.Log("trying to go");
                    this.transform.position = Vector3.MoveTowards(this.transform.position, rightPointToGo, moveSpeed * Time.deltaTime);
                }
                else
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, leftPointToGo, moveSpeed * Time.deltaTime);

                }
            }

        }
        else
        {

            if (this.transform.position.x == leftPointToGo.x)
            {

                if (startedWaitRoutine == false && reachedSide == false)
                {
                    StartCoroutine(waitOnSpot(waitAtSideTime));
                    reachedSide = true;
                }

            }
            if (this.transform.position.x == rightPointToGo.x)
            {

                if (startedWaitRoutine == false && reachedSide == true)
                {
                    StartCoroutine(waitOnSpot(waitAtSideTime));
                    reachedSide = false;
                }

            }

            // if not on cd
            if (isWaitingOnSpot == false)
            {

                if (reachedSide == false)
                {

                    this.transform.position = Vector3.MoveTowards(this.transform.position, leftPointToGo, moveSpeed * Time.deltaTime);
                }
                else
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, rightPointToGo, moveSpeed * Time.deltaTime);

                }
            }

        }



    }
    

    private bool startedWaitRoutine;
    private IEnumerator waitOnSpot(float waitTime)
    {
        startedWaitRoutine = true;

        isWaitingOnSpot = true;
        yield return new WaitForSeconds(waitTime);
        isWaitingOnSpot = false;

        //rotate goblin
        this.transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
        //

        startedWaitRoutine = false;
    }
}
