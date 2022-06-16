using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redMageFireballScript : MonoBehaviour
{

    private Rigidbody2D fireballRB;

    //object pooling obj for spreadfire
    public GameObject spreadFirePoolingObj;


   
    // Start is called before the first frame update
    void Start()
    {
        fireballRB = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, 4.5f);
    }

    private void FixedUpdate()
    {
        RaycastHit2D[] hitSurface;

        hitSurface = Physics2D.CircleCastAll(this.transform.position, 4.5f,Vector2.zero);


        foreach(RaycastHit2D hitObject in hitSurface)
        {
            // if the name is not solid part, which is a part of this fireball
            if (hitObject.collider.tag == "Wall" || hitObject.collider.tag == "Ground" || hitObject.collider.tag == "Ceiling")
            {

                fireballRB.velocity = Vector3.Reflect(fireballRB.velocity, hitObject.normal);

                // if the cooldown elapsed
                if (canFireProjectiles == true)
                {
                    StartCoroutine(AoeProjectileRoutine());
                }

            }

        }
       
        


    }
    private bool canFireProjectiles = true;

    private float cooldownCounter;
    private float cooldownTimer = 0.5f;
    private IEnumerator bounceCooldown()
    {

        canFireProjectiles = false;

      

        while (cooldownCounter <= cooldownTimer)
        {

            cooldownCounter += Time.deltaTime;
            yield return null;
        }

        cooldownCounter = 0f;
        canFireProjectiles = true;

    }
   
  


    //---------------------Spread into more projectiles-----------------//
    public GameObject spreadFireProjectile;

    public static bool startedaoeProjectileRoutine;
    
    public int numOfProjectiles;

    private IEnumerator AoeProjectileRoutine()
    {

        int i = mageBossObjectPool.pooledTimes * numOfProjectiles;

        //if we bounce , our aoeprojectilefiring will stop
        StartCoroutine(bounceCooldown());


        for (; i  < numOfProjectiles * (mageBossObjectPool.pooledTimes + 1); i++)
        {

            

            float theta = i * 2 * Mathf.PI / numOfProjectiles;
            float x = Mathf.Sin(theta) * 10;
            float y = Mathf.Cos(theta) * 10;

  
            spreadFirePoolingObj.GetComponent<mageBossObjectPool>().spreadFirePool[i].transform.position = new Vector3(x, y, 0) + this.transform.position;
            spreadFirePoolingObj.GetComponent<mageBossObjectPool>().spreadFirePool[i].transform.eulerAngles = new Vector3(0f, 0f, (-theta * Mathf.Rad2Deg));
            spreadFirePoolingObj.GetComponent<mageBossObjectPool>().spreadFirePool[i].SetActive(true);


            //start the disable routine when that will count a timer 
            
            

            //Instantiate(spreadFireProjectile, new Vector3(x, y, 0) + this.transform.position, Quaternion.Euler(0f, 0f, (-theta * Mathf.Rad2Deg)));


        }



        yield return null;
        

        mageBossObjectPool.pooledTimes += 1;



    }
}
