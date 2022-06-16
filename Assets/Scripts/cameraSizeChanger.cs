using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSizeChanger : MonoBehaviour
{


    public Camera sceneCamera;

    public float cameraSize;
    
    void Start()
    {
        
    }


    
    void Update()
    {
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "PlayerHitbox")
        {
            cameraFollow.sizeControlledByOther = true;


            sceneCamera.orthographicSize = cameraSize;
        }
      

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "PlayerHitbox")
        {
            cameraFollow.sizeControlledByOther = false;
        }

    }





}
