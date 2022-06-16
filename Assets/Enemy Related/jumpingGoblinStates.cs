using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class jumpingGoblinStates : MonoBehaviour
{

    



    private goblinJumpingStates state;

    public List<GameObject> jumpPlatformList;

   

    //Light to indicate blast
    public UnityEngine.Rendering.Universal.Light2D aoeLight;


    //Timers
    private float jumpPlatformTimer = 1.5f;
    private float jumpPlatformCounter;

    private float waitPlatformTimer = 0.5f;
    private float waitPlatformCounter;

    private int platformNumberTrack;

    

    
    // Middle point for the bezier curve
    //Vector3 middlePoint;


    //Couroutine bools
    private bool startedJumpingRoutine;
    private bool startedWaitingRoutine;



    public enum goblinJumpingStates
    {
        Idle,
        Jumping,
        WaitingForPlayer,

    }

    void Start()
    {
        

        state = goblinJumpingStates.Idle;

    }


    private void OnDrawGizmos()
    {


        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, 40f);

    }



    void Update()
    {

        stateChanger();


    }



    private void stateChanger()
    {

        if(state == goblinJumpingStates.Idle)
        {

            if(startedWaitingRoutine == false)
            {

                StartCoroutine(waitOnPlatform());
            }



        }

        if (state == goblinJumpingStates.WaitingForPlayer)
        {
            Collider2D[] mobRangeArray = Physics2D.OverlapCircleAll(this.transform.position, 40f);

            foreach (Collider2D mobRangeObjects in mobRangeArray)
            {

                if (mobRangeObjects.tag == "Player")
                {

                    state = goblinJumpingStates.Jumping;

                }

            }
        }


        if (state == goblinJumpingStates.Jumping)
        {

            if(startedJumpingRoutine == false)
            {
                
                StartCoroutine(jumpPlatforms());
            }


        }


    }

    private IEnumerator waitOnPlatform()
    {

        startedWaitingRoutine = true;

       // float randomWaitTimer = Random.Range(1, 3);

        while (waitPlatformCounter <= waitPlatformTimer)
        {
            aoeLight.intensity = Mathf.Lerp(0f, 20f, waitPlatformCounter / waitPlatformTimer);

            waitPlatformCounter += Time.deltaTime;

            yield return null;


        }

        Collider2D[] mobRangeArray = Physics2D.OverlapCircleAll(this.transform.position, 5f);

        foreach (Collider2D mobRangeObjects in mobRangeArray)
        {

            if (mobRangeObjects.tag == "Player")
            {

              
                mobRangeObjects.GetComponent<astroStats>().astroTakeDamage();
                          

            }

        }


        aoeLight.intensity = 0f;

        waitPlatformCounter = 0f;

        state = goblinJumpingStates.WaitingForPlayer;

        startedWaitingRoutine = false;


    }






    private IEnumerator jumpPlatforms()
    {
        // Random platform to jump to
        int randomPlatform = Random.Range(0, jumpPlatformList.Count -1);

        if(randomPlatform == platformNumberTrack)
        {
            randomPlatform += 1;
        }

        
        
        //Determine middle point for the bezier curve before continuing
        //middlePoint = startPoint.transform.position + (endPoint.transform.position - startPoint.transform.position) / 2 + Vector3.up * 25f;
        Vector3 middlePoint = jumpPlatformList[platformNumberTrack].transform.position + 
            (jumpPlatformList[randomPlatform].transform.position - jumpPlatformList[platformNumberTrack].transform.position) / 2 + Vector3.up * 25f;

        startedJumpingRoutine = true;

        while(jumpPlatformCounter <= jumpPlatformTimer)
        {


            jumpPlatformCounter +=  Time.deltaTime;

            //Vector3 m1 = Vector3.Lerp(startPoint.position, middlePoint, jumpPlatformCounter / jumpPlatformTimer);
            //Vector3 m2 = Vector3.Lerp(middlePoint, endPoint.position + new Vector3(0f, 1.5f, 0f), jumpPlatformCounter / jumpPlatformTimer);
            Vector3 m1 = Vector3.Lerp(jumpPlatformList[platformNumberTrack].transform.position, middlePoint, jumpPlatformCounter / jumpPlatformTimer);
            Vector3 m2 = Vector3.Lerp(middlePoint, jumpPlatformList[randomPlatform].transform.position + new Vector3(0f, 3f, 0f), jumpPlatformCounter / jumpPlatformTimer);

            this.transform.position = Vector3.Lerp(m1, m2, jumpPlatformCounter / jumpPlatformTimer);

            yield return null;


        }

        state = goblinJumpingStates.Idle;

        jumpPlatformCounter = 0f;

        startedJumpingRoutine = false;

        platformNumberTrack = randomPlatform;


    }
}
