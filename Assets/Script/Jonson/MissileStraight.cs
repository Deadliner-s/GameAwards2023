using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MissileStraight : MonoBehaviour
{
    public float Speed = 0.0f;        //ミサイルの速度
    public float MaxSpeed = 0.003f;      //速度制限
    //public float Accel = 0.001f;       //加速度
    public float MissRange = 2.0f;     //プレイヤーに外れるの距離
    public float UIFillSpeed = 0.005f;  //UIの速さ
    private GameObject canvas;         // キャンバス
    float off;
    bool Miss;
    GameObject newObj;
    GameObject OutsideObj;
    private Camera mainCamera;            // メインカメラ
    public GameObject otherObject;        // 生成するプレハブオブジェクト
    public GameObject outsideObject;
    private Vector3 targetScreenPosition; // 目標スクリーン座標
    private Vector3 targetWorldPosition;  // 目標ワールド座標

    int time;

    GameObject Player; 
    Vector3 ToPos;              //発射先
    Vector3 Move;               //移動方向
    Vector3 LateMove;           //滑らか動きをするためのmove変数

    // Start is called before the first frame update
    void Start()
    {
        if (Player = GameObject.Find("Player"))//プレイヤーは生きている（存在する）
        {
            ToPos = Player.transform.position; //Player
            mainCamera = Camera.main;                             // メインカメラを取得する
            canvas = GameObject.Find("Canvas");                  // キャンバスを指定

            //UI初期位置
            if (transform.position.x < ToPos.x)
            {
                targetScreenPosition.x = 1820 / -2;
            }
            else
            {
                targetScreenPosition.x = 0;
            }
            if (transform.position.y < ToPos.y)
            {
                targetScreenPosition.y = 980 / -2;
            }
            else if (transform.position.y > ToPos.y)
            {
                targetScreenPosition.y = 980 / 2;
            }
            else
            {
                targetScreenPosition.y = 0;
            }
            targetScreenPosition.z = 2.0f;

            //UI生成
            newObj = Instantiate(otherObject, targetScreenPosition, transform.rotation) as GameObject;  // 警告UIの生成                                                           
            newObj.transform.SetParent(canvas.transform, false);                                        // Canvasの子オブジェクトとして生成
            OutsideObj = Instantiate(outsideObject, targetScreenPosition, transform.rotation) as GameObject;  // 警告UIの生成                                                           
            OutsideObj.transform.SetParent(canvas.transform, false);                                        // Canvasの子オブジェクトとして生成
            newObj.GetComponent<Image>().fillAmount = 0;
            time = 0;
            Miss = false;
            off = 0.2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player)//プレイヤーは生きている（存在する）
        {
            time++;
            //world座標をcamera座標に変換
            targetWorldPosition = transform.position;
            targetWorldPosition = mainCamera.WorldToScreenPoint(targetWorldPosition);
            Vector3 NewPosFix = targetWorldPosition;

            if (time >= 120)
            {
                Miss = true;            //画面内に入った
                Speed = MaxSpeed;       //速度をMAX
                Destroy(newObj);        //UIを消す
                Destroy(OutsideObj);
            }
            else if (!Miss)              //画面内にまだ入ってない、追尾
            {
                ToPos =Player.transform.position;   //プレイヤーの位置
                Move = ToPos - transform.position;
                Move = Move.normalized;
                LateMove = (Move - LateMove) * off + (LateMove);
            }

            //UIを画面外にいかないように
            if (NewPosFix.y >= 1030)
            {
                NewPosFix.y = 1030;
            }
            if (NewPosFix.y <= 50)
            {
                NewPosFix.y = 50;
            }
            if (NewPosFix.x >= 1870)
            {
                NewPosFix.x = 1870;
            }
            if (NewPosFix.x <= 50)
            {
                NewPosFix.x = 50;
            }

            if (newObj)
            {
                newObj.transform.position = NewPosFix;  //UIの位置を更新
                OutsideObj.transform.position = NewPosFix;  //UIの位置を更新
                newObj.GetComponent<Image>().fillAmount +=　UIFillSpeed;
                if (newObj.GetComponent<Image>().fillAmount >= 1.0f)
                {
                    newObj.GetComponent<Image>().fillAmount = 1.0f;
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