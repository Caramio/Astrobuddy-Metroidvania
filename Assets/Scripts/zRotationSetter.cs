using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zRotationSetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
