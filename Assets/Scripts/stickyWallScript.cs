using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickyWallScript : MonoBehaviour
{
    public PhysicsMaterial2D fullFrictionMaterial;
    public PhysicsMaterial2D zeroFrictionMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "astroStickPoint")
        {
            //set can dash to true
            collision.GetComponentInParent<playerMovement>().canDash = true;

            collision.GetComponentInParent<Rigidbody2D>().sharedMaterial = fullFrictionMaterial;

            collision.GetComponentInParent<playerMovement>().canJump = true;
            collision.GetComponentInParent<playerMovement>().jumpTime= 0.3f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "astroStickPoint")
        {
            collision.GetComponentInParent<Rigidbody2D>().sharedMaterial = zeroFrictionMaterial;

            collision.GetComponentInParent<playerMovement>().canJump = true;
            collision.GetComponentInParent<playerMovement>().jumpTime = 0.3f;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "astroStickPoint")
        {

            //set can dash to true
            collision.GetComponentInParent<playerMovement>().canDash = true;

            collision.GetComponentInParent<playerMovement>().canJump = true;
            collision.GetComponentInParent<playerMovement>().jumpTime = 0.3f;
        }

    }
}
