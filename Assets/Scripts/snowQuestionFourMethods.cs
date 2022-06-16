using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowQuestionFourMethods : MonoBehaviour
{
    private GameObject playerObj;

    public GameObject riddleStartPoint;

    public GameObject doorToOpen;

    public GameObject fifthRiddleSpot;

    public GameObject spiritToMove;


    //question one canvas
    public Canvas questionFourCanvas;

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
        

        playerObj.transform.position = riddleStartPoint.transform.position;
        this.gameObject.SetActive(false);
        Debug.Log("Answer Three");

    }

    public void answerThree()
    {


        if (snowSpiritRiddle.startedMovementRoutine == false)
        {
            spiritToMove.GetComponent<snowSpiritRiddle>().StartCoroutine("moveSpirit");
            this.gameObject.SetActive(false);
        }



    }

    public void answerFour()
    {
        

        playerObj.transform.position = riddleStartPoint.transform.position;
        this.gameObject.SetActive(false);
        Debug.Log("Answer Four");


    }


}
