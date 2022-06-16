using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightPuzzleCorrectOrder : MonoBehaviour
{
    [HideInInspector]
    public Color correctColor;

    void Start()
    {
        // setting correct colors for the puzzle elements at the start
        if(this.gameObject.name == "puzzleLightTopLeft")
        {
            correctColor = Color.red;
        }

        if (this.gameObject.name == "puzzleLightTopRight")
        {
            correctColor = Color.blue;
        }


        if (this.gameObject.name == "puzzleLightBottomRight")
        {
            correctColor = Color.green;
        }

        if (this.gameObject.name == "puzzleLightBottomLeft")
        {
            correctColor = Color.yellow;
        }

        if (this.gameObject.name == "puzzleLightBottomMiddle")
        {
            correctColor = Color.black;
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
