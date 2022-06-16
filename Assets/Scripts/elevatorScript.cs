using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class elevatorScript : MonoBehaviour
{
    // speed to move elevator at
    public float movementSpeed;

    //Public reference to the elevators end point;
    public Transform elevatorEndPoint;
    //private for start point, it will be the start of this object
    private Vector2 elevatorStartPoint;


    //Components
    [HideInInspector]
    public Rigidbody2D elevatorBody;

    //Public elevator timers
    //public float elevatorDuration;
    //private float elevatorTimer;


    //Boolean
    [HideInInspector]
    public bool elevatorWentUp = false;

    [HideInInspector]
    public bool elevatorIsMoving = false;

    [HideInInspector]
    public bool wentUpOnce = false;

    

    void Start()
    {
        elevatorBody = this.gameObject.GetComponent<Rigidbody2D>();

        elevatorStartPoint = this.transform.position;
    }

    // CHANGE LATER FOR ELEVATOR
    void Update()
    {


        if (this.transform.position.y >= elevatorEndPoint.transform.position.y && 
            elevatorBody.velocity.y > 0f)
        {
            wentUpOnce = true;
            elevatorWentUp = true;
            elevatorBody.velocity = Vector2.zero;


        }


        if (this.transform.position.y <= elevatorStartPoint.y &&
            elevatorBody.velocity.y < 0f && wentUpOnce)
        {          
            elevatorWentUp = false;
            elevatorBody.velocity = Vector2.zero;
        }


    }


    public void moveElevatorUp()
    {
        if (!elevatorWentUp)
        {

            elevatorBody.velocity = new Vector2(0f, movementSpeed);

        }

        if (elevatorWentUp)
        {

            elevatorBody.velocity = new Vector2(0f, -movementSpeed);

        }
       
    }










}
