using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mageBossFightEntryHolder : MonoBehaviour
{

    public Camera sceneCamera;
    public GameObject cameraHolder;
    public Transform cameraMidPoint;
    public float cameraSize;

    public GameObject mageBosses;
    public GameObject spikeWallTester;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {
            sceneCamera.GetComponent<cameraFollow>().enabled = false;

            sceneCamera.orthographicSize = cameraSize;
            cameraHolder.transform.position = cameraMidPoint.position;

            mageBosses.SetActive(true);

            spikeWallTester.SetActive(true);
        }



    }
}
