using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_collision : MonoBehaviour
{
    private CapsuleCollider Col;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Col = GetComponent<CapsuleCollider>();
        Col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 2.0f)
        {
            Col.enabled = true;
        }        
    }
}
