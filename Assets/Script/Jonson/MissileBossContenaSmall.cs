using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileBossContenaSmall : MonoBehaviour
{
    public float Speed = 0.02f;        //ミサイルの速度
    public float MaxSpeed = 0.1f;      //速度制限
    public float Accel = 0.001f;       //加速度
    public float Spread = 0.02f;       //拡散
    //float off;
    bool Miss;

    System.Random rand = new System.Random();
    float randX;
    float randY;

    GameObject Player;
    Vector3 ToPos;              //発射先
    Vector3 Move;               //移動方向
    Vector3 LateMove;           //滑らか動きをするためのmove変数
    // Start is called before the first frame update
    void Start()
    {
        if (Player = GameObject.Find("Player"))
        {
            ToPos = Player.transform.position; //Player
            randX = rand.Next(16) - 8;
            randY = rand.Next(16) - 8;
            Miss = false;
            //off = 0.9f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player)
        {
            Speed += Accel;                                         //加速度
            if (Speed >= MaxSpeed)                                  //速度制限
                Speed = MaxSpeed;
            //if (distance >= MissRange && !Miss)
            if (!Miss)
            {
                Move = ToPos - transform.position;
                Move = Move.normalized;
                Move.x += randX * Spread;
                Move.y += randY * Spread;
                //LateMove = (Move - LateMove) * off + (LateMove);
                LateMove = Move;
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
        else
        {
            Destroy(this, 0.0f);
        }

    }
}
