using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileStraight : MonoBehaviour
{
    public float Speed = 0.01f;        //ミサイルの速度
    public float MaxSpeed = 2.0f;      //速度制限
    public float Accel = 0.001f;       //加速度
    public float MissRange = 2.0f;     //プレイヤーに外れるの距離
    private GameObject canvas;         // キャンバス
    float off;
    bool Miss;
    GameObject newObj;
    private Camera mainCamera;            // メインカメラ
    public GameObject otherObject;        // 生成するプレハブオブジェクト
    private Vector3 targetScreenPosition; // 目標スクリーン座標
    private Vector3 targetWorldPosition;  // 目標ワールド座標


    Vector3 ToPos;              //発射先
    Vector3 Move;               //移動方向
    Vector3 LateMove;           //滑らか動きをするためのmove変数

    // Start is called before the first frame update
    void Start()
    {      
        ToPos = GameObject.Find("Player").transform.position; //Player
        mainCamera = Camera.main;                             // メインカメラを取得する
        canvas = GameObject.Find("Canvas");                 　// キャンバスを指定


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
        Destroy(newObj, 3.0f);                                                                      // UIを消す

        newObj.transform.SetParent(canvas.transform, false);                                        // Canvasの子オブジェクトとして生成


        Miss = false;
        off = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        ToPos = GameObject.Find("Player").transform.position;   //プレイヤーの位置
        Speed += Accel;                                         //加速度
        if (Speed >= MaxSpeed)                                  //速度制限
            Speed = MaxSpeed;
        float distance = Vector3.Distance(transform.position, ToPos);
        if (distance >= MissRange && !Miss)                     //まだ外れてない
        {
            Move = ToPos - transform.position;
            Move = Move.normalized;
            LateMove = (Move - LateMove) * off + (LateMove);
        }
        else
        {
            Miss = true;
        }

        //world座標をcamera座標に変換
        targetWorldPosition = transform.position;
        targetWorldPosition = mainCamera.WorldToScreenPoint(targetWorldPosition);
        Vector3 NewPosFix = targetWorldPosition;

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

        if(newObj)
        {
            newObj.transform.position = NewPosFix;  //UIの位置を更新
        }   
        if(Miss)
        {
            Destroy(newObj);        //ミサイルがプレイヤーに外れたらUIを消す
        }
        Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
        transform.rotation = rot;
        transform.position += LateMove * Speed;
    }
}