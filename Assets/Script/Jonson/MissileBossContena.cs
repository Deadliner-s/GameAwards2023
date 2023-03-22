using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileBossContena : MonoBehaviour
{
    public float Speed;         //ミサイルの速度
    public float MaxSpeed = 2.0f;
    public float Accel;         //加速度
    public float ContenaRange = 7.0f;     //コンテナする距離
    public float Height;        //ミサイルの高さ
    public int ContenaNumber = 15;//分裂の数
    float off;
    bool Locked;                //ミサイルがロックオンしているか
    GameObject newObj;
    public GameObject otherObject;        // 生成するプレハブオブジェクト

    Vector3 FromPos;            //発射元
    Vector3 ToPos;              //発射先
    Vector3 Move;               //移動方向
    Vector3 LateMove;           //滑らか動きをするためのmove変数

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーの位置を取得
        ToPos = GameObject.Find("Player").transform.position;
        //ボスの位置を取得
        FromPos = GameObject.Find("EnemyBoss").transform.position;

        off = 0.2f;
        Locked = false;
    }

    // Update is called once per frame
    void Update()
    {
        ToPos = GameObject.Find("Player").transform.position;
        if (transform.position.y <= FromPos.y + Height && !Locked)
        {
            Move = new Vector3(0, 1.0f, 0);
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
            if (distance >= ContenaRange)
            {
                Move = ToPos - transform.position;
                Move = Move.normalized;
                LateMove = (Move - LateMove) * off + (LateMove);
            }
            else
            {
                for (int i = 0; i < ContenaNumber; i++)
                {
                    newObj = Instantiate(otherObject, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                }
                Destroy(gameObject, 0);
            }
        }
        Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
        transform.rotation = rot;
        transform.position += LateMove * Speed;
    }
}
