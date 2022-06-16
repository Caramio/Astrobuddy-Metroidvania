using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireLineParticleCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool canDamage;

    private void OnEnable()
    {
        
        StartCoroutine(miniDelay());
    }

    private void OnDisable()
    {
        canDamage = false;
    }

    
    private IEnumerator miniDelay()
    {

        yield return new WaitForSeconds(0.01f);

        canDamage = true;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        

        if(collision.tag == "PlayerHitbox" && canDamage)
        {
            //change later to deal daamage
            collision.GetComponentInParent<astroStats>().astroTakeDamage();
          

            
        }

    }






}
