using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Xml;

public class leverInteraction : MonoBehaviour
{

    //Lever type to distinguish between events
    public string leverType = "Unassigned";

    //Bools
    private bool isInRange = false;


    //Public interaction references
    public GameObject elevatorObject;

    public GameObject doorObject;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (leverType == "Elevator")
        {
            pullElevatorLever();
        }

        if(leverType == "Door")
        {
            openDoorLever();
        }

    }

    private void pullElevatorLever()
    {

        if (Input.GetKeyDown(KeyCode.E) && isInRange)
        {

            if (elevatorObject.GetComponent<elevatorScript>() != null)
            {

                elevatorObject.GetComponent<elevatorScript>().moveElevatorUp();

            }
            
        }

    }

    private void openDoorLever()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            this.transform.Rotate(0, 180, 0);         
            



            doorObject.SetActive(false);

        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" || collision.tag == "Frog")
        {
            isInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Frog")
        {
            isInRange = false;
        }

    }


}
