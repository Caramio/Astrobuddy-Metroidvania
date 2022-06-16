using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblinElevatorScript : MonoBehaviour
{
    //public reference to the elevator system
    public GameObject elevatorHolder;

    //Routes for the elevator to follow
    public List<GameObject> elevatorRouteList;

    //Doors to open when the elevator reaches the floor
    public List<GameObject> elevatorDoorList;


    //public float to show elevator speed
    public float elevatorSpeed;

    //Static int to show which floor the elevator is on
    public static int elevatorFloor = 0;

    //ROUTINE BOOLS
    public static bool startedMovingUpRoutine;
    public static bool startedMovingDownRoutine;
         

    
    //private bool to show if the player is in range
    private bool inLeverRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(elevatorFloor);
        moveElevatorUpButton();
        moveElevatorDownButton();
    }

   

    private void moveElevatorUpButton()
    {
        // if its a moving up lever
        if (this.gameObject.name == "movingUpLever")
        {
            // if we are in range of the lever
            if (inLeverRange == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {

                    if (startedMovingDownRoutine == false && startedMovingUpRoutine == false)
                    {
                        StartCoroutine(movingUp());
                    }
    
                  

                }
            }
        }

    }

    private void moveElevatorDownButton()
    {
        // if its a moving up lever
        if (this.gameObject.name == "movingDownLever")
        {
            // if we are in range of the lever
            if (inLeverRange == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {

                    if (startedMovingDownRoutine == false && startedMovingUpRoutine == false)
                    {
                        StartCoroutine(movingDown());
                    }



                }
            }
        }

    }
    private IEnumerator movingDown()
    {
        if (elevatorFloor >= 1)
        {
            //current door should close
            elevatorDoorList[elevatorFloor].SetActive(true);

            elevatorFloor -= 1;

            Debug.Log("started going down");

            startedMovingDownRoutine = true;

            while (elevatorHolder.transform.position.y != elevatorRouteList[elevatorFloor].transform.position.y)
            {
                // move towards the Y coordinate of the route without changing x
                elevatorHolder.transform.position = Vector3.MoveTowards(elevatorHolder.transform.position, 
                    new Vector3(elevatorHolder.transform.position.x, elevatorRouteList[elevatorFloor].transform.position.y , 0f), Time.deltaTime * elevatorSpeed);

                yield return null;
            }

            Debug.Log("ended going down");

            //Once we go down, the old door should be open
            elevatorDoorList[elevatorFloor].SetActive(false);

            startedMovingDownRoutine = false;

        }

    }

    private IEnumerator movingUp()
    {
        // before we move up , set the door to true to close it
        elevatorDoorList[elevatorFloor].SetActive(true);

        if (elevatorFloor < 5)
        {
            elevatorFloor += 1;
        }

        Debug.Log("started tyhis");

        startedMovingUpRoutine = true;

        while (elevatorHolder.transform.position.y != elevatorRouteList[elevatorFloor].transform.position.y)
        {

            elevatorHolder.transform.position = Vector3.MoveTowards(elevatorHolder.transform.position,
                    new Vector3(elevatorHolder.transform.position.x, elevatorRouteList[elevatorFloor].transform.position.y, 0f), Time.deltaTime * elevatorSpeed);

            yield return null;
        }

        Debug.Log("ended this");

        //Once we reach a floor, disable that door
        elevatorDoorList[elevatorFloor].SetActive(false);

        startedMovingUpRoutine = false;

        

    }


 





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {

            inLeverRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {

            inLeverRange = false;
        }
    }
}
