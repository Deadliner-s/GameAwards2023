using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        Vector3 p = Camera.main.transform.position;
        p.y = transform.position.y;
        transform.LookAt(p);
    }
}
