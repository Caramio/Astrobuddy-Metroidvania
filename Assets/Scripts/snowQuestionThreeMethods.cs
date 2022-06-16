using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowQuestionThreeMethods : MonoBehaviour
{
    private GameObject playerObj;

    public GameObject riddleStartPoint;

    public GameObject doorToOpen;

    public GameObject fourthRiddleSpot;

    public GameObject spiritToMove;


    //question one canvas
    public Canvas questionThreeCanvas;

    //holder to deactivate
    public GameObject canvasHolder;


    //Coroutine related
    private float riddleMovementCounter;
    private float riddleMovementTimer = 5f;

    private bool startedMovementRoutine;

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
        

        playerObj.transform.position = riddleStartPoint.transform.position;
        this.gameObject.SetActive(false);
        Debug.Log("Answer One");
        

    }

    public void answerTwo()
    {

        if (snowSpiritRiddle.startedMovementRoutine == false)
        {
            spiritToMove.GetComponent<snowSpiritRiddle>().StartCoroutine("moveSpirit");
            this.gameObject.SetActive(false);
        }




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
        questionThreeCanvas.enabled = false;
        Debug.Log("Answer Two");

        snowSpiritRiddle.isMovingToSpot = true;

        startedMovementRoutine = true;

        Vector3 startPos = spiritToMove.transform.position;

        while (riddleMovementCounter <= riddleMovementTimer)
        {

            Debug.Log("olmaz");

            //change later
            spiritToMove.transform.position = Vector3.Lerp(startPos, fourthRiddleSpot.transform.position, riddleMovementCounter / riddleMovementTimer);


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
