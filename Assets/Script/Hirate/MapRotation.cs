using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRotation : MonoBehaviour
{
    public float rotate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // �����Ă���悤�Ɍ�����
        transform.Rotate(new Vector3(0, rotate, 0));
    }

    void Update()
    {

    }
}
