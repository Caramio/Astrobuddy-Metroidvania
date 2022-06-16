using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowQuestionTwoMethods : MonoBehaviour
{
    private GameObject playerObj;

    public GameObject riddleStartPoint;

    public GameObject doorToOpen;

    public GameObject thirdRiddleSpot;

    public GameObject spiritToMove;


    //question one canvas
    public Canvas questionTwoCanvas;

    //holder to deactivate
    public GameObject canvasHolder;


    //Coroutine related
    private float riddleMovementCounter;
    private float riddleMovementTimer = 5f;

    private bool startedMovementRoutine;



    //private int for given answer
    private int givenAnswerNumber;
    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("Astrobuddy");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void answerOne()
    {

        if (snowSpiritRiddle.startedMovementRoutine == false)
        {
            spiritToMove.GetComponent<snowSpiritRiddle>().StartCoroutine("moveSpirit");
            this.gameObject.SetActive(false);
        }

    }

    public void answerTwo()
    {

        

        playerObj.transform.position = riddleStartPoint.transform.position;
        this.gameObject.SetActive(false);
        Debug.Log("Answer Two");

    }

    public void answerThree()
    {
       

        playerObj.transform.position = riddleStartPoint.transform.position;
        this.gameObject.SetActive(false);
        Debug.Log("Answer Three");

    }

    public void answerFour()
    {
        

        playerObj.transform.position = riddleStartPoint.transform.position;
        this.gameObject.SetActive(false);
        Debug.Log("Answer Four");

    }


    private IEnumerator moveSpirit()
    {

        doorToOpen.SetActive(false);
        questionTwoCanvas.enabled = false;
        Debug.Log("Answer Correct");

        snowSpiritRiddle.isMovingToSpot = true;

        startedMovementRoutine = true;

        Vector3 startPos = spiritToMove.transform.position;

        while (riddleMovementCounter <= riddleMovementTimer)
        {

            Debug.Log("olmaz");

            //change later
            spiritToMove.transform.position = Vector3.Lerp(startPos, thirdRiddleSpot.transform.position, riddleMovementCounter / riddleMovementTimer);


            riddleMovementCounter += Time.deltaTime;

            yield return null;

        }

        snowSpiritRiddle.questionCounter += 1;

        snowSpiritRiddle.isMovingToSpot = false;

        this.gameObject.SetActive(false);

        riddleMovementCounter = 0f;

        startedMovementRoutine = false;



    }
}
