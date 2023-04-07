using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileBossContenaSmall : MonoBehaviour
{
    public float Speed = 0.02f;        //ミサイルの速度
    public float MaxSpeed = 0.1f;      //速度制限
    public float Accel = 0.001f;       //加速度
    float off;
    bool Miss;

    System.Random rand = new System.Random();
    float randX;
    float randY;

    Vector3 ToPos;              //発射先
    Vector3 Move;               //移動方向
    Vector3 LateMove;           //滑らか動きをするためのmove変数
    // Start is called before the first frame update
    void Start()
    {
        ToPos = GameObject.Find("Player").transform.position; //Player
        randX = transform.rotation.x;
        randY = transform.rotation.y;
        Miss = false;
        off = 0.9f;
    }

    // Update is called once per frame
    void Update()
    {
        ToPos = GameObject.Find("Player").transform.position;   //プレイヤーの位置
        Speed += Accel;                                         //加速度
        if (Speed >= MaxSpeed)                                  //速度制限
            Speed = MaxSpeed;
        //if (distance >= MissRange && !Miss)
        if (!Miss)
        {
            Move = ToPos - transform.position;
            Move = Move.normalized;
            Move.x += randX * 0.05f;
            Move.y += randY * 0.05f;
            LateMove = (Move - LateMove) * off + (LateMove);
            Miss = true;
        }
        else
        {
            Destroy(gameObject, 3.0f);
        }
        Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
        transform.rotation = rot;
        transform.position += LateMove * Speed;
    }
}
