using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class batChaseSequenceEnder : MonoBehaviour
{
    public Camera currentCamera;

    public GameObject exitPreventingDoor;
    public GameObject exitPreventingDoorBottom;
    public GameObject startSequenceStarterObj;
    public GameObject bossObject;


    public static bool endedChaseSequence;


    //public GameObject batBoss;

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void endChaseSequence()
    {

        //set the camera to the player
        currentCamera.GetComponent<cameraFollow>().isFollowingOther = false;

        //set the start trigger to false;
        startSequenceStarterObj.SetActive(false);

        //exit preventing door should be open at this point
        exitPreventingDoor.SetActive(false);
        exitPreventingDoorBottom.SetActive(false);

        //setting bossobject to false
        bossObject.SetActive(false);

        endedChaseSequence = true;

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "chasingBoss")
        {


            Debug.Log("ended");

            endChaseSequence();

        }
    }


}
