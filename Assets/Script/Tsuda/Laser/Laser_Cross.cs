using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Cross : MonoBehaviour
{
    public LaserHead LaserBeam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.Euler(0.0f, 20.0f, 0.0f);
        //LaserBeam.GetComponent<LaserHead>().targetWorldPosition.x
    }
}
