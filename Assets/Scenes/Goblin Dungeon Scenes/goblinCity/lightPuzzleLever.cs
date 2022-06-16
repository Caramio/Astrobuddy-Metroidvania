using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class lightPuzzleLever : MonoBehaviour
{
    public GameObject puzzleLight;

    //Some colors to change the light
    private List<Color> colorList = new List<Color>();

    //Iterating through the color list with this
    private int colorCount = 0;

    private bool isInLeverRange;
    void Start()
    {
        colorList.Add(Color.red);
        colorList.Add(Color.blue);
        colorList.Add(Color.green);
        colorList.Add(Color.black);
        colorList.Add(Color.yellow);
        
    }

    // Update is called once per frame
    void Update()
    {

        changeLight();


    }

    private void changeLight()
    {

        if(isInLeverRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(colorCount >= colorList.Count)
                {
                    colorCount = 0;
                }

                
                puzzleLight.GetComponent<SpriteRenderer>().color = colorList[colorCount];
                puzzleLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = colorList[colorCount];

                colorCount += 1;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {

            isInLeverRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "PlayerHitbox")
        {

            isInLeverRange = false;

        }
    }

}
