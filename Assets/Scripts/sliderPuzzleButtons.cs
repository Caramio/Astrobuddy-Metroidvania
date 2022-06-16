using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class sliderPuzzleButtons : MonoBehaviour
{
    //public references
    public GameObject emptySlider;

    //private references
    private RectTransform sliderRectTransform;
    private Image sliderImage;


    private Vector3 holderVector;

    void Start()
    {
        sliderRectTransform = this.GetComponent<RectTransform>();
        sliderImage = this.GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {   
        
        sliderRectTransform = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>();    
            
    }


    // If statement to check whether or not the puzzle piece that is clicked is to the next to the empty slider
    // but not diagonal
    public void pressSlider()
    {
        
        if ((Mathf.Abs(emptySlider.GetComponent<RectTransform>().position.x - sliderRectTransform.position.x) < 100.5f &&
            Mathf.Abs(emptySlider.GetComponent<RectTransform>().position.y - sliderRectTransform.position.y) < 0.5f) ||
            
            (Mathf.Abs(emptySlider.GetComponent<RectTransform>().position.x - sliderRectTransform.position.x) < 0.5f &&
            Mathf.Abs(emptySlider.GetComponent<RectTransform>().position.y - sliderRectTransform.position.y) < 100.5f)
            )
        {

            holderVector = this.sliderRectTransform.position;

            this.sliderRectTransform.position = emptySlider.GetComponent<RectTransform>().position;

            emptySlider.GetComponent<RectTransform>().position = holderVector;

        }

    }



        
}
