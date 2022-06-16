using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class trollEnemyScript : MonoBehaviour
{

    //Private variables to reference
    private Rigidbody2D trollBody;
    private SpriteRenderer trollSpriteRenderer;
    private Transform trollTransform;

    // Health point of the troll
    public int trollHealth;

    //Public references and variables
    public float trollMovementSpeed;


    //Booleans
    private bool playerInRadius;

    private bool isChasing = false;

    private bool jumpedOnce;

    //Timers
    private float jumpTimerStart;
    private float jumpTimerEnd = 2f;
   
    //Object that is in the radius ( player ) 
    private GameObject playerObj;


    //Variables
    private float directionChecker;



    

  
    // Assigning certain variables at the start of the scene
    void Start()
    {
        playerObj = GameObject.Find("Astrobuddy");


        trollTransform = this.GetComponent<Transform>();
        trollBody = this.GetComponent<Rigidbody2D>();
        trollSpriteRenderer = this.GetComponent<SpriteRenderer>();
        
    }

    
    void Update()
    {
        trollDeath();


        checkPlayer();
        chasePlayer();



        

    }


    private void OnDrawGizmos()
    {
        // drawing custom rectangle
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(this.transform.position, new Vector2(30f, 10f));

    }

    // Checking for the player and seeing whether or not it is around
    private void checkPlayer()
    {

        RaycastHit2D[] rayArray = new RaycastHit2D[10];
        

        rayArray = Physics2D.BoxCastAll(this.transform.position, new Vector2(30f, 10f), 0f, this.transform.position);
        

        if (Array.Exists(rayArray, element => element.collider.tag == "Player"))
        {
            
            playerInRadius = true;
        }
        else
        {
            trollBody.velocity = new Vector3(0f, trollBody.velocity.y);
            playerInRadius = false;
        }

       
          
    }

    private void chasePlayer()
    {
        // change later
        if (playerInRadius)
        {
            trollBody.velocity = new Vector2(5f, 0f);
        }
           

        


    }

    private void trollDeath()
    {

        if(trollHealth <= 0)
        {
           
            trollTransform.eulerAngles = new Vector3(trollTransform.eulerAngles.x, trollTransform.eulerAngles.y, -90f);
    
            Destroy(this.gameObject, 2f);
        }

    }

    // Function that is called in "astroAttacks" to simulate damage taken
    public void takeDamage()
    {
        trollSpriteRenderer.color = new Color(1f, 0f, 0f);
        trollHealth -= 1;
      
        StartCoroutine(revertOriginal());
        Debug.Log("I took damage");
    }


   


    //Reverting values to its original state after getting hit
    IEnumerator revertOriginal()
    {
        yield return new WaitForSeconds(0.3f);
        trollSpriteRenderer.color = new Color(1f, 1f, 1f);  

    }


}
