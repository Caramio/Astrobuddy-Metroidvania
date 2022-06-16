using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileTrap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "PlayerHitbox")
        {

            if(startedFireProjectileRoutine == false)
            {
                StartCoroutine(fireProjectile());
            }

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {

            if (startedFireProjectileRoutine == false)
            {
                StartCoroutine(fireProjectile());
            }

        }
    }

    //ProjectileLaunch Routine

    private bool startedFireProjectileRoutine;

    
    private float projectileCooldownCounter;
    private float projectileCooldownTimer = 5f;

    public GameObject projectileObject;
    public Transform attackPoint;

    public bool firingUpOrDown;
    public bool fireDown;

    public bool fireToTheRight;

    public float projectileSpeed;

    
    private IEnumerator fireProjectile()   
    {

        startedFireProjectileRoutine = true;

        if (firingUpOrDown == false)
        {
            if (fireToTheRight == true)
            {
                GameObject projectileObj = Instantiate(projectileObject, attackPoint.position, Quaternion.Euler(0f, 0f, -90f));

                projectileObj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0f);
            }

            if (fireToTheRight == false)
            {

                GameObject projectileObj = Instantiate(projectileObject, attackPoint.position, Quaternion.Euler(0f, 0f, 90f));

                projectileObj.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileSpeed, 0f);


            }
        }

        if(firingUpOrDown == true)
        {
            if(fireDown == true)
            {
                GameObject projectileObj = Instantiate(projectileObject, attackPoint.position, Quaternion.Euler(0f, 0f, 180f));

                projectileObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -projectileSpeed);
            }
            if (fireDown == false)
            {
                GameObject projectileObj = Instantiate(projectileObject, attackPoint.position, Quaternion.Euler(0f, 0f, -0f));

                projectileObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, projectileSpeed);
            }

        }

        while(projectileCooldownCounter <= projectileCooldownTimer)
        {

            projectileCooldownCounter += Time.deltaTime;

            yield return null;

        }

        projectileCooldownCounter = 0f;
        startedFireProjectileRoutine = false;
    
    
    }
}
