using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MissileStraight : MonoBehaviour
{
    public float Speed = 0.0f;        //ミサイルの速度
    public float MaxSpeed = 0.003f;      //速度制限
    public float MissRange = 2.0f;     //プレイヤーに外れるの距離
    float UIFillSpeed = 0.01f;  //UIの速さ
    private GameObject canvas;         // キャンバス
    float off;
    bool Miss;
    public bool isDestroyed = false;
    GameObject newObj;
    GameObject OutsideObj;
    GameObject LightObj;
    private Camera mainCamera;            // メインカメラ
    public GameObject otherObject;        // 生成するプレハブオブジェクト
    public GameObject outsideObject;
    public GameObject LightObject;
    private Vector3 targetScreenPosition; // 目標スクリーン座標
    private Vector3 targetWorldPosition;  // 目標ワールド座標
    Vector3 NewPosFix;
    GameObject BossFlg;
    float time = 0;
    bool instant = false;

    GameObject Player; 
    Vector3 ToPos;              //発射先
    Vector3 Move;               //移動方向
    Vector3 LateMove;           //滑らか動きをするためのmove変数

    // Start is called before the first frame update
    void Start()
    {
        BossFlg = GameObject.Find("BossManager");
        if (!BossFlg.GetComponent<MainBossHp>().BreakFlag)
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
                if (!instant)
                {
                    //world座標をcamera座標に変換
                    targetWorldPosition = transform.position;
                    targetWorldPosition = mainCamera.WorldToScreenPoint(targetWorldPosition);
                    NewPosFix = targetWorldPosition;
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
                    if (GameObject.Find("Canvas"))
                    {
                        LightObj = Instantiate(LightObject, targetScreenPosition, transform.rotation) as GameObject;
                        LightObj.transform.SetParent(canvas.transform, false);
                        LightObj.GetComponent<Image>().fillAmount = 0;
                        newObj = Instantiate(otherObject, targetScreenPosition, transform.rotation) as GameObject;  // 警告UIの生成                                                           
                        newObj.transform.SetParent(canvas.transform, false);                                        // Canvasの子オブジェクトとして生成
                        newObj.GetComponent<Image>().fillAmount = 0;
                        OutsideObj = Instantiate(outsideObject, targetScreenPosition, transform.rotation) as GameObject;  // 警告UIの生成                                                           
                        OutsideObj.transform.SetParent(canvas.transform, false);                                        // Canvasの子オブジェクトとして生成

                        LightObj.transform.position = NewPosFix;    //UIの位置を更新
                        newObj.transform.position = NewPosFix;      //UIの位置を更新
                        OutsideObj.transform.position = NewPosFix;  //UIの位置を更新
                    }
                }
                time = 0.0f;
                Miss = false;
                off = 0.2f;
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
        if (Player&&!BossFlg.GetComponent<MainBossHp>().BreakFlag)//プレイヤーは生きている（存在する）
        {
            time += Time.timeScale;
            if (time >= 120.0f)
                instant = true;
            if(time >= 135.0f)
            {
                if (newObj)
                    Destroy(newObj);        //UIを消す
                if (outsideObject)
                    Destroy(OutsideObj);
                if (LightObj)
                    Destroy(LightObj);
            }
            if (!Miss)              //画面内にまだ入ってない、追尾
            {
                ToPos =Player.transform.position;   //プレイヤーの位置
                Move = ToPos - transform.position;
                Move = Move.normalized;
                LateMove = (Move - LateMove) * off + (LateMove);
            }
            if (instant)
            {
                Miss = true;            //画面内に入った
                Speed = MaxSpeed;       //速度をMAX
            }

            targetWorldPosition = transform.position;
            targetWorldPosition = mainCamera.WorldToScreenPoint(targetWorldPosition);

            if (targetWorldPosition.y > 1500 || targetWorldPosition.y < -200 || targetWorldPosition.x > 2100) 
            {
                isDestroyed = true;
            }

            if (isDestroyed)
            {
                Destroy(newObj);        //UIを消す
                Destroy(OutsideObj);
                Destroy(LightObj);
                Destroy(gameObject);
            }

            if (newObj)
            {
                //newObj.transform.position = NewPosFix;  //UIの位置を更新
                //OutsideObj.transform.position = NewPosFix;  //UIの位置を更新
                newObj.GetComponent<Image>().fillAmount +=　UIFillSpeed * Time.timeScale;
                if (newObj.GetComponent<Image>().fillAmount >= 0.9f)
                {
                    LightObj.GetComponent<Image>().fillAmount = 1.0f;
                    Destroy(newObj);
                }

            }
            Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
            transform.rotation = rot;
            transform.position += LateMove * Speed * Time.timeScale;
        }
        else
        {
            if(newObj)
                Destroy(newObj);        //UIを消す
            if(outsideObject)
                Destroy(OutsideObj);
            if (LightObj)
                Destroy(LightObj);
                Destroy(gameObject);
        }
    }
}