using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpeningPlate : MonoBehaviour
{
    [HideInInspector]
    public bool playerInRange;

    public GameObject doorToOpen;

    public GameObject illusionToClose;
    public GameObject illusionToOpen;

    public GameObject otherPlate;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange == true && otherPlate.GetComponent<doorOpeningPlate>().playerInRange == true)
        {

            doorToOpen.SetActive(false);

            illusionToClose.SetActive(false);

            if (illusionToOpen != null)
            {
                illusionToOpen.SetActive(true);
            }


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "PlayerHitbox")
        {
            playerInRange = false;
        }

    }
}
