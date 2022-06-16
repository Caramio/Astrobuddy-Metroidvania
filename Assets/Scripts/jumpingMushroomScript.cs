using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpingMushroomScript : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "PlayerGroundChecker")
        {
            collision.collider.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0f, 3000f));
            astroAbilities.recentlyAppliedJumpForce = true;

        }
    }
    
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerGroundChecker")
        {
            collision.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0f, 3000f));
            Debug.Log("bounced");
        }
    }
    */
}
