using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondIllusionCamera : MonoBehaviour
{
    public Camera sceneCamera;
    public GameObject cameraHolder;
    public float cameraSize;

    private GameObject playerObj;

    public Transform cameraMiddlePoint;
    void Start()
    {
        playerObj = GameObject.Find("Astrobuddy");
    }

    // Update is called once per frame
    void Update()
    {
        cameraHolder.transform.position = new Vector3(playerObj.transform.position.x  , playerObj.transform.position.y , -15f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.tag == "PlayerHitbox" && collision.transform.parent.name == "Astrobuddy" )
        {

            sceneCamera.GetComponent<cameraFollow>().enabled = false;

            sceneCamera.orthographicSize = cameraSize;

            cameraHolder.transform.position = cameraMiddlePoint.position;
            sceneCamera.transform.position = cameraHolder.transform.position;

        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "PlayerHitbox" && collision.transform.parent.name == "Astrobuddy")
        {

            sceneCamera.GetComponent<cameraFollow>().enabled = true;

        }

    }
}
