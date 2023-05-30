using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileBossCluster : MonoBehaviour
{
    public float Speed = 0.005f;        //ミサイルの速度
    public float MaxSpeed = 2.0f;      //速度制限
    public float Accel = 0.001f;       //加速度
    public float ClusterRange = 3.5f;     //プレイヤーに外れるの距離
    public int ClusterNumber = 6;

    float off;
    bool Cluster;

    GameObject newObj;
    public GameObject otherObject;        // 生成するプレハブオブジェクト
    private Camera mainCamera;            // メインカメラ
    private Vector3 targetWorldPosition;  // 目標ワールド座標

    bool BossFlg;
    GameObject Player;
    Vector3 ToPos;              //発射先
    Vector3 Move;               //移動方向
    Vector3 LateMove;           //滑らか動きをするためのmove変数

    // Start is called before the first frame update
    void Start()
    {
        BossFlg = GameObject.Find("BossManager").GetComponent<MainBossHp>();
        if(BossFlg)
        {
            if (Player = GameObject.Find("Player"))
            {
                ToPos = Player.transform.position; //Player
                mainCamera = Camera.main;                             // メインカメラを取得する

                Cluster = false;
                off = 1.0f;
            }
            else
            {
                Destroy(this, 0.0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player && BossFlg)
        {
            ToPos = Player.transform.position;   //プレイヤーの位置
            Speed += Accel;                                         //加速度
            if (Speed >= MaxSpeed)                                  //速度制限
                Speed = MaxSpeed;
            float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(ToPos.x, 0, ToPos.z));
            if (distance >= ClusterRange && !Cluster)               //まだ分裂してない
            {
                Move = ToPos - transform.position;
                Move = Move.normalized;
                LateMove.x = (Move.x - LateMove.x) * off + (LateMove.x);
                LateMove.z = (Move.z - LateMove.z) * off + (LateMove.z);
            }
            else
            {
                Cluster = true;
                for (int i = 0; i < ClusterNumber; i++)
                {
                    newObj = Instantiate(otherObject, transform.position, Quaternion.Euler(0.0f,0.0f,270.0f));
                    // MissileObjをタグ検索
                    GameObject missileObj = GameObject.FindGameObjectWithTag("MissileObj");
                    // ミサイルオブジェクトの子にする
                    newObj.transform.parent = missileObj.transform;
                }
                Destroy(gameObject, 0);
            }

            //world座標をcamera座標に変換
            targetWorldPosition = transform.position;
            targetWorldPosition = mainCamera.WorldToScreenPoint(targetWorldPosition);

            Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
            transform.rotation = rot;
            if (targetWorldPosition.x <= -100.0f)
            {
                transform.position = new Vector3(transform.position.x, ToPos.y, transform.position.z);
            }
            transform.position += LateMove * Speed * Time.timeScale;
        }
        else
        {
            Destroy(this, 0.0f);
        }
    }
}   