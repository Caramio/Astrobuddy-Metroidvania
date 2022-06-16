using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using System.IO;

public class sliderCompletionChecker : MonoBehaviour
{

    public GameObject openedObject;


    private List<GameObject> partsArray;

    public GameObject partOne, partTwo, partThree, partFour, partFive,partSix,partSeven,partEight,partNine,partTen,
        partEleven,partTwelve,partThirteen,partFourteen,partFifteen,partSixteen,partSeventeen,partEighteen,partNineteen,partTwenty,
        partTwentyOne,partTwentyTwo,partTwentyThree,partTwentyFour;

    void Start()
    {
        //adding all parts to the list

        partsArray = new List<GameObject>
        {
            partOne,
            partTwo,
            partThree,
            partFour,
            partFive,

            partSix,
            partSeven,
            partEight,
            partNine,
            partTen,

            partEleven,
            partTwelve,
            partThirteen,
            partFourteen,
            partFifteen,

            partSixteen,
            partSeventeen,
            partEighteen,
            partNineteen,
            partTwenty,

            partTwentyOne,
            partTwentyTwo,
            partTwentyThree,
            partTwentyFour
        };


    }

    

    //Checking (using System.Linq) if every placedSlider value is true, resembling that the puzzle is complete
    void Update()
    {
       

        if(partsArray.All(slider => slider.GetComponent<checkSliderPosition>().placedSlider))
        {
            Debug.Log("PUZZLE SOLVED!");
            
          
            openedObject.SetActive(false);

        }

              
    }
}
