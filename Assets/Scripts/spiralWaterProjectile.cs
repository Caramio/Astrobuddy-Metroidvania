using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiralWaterProjectile : MonoBehaviour
{
    Rigidbody2D waterBallRB;

    public float projectileSpeed;
    void Start()
    {
        waterBallRB = this.GetComponent<Rigidbody2D>();




         waterBallRB.velocity = this.transform.rotation * Vector3.up * projectileSpeed;


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.transform.forward);
        //changeRotation();
    }

    

    private void changeRotation()
    {

        Quaternion.LookRotation(waterBallRB.velocity, Vector3.forward);

        Vector3 dir = waterBallRB.velocity;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle - 90f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Ground" || collision.tag == "Wall" || collision.tag == "Water" || collision.tag == "Ceiling")
        {


            //reset the velocity to be able to use later
            waterBallRB.velocity = Vector2.zero;

            this.gameObject.SetActive(false);
        }

        if (collision.tag == "PlayerHitbox")
        {



            //reset the velocity to be able to use later
            waterBallRB.velocity = Vector2.zero;

            collision.GetComponentInParent<astroStats>().astroTakeDamage();
            this.gameObject.SetActive(false);
        }

    }
}
