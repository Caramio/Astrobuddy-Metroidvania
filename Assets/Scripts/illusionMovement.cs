using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class illusionMovement : MonoBehaviour
{

    //References to the components
    private Rigidbody2D playerRigidBody;

    //movement speed of the illusion, referenced from astrobuddy
    public float illusionMovementSpeed;
    public float illusionJumpSpeed;

    //Reference to the player
    private GameObject playerObj;

    //private booleans
    private bool facingRight = true;

    // Jump incrementer
    public float jumpIncerementer;

    //public booleans
    [HideInInspector]
    public bool canJump = true;

    [HideInInspector]
    public bool isJumping = false;

    private float leftRightInput;


    //Jump timers
    public float jumpTime;

    void Start()
    {
        playerObj = GameObject.Find("Astrobuddy");
        playerRigidBody = this.GetComponent<Rigidbody2D>();

     
    }

    // Update is called once per frame
    void Update()
    {

        flipCharacter();

        astroJump(illusionJumpSpeed);
        astroLongerJump(illusionJumpSpeed);


        if (Input.GetKey(controlsStaticClass.moveLeftControl))
        {

            leftRightInput = 1f;

        }
        if (Input.GetKey(controlsStaticClass.moveRightControl))
        {

            leftRightInput = -1f;

        }

        if (Input.GetKeyUp(controlsStaticClass.moveLeftControl) || Input.GetKeyUp(controlsStaticClass.moveRightControl))
        {
            leftRightInput = 0f;

        }

    }

    private void FixedUpdate()
    {



        playerRigidBody.velocity = new Vector2(leftRightInput * illusionMovementSpeed, playerRigidBody.velocity.y);

    }


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


    private void astroJump(float jumpSpeed)
    {

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {

            Debug.Log("tried illus jump");

            playerRigidBody.velocity = Vector2.up * jumpSpeed;

            canJump = false;

            //setting isJumping to true in order to be able to trigger longer jumps
            isJumping = true;

        }
    }

    // Ability to jump higher when the space key is held down
    private void astroLongerJump(float jumpSpeed)
    {
        if (Input.GetKey(KeyCode.Space))
        {



            if (jumpTime > 0 && isJumping)
            {

                playerRigidBody.velocity = Vector2.up * jumpIncerementer;

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
}
