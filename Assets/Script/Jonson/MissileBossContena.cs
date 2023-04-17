using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileBossContena : MonoBehaviour
{
    public float Speed;                  //ミサイルの速度
    public float ToSpeed = 0.01f;         //ミサイルプレイヤーにいく速度
    public float UpSpeed = 0.02f;         //ミサイル上にいく速度
    public float ContenaRange = 7.0f;    //コンテナする距離
    public float Height;                 //ミサイルの高さ
    public int ContenaNumber = 15;       //分裂の数

    float off;
    bool Locked;                         //ミサイルがロックオンしているか

    GameObject newObj;
    public GameObject otherObject;       //生成するプレハブオブジェクト

    GameObject Player;
    Vector3 FromPos;            //発射元
    Vector3 ToPos;              //発射先
    Vector3 Move;               //移動方向
    Vector3 LateMove;           //滑らか動きをするためのmove変数

    // Start is called before the first frame update
    void Start()
    {
        if (Player = GameObject.Find("Player"))//プレイヤーは生きている（存在する）
        {
            //プレイヤーの位置を取得
            ToPos = Player.transform.position;
            //ボスの位置を取得
            FromPos = transform.position;

            off = 0.05f;
            Locked = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player)//プレイヤーは生きている（存在する）
        {
            ToPos = Player.transform.position;
            if (transform.position.y <= FromPos.y + Height && !Locked)
            {
                Speed = UpSpeed;
                Move = new Vector3(0, 1.0f, 0);
                LateMove = Move;
            }
            else
            {
                Locked = true;
                Speed = ToSpeed;
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
                        float j = (i % 3) - 1;
                        float k = (i / 3) - 1;
                        newObj = Instantiate(otherObject, new Vector3(transform.position.x + j * 0.05f, transform.position.y + k * 0.1f, transform.position.z),Quaternion.identity);
                        if(i == ContenaNumber / 2)
                        {
                            //newObj.GetComponent<MissileBossContenaSmall>().Spread = 0.0f;
                            newObj.GetComponent<NewContena>().First = true;
                        }
                    }
                    Destroy(gameObject, 0);
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
