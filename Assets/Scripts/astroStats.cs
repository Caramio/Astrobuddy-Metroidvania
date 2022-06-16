using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class astroStats : MonoBehaviour
{

    //Health UI objects to close once we get hit
    public List<GameObject> healthList;

    // Health value of astrobuddy
    public int astroHealth;

    //private components
    private SpriteRenderer astroSpriteRenderer;

    //Timers
    private float damageTimerCounter;
    private float damageTimer = 0.2f;


    //Taking damage and invincibility period
    private float damageTakeCooldown = 1f;
    private float damageTakeCooldownCounter;

    //private bools for coroutines
    private bool takeDamageRoutineStarted;

    //Determine which location to start at after death has occured
    public static Vector3 deathStartPoint;



    void Start()
    {
        astroSpriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        //Debug.Log("my health is " + astroHealth);
        astroDeath();

        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "deathPoints")
        {

            deathStartPoint = collision.transform.position;

        }

    }

    //Publi method to access player
    public void astroTakeDamage()
    {

        if(takeDamageRoutineStarted == false)
        {
            StartCoroutine(astroTakeDamageRoutine());
        }



        //astroSpriteRenderer.color = new Color(0.7f, 0f, 0f);      
        //StartCoroutine(damageColorChange());


    }

    // indicating that we are dead once we reach 0 health
    public void astroDeath()
    {

        if(astroHealth == 0)
        {

            //when we die, the camera size should rest too, so the static var in camerafollow should be set
            if(cameraFollow.sizeControlledByOther == true)
            {
                cameraFollow.sizeControlledByOther = false;
            }

            string currentScene = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(currentScene);

            //healthlist re-enabled
            foreach(GameObject healthObj in healthList)
            {
                healthObj.SetActive(true);
            }

            this.transform.position = deathStartPoint;

            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            


            astroHealth = 3;
            

        }

    }

    public IEnumerator astroTakeDamageRoutine()
    {
        // disable gameobject of health bar icon once we are hit
        healthList[astroHealth -1].SetActive(false);

        astroSpriteRenderer.color = new Color(0.7f, 0f, 0f);

        takeDamageRoutineStarted = true;

        astroHealth -= 1;


        
        while (damageTakeCooldownCounter <= damageTakeCooldown)
        {
            damageTakeCooldownCounter += Time.deltaTime;
            yield return null;

        }

        takeDamageRoutineStarted = false;

        damageTakeCooldownCounter = 0f;

        astroSpriteRenderer.color = new Color(1f, 1f, 1f);

    }
    
    
    

    private IEnumerator damageColorChange()
    {


        while (damageTimerCounter <= damageTimer)
        {
            

            damageTimerCounter += Time.deltaTime;
          
            yield return null;
        }

        

        damageTimerCounter = 0f;

        astroSpriteRenderer.color = new Color(1f, 1f, 1f);

    }
}
