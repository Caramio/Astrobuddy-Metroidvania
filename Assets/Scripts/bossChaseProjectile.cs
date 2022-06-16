using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossChaseProjectile : MonoBehaviour
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

    private void selfDestruct()
    {

        Destroy(this.gameObject, destroyTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {
            collision.GetComponentInParent<astroStats>().astroTakeDamage();
        }

        if(collision.tag == "Ground" || collision.tag == "Wall" || collision.tag == "Ceiling")
        {

            Destroy(this.gameObject);

        }
    }

  
}
