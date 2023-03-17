using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Vector3 angle;
    // Start is called before the first frame update
    void Start()
    {
        angle = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        angle = Camera.main.transform.position;
        transform.LookAt(angle);
        transform.Rotate(0, 180, 0);
        //transform.rotation = Camera.main.transform.rotation;
    }
}
