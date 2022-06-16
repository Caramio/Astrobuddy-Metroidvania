using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimations : MonoBehaviour
{
    //References to the components
    private Rigidbody2D playerRigidBody;
    private playerMovement playerMovementComponent;

    //Animator that animates us
    public Animator playerAnimator;

    //Private variables
    private float currentSpeed;
    private float currentVerticalSpeed;

    void Start()
    {
        //Assigning references at the start
        playerRigidBody = this.GetComponent<Rigidbody2D>();
        playerMovementComponent = this.GetComponent<playerMovement>();

        
    }

    
    void Update()
    {

        // check the absolute value of the x velocity 
        currentSpeed = Mathf.Abs(playerRigidBody.velocity.x);
        currentVerticalSpeed = Mathf.Abs(playerRigidBody.velocity.y);


        
        playerAnimator.SetFloat("movementSpeed", currentSpeed);
        

        
        playerAnimator.SetFloat("movementSpeedVertical", currentVerticalSpeed);
        


        
    }
}
