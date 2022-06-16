using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalTurnOn : MonoBehaviour
{
    // Variables
    private SpriteRenderer portalSpriteRenderer;

    //Public references
    public Sprite portalOnSprite;
    public GameObject spriteMaskHolder;
    public BoxCollider2D nonTriggerCollider;

    void Start()
    {

        

    }

    
    void Update()
    {

        

    }
    // Changing the portal once this script is enabled
    private void OnEnable()
    {

       
        changePortal();
    }

    // Changing the portal to make it active
    private void changePortal()
    {

        // Changing the sprite to make it look different
        portalSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        portalSpriteRenderer.sprite = portalOnSprite;
        portalSpriteRenderer.color = new Color(1, 0.9f, 0);

        //Enabling the spriteMaskHolder to create a portal entrance effect
        spriteMaskHolder.SetActive(true);
        


        //Disabling non trigger colliders to be able to enter the portal
        nonTriggerCollider.enabled = false;

        //Changing the inspect text
        this.GetComponent<showTextOnCommand>().givenText = "The portal seems to be working";




    }
}
