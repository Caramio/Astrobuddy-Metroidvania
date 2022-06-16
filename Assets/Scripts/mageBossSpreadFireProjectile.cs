using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mageBossSpreadFireProjectile : MonoBehaviour
{
    public float projectileSpeed;
    

    private Vector3 startPosition;
    void Start()
    {
        startPosition = this.transform.position;
    }


    void Update()
    {
        moveProjectile();

        
    }

    private void moveProjectile()
    {

        this.GetComponent<Rigidbody2D>().velocity = this.transform.up * projectileSpeed;


    }


    public float selfDestructTime;
    IEnumerator selfDestructRoutine()
    {
        yield return new WaitForSeconds(selfDestructTime);
        this.gameObject.SetActive(false);
    }
    // once its enabled, start destruct routine
    private void OnEnable()
    {
        StartCoroutine(selfDestructRoutine());
    }




    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {

            collision.GetComponentInParent<astroStats>().astroTakeDamage();

           
            this.gameObject.SetActive(false);

           

        }

        if (collision.tag == "Ground" || collision.tag == "Wall" || collision.tag == "Ceiling" || collision.tag ==  "Water")
        {
            
            this.gameObject.SetActive(false);
            
        }
    }

    
}
