using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveShockwave : MonoBehaviour
{
    private Vector3 leftWaveVector = Vector3.zero;
    private Vector3 rightWaveVector = new Vector3(0f,180f,0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveWave();
    }

    private void moveWave()
    {

        if(this.transform.eulerAngles == leftWaveVector)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-40f, 0f);
        }
        if(this.transform.eulerAngles == rightWaveVector)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(40f, 0f);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {
            collision.GetComponentInParent<astroStats>().astroTakeDamage();
            Destroy(this.gameObject);
        }

        if(collision.name == "Boss Left Wall" ||  collision.name == "Boss Right Wall")
        {
            Destroy(this.gameObject);
        }

        if(collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
