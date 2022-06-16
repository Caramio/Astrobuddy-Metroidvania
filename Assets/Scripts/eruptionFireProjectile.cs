using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eruptionFireProjectile : MonoBehaviour
{
    Rigidbody2D fireballRB;
    void Start()
    {
        fireballRB = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        changeRotation();
    }

    private void changeRotation()
    {

        Quaternion.LookRotation(fireballRB.velocity, Vector3.forward);

        Vector3 dir = fireballRB.velocity;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle - 90f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Ground" || collision.tag == "Wall" || collision.tag == "Water" || collision.tag == "Ceiling")
        {
           

            //reset the velocity to be able to use later
            fireballRB.velocity = Vector2.zero;

            this.gameObject.SetActive(false);
        }

        if(collision.tag == "PlayerHitbox")
        {

           

            //reset the velocity to be able to use later
            fireballRB.velocity = Vector2.zero;

            collision.GetComponentInParent<astroStats>().astroTakeDamage();
            this.gameObject.SetActive(false);
        }
       
    }
}
