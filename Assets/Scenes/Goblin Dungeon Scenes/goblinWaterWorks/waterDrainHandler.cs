using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterDrainHandler : MonoBehaviour
{
    public GameObject drainageCameraHolder;



    public GameObject waterMask;

    public GameObject waterGear;

    //private reference to startpoint
    private Vector3 startTransform;


    //Timers
    private float showWaterDrainedTimer = 5f;
    private float showWaterDrainedCounter;


    //Bools
    private bool startedWaterDrainRoutine;


    public List<GameObject> waterList;


    void Start()
    {

        startTransform = waterMask.transform.position;

    }

    
    void Update()
    {
        if(startedWaterDrainRoutine == false)
        {
            StartCoroutine(showWaterDrained());
        }
    }
    

    //change this to include all scenes that need to be changed once the saving system is implemented
    private void drainWaterFromScenes()
    {
        //Make the static variable of waterIsDrained to true for other scenes to be able to see
        staticgoblinWaterWorks.waterIsDrained = true;

        foreach(GameObject waterObj in waterList)
        {
            waterObj.SetActive(false);
        }


    }



    private IEnumerator showWaterDrained()
    {

        startedWaterDrainRoutine = true;

        drainageCameraHolder.SetActive(true);


        Debug.Log("doinb");

        while(showWaterDrainedCounter <= showWaterDrainedTimer)
        {
            // moving the mask for the required amount, 45f can be changed to a variable if needed...
            waterMask.transform.position = new Vector3(startTransform.x, Mathf.Lerp(startTransform.y, 45f, showWaterDrainedCounter / showWaterDrainedTimer) , 0f);

            //turning the gear to make it loook more realistic
            waterGear.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0f, 360f, showWaterDrainedCounter / showWaterDrainedTimer));

            showWaterDrainedCounter += Time.deltaTime;

            yield return null;
        }
        

        //deactivate water in current scene
        drainWaterFromScenes();

        showWaterDrainedCounter = 0f;
        startedWaterDrainRoutine = false;
        drainageCameraHolder.SetActive(false);

        this.gameObject.SetActive(false);
    }



}
