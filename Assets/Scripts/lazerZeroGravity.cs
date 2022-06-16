using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazerZeroGravity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        
        StartCoroutine(miniDelay());
    }

    private void OnDisable()
    {
        canSetConstraints = false;
    }

    //unity bad programming, need to add mini delay 

    private bool canSetConstraints;
    private IEnumerator miniDelay()
    {


        yield return new WaitForSeconds(0.1f);

        canSetConstraints = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox" && canSetConstraints == true)
        {

            Debug.Log("Froze the player " +this.gameObject.name);

            collision.GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            collision.GetComponentInParent<playerMovement>().canDash = false;



        }
    }
}
