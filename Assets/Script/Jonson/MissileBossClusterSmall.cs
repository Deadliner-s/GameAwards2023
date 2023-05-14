using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MissileBossClusterSmall : MonoBehaviour
{
    float Speed = 0.01f;        //ミサイルの速度
    float MinSpeed = 0.004f;    //最小速度
    float MaxSpeed = 0.02f;     //最大速度
    float Accel = 0.0001f;      //加(減)速度

    public float MissRange = 2.0f;     //プレイヤーに外れるの距離
    float off;
    bool Miss;

    System.Random rand = new System.Random();
    float randSpeed;
    float randY;
    Vector3 PlusY;
    //OtherScript Particle = GetComponent<StartParticle>();

    GameObject Player;
    Vector3 ToPos;              //発射先
    Vector3 CheckPos;
    Vector3 Move;               //移動方向
    Vector3 LateMove;           //滑らか動きをするためのmove変数

    // Start is called before the first frame update
    void Start()
    {
        if (Player = GameObject.Find("Player"))//プレイヤーは生きている（存在する）
        {
            ToPos = Player.transform.position; //Player
            Miss = false;
            off = 0.2f;
            randSpeed = rand.Next(20);
            randSpeed *= 0.0001f;
            randY = rand.Next(30);
            randY -= 15;
            randY *= 0.01f;
            randY += PlusY.y;
            GetComponent<StartParticle>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player)//プレイヤーは生きている（存在する）
        {
            if (!Miss && Speed >= MinSpeed+randSpeed)
            {
                Speed -= Accel;
                ToPos = Player.transform.position;   //プレイヤーの位置
                Move = new Vector3(1.0f,randY,0.0f);
            }
            else
            {
                if(!GetComponent<StartParticle>().enabled)
                    GetComponent<StartParticle>().enabled= true;
                Miss = true;
                Speed = MaxSpeed;
                Move = new Vector3(1.0f, randY * 0.3f, 0.0f);
                Destroy(gameObject, 3.0f);
            }

            LateMove = (Move - LateMove) * off + (LateMove);
            Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
            transform.rotation = rot;
            transform.position += LateMove * Speed;
        }
        else
        {
            Destroy(this, 0.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(gameObject);
        }
    }
}