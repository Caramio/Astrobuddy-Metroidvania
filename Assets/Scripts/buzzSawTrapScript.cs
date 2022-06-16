using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buzzSawTrapScript : MonoBehaviour
{
    [SerializeField]
    private Transform startPoint, endPoint;

    private Transform buzzSawTransform;

    

    void Start()
    {

        buzzSawTransform = this.GetComponent<Transform>();


    }

    // Update is called once per frame
    void Update()
    {

        rotateSaw();


        moveBetweenPoints();
    }

    private void rotateSaw()
    {


        buzzSawTransform.Rotate(Vector3.back * Time.deltaTime * 1000f);

    }




    //function to move the blades
    private bool reachedEnd;

    [SerializeField]
    private float sawMoveSpeed;
    private void moveBetweenPoints()
    {

        if(buzzSawTransform.position == endPoint.position)
        {
            reachedEnd = true;
        }
        else if(buzzSawTransform.position == startPoint.position)
        {
            reachedEnd = false;
        }
         

        if (reachedEnd == false)
        {
            buzzSawTransform.position = Vector3.MoveTowards(buzzSawTransform.position, endPoint.position, sawMoveSpeed * Time.deltaTime);
        }
        else
        {
            buzzSawTransform.position = Vector3.MoveTowards(buzzSawTransform.position, startPoint.position, sawMoveSpeed * Time.deltaTime);
        }
        
         

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {

            collision.GetComponentInParent<astroStats>().astroHealth = 0;

        }

    }


}
