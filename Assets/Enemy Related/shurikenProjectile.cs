using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shurikenProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

  

    // Update is called once per frame
    void Update()
    {


        spinShuriken();

        Destroy(this.gameObject, 5f);
    }


    private void spinShuriken()
    {
        
        this.transform.Rotate(0, 0, 2);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {
            collision.GetComponentInParent<astroStats>().astroTakeDamage();

            Destroy(this.gameObject);
        }
    }


}
