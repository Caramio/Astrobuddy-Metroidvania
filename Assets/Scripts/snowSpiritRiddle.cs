using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowSpiritRiddle : MonoBehaviour
{
    public GameObject riddleCanvas;


    public List<GameObject> questionList;

    public List<GameObject> openedDoorsList;
    public List<GameObject> movedSpotsList;

    public static int questionCounter;
    // set this while moving between question spots
    // so the ontriggerexit wont interfere
    public static bool isMovingToSpot;


    //coroutine
    public static bool startedMovementRoutine;

    private float movementRoutineTimer = 3f;
    private float movementRoutineCounter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "PlayerHitbox" && isMovingToSpot == false)
        {

            questionList[questionCounter].SetActive(true);

        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "PlayerHitbox" && isMovingToSpot == false)
        {

            questionList[questionCounter].SetActive(false);

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "PlayerHitbox" && isMovingToSpot == false && questionList[questionCounter].activeInHierarchy == false)
        {

            questionList[questionCounter].SetActive(true);

        }


    }
    
    public IEnumerator moveSpirit()
    {
        isMovingToSpot = true;

        

        openedDoorsList[questionCounter].SetActive(false);
       
      
        startedMovementRoutine = true;

        Vector3 startPos = this.transform.position;

        while (movementRoutineCounter <= movementRoutineTimer)
        {

            Debug.Log("olmaz");

            //change later
            this.transform.position = Vector3.Lerp(startPos, movedSpotsList[questionCounter].transform.position, movementRoutineCounter / movementRoutineTimer);


            movementRoutineCounter += Time.deltaTime;

            yield return null;

        }

        questionCounter += 1;

        isMovingToSpot = false;

        movementRoutineCounter = 0f;

        startedMovementRoutine = false;

        //Set the gameobject to false after it reaches a certain point
        if(questionCounter == 5)
        {
            this.gameObject.SetActive(false);
        }


    }
    
}
