using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBullet : MonoBehaviour
{   
    public float turnSpeed = 5f;        // ミサイルの旋回速度
    private Rigidbody rb;               // ミサイルのRigidbodyコンポーネント
    public int cnt;
    
    public float Speed;                 // ミサイルの速度
    public float MaxSpeed;              // ミサイルの最高速度
    public float Accel;                 // 加速度
    private Transform target;     // 追跡する対象  
    public float maxFlightTime = 100f;  // ミサイルの最大飛行時間
    private float flightTime;           // ミサイルの現在の飛行時間
    public int State = 0;              // 状態(0:横移動  1:追跡)


    private void Start()
    {
        // ミサイルのRigidbodyコンポーネントを取得する
        rb = GetComponent<Rigidbody>();

        // 存在時間初期化
        flightTime = 0;

        // 状態設定
        State = 0;

        cnt = 0;

        // 向きをプレイヤーと同じにする
        Transform playertra = GameObject.Find("Player").transform;
        this.transform.LookAt(playertra);
    }
    
    //private void FixedUpdate()
    private void Update()
    {
        // 追跡対象が存在しないか、ミサイルの最大飛行時間を超えたかどうかをチェックする
        //if (target == null || flightTime > maxFlightTime)
        if (flightTime > maxFlightTime)
        {
            // ミサイルを破壊してメソッドを終了する
            DestroyMissile();
            return;
        }
        // スピードに加速度を加算
        Speed += Accel;
        if (Speed > MaxSpeed)
        {
            Speed = MaxSpeed;
        }

        switch (State)
        {
            case (0):
                // 上昇移動

                if (cnt > 5)
                {
                    State = 1;
                    break;
                }

                var pos = transform.up;
                transform.position += pos;

                cnt++;
                break;

            case (1):
                // 誘導移動

                if (target == null)
                {
                    State = 2;
                    break;
                }

                // ミサイルから追跡対象への方向を計算する
                Vector3 targetDirection = (target.position - transform.position).normalized;
                // ミサイルが追跡対象を向くために必要な回転を計算する
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                // Slerp補間を使用してミサイルを追跡対象に向けて回転する
                rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, turnSpeed * Time.deltaTime);
                // ミサイルを追跡対象に向かって移動させる
                rb.velocity = transform.forward * Speed;

                // 飛行時間を増やす
                //flightTime += Time.fixedDeltaTime;
                flightTime++;
                break;

            case (2):
                // 直線移動

                var pos1 = transform.forward;
                transform.position += pos1;

                flightTime += 100;
                break;
        }

    }

    // ミサイルがオブジェクトに衝突したときに呼び出される
    private void OnCollisionEnter(Collision collision)
    {       
        if (collision.gameObject.tag != "PlayerBullet")
        {
            DestroyMissile();
        }
    }

    // ミサイルの消去
    private void DestroyMissile()
    {
        Destroy(gameObject);
    }

    // ターゲット設定
    public void SetTarget(Transform targetObj)
    {
        target = targetObj;
    }
}