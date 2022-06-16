using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowQuestionFiveMethods : MonoBehaviour
{
    private GameObject playerObj;

    public GameObject riddleStartPoint;

    public GameObject doorToOpen;

    public GameObject sixthRiddleSpot;

    public GameObject spiritToMove;


    //question one canvas
    public Canvas questionFiveCanvas;

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
        Debug.Log("Answer Three");

    }

    public void answerThree()
    {
       

        playerObj.transform.position = riddleStartPoint.transform.position;
        this.gameObject.SetActive(false);
        Debug.Log("Answer One");



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
        questionFiveCanvas.enabled = false;
        Debug.Log("Answer Two");

        snowSpiritRiddle.isMovingToSpot = true;

        startedMovementRoutine = true;

     
        Color startCol = this.GetComponent<SpriteRenderer>().color;

        while (riddleMovementCounter <= riddleMovementTimer)
        {

            this.GetComponent<SpriteRenderer>().color = new Color(startCol.r, startCol.g, startCol.b, Mathf.Lerp(startCol.a, 0f , riddleMovementCounter/riddleMovementTimer));

            riddleMovementCounter += Time.deltaTime;

            yield return null;

        }

    
        this.gameObject.SetActive(false);
        spiritToMove.gameObject.SetActive(false);

        riddleMovementCounter = 0f;

        startedMovementRoutine = false;



    }
}
