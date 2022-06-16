using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkSliderPosition : MonoBehaviour
{

    [HideInInspector]
    public bool placedSlider = false;

    private RectTransform sliderRectTransform;


    public float xCoordinate;
    public float yCoordinate;
    
    void Start()
    {
        sliderRectTransform = this.GetComponent<RectTransform>();
    }

    
    void Update()
    {

        checkPlace();
        
    }


    private void checkPlace()
    {
        
        if((Mathf.Abs(sliderRectTransform.localPosition.x - xCoordinate) < 0.1) && (Mathf.Abs(sliderRectTransform.localPosition.y - yCoordinate) < 0.1))
        {
            //Debug.Log(this.gameObject.name + " is in place " + placedSlider);
            placedSlider = true;
        }
        else
        {
            placedSlider = false;
        }
       

    }
}
