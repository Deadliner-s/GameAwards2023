using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public float speed = 0.01f;
    public float rad = 0.85f;
    public bool IsLeft;
    private float angle;
    private float angle2;
    Vector3 move;
    Vector3 OriPos;

    // Start is called before the first frame update
    void Start()
    {
        OriPos = transform.position;
        angle2 = 180;
    }

    // Update is called once per frame
    void Update()
    {

        angle += speed * Time.deltaTime;
        angle2 += speed * Time.deltaTime;
        if (IsLeft)
        {
            move = new Vector3(Mathf.Cos(angle2), 0, Mathf.Sin(angle2)) * rad;
        }
        else
        {
            move = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * rad;
        }


        transform.position = OriPos + move;
    }
}
