using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstIllusionCamera : MonoBehaviour
{
    public Camera sceneCamera;
    public GameObject cameraHolder;
    public float cameraSize;

    public Transform cameraMiddlePoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if(collision.tag == "PlayerHitbox" && collision.transform.parent.name == "Astrobuddy")
        {

            sceneCamera.GetComponent<cameraFollow>().enabled = false;

            sceneCamera.orthographicSize = cameraSize;

            cameraHolder.transform.position = cameraMiddlePoint.position;
            sceneCamera.transform.position = cameraHolder.transform.position;

           
            

        }
    }


    private void OnTriggerExit2D(Collider2D collision )
    {

        if (collision.tag == "PlayerHitbox" && collision.transform.parent.name == "Astrobuddy")
        {

            sceneCamera.GetComponent<cameraFollow>().enabled = true;

        }

    }
}
