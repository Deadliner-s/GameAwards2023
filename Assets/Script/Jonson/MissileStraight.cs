using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileStraight : MonoBehaviour
{
    public float Speed;         //ミサイルの速度
    public float MaxSpeed = 2.0f;
    public float Accel;         //加速度
    public float MissRange = 2.0f;     //プレイヤーに外れるの距離
    float off;
    bool Miss;
    public string FromName;

    //Vector3 FromPos;            //発射元
    Vector3 ToPos;              //発射先
    Vector3 Move;               //移動方向
    Vector3 LateMove;           //滑らか動きをするためのmove変数

    // Start is called before the first frame update
    void Start()
    {
        //EnemyBossという名前のものが存在する
        //FromPos = GameObject.Find(FromName).transform.position;
        //Playerという名前のものが存在する
        ToPos = GameObject.Find("Player").transform.position;
        //ミサイルの初期位置を設定
        //transform.position = FromPos;
        Miss = false;
        off = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        ToPos = GameObject.Find("Player").transform.position;
        Speed += Accel;
        if (Speed >= MaxSpeed)
            Speed = MaxSpeed;
        float distance = Vector3.Distance(transform.position, ToPos);
        if (distance >= MissRange && !Miss)
        {
            Move = ToPos - transform.position;
            Move = Move.normalized;
            LateMove = (Move - LateMove) * off + (LateMove);
        }
        else
        {
            Miss = true;
        }

        Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
        transform.rotation = rot;
        transform.position += LateMove * Speed;
    }
}