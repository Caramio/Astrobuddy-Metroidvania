using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class routeVisibilityGizmo : MonoBehaviour
{

   

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }


    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(this.transform.position, 1f);

    }
}
