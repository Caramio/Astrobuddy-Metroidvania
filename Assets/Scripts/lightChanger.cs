using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class lightChanger : MonoBehaviour
{

    private UnityEngine.Rendering.Universal.Light2D thisLight;

    public float lightChangeTime;
    private float lightChangeCounter;


    public float startIntensity;
    public float endIntensity;

    //References at the start...
    void Start()
    {
        thisLight = this.GetComponent<UnityEngine.Rendering.Universal.Light2D>();

        StartCoroutine(LerpLightRepeat());
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    
    
    IEnumerator LerpLightRepeat()
    {
        while (true)
        {
            //Lerp to intensity1
            yield return LerpLight(thisLight, startIntensity, 2f);
            //Lerp to intensity2
            yield return LerpLight(thisLight, endIntensity, 2f);
        }
    }

    IEnumerator LerpLight(UnityEngine.Rendering.Universal.Light2D targetLight, float toIntensity, float duration)
    {
        float currentIntensity = targetLight.intensity;

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            targetLight.intensity = Mathf.Lerp(currentIntensity, toIntensity, counter / duration);
            yield return null;
        }
    }

}
