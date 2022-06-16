using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indicatorMoving : MonoBehaviour
{

    //Transform of this object
    private Transform indicatorTransform;

    //starting positions
    private float startingX;
    private float startingY;

    void Start()
    {
        //Setting initial references
        indicatorTransform = this.gameObject.GetComponent<Transform>();

        startingX = this.transform.position.x;
        startingY = this.transform.position.y;


    }

    
    void Update()
    {

        moveIndicatorVertical();

    }


    //Moving the indicator up and down to make the player see it easier
    private void moveIndicatorVertical()
    {
        
        indicatorTransform.position = new Vector2(startingX , startingY + Mathf.PingPong(Time.time*2 , 1f));
     
    }

    private void moveIndicatorHorizontal()
    {

        indicatorTransform.position = new Vector2(startingX + Mathf.PingPong(Time.time * 2, 1f), startingY);


    }

}
