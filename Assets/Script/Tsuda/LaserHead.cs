using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHead : MonoBehaviour
{
    public static LaserHead instance;

    public float speed = 5f;  // 移動速度
//    public GameObject otherObject;  // 生成するプレハブオブジェクト    
    public int Split;
    public float LaserTime = 4.0f;
    public float LaserSpeed = 3.0f;

    private int LaserMove;
    private float lifetime = 10.0f;  // オブジェクトの寿命（秒）
    private float splitX;
    private float splitY;
    public Vector3 targetScreenPosition;  // 目標スクリーン座標
    public Vector3 targetWorldPosition;  // 目標ワールド座標
    private Camera mainCamera;  // メインカメラ
    private float timer;  // タイマー    
    GameObject newObj;    

    void Start()
    {
        LaserMove = Random.Range(0, 2);
    }

    void Update()
    {        
        timer += Time.deltaTime;  // タイマーを減算する        

        if (timer >= 2.0f && timer <= 2.0f + LaserTime)
        {                                    
            targetWorldPosition = mainCamera.ScreenToWorldPoint(targetScreenPosition);  // 目標スクリーン座標をワールド座標に変換する
            transform.LookAt(targetWorldPosition);
        }

        if (timer >= 3.0f && timer <= 2.0f + LaserTime)
        {            
            switch(Split)
            {            
                case 1: 
                    switch(LaserMove)
                    {
                        case 0: targetScreenPosition.x += LaserSpeed; break;
                        case 1: targetScreenPosition.x += LaserSpeed; break;
                        case 2: targetScreenPosition.y -= LaserSpeed; break;
                    }
                    break;

                case 2:
                    switch (LaserMove)
                    {
                        case 0: targetScreenPosition.x += LaserSpeed; break;
                        case 1: targetScreenPosition.x -= LaserSpeed; break;
                        case 2: targetScreenPosition.y -= LaserSpeed; break;
                    }
                    break;

                case 3:
                    switch (LaserMove)
                    {
                        case 0: targetScreenPosition.x -= LaserSpeed; break;
                        case 1: targetScreenPosition.x -= LaserSpeed; break;
                        case 2: targetScreenPosition.y -= LaserSpeed; break;
                    }
                    break;

                case 4:
                    switch (LaserMove)
                    {
                        case 0: targetScreenPosition.x += LaserSpeed; break;
                        case 1: targetScreenPosition.x += LaserSpeed; break;
                        case 2: targetScreenPosition.y += LaserSpeed; break;
                    }
                    break;

                case 5:
                    switch (LaserMove)
                    {
                        case 0: targetScreenPosition.x += LaserSpeed; break;
                        case 1: targetScreenPosition.x -= LaserSpeed; break;
                        case 2: targetScreenPosition.y += LaserSpeed; break;
                    }
                    break;

                case 6:
                    switch (LaserMove)
                    {
                        case 0: targetScreenPosition.x -= LaserSpeed; break;
                        case 1: targetScreenPosition.x -= LaserSpeed; break;
                        case 2: targetScreenPosition.y += LaserSpeed; break;
                    }
                    break;
            }                           
        }

        if (timer >= 2.0f + LaserTime)
        {
            transform.position += transform.forward * speed * Time.deltaTime;  // 目標座標の方向に移動する
        }

        if (timer >= lifetime)
        {
            Destroy(gameObject);  // オブジェクトを削除する
        }
    }

    public void SetSplit(int num)
    {
        mainCamera = Camera.main;  // メインカメラを取得する

        Split = num;

        switch (Split)
        {
            case 1: splitX = 1; splitY = 3; break;
            case 2: splitX = 3; splitY = 3; break;
            case 3: splitX = 5; splitY = 3; break;
            case 4: splitX = 1; splitY = 1; break;
            case 5: splitX = 3; splitY = 1; break;
            case 6: splitX = 5; splitY = 1; break;
        }

        targetScreenPosition.x = 320 * splitX;
        targetScreenPosition.y = 270 * splitY;
        targetScreenPosition.z = 1.0f;
        targetWorldPosition = mainCamera.ScreenToWorldPoint(targetScreenPosition);  // 目標スクリーン座標をワールド座標に変換する        

        transform.LookAt(targetWorldPosition);  // 目標座標の方向を向く

//        newObj = Instantiate(otherObject, targetWorldPosition, transform.rotation);  // 警告UIの生成

        timer = 0.0f;  // タイマーを設定する
    }
}

