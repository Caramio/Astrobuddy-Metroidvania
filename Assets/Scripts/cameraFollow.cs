using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    // coordiantes for the player
    private float playerXCord;
    private float playerYCord;

    private float frogXCord;
    private float frogYCord;

    public static bool sizeControlledByOther;
        

    // Camera that is going to be worked with
    public Camera sceneCamera;

    // Object to follow
    private GameObject playerCharacter;
    private GameObject frogCharacter;

    // camera holding object
    public GameObject cameraHolder;

    //public boolean to check from astrointeractions mainly
    [HideInInspector]
    public bool isCheckingInformation = false;

    //Check if we should be following the player or something else
    [HideInInspector]
    public bool isFollowingOther;
    //private transform to determine the place where the camera should be ( assigned from astroInteractions )
    [HideInInspector]
    public Transform informationTransform;

    //If the camera is to follow something else
    [HideInInspector]
    public Transform followedObject;


    //Static bool that will show whether or not we are fighting a boss or not
    [HideInInspector]
    public static bool fightingBoss;


   


   

    void Start()
    {
        playerCharacter = GameObject.Find("Astrobuddy");
        frogCharacter = GameObject.Find("Astrobuddy").transform.GetChild(0).gameObject;

        //Making the camera snap to the player on scene entries
        cameraHolder.transform.position = playerCharacter.transform.position;
    }

    //REMEMBER TO ASSIGN PLAYER OBJECTS
    void Update()
    {
        


        //Player current coordinates
        if (playerMovement.movingAstro)
        {
            

            playerXCord = playerCharacter.transform.position.x;
            playerYCord = playerCharacter.transform.position.y;
        }
        //Frog coordinates
        else
        {
            
            
            frogXCord = frogCharacter.transform.position.x;
            frogYCord = frogCharacter.transform.position.y;
        }


    }

    private void FixedUpdate()
    {

        // If we are not fighting a boss, we will perform camera operations
        if (!fightingBoss || !isFollowingOther)
        {

            // If we are not checking an information wall (determined in astroInteraction), we will be following either frogman or astrobuddy
            // changing the size back to 25f once not checking info
            if (!isCheckingInformation)
            {

                if (sizeControlledByOther == false)
                {
                    sceneCamera.orthographicSize = 25f;
                }

                if (playerMovement.movingAstro)
                {
                    {
                        cameraHolder.transform.position = new Vector3(Mathf.Lerp((cameraHolder.transform.position.x), playerXCord, 0.05f),
                           Mathf.Lerp(cameraHolder.transform.position.y, playerYCord, 0.05f), -5f);

                        sceneCamera.transform.position = cameraHolder.transform.position;
                    }
                }
                else
                {
                    {

                        //lower size when controlling frogman
                        sceneCamera.orthographicSize = 15f;

                        cameraHolder.transform.position = new Vector3(Mathf.Lerp((cameraHolder.transform.position.x), frogXCord, 0.05f),
                           Mathf.Lerp(cameraHolder.transform.position.y, frogYCord, 0.05f), -5f);

                        sceneCamera.transform.position = cameraHolder.transform.position;
                    }

                }


                //if the camera is too far back, snap back instantly, remember, scenes need to be placed away in the X axis
                // for the camera to snap back, this applies only when astro is being moved and not frogman

                if (Mathf.Abs(cameraHolder.transform.position.x - playerCharacter.transform.position.x) > 50f && playerMovement.movingAstro)
                {
                    cameraHolder.transform.position = new Vector3(playerXCord, playerYCord, -5f);

                }


            }

            if (isCheckingInformation)
            {
                // changing to the place to be viewed and also changing camera size
                cameraHolder.transform.position = new Vector3(informationTransform.position.x, informationTransform.position.y, -5f);
                sceneCamera.orthographicSize = 7f;
                sceneCamera.transform.position = cameraHolder.transform.position;


            }

            if (isFollowingOther)
            {

                cameraHolder.transform.position = new Vector3(followedObject.position.x, followedObject.position.y, -5f);
                sceneCamera.transform.position = cameraHolder.transform.position;

            }

            

        }


    }



    
}
