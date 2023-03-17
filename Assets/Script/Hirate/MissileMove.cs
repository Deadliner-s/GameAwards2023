using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : MonoBehaviour
{
    // •Ï”éŒ¾
    private Vector3 initPos = new Vector3(0, 0, 0);
    public Vector3 moveAdd;

    // Start is called before the first frame update
    void Start()
    {
        // = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos += moveAdd;
        if (pos.z <= -10)
        {
            pos.z = 0;
        }
        transform.position = pos;
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
