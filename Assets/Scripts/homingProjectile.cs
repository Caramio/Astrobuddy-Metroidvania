using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homingProjectile : MonoBehaviour
{
    public float projectileSpeed;

    private GameObject playerObj;

    void Start()
    {
        playerObj = GameObject.Find("Astrobuddy");
    }

    // Update is called once per frame
    void Update()
    {

        followPlayer();


    }
    //rotating the projectile towards the player and also moving it toward sthe player
    private void followPlayer()
    {

        this.transform.position = Vector3.MoveTowards(transform.position, playerObj.transform.position, Time.deltaTime * projectileSpeed);

        Vector3 dir = playerObj.transform.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle - 90f);




    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
        if(collision.tag == "PlayerHitbox")
        {
            collision.GetComponentInParent<astroStats>().astroTakeDamage();
            Destroy(this.gameObject);
        }

    }
}
