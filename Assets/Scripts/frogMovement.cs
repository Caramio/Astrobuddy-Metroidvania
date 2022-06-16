using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frogMovement : MonoBehaviour
{

    //References to the components
    private Rigidbody2D frogRigidBody;

    //Public floats
    public float movementSpeed;
    public float jumpSpeed;

    //Jump timers
    public float jumpTime;

    public float jumpIncerementer;

    //public booleans
    [HideInInspector]
    public bool canJump = true;

    [HideInInspector]
    public bool isJumping = false;

    [HideInInspector]
    public bool isSwimming = false;

    //private booleans
    private bool facingRight = true;


    //Assigning start references
    void Start()
    {
        frogRigidBody = this.GetComponent<Rigidbody2D>();
    }

    // only allow jumps when the frog is not swimming
    void Update()
    {

        if (!playerMovement.movingAstro)
        {
            if (!isSwimming)
            {
                frogJump(jumpSpeed);
                frogLongerJump(jumpSpeed);
            }

            if (isSwimming)
            {
                frogSwim();
            }



            flipCharacter();

        }


    }

    private void FixedUpdate()
    {

        // updating for frogman
        if (!playerMovement.movingAstro && !isSwimming)
        {
            float moveInput = Input.GetAxisRaw("Horizontal");

            frogRigidBody.velocity = new Vector2(moveInput * movementSpeed, frogRigidBody.velocity.y);
        }

        // reducing velocity if moving in water
        if(!playerMovement.movingAstro && isSwimming)
        {
            float moveInput = Input.GetAxisRaw("Horizontal");

            frogRigidBody.velocity = new Vector2(moveInput * movementSpeed /2, frogRigidBody.velocity.y);
        }




        

        
    }

    //fixing the rotation on object enable
    private void OnEnable()
    {
       
    }


    //Flipping the characters rotation depending on which way the character is moving
    private void flipCharacter()
    {



        if (Input.GetKeyDown(KeyCode.A) && facingRight)
        {

            this.transform.Rotate(0, 180, 0);
            facingRight = false;

        }

        if (Input.GetKeyDown(KeyCode.D) && !facingRight)
        {

            this.transform.Rotate(0, 180, 0);
            facingRight = true;

        }


    }


    // Remember to check frogGroundCheck script to change jump timer
    // Jumping function, once a jump is done "canJump" will be set to false, which will be reactivated through the
    // "groundCheck" script
    private void frogJump(float jumpSpeed)
    {

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {


            frogRigidBody.velocity = Vector2.up * jumpSpeed;

            canJump = false;

            //setting isJumping to true in order to be able to trigger longer jumps
            isJumping = true;

        }
    }

    // Ability to jump higher when the space key is held down
    private void frogLongerJump(float jumpSpeed)
    {
        if (Input.GetKey(KeyCode.Space))
        {



            if (jumpTime > 0 && isJumping)
            {

                frogRigidBody.velocity = Vector2.up * jumpIncerementer;

                
                jumpTime -= Time.deltaTime;

            }

        }

        // Releasing the space button to stop jumping
        if (Input.GetKeyUp(KeyCode.Space))
        {

            isJumping = false;
            canJump = false;


        }

    }

    // swimming upwards, dividing incrementer by two to simulate a slower speed
    private void frogSwim()
    {

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {

            frogRigidBody.velocity = Vector2.up * jumpIncerementer / 1.5f;
        

        }

        if ( Input.GetKey(KeyCode.S))
        {

            frogRigidBody.velocity = Vector2.down * jumpIncerementer / 1.5f;


        }


    }



    // On triggers to check for watter
    //slowing the character down while in water and reducing gravity scale to make it more water-like
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Water")
        {
            

            isSwimming = true;
            frogRigidBody.gravityScale = 1f;


        }

    }

    // revertying the changes to the original once out of water
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            // resetting jump values to be able to get out of water, change if neccessary for later
            isJumping = true;
            jumpTime = 0.5f;

            frogRigidBody.gravityScale = 10f;

            isSwimming = false;
            
            

        }
    }

}
