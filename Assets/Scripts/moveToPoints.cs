using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToPoints : MonoBehaviour
{

    private Transform wallTransform;
   
    void Start()
    {

        wallTransform = this.GetComponent<Transform>();


    }

    
    void Update()
    {
        moveToPoint();
    }


  

    [SerializeField]
    private float moveSpeed;

    public List<Transform> routeList;
    private int routeCounter;

    private void moveToPoint()
    {

        if (routeCounter < routeList.Count)
        {

            wallTransform.position = Vector3.MoveTowards(wallTransform.position, routeList[routeCounter].position, moveSpeed * Time.deltaTime);

            if (wallTransform.position == routeList[routeCounter].position)
            {
                routeCounter += 1;
            }


        }

    }
    

}
