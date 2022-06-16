using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hiddenLayerEnabler : MonoBehaviour
{

    public GameObject interactedObjectOne, interactedObjectTwo, interactedObjectThree;

    public Transform entranceChecker;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    
    
    public void accessLayer()
    {

        interactedObjectOne.SetActive(false);


        this.gameObject.GetComponent<Collider2D>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;




    }

    
}
