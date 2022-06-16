using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubblingWaterScript : MonoBehaviour
{
    //private reference
    private Vector2 startPosition;

    //public references
    public float movementSpeedX;
    public float movementSpeedY;
    
    void Start()
    {

        startPosition = this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // If the bubble exit the water, then we will make it so that it restarts at its original position
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Water")
        {

            this.GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeedX, movementSpeedY);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            this.transform.position = startPosition;
        }

    }
}
