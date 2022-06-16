using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class illusionGroundCheck : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject illusionObject;
    void Start()
    {

        illusionObject = GameObject.Find("Astroillusion");
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Ground")
        {

            illusionObject.GetComponent<illusionMovement>().isJumping = true;
            illusionObject.GetComponent<illusionMovement>().canJump = true;
            illusionObject.GetComponent<illusionMovement>().jumpTime = 0.3f;
            
            

        }


    }

}
