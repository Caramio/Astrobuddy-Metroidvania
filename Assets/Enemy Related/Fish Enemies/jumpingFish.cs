using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpingFish : MonoBehaviour
{


    private fishJumpingStates state;

    //Timers
    public float jumpTimer;
    private float jumpCounter;

    private float cooldownTimer = 2f;
    private float cooldownCounter;

    public Transform startPoint, endPoint;

    //Bools for coroutines
    private bool startedJumpingRoutine;
    private bool startedCooldownRoutine;

    //public references
    public bool leftToRightJumper;
    public float upperLimit;


    private enum fishJumpingStates
    {
        Idle,
        Jumping,
       
    }

    void Start()
    {
        state = fishJumpingStates.Idle;
    }

    // Update is called once per frame
    void Update()
    {

        stateChanger();

    }

    private void stateChanger()
    {

        if(state == fishJumpingStates.Idle)
        {
            if (startedCooldownRoutine == false)
            {
                StartCoroutine(jumpCooldown());
            }
        }

        if(state == fishJumpingStates.Jumping)
        {
            if(startedJumpingRoutine == false)
            {
                StartCoroutine(jumpParabola());
            }
        }

    }


    private IEnumerator jumpCooldown()
    {
        startedCooldownRoutine = true;

        while(cooldownCounter <= cooldownTimer)
        {
            cooldownCounter += Time.deltaTime;

            yield return null;

        }

        cooldownCounter = 0f;

        startedCooldownRoutine = false;

        state = fishJumpingStates.Jumping;
    }



    // jumping in a parabola
    private IEnumerator jumpParabola()
    {
        


        //Determine middle point for the bezier curve before continuing
        //middlePoint = startPoint.transform.position + (endPoint.transform.position - startPoint.transform.position) / 2 + Vector3.up * 25f;
        Vector3 middlePoint = startPoint.transform.position +
            (endPoint.transform.position - startPoint.transform.position) / 2 + Vector3.up * upperLimit;



        startedJumpingRoutine = true;

        while (jumpCounter <= jumpTimer)
        {


            jumpCounter += Time.deltaTime;

            //Vector3 m1 = Vector3.Lerp(startPoint.position, middlePoint, jumpPlatformCounter / jumpPlatformTimer);
            //Vector3 m2 = Vector3.Lerp(middlePoint, endPoint.position + new Vector3(0f, 1.5f, 0f), jumpPlatformCounter / jumpPlatformTimer);
            Vector3 m1 = Vector3.Lerp(startPoint.transform.position, middlePoint, jumpCounter / jumpTimer);
            Vector3 m2 = Vector3.Lerp(middlePoint, endPoint.transform.position + new Vector3(0f, 3f, 0f), jumpCounter / jumpTimer);

            this.transform.position = Vector3.Lerp(m1, m2, jumpCounter / jumpTimer);


            if (leftToRightJumper == true)
            {
                this.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Lerp(270f, 90f, jumpCounter / jumpTimer));
            }
            if(leftToRightJumper == false)
            {
                this.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Lerp(270f, 450f, jumpCounter / jumpTimer));
            }

            yield return null;


        }

        state = fishJumpingStates.Idle;

        jumpCounter = 0f;

        startedJumpingRoutine = false;

        


    }
}
