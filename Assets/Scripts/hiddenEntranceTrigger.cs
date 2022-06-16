using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Experimental.Rendering.Universal;

public class hiddenEntranceTrigger : MonoBehaviour
{

    public GameObject activatedLight;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Frog")
        {
            activatedLight.SetActive(true);
            Debug.Log("Entered hidden area");
        }
        
    }

    
}
