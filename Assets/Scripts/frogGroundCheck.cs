using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frogGroundCheck : MonoBehaviour
{
    //Self reference to check scripts
    public GameObject frogObj;

    //Accessing this characters "playerMovement" script
    private frogMovement frogMovement;

    




    //Assigning initial values to components
    void Start()
    {
        frogMovement = frogObj.GetComponent<frogMovement>();
    }


    void Update()
    {

    }


    // Chekc later for "jumpTime" to change here
    //Collisions for the lower hitbox of the player model
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Ground" || collision.collider.tag =="Player")
        {
            frogMovement.isJumping = true;
            frogMovement.canJump = true;
            frogMovement.jumpTime = 0.5f;
        }


    }
    

   
}
