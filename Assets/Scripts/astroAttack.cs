using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class astroAttack : MonoBehaviour
{
    //Public references
    public GameObject astroSword;

    //beginning and ending points for the linecast(sword end and start points)
    public Transform firstInput;
    public Transform secondInput;

    //timer for the swings
    private float timerStart;
    private float timerEnd = 0.2f;

    [HideInInspector]
    public bool startedSwinging = false;


    // list containing enemies
    private List<GameObject> enemyList = new List<GameObject>();


    


    void Start()
    {
        
    }

    
    void Update()
    {

        swingButton();

        if (startedSwinging)
        {

            hitEnemy();
            swingSword();
        }

        Debug.DrawLine(firstInput.position, secondInput.position,Color.red);

    }


    // If an enemy is hit and the current list doesn't contain the enemy, we will add it to the list
    // setting raycastarray size to 10 , can change but 10 objects should be enough 
    private void hitEnemy()
    {
        RaycastHit2D[] rayArray = new RaycastHit2D[10];
        rayArray = Physics2D.LinecastAll(firstInput.position, secondInput.position);

        foreach(RaycastHit2D rayHit in rayArray)
        {

            if (rayHit)
            {
                if (!enemyList.Contains(rayHit.collider.gameObject))
                {

                    enemyList.Add(rayHit.collider.gameObject);

                }
            }
        }

    }
    

    // Indicating that the swing timer started
    private void swingButton()
    {

        if (!startedSwinging)
        {

            if (Input.GetMouseButtonDown(0))
            {

                StartCoroutine(swingTimer());
                startedSwinging = true;

            }
        }

    }
    // function to change the rotation of the sword over time , the *5 should always be equal to one with the timer variable
    private void swingSword()
    {
        
        

        astroSword.transform.eulerAngles = new Vector3(astroSword.transform.eulerAngles.x, astroSword.transform.eulerAngles.y,
               Mathf.Lerp(70, 5, timerStart * 5f));

    }


    //IEnumerator that keeps track of the swing timer, once the timer is done we will revert the sword to its original position
    //We will check the enemyList and at the end we will damage all of them once and then clear the list , getting ready for the
    //next swing
    IEnumerator swingTimer()
    {

        while(timerStart <= timerEnd)
        {
            timerStart += Time.deltaTime;

            yield return null;
        }

        astroSword.transform.eulerAngles = new Vector3(astroSword.transform.eulerAngles.x, astroSword.transform.eulerAngles.y,
               70f);


        foreach (GameObject enemyObject in enemyList)
        {
            if (enemyObject.GetComponent<trollEnemyScript>() != null)
            {
                enemyObject.GetComponent<trollEnemyScript>().takeDamage();
            }
        }

        enemyList.Clear();

        timerStart = 0f;

        startedSwinging = false;





    }

  
}
