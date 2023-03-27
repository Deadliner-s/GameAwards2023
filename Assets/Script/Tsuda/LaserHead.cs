using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHead : MonoBehaviour
{
    public float speed = 5f;  // 移動速度
    public GameObject otherObject;  // 生成するプレハブオブジェクト
    public float lifetime = 5f;  // オブジェクトの寿命（秒）
    public float Split = 1;

    private float splitX;
    private float splitY;
    private Vector3 targetScreenPosition;  // 目標スクリーン座標
    private Vector3 targetWorldPosition;  // 目標ワールド座標
    private Camera mainCamera;  // メインカメラ
    private float timer;  // タイマー
    GameObject newObj;

    void Start()
    {
        mainCamera = Camera.main;  // メインカメラを取得する

        switch(Split)
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
        targetScreenPosition.z = 1.3f;
        targetWorldPosition = mainCamera.ScreenToWorldPoint(targetScreenPosition);  // 目標スクリーン座標をワールド座標に変換する

        transform.LookAt(targetWorldPosition);  // 目標座標の方向を向く

        newObj = Instantiate(otherObject, targetWorldPosition, transform.rotation);  // 警告UIの生成

        timer = lifetime;  // タイマーを設定する
    }

    void Update()
    {
        timer -= Time.deltaTime;  // タイマーを減算する

        if (timer >= lifetime - 12.0f)
        {
            targetWorldPosition = mainCamera.ScreenToWorldPoint(targetScreenPosition);  // 目標スクリーン座標をワールド座標に変換する
            transform.LookAt(targetWorldPosition);

//            newObj.transform.position = targetWorldPosition;
 //           newObj.transform.LookAt(targetWorldPosition);
        }


        if (timer <= lifetime - 12.0f)
        {
            transform.position += transform.forward * speed * Time.deltaTime;  // 目標座標の方向に移動する
        }
     
        if (timer <= 0)
        {
            Destroy(gameObject);  // オブジェクトを削除する
        }
    }
}

