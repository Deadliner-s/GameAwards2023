using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHead : MonoBehaviour
{
    public float speed = 5f;  // 移動速度
    public GameObject otherObject;  // 生成するプレハブオブジェクト
    public float lifetime = 5f;  // オブジェクトの寿命（秒）

    private Vector3 targetScreenPosition;  // 目標スクリーン座標
    private Vector3 targetWorldPosition;  // 目標ワールド座標
    private Camera mainCamera;  // メインカメラ
    private float timer;  // タイマー
    GameObject newObj;

    void Start()
    {
        mainCamera = Camera.main;  // メインカメラを取得する

        targetScreenPosition.x = Random.Range(200.0f, 1720.0f);  // 0～1920のランダムな数値
        targetScreenPosition.y = Random.Range(150.0f, 830.0f);  // 0～1080のランダムな数値
        targetScreenPosition.z = 2.0f;
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

