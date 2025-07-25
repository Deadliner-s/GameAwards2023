using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileBossHoming : MonoBehaviour
{
    private float UpSpeed = 1.02f;
    public float Speed;         //ミサイルの速度
    public float MaxSpeed = 0.1f;
    public float Accel;         //加速度
    public float MissRange;     //プレイヤーに外れるの距離
    public float Height;        //ミサイルの高さ
    float HeightHalf;
    float off;
    bool Locked;                //ミサイルがロックオンしているか
    bool Miss;
    float randomX;
    float mult;
    GameObject BossFlg;
    System.Random rand = new System.Random();
    Vector3 FromPos;            //発射元
    Vector3 ToPos;              //発射先
    GameObject Player;
    Vector3 Move;               //移動方向
    Vector3 LateMove;           //滑らか動きをするためのmove変数

    // Start is called before the first frame update
    void Start()
    {
        BossFlg = GameObject.Find("BossManager");
        if (!BossFlg.GetComponent<MainBossHp>().BreakFlag)
        {
            if (Player = GameObject.Find("Player"))
            {
                //EnemyBossという名前のものが存在する
                FromPos = transform.position;
                //Playerという名前のものが存在する
                ToPos = Player.transform.position;
                //ミサイルの初期位置を設定
                transform.position = FromPos;

                randomX = (rand.Next(2) + 1) * 0.1f;
                HeightHalf = Height / 3.0f * 2.0f;
                if (transform.position.x < 0.0f)
                {
                    randomX *= -1.0f;
                }
                off = 0.03f;
                Locked = false;
                Miss = false;
                mult = 5.0f;
            }
            else
            {
                Destroy(gameObject, 0.0f);
            }
        }           
    }
    // Update is called once per frame
    void Update()
    {
        if(!BossFlg.GetComponent<MainBossHp>().BreakFlag)
        {
            if (Player)
            {
                ToPos = Player.transform.position;
                if (transform.position.y <= FromPos.y + HeightHalf && !Locked)
                {
                    Move = new Vector3(randomX, 1.0f, -1.0f);
                    Move = Move.normalized;
                    LateMove = (Move - LateMove) * off + (LateMove);
                    LateMove *= UpSpeed;
                }
                else if (transform.position.y <= FromPos.y + Height && !Locked)
                {
                    mult += 0.2f;
                    if (mult >= 10.0f)
                        mult = 10.0f;
                    Move = new Vector3(randomX * mult, 1.0f, -1.0f);
                    Move = Move.normalized;
                    LateMove = (Move - LateMove) * off + (LateMove);
                    LateMove *= UpSpeed;
                }
                else
                {
                    off = 0.12f;
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
                transform.position += LateMove * Speed * Time.timeScale;

                if (transform.position.z <= -12.0f)  //画面外に(カメラの後ろ)出たとき
                {
                    Destroy(this, 0.0f);
                }
            }
            else
            {
                Destroy(this, 0.0f);
            }
        }
        else
        {
            Destroy(gameObject, 0.0f);
        }        
    }
}