using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batBossProjectile : MonoBehaviour
{
    public float projectileSpeed;
    public float destroyTime;

    private Vector3 startPosition;
    void Start()
    {
        startPosition = this.transform.position;
    }

    
    void Update()
    {
        moveProjectile();

        Destroy(this.gameObject, destroyTime);
    }

    private void moveProjectile()
    {

        this.GetComponent<Rigidbody2D>().velocity = this.transform.up * projectileSpeed;


    }

    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {

            collision.GetComponentInParent<astroStats>().astroTakeDamage();
            Destroy(this.gameObject);

        }

        if(collision.tag == "Ground" || collision.tag == "Wall" || collision.tag == "Ceiling")
        {

            Destroy(this.gameObject);
        }
    }


}
