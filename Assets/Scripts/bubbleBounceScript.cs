using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleBounceScript : MonoBehaviour
{
    Rigidbody2D bubbleRB;


    AudioSource bubbleAudio;
    void Start()
    {
        bubbleAudio = this.GetComponent<AudioSource>();
        bubbleRB = this.GetComponent<Rigidbody2D>();

        moveUp();
    }

    // Update is called once per frame
    void Update()
    {

        //destroy this after 8 seconds
        Destroy(this.gameObject, 8f);


    }

    private void OnEnable()
    {
        
    }


    public AudioClip bubblePopClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {

            //play bubble pop 
            bubbleAudio.clip = bubblePopClip;
            bubbleAudio.Play();
      
            
          
            Rigidbody2D playerBody = collision.GetComponentInParent<Rigidbody2D>();
            playerMovement playerMovementComponent = collision.GetComponentInParent<playerMovement>();

            playerBody.constraints = RigidbodyConstraints2D.FreezeRotation;

            playerBody.velocity = new Vector2(playerBody.velocity.x, 0f);
            playerBody.AddForce(new Vector2(0f, 3000f));



            //also reset jumps etc
            playerMovementComponent.isJumping = true;
            playerMovementComponent.canJump = true;
            playerMovementComponent.jumpTime = 0.3f;

            playerMovementComponent.canDash = true;

            //set recentlyapplied to true
            astroAbilities.recentlyAppliedJumpForce = true;

            StartCoroutine(destroyWithDelay());


        }
    }

   

    private IEnumerator destroyWithDelay()
    {
        this.GetComponent<CircleCollider2D>().enabled = false;
        //close particles
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.transform.GetChild(0).gameObject.SetActive(false);
       
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    private void moveUp()
    {

        bubbleRB.velocity = new Vector2(bubbleRB.velocity.x, 10f);

  

    }
}
