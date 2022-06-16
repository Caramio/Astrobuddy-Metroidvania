using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbitStarScript : MonoBehaviour
{

    public GameObject orbitPoint;

    public float orbitSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        transform.RotateAround(orbitPoint.transform.position, Vector3.forward, orbitSpeed * Time.deltaTime);



    }



}
