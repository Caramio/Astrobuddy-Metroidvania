using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowQuestionOneMethods : MonoBehaviour
{

    private GameObject playerObj;

    public GameObject riddleStartPoint;

    public GameObject doorToOpen;

    public GameObject secondRiddleSpiritSpot;

    public GameObject spiritToMove;


    //question one canvas
    public Canvas questionOneCanvas;

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
        if(snowSpiritRiddle.startedMovementRoutine == false)
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


   


}
