using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileBoss : MonoBehaviour
{
    public float Speed;         //ミサイルの速度
    public float MaxSpeed = 2.0f;
    public float Accel;         //加速度
    public float MissRange;     //プレイヤーに外れるの距離
    public float Height;        //ミサイルの高さ
    float off;
    bool Locked;                //ミサイルがロックオンしているか
    bool Miss;

    System.Random rand = new System.Random();
    float randomX;
    float randomZ;

    Vector3 FromPos;            //発射元
    Vector3 ToPos;              //発射先
    GameObject Player;
    Vector3 Move;               //移動方向
    Vector3 LateMove;           //滑らか動きをするためのmove変数

    // Start is called before the first frame update
    void Start()
    {
        if (Player = GameObject.Find("Player"))
        {
            //EnemyBossという名前のものが存在する
            FromPos = transform.position;
            //Playerという名前のものが存在する
            ToPos = Player.transform.position;
            //ミサイルの初期位置を設定
            transform.position = FromPos;

            randomX = (rand.Next(10) - 5) * 0.1f;
            randomZ = (rand.Next(10) - 5) * 0.1f;
            off = 0.2f;
            Locked = false;
            Miss = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Player)
        {
            ToPos = Player.transform.position;
            if (transform.position.y <= FromPos.y + Height && !Locked)
            {
                Move = new Vector3(randomX, 1.0f, randomZ);
                LateMove = Move;
            }
            else
            {
                Locked = true;
                Speed += Accel;
                if (Speed >= MaxSpeed)
                {
                    Speed = MaxSpeed;
                }
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