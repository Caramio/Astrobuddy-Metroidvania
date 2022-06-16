using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REMOVVE LATER IF NECCESARY
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{

    

    //References to the components
    private Rigidbody2D playerRigidBody;

    //frogman object
    public GameObject frogObj;
    public GameObject frogHolderPoint;

    //public variables
    public float movementSpeed;
    public float jumpSpeed;

    public bool canMove = true;

    //Jump speed incrementer for extended jumps
    public float jumpIncerementer;

    
    //Jump timers
    public float jumpTime;


    //public booleans
    [HideInInspector]
    public bool canJump = true;

    [HideInInspector]
    public bool isJumping = false;


    //private booleans
    private bool facingRight = true;

    //boolean to check whether we are moving FrogMan or Astrobuddy
    [HideInInspector]
    public static bool movingAstro = true;

    //Side determine
    private float leftRightInput;


    //Dash routine related
    public ParticleSystem dashParticles;
    [HideInInspector]
    public bool canDash;

    private bool startedDashRoutine;
   
    private float dashTimer = 0.5f;
    [HideInInspector]
    public float dashCounter;

  

    


    void Start()
    {
        //Keeping the game object as a total and the stats attached while going between scenes
        DontDestroyOnLoad(this.gameObject);
        //Assigning references at the start
        playerRigidBody = this.GetComponent<Rigidbody2D>();

        
    }
    // On disable will make the input disappear once this is disabled, will also do this ondisable

    private void OnEnable()
    {
       
        
        leftRightInput = 0;
    }
    private void OnDisable()
    {
        playerRigidBody.velocity = Vector2.zero;
            
        leftRightInput = 0;
    }

    //game dev console

    public GameObject devConsoleHolder;

    public UnityEngine.UI.Text sceneText;
    private void devConsole()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            devConsoleHolder.SetActive(!devConsoleHolder.activeInHierarchy);        
        }


        if (devConsoleHolder.activeSelf == true)
        {
            Debug.Log("its true!");

            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("asdasd" + sceneText.text);
                SceneManager.LoadScene(sceneText.text);

                devConsoleHolder.SetActive(false);
            }
        }
    }
    // if canMove, the player will be able to use player controls


    //Audio source...
    public playerAudioSource playerAudioSource;

    void Update()
    {
        //dev console
        //devConsole();


        
      

        /*
        // CHANGE LATER THIS IS FOR TESTING PURPOSES
        if (Input.GetKeyDown(KeyCode.J))
        {
            SceneManager.LoadScene("mageBossRoom");

            this.transform.position = new Vector2(-41.1f, 121.5f);
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("Prison");

            this.transform.position = new Vector3(-76.5f, -27.3f, 0f);
        }
        */
        //change characters rotation
        if (movingAstro)
        {
            flipCharacter();
        }


        if (canMove && movingAstro && this.GetComponent<astroAbilities>().usingWings == false)
        {
            astroJump(jumpSpeed);
            astroLongerJump(jumpSpeed);
            moveDown();
            

           
        }

        // can dash is set on ground checker

        //CHANGE ASTROHASDASH == TO FALSE WHEN IN PROPER GAMEPLAY
        if (Input.GetKeyDown(controlsStaticClass.dashControl) && startedDashRoutine == false && canDash == true && astroBuddyStaticClass.astroHasDash == true)
        {
            StartCoroutine(dashRoutine());
            canDash = false;
        }



        //Movement Input Changer for the horizontal axis
        if (Input.GetKey(controlsStaticClass.moveLeftControl))
        {
            //audioclip FOR SOME REASON IS JUMPING IS INVERTED, FIX LATER MAYBE
            if (playerRigidBody.velocity.y == 0)
            {
                playerAudioSource.playFootstepGroundSound();
            }
            //---

            leftRightInput = -1f;

        }
        if (Input.GetKey(controlsStaticClass.moveRightControl))
        {
            //audioclip
            if (playerRigidBody.velocity.y == 0)
            {
                playerAudioSource.playFootstepGroundSound();
            }
            //---

            leftRightInput = 1f;

        }

        if(Input.GetKeyUp(controlsStaticClass.moveLeftControl) || Input.GetKeyUp(controlsStaticClass.moveRightControl))
        {

            //audioclip related
            playerAudioSource.stopFootstepGroundSound();
            //---

            leftRightInput = 0f;

        }

       

        //stop sound if is jumping
        if(isJumping == false)
        {
            playerAudioSource.stopFootstepGroundSound();
        }

    }

    private void FixedUpdate()
    {

        //adjust fall speed to a max, we dont want physics acceleration to cause too much trouble
        if(playerRigidBody.velocity.y <= -40f)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, -40f);
        }


        // updating for astrobuddy
        if (canMove && movingAstro)
        {


            //float moveInput = Input.GetAxisRaw("Horizontal");

            //playerRigidBody.velocity = new Vector2(moveInput * movementSpeed, playerRigidBody.velocity.y);  

            //if it is not nan, or else some error will happen
            if (float.IsNaN(playerRigidBody.velocity.y) == false)
            {
                playerRigidBody.velocity = new Vector2(leftRightInput * movementSpeed, playerRigidBody.velocity.y);
            }
            
        }

       

    }
    // Possibly change this later on
    private void moveDown()
    {

        if (Input.GetKey(KeyCode.S))
        {
            if(playerRigidBody.velocity.y >= 0f)
            {
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0f);
            }
            playerRigidBody.AddForce(new Vector2(0f,-40f));

        }


    }

    //Dash ability that is unlocked after the spirit gives us power

    

    public IEnumerator dashRoutine()
    {

        //play dash sound
        playerAudioSource.playDashSound();
        //


        startedDashRoutine = true;

        bool frozeRotationOnce = false;

        while(dashCounter <= dashTimer)
        {

            
            if(dashCounter <= 0.2f )
            {

                if (frozeRotationOnce == false)
                {
                    playerRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
                    frozeRotationOnce = true;
                }

                dashParticles.Play();
                movementSpeed = 60f;
            }
            else
            {
               
                playerRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                dashParticles.Stop();
                movementSpeed = 20f;
            }
            

            dashCounter += Time.deltaTime;

            yield return null;
        }

        

        movementSpeed = 20f;

        dashCounter = 0f;
        startedDashRoutine = false;

      



    }
  

    
  

    //Flipping the characters rotation depending on which way the character is moving
    private void flipCharacter()
    {

       

        if (Input.GetKeyDown(controlsStaticClass.moveLeftControl) && facingRight)
        {

            this.transform.Rotate(0, 180, 0);
            facingRight = false;

        }

        if(Input.GetKeyDown(controlsStaticClass.moveRightControl) && !facingRight)
        {

            this.transform.Rotate(0, 180, 0);
            facingRight = true;

        }


    }
 

    // Jumping function, once a jump is done "canJump" will be set to false, which will be reactivated through the
    // "groundCheck" script
    private void astroJump(float jumpSpeed)
    {

        if (Input.GetKeyDown(controlsStaticClass.moveJumpControl) && canJump)
        {
            //--stop footstep audio after jumping--//
            playerAudioSource.stopFootstepGroundSound();
            //----//

            playerRigidBody.velocity = Vector2.up * jumpSpeed;

            canJump = false;

            //setting isJumping to true in order to be able to trigger longer jumps
            isJumping = true;

        }
    }

    // Ability to jump higher when the space key is held down
    private void astroLongerJump(float jumpSpeed)
    {
        if (Input.GetKey(controlsStaticClass.moveJumpControl))
        {



            if (jumpTime > 0 && isJumping)
            {
            
                playerRigidBody.velocity = Vector2.up * jumpIncerementer;

                jumpTime -= Time.deltaTime; 

            }
            
        }

        // Releasing the space button to stop jumping
        if (Input.GetKeyUp(controlsStaticClass.moveJumpControl))
        {
        
            isJumping = false;
            canJump = false;

            
        }

    }



}





