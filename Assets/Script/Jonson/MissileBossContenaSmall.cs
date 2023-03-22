using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileBossContenaSmall : MonoBehaviour
{
    public float Speed = 0.01f;        //ミサイルの速度
    public float MaxSpeed = 2.0f;      //速度制限
    public float Accel = 0.005f;       //加速度
    public float MissRange = 5.0f;     //プレイヤーに外れるの距離
    float off;
    bool Miss;

    System.Random rand = new System.Random();
    float randXZ;
    float randY;
    Vector3 PlusRandomMove;

    Vector3 ToPos;              //発射先
    Vector3 Move;               //移動方向
    Vector3 LateMove;           //滑らか動きをするためのmove変数
    // Start is called before the first frame update
    void Start()
    {
        ToPos = GameObject.Find("Player").transform.position; //Player
        PlusRandomMove = ToPos - transform.position;
        PlusRandomMove = PlusRandomMove.normalized;

        randXZ = rand.Next(32);
        randY = rand.Next(32);

        randXZ -= 16;
        randY -= 16;

        randXZ *= 0.1f;
        randY *= 0.1f;

        randXZ += PlusRandomMove.x;
        randY += PlusRandomMove.y;

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
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(ToPos.x, 0, ToPos.z));
        if (distance >= MissRange && !Miss)
        {
            Move = ToPos - transform.position;
            Move.x += randXZ;
            Move.y += randY;
            Move = Move.normalized;
            LateMove = (Move - LateMove) * off + (LateMove);
        }
        else
        {
            Miss = true;
            Destroy(gameObject, 3.0f);
        }
        Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
        transform.rotation = rot;
        transform.position += LateMove * Speed;
    }
}
