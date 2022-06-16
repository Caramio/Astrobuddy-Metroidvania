using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class batBossChaseScript : MonoBehaviour
{
    private GameObject playerObj;

    //Routelist to keep track of routes
    //Cameraspot list to keep track of when the camera should be focused to one place
    public List<Transform> routeList;
    public List<Transform> cameraSpots;

    public Camera currentCamera;

    // entrance to scene point;
    public GameObject startPoint;

    //current route
    int currentRoute = 0;


    //Public variables
    public float routeSpeed;


    //Projectile to throw
    public GameObject projectileObj;

    public List<GameObject> projectileThrowPoints;
    public List<GameObject> downwardProjectileThrowPoints;



    //checkers to see if certain projefctile routine was done
    private bool firstProjectileWasShot;
    private bool secondProjectileWasShot;
    private bool thirdProjectileWasShot;
    private bool fourthProjectileWasShot;

    //Charge routine bools to check after
    private bool firstChargeWasDone;
    private bool secondChargeWasDone;
    private bool thirdChargeWasDone;

    // rotation cahgne checking bool
    private bool firstRotationWasDone;
    private bool secondRotationWasDone;
    
    

    //Coroutine related
    //projectile related
    private bool startedProjectileRoutine;

    private float projectileRoutineTimer = 1.5f;
    private float projectileRoutineCounter;


    //Downwards projectile relaetd
    private bool startedDownwardsProjectileRoutine;

    private float downwardsProjectileTimer = 1.5f;
    private float downwardsProjectileCounter;

    //charge related
    private bool startedChargeRoutine;

    private float chargeRoutineCounter;
    private float chargeRoutineTimer = 2f;

    //pre charge related
    private bool startedPreChargeRoutine;

    private float preChargeCounter;
    private float preChargeTimer = 1.5f;


    //go back from charge related
    private bool startedGoBackRoutine;

    private float goBackRoutineCounter;
    private float goBackRoutineTimer = 1f;
    void Start()
    {
        var playerObjTry = GameObject.Find("Astrobuddy");

        if (playerObjTry != null)
        {

            playerObj = playerObjTry;

        }
    }

    // Update is called once per frame
    void Update()
    {


        followRoute();

    }

    //following the route indicators one by one
    private void followRoute()
    {

        if (startedChargeRoutine == false && startedGoBackRoutine == false && startedProjectileRoutine == false && startedPreChargeRoutine == false)
        {

            transform.position = Vector3.MoveTowards(transform.position, routeList[currentRoute].position, Time.deltaTime * routeSpeed);

            if (transform.position.x == routeList[currentRoute].transform.position.x && transform.position.y == routeList[currentRoute].transform.position.y)
            {
                currentRoute += 1;
            }

        }

        if (currentRoute == 2)
        {

            if (startedProjectileRoutine == false && firstProjectileWasShot == false)
            {
                StartCoroutine(throwProjectileRoutine());
                firstProjectileWasShot = true;
            }

        }

        //Doing certain actions when specific route points are reached
        if (currentRoute == 3)
        {
            if (secondProjectileWasShot == false)
            {
                if (startedProjectileRoutine == false)
                {
                    StartCoroutine(throwProjectileRoutine());
                    secondProjectileWasShot = true;

                }

            }
        }

        if (currentRoute == 4)
        {


            if (startedProjectileRoutine == false && thirdProjectileWasShot == false)
            {
                StartCoroutine(throwProjectileRoutine());
                thirdProjectileWasShot = true;
            }



        }

        if (currentRoute == 5)
        {

            if (startedPreChargeRoutine == false && firstChargeWasDone == false)
            {
                StartCoroutine(preChargeSetup(1f, 0.5f));
            }

        }

        if (currentRoute == 6)
        {

            if (startedDownwardsProjectileRoutine == false && fourthProjectileWasShot == false)
            {
                StartCoroutine(throwProjectileDownwardsRoutine());
                fourthProjectileWasShot = true;

                routeSpeed = 15f;
            }

        }

        if(currentRoute == 7)
        {
            if (firstRotationWasDone == false)
            {
                this.transform.Rotate(0, 180, 0);
                firstRotationWasDone = true;
                routeSpeed = 25f;
            }
        }

        if (currentRoute == 12)
        {
            if (secondRotationWasDone == false)
            {
                this.transform.Rotate(0, 180, 0);
                secondRotationWasDone = true;
            }
        }

        if (currentRoute == 13)
        {
            if (startedPreChargeRoutine == false && secondChargeWasDone == false)
            {
                StartCoroutine(preChargeSetup(1.5f, 0.5f));
                secondChargeWasDone = true;

            }
        }

        if (currentRoute == 15)
        {
            if (startedPreChargeRoutine == false && thirdChargeWasDone == false)
            {
                StartCoroutine(preChargeSetup(1.5f, 0.5f));
                thirdChargeWasDone = true;

            }
        }




    }
    private IEnumerator throwProjectileDownwardsRoutine()
    {
        startedDownwardsProjectileRoutine = true;

       

        for (int i = 0; i < downwardProjectileThrowPoints.Count; i++)
        {      
            GameObject thrownProjectile = Instantiate(projectileObj, downwardProjectileThrowPoints[i].transform.position, Quaternion.Euler(0f,0f, 180f));
            thrownProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -40f);
        }

        while (downwardsProjectileCounter <= downwardsProjectileTimer)
        {

            downwardsProjectileCounter += Time.deltaTime;

            yield return null;

        }

        // Enable projectile shooting again for later
        startedDownwardsProjectileRoutine = false;

        downwardsProjectileCounter = 0f;

    }


    private IEnumerator throwProjectileRoutine()
    {
        startedProjectileRoutine = true;

        int randomNum = Random.Range(2, 4);

        for (int i = 0; i < projectileThrowPoints.Count; i++)
        {
            if (i == randomNum)
            {
                continue;
            }
            GameObject thrownProjectile = Instantiate(projectileObj, projectileThrowPoints[i].transform.position, projectileObj.transform.rotation);
            thrownProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(30f, 0f);
        }

        while(projectileRoutineCounter <= projectileRoutineTimer)
        {

            projectileRoutineCounter += Time.deltaTime;

            yield return null;

        }

        // Enable projectile shooting again for later
        startedProjectileRoutine = false;

        projectileRoutineCounter = 0f;
     
    }
    private IEnumerator preChargeSetup(float afterChargeTimer ,float chargeTimer)
    {

        SpriteRenderer bossSpriteRenderer = this.GetComponent<SpriteRenderer>();

        startedPreChargeRoutine = true;

        while(preChargeCounter <= preChargeTimer)
        {

            bossSpriteRenderer.color = new Color(1f, Mathf.Lerp(1f, 0f, preChargeCounter / preChargeTimer) ,1f);

            preChargeCounter += Time.deltaTime;
            yield return null;
        }
        preChargeCounter = 0f;

        if (startedChargeRoutine == false)
        {
            StartCoroutine(chargeRoutine(chargeTimer));
        }

    }
    private IEnumerator chargeRoutine(float routineTimer)
    {
        this.GetComponent<Animator>().SetBool("isAttacking" ,true);

        Debug.Log("charged");
        startedChargeRoutine = true;

        Vector3 firstPos = routeList[currentRoute -1].transform.position;

        while (chargeRoutineCounter <= routineTimer)
        {
            this.transform.position = Vector3.Lerp(firstPos , routeList[currentRoute].transform.position, chargeRoutineCounter / routineTimer);
            chargeRoutineCounter += Time.deltaTime;

            yield return null;
        }

        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);

        this.transform.position = firstPos;

        chargeRoutineCounter = 0f;

        StartCoroutine(goBackRoutine());

    }

    private IEnumerator goBackRoutine()
    {
        this.GetComponent<Animator>().SetBool("isAttacking", false);

        startedGoBackRoutine = true;

        Vector3 firstPos = routeList[currentRoute].transform.position;

        while (goBackRoutineCounter <= goBackRoutineTimer)
        {

            this.transform.position = Vector3.Lerp(firstPos, routeList[currentRoute - 1].transform.position , goBackRoutineCounter/goBackRoutineTimer);
            
            goBackRoutineCounter += Time.deltaTime;

            yield return null;
        }

        goBackRoutineCounter = 0f;

        startedPreChargeRoutine = false;
        startedGoBackRoutine = false;
        startedChargeRoutine = false;

        //currentRoute += 1;
        firstChargeWasDone = true;

    }

  

  

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {
            Debug.Log(" collided with the player");

            collision.GetComponentInParent<astroStats>().astroHealth = 0;
            
            
        }
    }
}
