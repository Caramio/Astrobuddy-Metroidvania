using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickableIndicator : MonoBehaviour
{

    //Public references
    public GameObject pickIndicator;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {

            pickIndicator.SetActive(true);

        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {

            pickIndicator.SetActive(false);

        }


    }
}
