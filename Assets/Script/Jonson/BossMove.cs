using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public float speed = 0.01f;
    public float rad = 0.85f;
    private float angle;
    Vector3 move;
    Vector3 OriPos;

    // Start is called before the first frame update
    void Start()
    {
        OriPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        angle += speed * Time.deltaTime;
        move = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * rad;


        transform.position = OriPos + move;
    }
}
