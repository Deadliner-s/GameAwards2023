using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZakoMissile : MonoBehaviour
{
    public float Speed = 0.01f;        //ミサイルの速度
    Vector3 Move;               //移動方向
    public float DestroyTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        Move = new Vector3(1.0f, 0.0f, 0.0f);
        Destroy(gameObject, DestroyTime);
    }

    // Update is called once per frame
    void Update()
    {

        Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), Move);
        transform.rotation = rot;
        transform.position += Move * Speed;

    }
}
