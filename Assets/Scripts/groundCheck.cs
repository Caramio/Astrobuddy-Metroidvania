using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    //Self reference to check scripts
    public GameObject playerObj;

    //Accessing this characters "playerMovement" script
    private playerMovement astroMovement;

    
    

    //Assigning initial values to components
    void Start()
    {
        astroMovement = playerObj.GetComponent<playerMovement>();
    }


    void Update()
    {


      

    }



    //Collisions for the lower hitbox of the player model
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.collider.tag == "Ground")
        {
            astroMovement.isJumping = true;
            astroMovement.canJump = true;
            astroMovement.jumpTime = 0.3f;

            astroMovement.canDash = true;

          
        }


    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (astroMovement.canJump == false)
            {
                astroMovement.isJumping = true;
                astroMovement.canJump = true;
                astroMovement.jumpTime = 0.3f;

                astroMovement.canDash = true;
            }


        }

    }

    

    //add drowning effect later
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.GetComponent<deepWater>() != null)
        {
            Debug.Log("Drowned");
            this.GetComponentInParent<astroStats>().astroHealth = 0;
        }







        // same as collision
        if (collision.tag == "Ground")
        {
            astroMovement.isJumping = true;
            astroMovement.canJump = true;
            astroMovement.jumpTime = 0.3f;

            astroMovement.canDash = true;


        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Ground")
        {
            if (astroMovement.canJump == false)
            {
                astroMovement.isJumping = true;
                astroMovement.canJump = true;
                astroMovement.jumpTime = 0.3f;

                astroMovement.canDash = true;
            }


        }

    }
}
