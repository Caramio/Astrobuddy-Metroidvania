using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossShadowRush : MonoBehaviour
{

    private Transform startPoint;
    public float rushSpeed;
    
    void Start()
    {
        startPoint = this.transform;
    }

    
    void Update()
    {

        shadowRush();
        
    }




    private void shadowRush()
    {

        transform.position = Vector3.MoveTowards(transform.position, new Vector2(startPoint.transform.position.x + 30f , startPoint.position.y), Time.deltaTime * rushSpeed);

        if (transform.position.x == startPoint.transform.position.x+30f && transform.position.y == startPoint.transform.position.y)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {
            collision.GetComponentInParent<astroStats>().astroTakeDamage();
        }
    }

}
