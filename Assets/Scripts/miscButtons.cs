using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class miscButtons : MonoBehaviour
{
    public Sprite waterNotesOneSprite;

    public GameObject waterNotesHolder;


    //private references
    private GameObject selectedObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            //Debug.Log(EventSystem.current.currentSelectedGameObject.gameObject.name);
        }
        
    }

    public void clickMiscItem()
    {
        Debug.Log("CLANK");
        if (Time.timeScale == 1)
        {

            if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite == waterNotesOneSprite)
            {
                selectedObject = waterNotesHolder;
                waterNotesHolder.SetActive(true);

            }
        }
        
    }

    public void backButton()
    {
        if (Time.timeScale == 1)
        {

            selectedObject.SetActive(false);

        }


    }
}
