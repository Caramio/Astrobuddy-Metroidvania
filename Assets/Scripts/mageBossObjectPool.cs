using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mageBossObjectPool : MonoBehaviour
{
    //-------------------//
    //related to spreadfire//
    //-------------------//
    public static int pooledTimes = 0;

    public GameObject spreadFirePooledObject;
    public int spreadFirepoolAmount;

    // use this to access the number of projectiles property
    public GameObject redMageFireballScriptObj;

    

    [HideInInspector]
    public List<GameObject> spreadFirePool;

    //-------------------//
    //related to eruption//
    //-------------------//
    //which number gameobjects did we erupt from the list, use this in the redmagestates?
    public static int pooledEruptionTimes = 0;

    //the object and amount of it to be pooled
    public GameObject eruptionPooledObject;
    public int eruptionPoolAmount;

    [HideInInspector]
    public List<GameObject> eruptionPool;


    //-------------------//
    //related to eruption//
    //-------------------//
    [HideInInspector]
    public List<GameObject> waterSpiralPool;

    public GameObject waterSpiralObject;
    public int waterPoolAmount;

      


    void Start()
    {
        //--------------//
        //spreadfire related
        //--------------//
        spreadFirePool = new List<GameObject>();

        for(int i = 0; i < spreadFirepoolAmount; i++)
        {
            GameObject instantiatedObj = Instantiate(spreadFirePooledObject);
            instantiatedObj.SetActive(false);

            spreadFirePool.Add(instantiatedObj);
        }
        //--------------//
        //eruption related
        //--------------//
        eruptionPool = new List<GameObject>();

        for (int i = 0; i < eruptionPoolAmount; i++)
        {
            GameObject instantiatedObj = Instantiate(eruptionPooledObject);
            instantiatedObj.SetActive(false);

            eruptionPool.Add(instantiatedObj);
        }


        //--- WATER ATTACKS RELATED --- //

        //--------------//
        //spiral water related
        //--------------//
        waterSpiralPool = new List<GameObject>();

        for (int i = 0; i < waterPoolAmount; i++)
        {
            GameObject instantiatedObj = Instantiate(waterSpiralObject);
            instantiatedObj.SetActive(false);

            waterSpiralPool.Add(instantiatedObj);
        }


    }

    // Update is called once per frame
    void Update()
    {


        //Debug.Log("pooled times is" +  pooledTimes);

    }

    //--------------//
    //Disabling objects for the eruption pool//
    //--------------//




    //--------------//
    //Disabling objects for the spread pool//
    //--------------//
    private float disableTimer = 5f;
    
    
    public IEnumerator disableObjectsRoutine()
    {

        if(redMageStates.startedtorchFireballRoutine == false)
        {
            foreach(GameObject pooledObj in spreadFirePool)
            {
                pooledObj.SetActive(false);
            }
        }

        float disableCounter = 0f;

        while(disableCounter <= disableTimer)
        {
            disableCounter += Time.deltaTime;
            yield return null;

        }

        Debug.Log("pooled times is" + pooledTimes);
        Debug.Log("set them to inactive");
        pooledTimes -= 1;
        disableCounter = 0f;

 
    }
}
