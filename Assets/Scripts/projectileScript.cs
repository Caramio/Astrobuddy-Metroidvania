using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour
{

    public float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        selfDestruct();
    }

    // destroying the projectile after some time
    private void selfDestruct()
    {

        Destroy(this.gameObject, destroyTime);

    }

    // If the projectile hits astrobuddy, make him take damage , then destroy the projectile
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {

            collision.GetComponent<astroStats>().astroTakeDamage();

            

            Destroy(this.gameObject);

        }

        if(collision.tag == "Ground" || collision.tag == "Ceiling" || collision.tag == "Wall")
        {

            Destroy(this.gameObject);
        }

    }

}
