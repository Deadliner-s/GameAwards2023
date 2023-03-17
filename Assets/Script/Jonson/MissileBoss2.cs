using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileBoss2 : MonoBehaviour
{
    public float Speed;         //ミサイルの速度
    public float MaxSpeed = 2.0f;      
    public float Accel;         //加速度
    public float MissRange;     //プレイヤーに外れるの距離
    float range = 10.0f;
    float off;
    bool Locked;                //ミサイルがロックオンしているか

    System.Random rand = new System.Random();
    float randomY;

    Vector3 FromPos;            //発射元
    Vector3 ToPos;              //発射先
    Vector3 Move;               //移動方向
    Vector3 LateMove;           //滑らか動きをするためのmove変数

    // Start is called before the first frame update
    void Start()
    {
        //EnemyBossという名前のものが存在する
        FromPos = GameObject.Find("EnemyBoss").transform.position;
        //Playerという名前のものが存在する
        ToPos = GameObject.Find("Player").transform.position;
        //ミサイルの初期位置を設定
        transform.position = FromPos;

        randomY = (rand.Next(10) - 5);
        off = 0.2f;
        Locked = false;
    }

    // Update is called once per frame
    void Update()
    {
        ToPos = GameObject.Find("Player").transform.position;
        if (!Locked)
        {
            if (transform.position.x >= FromPos.x - range)
                Move = new Vector3(-range,randomY, 0.0f);
            else
                Locked = true;
            Move = Move.normalized;
            LateMove = Move;
        }
        else 
        {
            Speed += Accel;
            if (Speed >= MaxSpeed)
                Speed = MaxSpeed;
            if (transform.position.x <= ToPos.x - MissRange)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, ToPos.z);
                Move = ToPos - transform.position;
                Move = Move.normalized;
                LateMove = (Move - LateMove) * off + (LateMove);
            }
        }

        Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
        transform.rotation = rot;
        transform.position += LateMove * Speed;
    }
}